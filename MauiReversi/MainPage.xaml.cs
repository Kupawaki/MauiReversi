using System.Diagnostics;
using System.Windows.Input;

namespace MauiReversi
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TileClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            var param = button.CommandParameter as String;
            Debug.WriteLine(param);
        }
    }
}