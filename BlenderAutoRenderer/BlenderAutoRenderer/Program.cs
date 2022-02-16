using System;

namespace BlenderAutoRenderer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello! Welcome to the Blender Auto Renderer\nMade by Caspar Stanley, 2022");

            IAutoRenderer autoRenderer = new AutoRendererBlender();

            autoRenderer.Start();

            //Test command prompt:
            // autoRenderer.Run("cd");
        }
    }
}
