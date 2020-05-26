using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortingVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int listSize = 200;
        private const double heightScale = 1.73;
        private double height;
        //private Dictionary<Rectangle, int> map = new Dictionary<Rectangle, int>();
        private List<int> list = new List<int>();
        public List<Rectangle> rects = new List<Rectangle>();
        private List<TextBlock> labels = new List<TextBlock>();
        private static int lineWidth = 15, lineMargin = 0, min = 1, max = 200;
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            height = 600 - canvas.Margin.Top - canvas.Margin.Bottom;
            lineWidth = (int) MathF.Abs((float) (900 - canvas.Margin.Left - canvas.Margin.Right - lineMargin * listSize) / listSize);
            //UpdateVisuals();
            //InsertionSort sorter = new InsertionSort(list, this);
            //UpdateVisuals();
            //sorter.Sort();
            UpdateVisuals();
        }

        public void UpdateVisuals(int current = -1, int search = -1)
        {
            if (list.Count > 0)
            {
                intlist.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    int number = list[i];
                    Rectangle element = rects[i];
                    element.Height = number * 1.73;
                    labels[i].Text = Convert.ToString(number);
                    intlist.Items.Add(number);
                    if (i == current)
                    {
                        element.Stroke = Brushes.Crimson;
                        element.Fill = Brushes.Crimson;
                    } else if (i == search)
                    {
                        element.Stroke = Brushes.DarkGray;
                        element.Fill = Brushes.DarkGray;
                    }
                    else
                    {
                        element.Stroke = Brushes.Aqua;
                        element.Fill = Brushes.Aqua;
                    }
                }
            }
            else
            {
                for (int i = 0; i < listSize; i++)
                {
                    int number = rnd.Next(min, max);
                    Rectangle element = new Rectangle()
                    {
                        Width = lineWidth,
                        Height = number * heightScale,
                        ToolTip = number,
                        Stroke = Brushes.Aqua,
                        Fill = Brushes.Aqua
                    };
                    TextBlock label = new TextBlock();
                    Canvas.SetLeft(label, i * (lineWidth + lineMargin));
                    Canvas.SetBottom(label, -20);
                    label.FontSize = 10;
                    label.Text = Convert.ToString(number);
                    canvas.Children.Add(label);
                    list.Add(number);
                    rects.Add(element);
                    labels.Add(label);
                    //map.Add(element, number);
                    Canvas.SetLeft(element, i * (lineWidth + lineMargin));
                    Canvas.SetBottom(element, 0);
                    canvas.Children.Add(element);
                }
            }
        }

        private void Randomize_OnClick(object sender, RoutedEventArgs e)
        {
            list.Clear();
            for (int i = 0; i < listSize; i++)
            {
                int number = rnd.Next(min, max);
                Rectangle element = rects[i];
                element.Height = number * heightScale;
                element.ToolTip = number;
                list.Add(number);
                Canvas.SetLeft(element, i * (lineWidth + lineMargin));
                Canvas.SetBottom(element, 0);
                labels[i].Text = Convert.ToString(number);
            }
        }

        private void Sort_OnClick(object sender, RoutedEventArgs e)
        {
            InsertionSort sorter = new InsertionSort(list, this);
            sorter.Sort();
            UpdateVisuals();
        }
    }
}
