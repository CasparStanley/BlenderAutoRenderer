using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderAutoRenderer
{
    public class AutoRendererBlender : AutoRenderer
    {
        #region PROPERTIES
        // Render Options as specified by Blender's Command Line Arguments
        public override string Opt_Animation { get; set; }          = "-a"; // Render frames from start to end (inclusive).
        public override string Opt_RunInBackground { get; set; }    = "-b"; // Run in background (often used for UI-less rendering).
        public override string Opt_SceneName { get; set; }          = "-S"; // Set the active scene <name> for rendering.

        /// <summary>
        /// +<frame> start frame relative, -<frame> end frame relative. (-f 10 would render 10th frame, -f -2 would render second last frame)
        /// A comma separated list of frames can also be used (no spaces).
        /// A range of frames can be expressed using .. separator between the first and last frames (inclusive).
        /// </summary>
        public override string Opt_RenderFrame { get; set; }        = "-f"; // Render frame <frame> and save it.
        public override string Opt_FrameStart { get; set; }         = "-s"; // Set start to frame <frame>, supports +/- for relative frames too.
        public override string Opt_FrameEnd { get; set; }           = "-e"; // Set end to frame <frame>, supports +/- for relative frames too.
        public override string Opt_FrameJump { get; set; }          = "-j"; // Set number of frames to step forward after each rendered frame.

        /// <summary>
        /// Use // at the start of the path to render relative to the blend-file.
        /// The # characters are replaced by the frame number, and used to define zero padding.
        /// animation_##_test.png becomes animation_01_test.png
        /// test-######.png becomes test-000001.png
        /// When the filename does not contain #, The suffix #### is added to the filename.
        /// </summary>
        public override string Opt_OutputPath { get; set; }         = "-o"; // Set the render path and file name. 
        #endregion

        #region QUESTIONS
        public override string Q_ProgramExeLocation()
        {
            Question("Please enter the location of your Blender.exe file (Not shortcut!)");
            Write_Note("Note: You can drag and drop the .exe file from your file explorer");
            return Console.ReadLine();
        }

        public override string Q_Scene()
        {
            Question("Please enter the name of the scene you would like to render from");
            Write_Note("If you simply want to render the current scene in your Blender file, just press Enter");
            return Console.ReadLine();
        }

        public override string Q_FileLocation()
        {
            Question("Please enter the location of your .blend project file");
            Write_Note("Note: You can drag and drop the .blend file from your file explorer");
            return Console.ReadLine();
        }

        public override string Q_OutputLocation()
        {
            Question("Please enter the location you wish to use as the Output location");
            Write_Note("Note: You can copy and paste the directory from your file explorer (ctrl+v or right click)");
            Write_Note("Leave empty to use the output location set in Blender");
            return Console.ReadLine();
        }

        public override bool Q_Animation()
        {
            while (true)
            {
                Question("Do you want to run this as an animation? (y/n)");
                Write_Note("Type 'y' for Yes, or 'n' for No");
                string answer = Console.ReadLine();

                if (answer.ToLower() == "y")
                {
                    return true;
                }
                else if (answer.ToLower() == "n")
                {
                    return false;
                }
                else
                {
                    Write_Warning("Wrong input, please try again:");
                }
            }
        }

        public override void Q_AnimationFrameRange()
        {
            bool frameStartChosen = false;
            while (!frameStartChosen)
            {
                Question("At what frame do you want your animation to start?");

                try
                {
                    FrameStart = Convert.ToInt32(Console.ReadLine());
                    frameStartChosen = true;
                }
                catch
                {
                    Write_Warning("Looks like that was not a valid number. Try again: ");
                }
            }

            bool frameEndChosen = false;
            while (!frameEndChosen)
            {
                Question("At what frame do you want your animation to end?");

                try
                {
                    FrameEnd = Convert.ToInt32(Console.ReadLine());
                    frameEndChosen = true;
                }
                catch
                {
                    Write_Warning("Looks like that was not a valid number. Try again: ");
                }
            }
        }

        public override void Q_SingleOrMutlipleFrames()
        {
            bool settingFrames = true;
            while (settingFrames)
            {
                Question("What frame(s) do you want to render?");
                Write_Note("Write the number of the frame you want to render. If multiple, split by a comma");
                string input;
                try
                {
                    // Beware reader: This is ugly _(._.)_
                    input = Console.ReadLine();
                    string[] frames = input.Split(',');

                    // Looks like someone chose MORE than one frame! Spicey!
                    if (frames.Length > 1)
                    {
                        List<string> framesChosen = new List<string>();
                        foreach (var f in frames)
                        {
                            framesChosen.Add(f);
                        }

                        //framesChosen.Sort();
                        string framesToRender = "";

                        for (int i = 0; i < frames.Length; i++)
                        {
                            framesToRender += frames[i];

                            // Add this in between frame numbers, unless it's the last one
                            if (i + 1 < frames.Length)
                                framesToRender += ",";
                        }
                        SingleFramesToRender = framesToRender;

                        // Set the last frame, if it's higher than what is currently set
                        int lastFrame = Convert.ToInt32(framesChosen.Last());
                        if (lastFrame > FrameEnd)
                        {
                            FrameEnd = lastFrame;
                        }
                    }
                    // Just a single frame today. Solid choice.
                    else
                    {
                        SingleFramesToRender = frames[0];
                    }

                    settingFrames = false;
                }
                catch
                {
                    Write_Warning("Looks like that was not a valid number. Try again: ");
                }
            }
        }

        public override void Q_FrameJump()
        {
            bool frameJumpChosen = false;
            while (!frameJumpChosen)
            {
                Question("Skip frames at a regular interval?");
                Write_Note("(Type number, 0 to render normally)");
                try
                {
                    FrameJump = Convert.ToInt32(Console.ReadLine());
                    frameJumpChosen = true;
                }
                catch
                {
                    Write_Warning("Looks like that was not a valid number. Try again: ");
                }
            }
        }

        public override void ReviewAnswers()
        {
            WriteConsoleTop();

            Write("These are your current settings. Please review.");
            Write_Note("If you are happy with these settings, press enter");
            Write_Note("If you would like to change a setting, type the letter in parenthethis");
            Write_Note("If you would like to start over, type 'Reset'");

            string choice = Console.ReadLine();
            switch (choice.ToLower())
            {
                // ToDo: Change settings
                case "":
                    Console.WriteLine("Confirmed Settings, Creating command!");
                    break;
                case "p":
                    Console.WriteLine("Editing Blender.exe path");
                    ProgramPath = Q_ProgramExeLocation();
                    break;
                case "f":
                    Console.WriteLine("Editing file path");
                    FilePath = Q_FileLocation();
                    break;
                case "t":
                    Console.WriteLine("Editing render type (render/animation)");
                    Animation = Q_Animation();
                    // ToDo: This one requires you to restart the flow of frames and such??
                    break;
                case "s":
                    Console.WriteLine("Editing start frame");
                    break;
                case "e":
                    Console.WriteLine("Editing end frame");
                    break;
                case "n":
                    Console.WriteLine("Editing scene name");
                    SceneName = Q_Scene();
                    break;
                case "j":
                    Console.WriteLine("Editing frame jump");
                    break;
                case "o":
                    Console.WriteLine("Editing output path");
                    break;
                case "reset":
                    Console.WriteLine("Resetting settings. Starting over!");
                    break;
                default:
                    break;
            }

            RUNNING = false;
        }

        public override void CreateCommand()
        {
            string cmdOutputPath, cmdScene = "", cmd2 = "", cmd3 = "", cmd4 = "", cmdEnd;

            if (!String.IsNullOrEmpty(OutputPath))
            {
                cmdOutputPath = $" {Opt_OutputPath} {OutputPath}";
            }
            else
            {
                cmdOutputPath = "";
            }

            cmdScene = $" {Opt_SceneName} \"{SceneName}\"";

            // Animation(-a)/Render(-f) HAS to be the last option in the final command
            if (Animation) 
            {
                cmd2 = $" {Opt_FrameStart} {FrameStart}";
                cmd3 = $" {Opt_FrameEnd} {FrameEnd}";
                cmd4 = $" {Opt_FrameJump} {FrameJump}";
                cmdEnd = Opt_Animation; 
            }
            else 
            {
                cmdEnd = $"{Opt_RenderFrame} {SingleFramesToRender}";
            }

            string command;
            if (!BATCH)
            {
                command = $"{ProgramPath} {Opt_RunInBackground} {FilePath}{cmdScene}{cmdOutputPath}{cmd2}{cmd3}{cmd4} {cmdEnd}";
            }
            else
            {
                // This way we can add the Program path only on the first command in the list.
                command = $"{FilePath}{cmdScene}{cmdOutputPath}{cmd2}{cmd3}{cmd4} {cmdEnd}";
            }
            Console.WriteLine("Command: " + command);
            COMMAND = command;
        }
        #endregion
    }
}
