using System.Diagnostics;
using System.Windows.Input;

namespace MauiReversi
{
    public partial class MainPage : ContentPage
    {
        private int turn = 0;
        private Color[] colors = { Color.FromRgb(255,50,50), Color.FromRgb(50,255,50) };

        private Button[,] tiles = new Button[3,3];

        public MainPage()
        {
            InitializeComponent();
            fillTileArray();
            update();
        }

        private void fillTileArray()
        {
            Debug.WriteLine(grid.Children.ToArray());

            tiles[0,0] = b00;
            tiles[0,1] = b01;
            tiles[0,2] = b02;

            tiles[1,0] = b10;
            tiles[1,1] = b11;
            tiles[1,2] = b12;

            tiles[2,0] = b20;
            tiles[2,1] = b21;
            tiles[2,2] = b22;
        }

        private void update()
        {
            turnLB.Text = $"Player {turn + 1}'s turn";
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
                    button.Text = " ";
                    turn = 1;
                }
                else
                {
                    button.BackgroundColor = colors[turn];
                    button.Text = " ";
                    turn = 0;
                }
            }
            else
            {
                Debug.WriteLine("You cant play here");
            }
        }

        private void ResetGame(object sender, EventArgs e)
        {
            foreach (Button b in tiles)
            {
                b.Text = "";
                b.BackgroundColor = Color.FromRgb(255, 255, 255);
                turn = 0;
            }
        }
    }
}