using System.Diagnostics;
using System.Windows.Input;

namespace MauiReversi
{
    public partial class MainPage : ContentPage
    {
        private int turn = 0;
        private Color[] colors = { Color.FromRgb(255,50,50), Color.FromRgb(50,255,50) };

        public MainPage()
        {
            InitializeComponent();
        }

        private void TileClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            //Debug.WriteLine(grid.GetColumn(button));
            //Debug.WriteLine(grid.GetRow(button));

            if (button.Text == "")
            {
                if (turn == 0)
                {
                    button.BackgroundColor = colors[turn];
                    button.Text = colors[turn].ToString();
                    turn = 1;
                }
                else
                {
                    button.BackgroundColor = colors[turn];
                    button.Text = colors[turn].ToString();
                    turn = 0;
                }
            }
            else
            {
                Debug.WriteLine("You cant play here");
            }
        }
    }
}