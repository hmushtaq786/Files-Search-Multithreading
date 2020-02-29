using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Threading;

namespace Threading
{
    /// <summary>
    /// Interaction logic for ThreadPool.xaml
    /// </summary>
    public partial class ThreadPool : Window
    {
        WaitCallback callback;
        public ThreadPool()
        {
            InitializeComponent();
            
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            callback = new WaitCallback(WorktoDo);
            System.Threading.ThreadPool.QueueUserWorkItem(callback, i);
        }

        private void WorktoDo(object objName)
        {
            int i = (int)objName;
            for (; i<1000000; i++)
            {
                System.Threading.Thread.Sleep(100);
                label.Content = objName;
            }
            
        }
    }
}
