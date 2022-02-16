using System;

namespace BlenderAutoRenderer
{
    class Program
    {
        static void Main()
        {

            IAutoRenderer autoRenderer = new AutoRendererBlender();
            autoRenderer.Start();

            //Test command prompt:
            // autoRenderer.Run("cd");
        }
    }
}
