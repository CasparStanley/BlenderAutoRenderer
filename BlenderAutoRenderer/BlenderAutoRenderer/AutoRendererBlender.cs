using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderAutoRenderer
{
    class AutoRendererBlender : AutoRenderer
    {
        #region PROPERTIES
        // Render Options as specified by Blender's Command Line Arguments
        public override string Opt_Animation { get; set; }          = " -a "; // Render frames from start to end (inclusive).
        public override string Opt_RunInBackground { get; set; }    = " -b "; // Run in background (often used for UI-less rendering).
        public override string Opt_SceneName { get; set; }          = " -S "; // Set the active scene <name> for rendering.

        /// <summary>
        /// +<frame> start frame relative, -<frame> end frame relative. (-f 10 would render 10th frame, -f -2 would render second last frame)
        /// A comma separated list of frames can also be used (no spaces).
        /// A range of frames can be expressed using .. separator between the first and last frames (inclusive).
        /// </summary>
        public override string Opt_RenderFrame { get; set; }        = " -f "; // Render frame <frame> and save it.
        public override string Opt_FrameStart { get; set; }         = " -s "; // Set start to frame <frame>, supports +/- for relative frames too.
        public override string Opt_FrameEnd { get; set; }           = " -e "; // Set end to frame <frame>, supports +/- for relative frames too.
        public override string Opt_FrameJump { get; set; }          = " -j "; // Set number of frames to step forward after each rendered frame.

        /// <summary>
        /// Use // at the start of the path to render relative to the blend-file.
        /// The # characters are replaced by the frame number, and used to define zero padding.
        /// animation_##_test.png becomes animation_01_test.png
        /// test-######.png becomes test-000001.png
        /// When the filename does not contain #, The suffix #### is added to the filename.
        /// </summary>
        public override string Opt_OutputPath { get; set; }         = " -o "; // Set the render path and file name. 
        #endregion

        #region QUESTIONS
        public override string Q_ProgramExeLocation()
        {
            Question("Please enter the location of your Blender.exe file (Not shortcut!)");
            Write_Note("Note: You can drag and drop the .exe file from your file explorer");
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
            Write_Note("Note: You can copye and paste the directory from your file explorer (ctrl+v or right click)");
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
            bool multipleFramesChosen = false;
            while (!multipleFramesChosen)
            {
                Question("What frame(s) do you want to render?");
                Write_Note("Write the number of the frame you want to render. If multiple, split by a comma");
                string input;
                try
                {
                    input = Console.ReadLine();
                    string[] frames = input.Split(',');
                    List<int> framesChosen = new List<int>();
                    foreach (var f in frames)
                    {
                        framesChosen.Add(Convert.ToInt32(f));
                    }

                    framesChosen.Sort();
                    SingleFramesToRender = framesChosen.ToArray();

                    if (framesChosen.Last() > FrameEnd)
                    {
                        FrameEnd = framesChosen.Last();
                    }

                    multipleFramesChosen = true;
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
        #endregion
    }
}
