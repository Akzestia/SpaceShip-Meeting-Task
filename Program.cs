
using Lib1;
using Lib1.MathFuncPlugins;
using System.Drawing;

Console.ForegroundColor = ConsoleColor.Cyan;

//Plugins
var ap = new AccessibleAdditionPlugin("1.0", "Use to add int nums",
    Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"PluginImages\Icon1.png")));
var sp = new AccessibleSubtractionPlugin("1.1", "Use to subtract int nums",
    Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"PluginImages\Icon2.png")));
var mp = new AccessibleMultyplyPlugin("1.2", "Use to multiply int nums",
    Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"PluginImages\Icon3.png")));
var dp = new AccessibleDivisionPlugin("1.3", "Use to divide int nums",
    Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"PluginImages\Icon4.png")));
var iplugins = new List<IPlugin> { ap.getplugin(), sp.getplugin(), mp.getplugin(), dp.getplugin() };
var accessiblePluginFactory = new Plugins.AccessiblePluginFactory(iplugins);
//Plugins

ConsoleKeyInfo key;
var names = accessiblePluginFactory.GetPluginNames; //Array that contains all plugins names

do //Menu
{
    Console.Clear();
    Console.WriteLine("\tPlugin Menu");
    Console.WriteLine("S.Show All plagins names");
    Console.WriteLine("C.Show Plugins amount");
    Console.WriteLine("U.Use plugin");
    key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.S: //Shows plugins names
            Console.Clear();
            for (var i = 0; i < names.Length; i++) Console.WriteLine($"Plugin #{i + 1}(Name) -> {names[i]}");
            Console.ReadKey();
            break;
        case ConsoleKey.C: //Shows total amount of plugins
            Console.Clear();
            Console.WriteLine("Plugins amount -> " + accessiblePluginFactory.PluginsCount);
            Console.ReadKey();
            break;
        case ConsoleKey.U: //Using plugins
            Console.Clear();
        ret:
            Console.Write("Enter plugin's name -> ");
            var plugname = Console.ReadLine();
            if (plugname.Length == 0)
            {
                Console.WriteLine("Wrong input! Try again.");
                Console.ReadKey();
                Console.Clear();
                goto ret;
            }

            IPlugin temp_plugin = null;
            for (var i = 0; i < names.Length; i++)
                if (names[i] == plugname)
                {
                    temp_plugin = accessiblePluginFactory.GetPlugin(names[i]);
                    break;
                }

            if (temp_plugin != null)
            {
                Console.Clear();
                Console.WriteLine("Plagin Icon: ");
                Console.WriteLine("\n\n\n\n\nCurrent Plugin -> "); //Plugin Info
                Console.WriteLine("Plugin Name: [" + temp_plugin.PluginName + "]");
                Console.WriteLine("Plugin Version: [" + temp_plugin.Version + "]");
                Console.WriteLine("Plugin Description: [" + temp_plugin.Description + "]");
                Thread.Sleep(10);
                var location = new Point(0, 1);
                var imageSize = new Size(10, 5);
                try
                {
                    using (var g = Graphics.FromHwnd(ImageViewer.GetConsoleWindow())) //Draws plugin Icon
                    {
                        var image = temp_plugin.Image;
                        var fontSize = ImageViewer.GetConsoleFontSize();
                        var imageRect = new Rectangle(
                            location.X * fontSize.Width,
                            location.Y * fontSize.Height,
                            imageSize.Width * fontSize.Width,
                            imageSize.Height * fontSize.Height);
                        g.DrawImage(image, imageRect);
                    }

                    Console.ReadKey();
                    int input1 = 0, input2 = 0;
                retinput1:
                    try
                    {
                        Console.Clear();
                        Console.Write("Enter input1 -> ");
                        input1 = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        goto retinput1;
                    }

                retinput2:
                    try
                    {
                        Console.Clear();
                        Console.Write("Enter input2 -> ");
                        input2 = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        goto retinput2;
                    }

                    Console.Clear();
                    switch (temp_plugin.PluginName) //Use of plugins Run function
                    {
                        case "Multiplication":
                            Console.WriteLine("Result -> " + input1 + " * " + input2 + " = " + mp.Run(input1, input2));
                            break;
                        case "Division":
                            var remainderOfDivision = "0";
                            if (input1 % input2 != 0) remainderOfDivision = (input1 % input2).ToString();
                            if (input2 != 0)
                                Console.WriteLine("Result -> " + input1 + " / " + input2 + " = " +
                                                  dp.Run(input1, input2) + "\nremainder of the division: " +
                                                  remainderOfDivision);
                            else
                                Console.WriteLine("Exception: \"Division by zero\"");
                            break;
                        case "Addition":
                            Console.WriteLine("Result -> " + input1 + " + " + input2 + " = " + ap.Run(input1, input2));
                            break;
                        case "Subtraction":
                            Console.WriteLine("Result -> " + input1 + " - " + input2 + " = " + sp.Run(input1, input2));
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Wrong plugin Name. No plugins weren't found");
            }

            Console.ReadKey();
            break;
    }
} while (key.Key != ConsoleKey.Escape);
