using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SortingVisualizer
{
    public class InsertionSort
    {
        private List<int> list;
        private bool isSorted = false;
        private MainWindow main;
        private int i;

        public InsertionSort(List<int> list, MainWindow main)
        {
            this.list = list;
            this.main = main;
        }

        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate

            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }

        public void Sort()
        {
            isSorted = true;
            i = 1;
            DelayAction(0, SortBody);
        }

        public void SortBody()
        {
            Trace.WriteLine("Tick!");
            int previous = list[i - 1];
            int current = list[i];
            if (current < previous)
            {
                for (int j = i - 1; j > -1; j--)
                {
                    int target = list[j];
                    if (target <= current || j == 0)
                    {
                        list.RemoveAt(i);
                        list.Insert(j, current);
                        isSorted = false;
                        main.UpdateVisuals(i, j);
                        break;
                    }
                }
            }
            previous = current;
            i++;
            if (i < list.Count)
            {
                DelayAction(0, SortBody);
            }
            else if (!isSorted)
            {
                DelayAction(0, Sort);
            }
        }
    }
}
