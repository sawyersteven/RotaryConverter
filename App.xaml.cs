using System.Windows;

namespace RotaryConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Settings? Settings;

        public App()
        {
            Settings = new Settings();
            this.Exit += new ExitEventHandler((_, _) => Settings.Write());
        }
    }
}
