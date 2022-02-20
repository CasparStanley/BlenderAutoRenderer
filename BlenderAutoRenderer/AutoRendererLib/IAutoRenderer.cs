using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderAutoRenderer
{
    public interface IAutoRenderer
    {
        // Properties
        bool RUNNING { get; set; }
        string COMMAND { get; set; }
        string ProgramPath { get; set; }
        bool BATCH { get; set; }

        // Render Options as specified by the program specific Command Line Arguments
        string Opt_Animation { get; set; }
        string Opt_RunInBackground { get; set; }
        string Opt_SceneName { get; set; }
        string Opt_RenderFrame { get; set; }
        string Opt_FrameStart { get; set; }
        string Opt_FrameEnd { get; set; }
        string SingleFramesToRender { get; set; }
        string Opt_FrameJump { get; set; }
        string Opt_OutputPath { get; set; }

        // ToDo: Convert this to a class so multiple render-ranges can be set up (multiple scenes for example)
        // Settings
        string SceneName { get; set; }
        int FrameStart { get; set; }
        int FrameEnd { get; set; }
        int FrameJump { get; set; }
        bool Animation { get; set; }
        string OutputPath { get; set; }

        #region GENERAL METHODS
        void Start();

        void AskQuestions();

        void ReviewAnswers();

        void CreateCommand();

        void Run(string command);

        void Question(string q);
        #endregion

        #region QUESTIONS
        string Q_ProgramExeLocation();

        string Q_FileLocation();

        string Q_OutputLocation();

        bool Q_Animation();

        void Q_AnimationFrameRange();

        void Q_SingleOrMutlipleFrames();

        void Q_FrameJump();
        #endregion
    }
}
