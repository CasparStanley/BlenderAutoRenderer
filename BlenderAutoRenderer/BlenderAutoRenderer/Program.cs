using System;

namespace BlenderAutoRenderer
{
    class Program
    {
        // Render Options
        const string Opt_Animation = " -a "; // Render frames from start to end (inclusive).
        const string Opt_RunInBackground = " -b "; // Run in background (often used for UI-less rendering).
        const string Opt_SceneName = " -S "; // Set the active scene <name> for rendering.
        const string Opt_RenderFrame = " -f "; // Render frame <frame> and save it.

        // Render Settings


        // Animation Settings
        private bool Animation = false;
        private int FrameStart = 1;
        private int FrameEnd = 250;


        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Welcome to the Blender Auto Renderer\nMade by Caspar Stanley, 2022");
            Start();
        }

        #region GENERAL METHODS
        static void Start()
        {
            string command = "";

            Q_BlenderExeLocation();

            // Run in background as default
            command += Opt_RunInBackground;

            Q_Animation();

            Run(command);
        }

        static void Run(string command)
        {
            string strCmdText;
            strCmdText = "/C " + command;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        static void Question(string q)
        {
            Console.WriteLine(q);
        }
        #endregion

        #region QUESTIONS
        private static void Q_BlenderExeLocation()
        {
            Question("Please enter the location of your Blender.exe file (Not shortcut!)");
            throw new NotImplementedException();
        }

        private static void Q_Animation()
        {
            Question("Do you want to run this as an animation?");
            throw new NotImplementedException();
        }

        private static void Q_SingleOrMutlipleFrames()
        {
            Question("Do you want to render a single or multiple frames?");
            throw new NotImplementedException();
        }

        private static void Q_AnimationFrameRange()
        {
            Question("At what frame do you want your animation to start?");



            Question("At what frame do you want your animation to end?");
            throw new NotImplementedException();
        }
        #endregion
    }
}
