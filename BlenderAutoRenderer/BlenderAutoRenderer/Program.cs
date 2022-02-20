using System;
using System.Collections.Generic;

namespace BlenderAutoRenderer
{
    class Program
    {
        static void Main()
        {
            //RunSingle();
            RunMultiple();

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

                // Make sure to set all of them to BATCH rendering, so their command is generated correctly
                autoRenderer.BATCH = true;

                // Start the setup of this Auto Renderer
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

            IAutoRenderer autoRendererForRunning = new AutoRendererBlender();
            string command = "";
            int i = 0;
            foreach (IAutoRenderer aR in autoRenderers)
            {
                // Add the program location to the very start of the command
                if (i == 0)
                {
                    command += aR.ProgramPath + " " + aR.Opt_RunInBackground + " ";
                }

                if (i < autoRenderers.Count)
                {
                    command += aR.COMMAND + " ";
                } else
                {
                    command += aR.COMMAND;
                }

                i++;
            }

            Console.WriteLine("Command for batch rendering: " + command);
            Console.ReadLine();
            autoRendererForRunning.Run(command);
        }
    }
}
