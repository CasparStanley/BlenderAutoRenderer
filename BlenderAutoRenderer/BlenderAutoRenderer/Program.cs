using System;

namespace BlenderAutoRenderer
{
    class Program
    {
        // Render Options
        const string Opt_Animation = "-a"; // Run in background (often used for UI-less rendering).
        const string Opt_RunInBackground = "-b"; // Render frames from start to end (inclusive).
        const string Opt_SceneName = "-S"; // Set the active scene <name> for rendering.
        const string Opt_RenderFrame = "-f"; // Render frame <frame> and save it.

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

        static void Start()
        {


            Run();
        }

        static void Run()
        {

        }
    }
}
