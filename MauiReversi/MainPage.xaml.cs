using System.Diagnostics;

namespace MauiReversi
{
    public partial class MainPage : ContentPage
    {
        //Grid generation vars
        private Grid grid;
        private int gridSize;
        private int gridSpacing = 5;
        private int tileSize = 50;

        //Bound vars
        int[] topBounds;
        int[] leftBounds;
        int[] rightBounds;
        int[] bottomBounds;

        //UI vars
        private int turn = 0;
        private Color[] colors = { Color.FromRgb(255, 50, 50), Color.FromRgb(50, 255, 50) };


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
            //gridSpacingET.IsEnabled = false;
            //tileSizeET.IsEnabled = false;

            generateBTN.IsVisible = false;
            gridSizeET.IsVisible = false;
            //gridSpacingET.IsVisible = false;
            //tileSizeET.IsVisible = false;

            //Grab generation vars
            try
            {
                gridSize = Int16.Parse(gridSizeET.Text);
                //gridSpacing = Int16.Parse(gridSpacingET.Text);
                //tileSize = Int16.Parse(tileSizeET.Text);
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
                    Text = $"{i}"
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

            findBounds();
        }

        private void findBounds()
        {
            List<int> topBoundsL = new List<int>();
            for (int i = 0; i < gridSize; i++)
            {
                topBoundsL.Add(i);
            }

            List<int> leftBoundsL = new List<int>();
            for (int i = 0; i <= ((gridSize * gridSize) - gridSize); i += gridSize)
            {
                leftBoundsL.Add(i);
            }

            List<int> rightBoundsL = new List<int>();
            for (int i = gridSize - 1; i <= ((gridSize * gridSize) - 1); i += gridSize)
            {
                rightBoundsL.Add(i);
            }

            List<int> bottomBoundsL = new List<int>();
            for (int i = (gridSize * gridSize) - gridSize; i <= ((gridSize * gridSize) - 1); i++)
            {
                bottomBoundsL.Add(i);
            }

            topBounds = topBoundsL.ToArray();
            leftBounds = leftBoundsL.ToArray();
            rightBounds = rightBoundsL.ToArray();
            bottomBounds = bottomBoundsL.ToArray();

            //foreach (int x in topBounds)
            //{
            //    Debug.WriteLine(x);
            //}
            //Debug.WriteLine("");

            //foreach (int x in leftBounds)
            //{
            //    Debug.WriteLine(x);
            //}
            //Debug.WriteLine("");

            //foreach (int x in rightBounds)
            //{
            //    Debug.WriteLine(x);
            //}
            //Debug.WriteLine("");

            //foreach (int x in bottomBounds)
            //{
            //    Debug.WriteLine(x);
            //}
            //Debug.WriteLine("");
        }


        //Playability functions.................................................

        private void TileClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            int c = grid.GetColumn(button);
            int r = grid.GetRow(button);
            int literalIndex = ((r * gridSize) + c);

            //Debug.WriteLine($"Row: {r} and col: {c}");
            //Debug.WriteLine($"Literal Index: {literalIndex}");
            //Debug.WriteLine($"Text: {(grid.ElementAt(literalIndex) as Button).Text}");
            //Debug.WriteLine("");

            findLeftBound(c, r, literalIndex);
            findRightBound(c, r, literalIndex);
            findTopBounds(c, r, literalIndex);
            findBottomBound(c, r, literalIndex);
         
            

            //if (button.Text == "")
            //{
            //    if (turn == 0)
            //    {
            //        button.BackgroundColor = colors[turn];
            //        button.Text = " ";
            //        turn = 1;
            //    }
            //    else
            //    {
            //        button.BackgroundColor = colors[turn];
            //        button.Text = " ";
            //        turn = 0;
            //    }
            //}
            //else
            //{
            //    Debug.WriteLine("You cant play here");
            //}
        }

        private void findLeftBound(int c, int r, int literalIndex)
        {
            Debug.WriteLine("Finding left bound..............................");

            int left = literalIndex;
            try
            {
                if (leftBounds.Contains(left))
                {
                    Debug.WriteLine("Tile is the left bound");
                    return;
                }
                else
                {
                    while (!leftBounds.Contains(left))
                    {
                        left -= 1;
                        if (leftBounds.Contains(left))
                        {
                            Debug.WriteLine($"Left Bound: {(grid.ElementAt(left) as Button).Text}");
                            Debug.WriteLine("");
                            return;
                        }
                        else
                        {
                            Debug.WriteLine($"Still looking: {(grid.ElementAt(left) as Button).Text}");
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Error in LEFT");
            }
        }

        private void findRightBound(int c, int r, int literalIndex)
        {
            Debug.WriteLine("Finding right bound..............................");

            int right = literalIndex;
            try
            {
                if (rightBounds.Contains(right))
                {
                    Debug.WriteLine("Tile is the right bound");
                    return;
                }
                else
                {
                    while (!rightBounds.Contains(right))
                    {
                        right += 1;
                        if (rightBounds.Contains(right))
                        {
                            Debug.WriteLine($"Right Bound: {(grid.ElementAt(right) as Button).Text}");
                            Debug.WriteLine("");
                            return;
                        }
                        else
                        {
                            Debug.WriteLine($"Still looking: {(grid.ElementAt(right) as Button).Text}");
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Error in RIGHT");
            }
        }

        private void findTopBounds(int c, int r, int literalIndex)
        {
            Debug.WriteLine("Finding top bound..............................");

            int top = literalIndex;
            try
            {
                if (topBounds.Contains(top))
                {
                    Debug.WriteLine("Tile is the top bound");
                    return;
                }
                else
                {
                    while (!topBounds.Contains(top))
                    {
                        top -= gridSize;
                        if (topBounds.Contains(top))
                        {
                            Debug.WriteLine($"Top Bound: {(grid.ElementAt(top) as Button).Text}");
                            Debug.WriteLine("");
                            return;
                        }
                        else
                        {
                            Debug.WriteLine($"Still looking: {(grid.ElementAt(top) as Button).Text}");
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Error in TOP");
            }
        }

        private void findBottomBound(int c, int r, int literalIndex)
        {
            Debug.WriteLine("Finding top bound..............................");

            int bottom = literalIndex;
            try
            {
                if (bottomBounds.Contains(bottom))
                {
                    Debug.WriteLine("Tile is the bottom bound");
                    return;
                }
                else
                {
                    while (!bottomBounds.Contains(bottom))
                    {
                        bottom += gridSize;
                        if (bottomBounds.Contains(bottom))
                        {
                            Debug.WriteLine($"Bottom Bound: {(grid.ElementAt(bottom) as Button).Text}");
                            Debug.WriteLine("");
                            return;
                        }
                        else
                        {
                            Debug.WriteLine($"Still looking: {(grid.ElementAt(bottom) as Button).Text}");
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Error in BOTTOM");
            }
        }


        private void ResetGame(object sender, EventArgs e)
        {
            //Turn on the generate button and grid stats
            generateBTN.IsEnabled = true;
            gridSizeET.IsEnabled = true;
            //gridSpacingET.IsEnabled = true;
            //tileSizeET.IsEnabled = true;

            generateBTN.IsVisible = true;
            gridSizeET.IsVisible = true;
            //gridSpacingET.IsVisible = true;
            //tileSizeET.IsVisible = true;

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