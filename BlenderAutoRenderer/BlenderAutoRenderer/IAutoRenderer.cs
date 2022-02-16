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
        string PROGRAM_PATH { get; set; }

        // Render Options
        string Opt_Animation { get; set; }
        string Opt_RunInBackground { get; set; }
        string Opt_SceneName { get; set; }
        string Opt_RenderFrame { get; set; }

        // Render Settings


        // Animation Settings
        bool Animation { get; set; }
        int FrameStart { get; set; }
        int FrameEnd { get; set; }

        #region GENERAL METHODS
        void Start();

        void AskQuestions();

        void ConfirmQuestions();

        void Run(string command);

        void Question(string q);
        #endregion

        #region QUESTIONS
        string Q_ProgramExeLocation();

        bool Q_Animation();

        void Q_SingleOrMutlipleFrames();

        void Q_AnimationFrameRange();
        #endregion
    }
}
