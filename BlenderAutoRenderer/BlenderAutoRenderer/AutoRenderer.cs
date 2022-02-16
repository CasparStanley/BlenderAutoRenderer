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

        public bool Animation { get; set; } = false;
        public int FrameStart { get; set; } = 1;
        public int FrameEnd { get; set; } = 255;

        #region GENERAL METHODS
        public void Start()
        {
            RUNNING = true;
            string command = "";

            // Run in background as default
            command += Opt_RunInBackground;

            while (RUNNING)
            {
                AskQuestions();
            }

            Run(command);
        }

        public void Question(string q)
        {
            Console.WriteLine(q);
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

            }
            // Rendering separate frames
            else
            {
                Q_SingleOrMutlipleFrames();
            }
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
    }
}
