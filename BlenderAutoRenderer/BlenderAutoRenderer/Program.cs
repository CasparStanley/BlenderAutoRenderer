using System;
using System.Collections.Generic;

namespace BlenderAutoRenderer
{
    class Program
    {
        static void Main()
        {
            RunSingle();

            //Test command prompt:
            // autoRenderer.Run("cd");
        }

        static void RunSingle()
        {
            IAutoRenderer autoRenderer = new AutoRendererBlender();
            Console.WriteLine("Hi. Let's create an auto renderer or two for you!");
            System.Threading.Thread.Sleep(2000);
            autoRenderer.Start();
            autoRenderer.Run(autoRenderer.COMMAND);
        }

        static void RunMultiple()
        {
            List<IAutoRenderer> autoRenderers = new List<IAutoRenderer>();
            bool settingUp = true;

            while (settingUp)
            {
                bool askingToSetUpNewOne = true;

                Console.WriteLine("Hi. Let's create an auto renderer or two for you!");
                System.Threading.Thread.Sleep(2000);
                IAutoRenderer autoRenderer = new AutoRendererBlender();
                autoRenderer.Start();
                autoRenderers.Add(autoRenderer);

                while (askingToSetUpNewOne)
                {
                    Console.WriteLine("Do you want to set up another auto renderer? (y/n)");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "n")
                    {
                        settingUp = false;
                        askingToSetUpNewOne = false;
                    }
                    else if (answer == "y")
                    {
                        askingToSetUpNewOne = false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, your answer was not understood. Try again:");
                    }
                }
            }

            foreach (IAutoRenderer aR in autoRenderers)
            {
                bool runThisAR = true;
                while (runThisAR)
                {
                    // This doesn't work, as GetCurrentProcess gets the console and associates the process with the console that is obviously open...
                    if (System.Diagnostics.Process.GetCurrentProcess() == null)
                    {
                        aR.Run(aR.COMMAND);
                        runThisAR = false;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}
