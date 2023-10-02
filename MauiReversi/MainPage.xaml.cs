using System.Diagnostics;

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

        //Setup functions.......................................................

        private async void GenerateGrid(object sender, EventArgs e)
        {
            //Disable the generate button and grid stats
            generateBTN.IsEnabled = false;
            gridSizeET.IsEnabled = false;
            gridSpacingET.IsEnabled = false;
            tileSizeET.IsEnabled = false;

            generateBTN.IsVisible = false;
            gridSizeET.IsVisible = false;
            gridSpacingET.IsVisible = false;
            tileSizeET.IsVisible = false;

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
                Button b = new Button
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    Text = $"{row}{col}"
                };
                b.Clicked += TileClicked;

                grid.Add(b, col, row);

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
        }


        //Playability functions.................................................

        private void TileClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            int c = grid.GetColumn(button);
            int r = grid.GetRow(button);
            int literalIndex = ((r * gridSize) + c);

            Debug.WriteLine($"This button is at row {r} and col {c}");
            Debug.WriteLine($"Literal Index is {literalIndex}");
            Debug.WriteLine($"The tex of this button should be {(grid.ElementAt(literalIndex) as Button).Text}");

            Debug.WriteLine($"Left is {(grid.ElementAt(literalIndex - 1) as Button).Text}");
            Debug.WriteLine($"Right is {(grid.ElementAt(literalIndex + 1) as Button).Text}");
            Debug.WriteLine($"Top is {1}");
            Debug.WriteLine($"Bottom is {1}");

            try
            {
                Debug.WriteLine($"Left is {(grid.ElementAt(literalIndex - 1) as Button).Text}");
            }
            catch (IndexOutOfRangeException) 
            {
                Debug.WriteLine($"Left is N/A");
            }

            try
            {
                Debug.WriteLine($"Right is {(grid.ElementAt(literalIndex + 1) as Button).Text}");
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine($"Right is N/A");
            }

            try
            {
                Debug.WriteLine($"Top is {1}");
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine($"Top is N/A");
            }

            try
            {
                Debug.WriteLine($"Bottom is {1}");
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine($"Bottom is N/A");
            }

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
            //Turn on the generate button and grid stats
            generateBTN.IsEnabled = true;
            gridSizeET.IsEnabled = true;
            gridSpacingET.IsEnabled = true;
            tileSizeET.IsEnabled = true;

            generateBTN.IsVisible = true;
            gridSizeET.IsVisible = true;
            gridSpacingET.IsVisible = true;
            tileSizeET.IsVisible = true;

            //Delete the grid
            holder.Remove(grid);
        }

        //UI Management functions...............................................

        //private void update()
        //{
        //    turnLB.Text = $"Player {turn + 1}'s turn";
        //}
    }
}