using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderAutoRenderer
{
    class AutoRendererBlender : AutoRenderer
    {
        public override string Opt_Animation { get; set; } = " -a "; // Render frames from start to end (inclusive).
        public override string Opt_RunInBackground { get; set; } = " -b "; // Run in background (often used for UI-less rendering).
        public override string Opt_SceneName { get; set; } = " -S "; // Set the active scene <name> for rendering.
        public override string Opt_RenderFrame { get; set; } = " -f "; // Render frame <frame> and save it.

        #region QUESTIONS
        public override string Q_ProgramExeLocation()
        {
            Question("Please enter the location of your Blender.exe file (Not shortcut!)");
            return Console.ReadLine();
        }

        public override bool Q_Animation()
        {
            Question("Do you want to run this as an animation?");
            throw new NotImplementedException();
        }

        public override void Q_SingleOrMutlipleFrames()
        {
            Question("Do you want to render a single or multiple frames?");
            throw new NotImplementedException();
        }

        public override void Q_AnimationFrameRange()
        {
            Question("At what frame do you want your animation to start?");



            Question("At what frame do you want your animation to end?");
            throw new NotImplementedException();
        }
        #endregion
    }
}
