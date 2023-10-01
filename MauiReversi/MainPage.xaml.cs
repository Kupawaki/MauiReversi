using System.Diagnostics;
using System.Windows.Input;

namespace MauiReversi
{
    public partial class MainPage : ContentPage
    {
        private int turn = 0;
        private Color[] colors = { Color.FromRgb(255,50,50), Color.FromRgb(50,255,50) };

        //Grid generation vars
        private Grid grid;
        private int gridSize;
        private int gridSpacing;
        private int tileSize;

        //Button interaction vars
        private Button[,] tiles;


        public MainPage()
        {
            InitializeComponent();
        }

        private void fillTileArray()
        {
            Debug.WriteLine("Filling array");

            int r = 0;
            int c = 0;
            tiles = new Button[gridSize, gridSize];
            for (int i = 0; i < (gridSize * gridSize); i++)
            {
                //Debug.WriteLine($"grid at i is {(grid.ElementAt(i) as Button).Text}");
                //tiles[r, c] = grid.ElementAt(i) as Button;

                //Debug.WriteLine($"FTA: i={i}, r={r}, c={c}");

                if(c == gridSize)
                {
                    r++;
                    c = 0;

                    //Debug.WriteLine($"c was == to {gridSize} and is now {c} and r is {r}");
                }
                else
                {
                    c++;
                    //Debug.WriteLine($"c was != {gridSize} and is now {c}");
                }
            }

            //Debug.WriteLine(tiles.Length);

            tiles[0, 0] = grid.ElementAt(0) as Button;
            Debug.WriteLine(tiles[0, 0].Text);
        }

        private async void GenerateGrid(object sender, EventArgs e)
        {
            //Grab generation vars
            try
            {
                gridSize = Int16.Parse(gridSizeET.Text);
                gridSpacing = Int16.Parse(gridSpacingET.Text);
                tileSize = Int16.Parse(tileSizeET.Text);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "K");
            }


            //Make a new instance of a grid element
            grid = new()
            {
                //Give it its stylings
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                ColumnSpacing = gridSpacing,
                RowSpacing = gridSpacing
            };

            //Give it its row and column definitions
            for (int i = 0; i < gridSize; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(tileSize) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(tileSize) });
            }

            int row = 0;
            int col = 0;

            //Add the button elements to the grid
            for (int i = 0; i < (gridSize * gridSize); i++)
            {
                grid.Add(new Button
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    Text = $"{row}{col}"
                }, col, row);

                if(col == (gridSize-1))
                {
                    row++;
                    col = 0;
                }
                else
                {
                    col++;
                }
            }

            //Add the grid to the page
            holder.Add(grid);

            fillTileArray();
        }

        //Playability functions.................................................

        private void ResetGame(object sender, EventArgs e)
        {
            foreach (Button b in tiles)
            {
                b.Text = "";
                b.BackgroundColor = Color.FromRgb(255, 255, 255);
                turn = 0;
            }
        }

        private void TileClicked(object sender, EventArgs e)
        {

            //Debug.WriteLine((grid.ElementAt(0) as Button).Text);

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


        //UI Management functions...............................................

        //private void update()
        //{
        //    turnLB.Text = $"Player {turn + 1}'s turn";
        //}
    }
}