﻿using System;
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

        public abstract string Opt_Animation { get; set; }
        public abstract string Opt_RunInBackground { get; set; }
        public abstract string Opt_SceneName { get; set; }
        public abstract string Opt_RenderFrame { get; set; }
        public abstract string Opt_FrameStart { get; set; }
        public abstract string Opt_FrameEnd { get; set; }
        public abstract string Opt_FrameJump { get ; set ; }
        public abstract string Opt_OutputPath { get ; set ; }

        public string ProgramPath { get; set; } = "";
        public bool Animation { get; set; } = false;
        public int FrameStart { get; set; } = 1;
        public int FrameEnd { get; set; } = 255;
        public int[] SingleFramesToRender { get; set; }
        public string SceneName { get; set; } = "Scene (default)";
        public int FrameJump { get; set; } = 0;
        public string FilePath { get; set; } = "";
        public string OutputPath { get; set; } = "";

        // Console choices
        public string ConsoleChoicesLog     = "";
        private const string PROGRAM_EXE    = "\n(p)Program Path: ";
        private const string FILE_PATH      = "\n(f)File Path: ";
        private const string RENDER_TYPE    = "\n(t)Type: ";
        private const string START_FRAME    = "\n(s)Start Frame: ";
        private const string END_FRAME      = "\n(e)End Frame: ";
        private const string SCENE_NAME     = "\n(n)Scene Name: ";
        private const string FRAME_JUMP     = "\n(j)Frame Jump: ";
        private const string OUTPUT_PATH    = "\n(o)Output Path: ";

        #region GENERAL METHODS
        public void Start()
        {
            WriteConsoleTop();

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

        private void WriteConsoleTop()
        {
            Console.Clear();
            Write("Hello! Welcome to the Blender Auto Renderer\nMade by Caspar Stanley, 2022");

            UpdateConsoleChoices();
            Write_Note(ConsoleChoicesLog);
        }

        public void Question(string q)
        {
            WriteConsoleTop();

            Console.WriteLine();
            Write_Question(q);
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
            ProgramPath = Q_ProgramExeLocation();
            // ToDo: Make these into classes/loops so you can render from different files and to different paths
            FilePath = Q_FileLocation();
            OutputPath = Q_OutputLocation();


            // Rendering an animation
            if (Q_Animation())
            {
                Animation = true;

                Q_AnimationFrameRange();
                Q_FrameJump();
            }
            // Rendering separate frames
            else
            {
                Animation = false;

                Q_SingleOrMutlipleFrames();
            }

            // Done asking questions, time to review!
        }


        public void ReviewAnswers()
        {
            WriteConsoleTop();

            Write("These are your current settings. Please review.");
            Write_Note("If you are happy with these settings, press enter");
            Write_Note("If you would like to change a setting, type the letter in parenthethis");
            Write_Note("If you would like to start over, type 'Reset'");

            string choice = Console.ReadLine();
            switch(choice.ToLower())
            {
                case "":
                    Console.WriteLine("Confirmed Settings");
                    break;
                case "p":
                    Console.WriteLine("Editing Blender.exe path");
                    break;
                case "f":
                    Console.WriteLine("Editing file path");
                    break;
                case "t":
                    Console.WriteLine("Editing render type (render/animation)");
                    break;
                case "s":
                    Console.WriteLine("Editing start frame");
                    break;
                case "e":
                    Console.WriteLine("Editing end frame");
                    break;
                case "n":
                    Console.WriteLine("Editing scene name");
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

            Console.ReadLine();
        }

        public void ConfirmQuestions()
        {

        }
        #endregion

        #region QUESTIONS
        public abstract bool Q_Animation();

        public abstract void Q_AnimationFrameRange();

        public abstract string Q_ProgramExeLocation();

        public abstract string Q_FileLocation();

        public abstract string Q_OutputLocation();

        public abstract void Q_SingleOrMutlipleFrames();

        public abstract void Q_FrameJump();
        #endregion

        public void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }
        public void Write_Question(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
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

        private void UpdateConsoleChoices()
        {
            // Local variables to add to Console Log
            string typeChoice, sceneName, outputPath, programPath, filePath;
            int startChoice = 1, endChoice = 250, frameJump = 0;

            // Check if we have edited the settings, and if so, update the text
            if (Animation) { typeChoice = "Animation"; }
            else { typeChoice = "Render"; }

            if (FrameStart != 1) { startChoice = FrameStart; }
            if (FrameEnd != 250) { endChoice = FrameEnd; }

            if (SceneName != "Scene (default)") { sceneName = SceneName; }
            else { sceneName = "Scene (default)"; }

            if (FrameJump != 0) { frameJump = FrameJump; }

            if (!String.IsNullOrEmpty(OutputPath)) { outputPath = OutputPath; }
            else { outputPath = ""; }

            if (!String.IsNullOrEmpty(ProgramPath)) { programPath = ProgramPath; }
            else { programPath = ""; }

            if (!String.IsNullOrEmpty(FilePath)) { filePath = FilePath; }
            else { filePath = ""; }

            ConsoleChoicesLog = $"{PROGRAM_EXE + programPath + FILE_PATH + filePath + OUTPUT_PATH + outputPath + RENDER_TYPE + typeChoice + START_FRAME + startChoice + END_FRAME + endChoice.ToString() + SCENE_NAME + sceneName + FRAME_JUMP + frameJump}";
        }
    }
}
