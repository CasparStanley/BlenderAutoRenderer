using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderAutoRenderer
{
    public abstract class AutoRenderer : IAutoRenderer
    {
        public bool RUNNING { get; set; } = false;
        public string COMMAND { get; set; } = "";
        public string PROGRAM_PATH { get; set; } = "";

        public abstract string Opt_Animation { get; set; }
        public abstract string Opt_RunInBackground { get; set; }
        public abstract string Opt_SceneName { get; set; }
        public abstract string Opt_RenderFrame { get; set; }
        public abstract string Opt_FrameStart { get; set; }
        public abstract string Opt_FrameEnd { get; set; }
        public abstract string Opt_FrameJump { get ; set ; }
        public abstract string Opt_OutputPath { get ; set ; }

        public bool Animation { get; set; } = false;
        public int FrameStart { get; set; } = 1;
        public int FrameEnd { get; set; } = 255;

        #region GENERAL METHODS
        public void Start()
        {
            Write("Hello! Welcome to the Blender Auto Renderer\nMade by Caspar Stanley, 2022");

            RUNNING = true;
            string command = "";

            // Run in background as default
            command += Opt_RunInBackground;

            while (RUNNING)
            {
                AskQuestions();
                ReviewAnswers();
            }

            //Run(command);
        }

        public void Question(string q)
        {
            Console.WriteLine();
            Write(q);
        }

        public void Run(string command)
        {
            string strCmdText;
            strCmdText = "/C " + command;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        public void AskQuestions()
        {
            // ToDo: make this save for later use
            PROGRAM_PATH = Q_ProgramExeLocation();

            // Rendering an animation
            if (Q_Animation())
            {
                Animation = true;
                Write("You chose to render an animation!");
            }
            // Rendering separate frames
            else
            {
                Animation = false;
                Write("You chose to render frames!");
                Q_SingleOrMutlipleFrames();
            }

            // Done asking questions, time to review!
        }

        public void ReviewAnswers()
        {
            Write("These are your current settings. Please review.");

        }

        public void ConfirmQuestions()
        {

        }
        #endregion

        #region QUESTIONS
        public abstract bool Q_Animation();

        public abstract void Q_AnimationFrameRange();

        public abstract string Q_ProgramExeLocation();

        public abstract void Q_SingleOrMutlipleFrames();
        #endregion

        public void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }
        public void Write_Note(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Write_Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
