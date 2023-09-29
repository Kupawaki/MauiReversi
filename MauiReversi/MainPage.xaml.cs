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

            Debug.WriteLine(grid.ElementAt(0));

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

        private void GenerateGrid(object sender, EventArgs e)
        {
            Grid grid = new()
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(50) },
                    new RowDefinition { Height = new GridLength(50) },
                    new RowDefinition { Height = new GridLength(50) },
                    new RowDefinition { Height = new GridLength(50) },
                    new RowDefinition { Height = new GridLength(50) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(50) },
                    new ColumnDefinition { Width = new GridLength(50) },
                    new ColumnDefinition { Width = new GridLength(50) },
                    new ColumnDefinition { Width = new GridLength(50) },
                    new ColumnDefinition { Width = new GridLength(50) }
                }
            };

            grid.ElementAt(0);

            grid.Add(new BoxView
            {
                Color = Colors.Blue,
            }, 1, 0);
            grid.Add(new Label
            {
                Text = "Row 0, Column 1",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 1, 0);

            // Row 1
            // This BoxView and Label are in row 1 and column 0, which are specified as arguments
            // to the Add method overload.
            grid.Add(new BoxView
            {
                Color = Colors.Teal
            }, 0, 1);
            grid.Add(new Label
            {
                Text = "Row 1, Column 0",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 0, 1);

            // This BoxView and Label are in row 1 and column 1, which are specified as arguments
            // to the Add method overload.
            grid.Add(new BoxView
            {
                Color = Colors.Purple
            }, 1, 1);
            grid.Add(new Label
            {
                Text = "Row1, Column 1",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 1, 1);

            // Row 2
            // Alternatively, the BoxView and Label can be positioned in cells with the Grid.SetRow
            // and Grid.SetColumn methods.

            BoxView boxView = new BoxView { Color = Colors.Red };
            Grid.SetRow(boxView, 2);
            Grid.SetColumnSpan(boxView, 2);
            Label label = new Label
            {
                Text = "Row 2, Column 0 and 1",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            Grid.SetRow(label, 2);
            Grid.SetColumnSpan(label, 2);

            grid.Add(boxView);
            grid.Add(label);

            Title = "Basic Grid demo";
            Content = grid;
        }
    }
}