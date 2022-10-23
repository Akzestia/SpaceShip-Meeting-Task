using System.Drawing;
using System.Runtime.InteropServices;

namespace Lib1
{
    public static class Plugins
    {
        interface PluginFactory //Plugin Factory interface
        {
            int PluginsCount { get; }
            string[] GetPluginNames { get; }
            IPlugin GetPlugin(string pluginName);
        }
        public class AccessiblePluginFactory : PluginFactory //Plugin Factory interface realization
        {
            public List<IPlugin> plugins = new List<IPlugin>();
            public AccessiblePluginFactory() { }
            public AccessiblePluginFactory(List<IPlugin> plugins)
            {
                this.plugins = plugins;
                PluginsCount = plugins.Count;
                string[] pluginNames = new string[plugins.Count];
                for (int i = 0; i < plugins.Count; i++)
                {
                    pluginNames[i] = plugins[i].PluginName;
                }
                GetPluginNames = pluginNames;
            }
            public int PluginsCount { get; }
            public string[] GetPluginNames { get; }
            public IPlugin GetPlugin(string pluginName)
            {
                for (int i = 0; i < this.plugins.Count; i++)
                {
                    if (plugins[i].PluginName == pluginName)
                    {
                        return this.plugins[i];
                    }
                }
                return this.plugins[-1];
            }
        }
    }


    public interface IPlugin // IPlugin interface
    {
        string PluginName { get; }
        string Version { get; }
        Image Image { get; }
        string Description { get; }
    }


    namespace MathFuncPlugins
    {
        abstract class ForIPlugin // Abstract class that contains part of IPlugin's realisations
        {
            public abstract int Run(int input1, int input2);
        }

        public class AccessibleMultyplyPlugin //Gives access to closed plugin MultyplyPlugin
        {
            MultyplyPlugin temp_plugin;
            public string PluginName { get { return temp_plugin.PluginName; } }
            public string Version { get { return temp_plugin.Version; } }
            public Image Image { get { return temp_plugin.Image; } }
            public string Description { get { return temp_plugin.Description; } }

            public int Run(int input1, int input2)
            {
                return temp_plugin.Run(input1, input2);
            }

            public AccessibleMultyplyPlugin(string version, string desc, Image img)
            {
                temp_plugin = new MultyplyPlugin(version, desc, img);
            }

            public IPlugin getplugin()
            {
                return temp_plugin;
            }
        }

        class MultyplyPlugin : ForIPlugin, IPlugin //Multyply Plugin
        {
            public string PluginName { get; }
            public string Version { get; }
            public Image Image { get; }
            public string Description { get; }

            public override int Run(int input1, int input2)
            {
                return input1 * input2;
            }

            public MultyplyPlugin(string version, string desc, Image img)
            {
                PluginName = "Multiplication";
                Version = version;
                Description = desc;
                Image = img;
            }
        }
        public class AccessibleDivisionPlugin //Gives access to closed plugin DivisionPlugin
        {
            DivisionPlugin temp_plugin;
            public string PluginName { get { return temp_plugin.PluginName; } }
            public string Version { get { return temp_plugin.Version; } }
            public Image Image { get { return temp_plugin.Image; } }
            public string Description { get { return temp_plugin.Description; } }

            public int Run(int input1, int input2)
            {
                return temp_plugin.Run(input1, input2);
            }

            public AccessibleDivisionPlugin(string version, string desc, Image img)
            {
                temp_plugin = new DivisionPlugin(version, desc, img);
            }

            public IPlugin getplugin()
            {
                return temp_plugin;
            }
        }

        class DivisionPlugin : ForIPlugin, IPlugin //Division Plugin
        {
            public string PluginName { get; }
            public string Version { get; }
            public Image Image { get; }
            public string Description { get; }

            public override int Run(int input1, int input2)
            {
                return input1 / input2;
            }

            public DivisionPlugin(string version, string desc, Image img)
            {
                PluginName = "Division";
                Version = version;
                Description = desc;
                Image = img;
            }
        }

        public class AccessibleAdditionPlugin //Gives access to closed plugin AdditionPlugin
        {
            AdditionPlugin temp_plugin;
            public string PluginName { get { return temp_plugin.PluginName; } }
            public string Version { get { return temp_plugin.Version; } }
            public Image Image { get { return temp_plugin.Image; } }
            public string Description { get { return temp_plugin.Description; } }

            public int Run(int input1, int input2)
            {
                return temp_plugin.Run(input1, input2);
            }

            public AccessibleAdditionPlugin(string version, string desc, Image img)
            {
                temp_plugin = new AdditionPlugin(version, desc, img);
            }

            public IPlugin getplugin()
            {
                return temp_plugin;
            }

        }

        class AdditionPlugin : ForIPlugin, IPlugin //Addition Plugin
        {
            public string PluginName { get; }
            public string Version { get; }
            public Image Image { get; }
            public string Description { get; }

            public override int Run(int input1, int input2)
            {
                return input1 + input2;
            }

            public AdditionPlugin() { }

            public AdditionPlugin(string version, string desc, Image img)
            {
                PluginName = "Addition";
                Version = version;
                Description = desc;
                Image = img;
            }
        }

        public class AccessibleSubtractionPlugin //Gives access to closed plugin SubtractionPlugin
        {
            SubtractionPlugin temp_plugin;
            public string PluginName { get { return temp_plugin.PluginName; } }
            public string Version { get { return temp_plugin.Version; } }
            public Image Image { get { return temp_plugin.Image; } }
            public string Description { get { return temp_plugin.Description; } }

            public int Run(int input1, int input2)
            {
                return temp_plugin.Run(input1, input2);
            }

            public AccessibleSubtractionPlugin(string version, string desc, Image img)
            {
                temp_plugin = new SubtractionPlugin(version, desc, img);
            }

            public IPlugin getplugin()
            {
                return temp_plugin;
            }
        }

        class SubtractionPlugin : ForIPlugin, IPlugin//Subtraction Plugin
        {
            public string PluginName { get; }
            public string Version { get; }
            public Image Image { get; }
            public string Description { get; }

            public override int Run(int input1, int input2)
            {
                return input1 - input2;
            }

            public SubtractionPlugin(string version, string desc, Image img)
            {
                PluginName = "Subtraction";
                Version = version;
                Description = desc;
                Image = img;
            }
        }
    }

    public class ImageViewer//Uses to draw plugins icons
    {
        public static Size GetConsoleFontSize()
        {
            IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero);
            int errorCode = Marshal.GetLastWin32Error();
            if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            ConsoleFontInfo cfi = new ConsoleFontInfo();
            if (!GetCurrentConsoleFont(outHandle, false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }


        //The required additional WinApi calls, constants and types
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,
            IntPtr lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCurrentConsoleFont(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [Out][MarshalAs(UnmanagedType.LPStruct)] ConsoleFontInfo lpConsoleCurrentFont);

        [StructLayout(LayoutKind.Sequential)]
        internal class ConsoleFontInfo
        {
            internal int nFont;
            internal Coord dwFontSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Coord
        {
            [FieldOffset(0)]
            internal short X;
            [FieldOffset(2)]
            internal short Y;
        }

        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int OPEN_EXISTING = 3;
        //The required additional WinApi calls, constants and types
    }
}