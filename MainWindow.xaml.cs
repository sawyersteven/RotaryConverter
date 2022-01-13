using System;
using System.ComponentModel;
using System.Reactive;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;


namespace RotaryConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new MainWindowDataContext(this);
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            var dlg = new HelpDialog();
            dlg.Owner = this;
            dlg.Show();
        }

        public class MainWindowDataContext : ReactiveObject
        {
            [Reactive] public double RotaryLength { get; set; }
            [Reactive] public double StockDiameter { get; set; } = 80;
            [Reactive] public double ImageYSize { get; set; } = 40;
            [Reactive] public double OriginalDPI { get; set; } = 500;

            [Reactive] public double YScale { get; set; }
            [Reactive] public double ScaledImageSize { get; set; }
            [Reactive] public double ScaledDPI { get; set; }

            public double WindowWidth { get; set; }

            public MainWindowDataContext(MetroWindow window)
            {

                RotaryLength = App.Settings.User.RotaryLength;
                WindowWidth = window.Width;
                this.WhenAnyValue(x => x.RotaryLength, x => x.StockDiameter, x => x.ImageYSize, x => x.OriginalDPI).Subscribe(_ => Recalculate());
                this.WhenAnyValue(x => x.RotaryLength).Subscribe(v => Set(v));
            }

            private void Set(double v)
            {
                App.Settings.User.RotaryLength = v;
            }

            private void Recalculate()
            {
                YScale = RotaryLength / (Math.PI * StockDiameter);
                ScaledImageSize = YScale * ImageYSize;
                ScaledDPI = YScale * OriginalDPI;
            }
        }
    }
}
