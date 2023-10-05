using System.Diagnostics;

namespace MauiReversi
{
    public partial class MainPage : ContentPage
    {
        //Grid generation vars
        private Grid grid;
        private int gridSize;
        private readonly int gridSpacing = 5;
        private readonly int tileSize = 50;

        //Vars for tracking grid boundaries
        List<int> topBounds;
        List<int> rightBounds;
        List<int> bottomBounds;
        List<int> leftBounds;

        //Vars for clicking on a tile
        int startingColumn;
        int startingRow;
        int startingIndex;

        //Vars for processing moves
        List<int> passedTiles;
        int operatorCode = 1;
        int operativeIndex;

        //UI vars
        private int turn = 0;
        private readonly Color[] colors = { Colors.Red, Colors.Green};


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

            generateBTN.IsVisible = false;
            gridSizeET.IsVisible = false;

            //Grab generation vars
            try
            {
                gridSize = Int16.Parse(gridSizeET.Text);
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
                Button b = new()
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

            FindBounds();
        }

        private void FindBounds()
        {
            topBounds = new List<int>();
            rightBounds = new List<int>();
            bottomBounds = new List<int>();
            leftBounds = new List<int>();
            passedTiles = new List<int>();

            //Top of grid
            for (int i = 0; i < gridSize; i++)
            {
                topBounds.Add(i);
            }

            //Right of grid
            for (int i = gridSize - 1; i <= ((gridSize * gridSize) - 1); i += gridSize)
            {
                rightBounds.Add(i);
            }

            //Bottom of grid
            for (int i = (gridSize * gridSize) - gridSize; i <= ((gridSize * gridSize) - 1); i++)
            {
                bottomBounds.Add(i);
            }

            //Left of grid
            for (int i = 0; i <= ((gridSize * gridSize) - gridSize); i += gridSize)
            {
                leftBounds.Add(i);
            }

            SetTiles();

        }

        //Playability functions.................................................

        private void TileClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            startingColumn = grid.GetColumn(button);
            startingRow = grid.GetRow(button);
            startingIndex = (startingRow * gridSize) + startingColumn;

            //Change the color of the tile we clicked
            ChangeColor(startingIndex);

            //Process what other tiles we need to change
            ProcessMove(startingIndex);
        }

        private void ProcessMove(int currentIndex)
        {
            //Check the operator code to see where we are in the process
            //If the operator code is 9 (not a valid code) then stop working
            if (operatorCode == 9)
            {
                Debug.WriteLine($"OPC is {operatorCode}! Should now return");
                return;
            }
            else
            {
                //Find what operation to preform
                switch (operatorCode)
                {
                    //Move Top
                    case 1:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex - gridSize;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //Move TopRight
                    case 2:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex - gridSize + 1;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //Right
                    case 3:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex + 1;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //BottomRight
                    case 4:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex + 1 + gridSize;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //Bottom
                    case 5:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex + gridSize;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //BottomLeft
                    case 6:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex + gridSize - 1;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //Left
                    case 7:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex - 1;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                    //TopLeft
                    case 8:
                        Debug.WriteLine($"OPC is {operatorCode}!");
                        operativeIndex = currentIndex - gridSize - 1;

                        SeekTiles(currentIndex, operativeIndex);
                        break;

                }
            }
        }

        private void SeekTiles(int currentIndex, int operativeIndex)
        {
            //The move we are about to make does not land us on a boundary tile
            if (!topBounds.Contains(operativeIndex) && !rightBounds.Contains(operativeIndex))
            {
                Debug.WriteLine($"The move from {currentIndex} to {operativeIndex} is not an issue");

                //Update current index to the new position
                currentIndex = operativeIndex;

                //If the color of the new tile is our color,
                //Set the passed tiles to our color,
                //update the operation and recursively call this function,
                //with this starting index with a new operator
                if (GetColor(currentIndex) == colors[turn])
                {
                    Debug.WriteLine("This new tile our color");
                    SetColors();
                    operatorCode++;
                    ProcessMove(startingIndex);
                }
                //Otherwise add the tile to the list of passed tiles,
                //and start again at the new tile
                else
                {
                    Debug.WriteLine("This new tile is not out color");
                    passedTiles.Add(currentIndex);
                    ProcessMove(currentIndex);
                }
            }
            //The move does land us on a boundary tile
            else
            {
                Debug.WriteLine($"The move from {currentIndex} to {operativeIndex} will put us on a boundary tile");

                //If we get to this point, and have yet to find our color,
                //Then we need to clear the passedTiles list,
                //and return to our starting point with a new operator
                if (!(GetColor(operativeIndex) == colors[turn]))
                {
                    passedTiles.Clear();
                    operatorCode++;
                    ProcessMove(startingIndex);
                }
                //Otherwise we should set all the tiles in the list to our color,
                //and return with a new operator at the starting index
                else
                {
                    SetColors();
                    operatorCode++;
                    ProcessMove(startingIndex);
                }
            }
        }

        private void SetColors()
        {
            foreach (int x in passedTiles)
            {
                GetButton(x).BackgroundColor = colors[turn];
            }
        }

        private void NextTurn()
        {
            if(turn == 0)
            {
                turn = 1;
            }
            else
            {
                turn = 0;
            }
        }

        private void ResetGame(object sender, EventArgs e)
        {
            //Turn on the generate button and grid stats
            generateBTN.IsEnabled = true;
            gridSizeET.IsEnabled = true;

            generateBTN.IsVisible = true;
            gridSizeET.IsVisible = true;

            //Delete the grid
            holder.Remove(grid);
        }

        //UI Management functions...............................................

        //private void update()
        //{
        //    turnLB.Text = $"Player {turn + 1}'s turn";
        //}


        //QOL functions.........................................................

        private Button GetButton(int index)
        {
            return grid.ElementAt(index) as Button;
        }

        private void ChangeColor(int index)
        {
            GetButton(index).BackgroundColor = colors[turn];
        }

        private Color GetColor(int index)
        {
            return GetButton(index).BackgroundColor;
        }

        private void SetTiles()
        {
            GetButton(17).BackgroundColor = Colors.Pink;
            GetButton(10).BackgroundColor = Colors.Pink;
            GetButton(3).BackgroundColor = colors[turn];

            GetButton(18).BackgroundColor = Colors.Pink;
            GetButton(12).BackgroundColor = Colors.Pink;
            GetButton(6).BackgroundColor = colors[turn];

            GetButton(25).BackgroundColor = Colors.Pink;
            GetButton(26).BackgroundColor = Colors.Pink;
            GetButton(27).BackgroundColor = colors[turn];

            GetButton(32).BackgroundColor = Colors.Pink;
            GetButton(40).BackgroundColor = Colors.Pink;
            GetButton(48).BackgroundColor = colors[turn];

            GetButton(31).BackgroundColor = Colors.Pink;
            GetButton(38).BackgroundColor = Colors.Pink;
            GetButton(45).BackgroundColor = colors[turn];

            GetButton(30).BackgroundColor = Colors.Pink;
            GetButton(36).BackgroundColor = Colors.Pink;
            GetButton(42).BackgroundColor = colors[turn];

            GetButton(23).BackgroundColor = Colors.Pink;
            GetButton(22).BackgroundColor = Colors.Pink;
            GetButton(21).BackgroundColor = colors[turn];

            GetButton(16).BackgroundColor = Colors.Pink;
            GetButton(8).BackgroundColor = Colors.Pink;
            GetButton(0).BackgroundColor = colors[turn];
        }
    }
}