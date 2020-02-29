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
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for BackgroundWorkerDemo.xaml
    /// </summary>
    public partial class BackgroundWorkerDemo : Window
    {
        string foldername="D:\\Study";
        List<string> files = new List<string>();
        string ext = "";
        string imageFile = "";

        BackgroundWorker bgWorker = new BackgroundWorker();
        public BackgroundWorkerDemo()
        {
            InitializeComponent();

            //int[] array = new int[] { 32, 3, 2, 21, 4, 35, 34, 13, 546, 24, 5, 3, 1, 43, 3, 1, 43, 46, 78, 56, 43, 12, 577 };
            //Array.Sort(array);
            //var result = from arr in array
            //             where (arr%5==0 ||arr%7==0 ||arr%13==0) && arr > 100 && arr < 1000
            //             select arr;
            //foreach (var x in result)
            //{
            //    Console.WriteLine(x);
            //}
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;

            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;

            // TODO: Start, Pause, Resume States
            // TODO: Cancel/Stop Button
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                System.Windows.MessageBox.Show("Cancelled!");
            }
            else
            {
                //this.filesList.ItemsSource = null;
               // this.filesList.ItemsSource = files;
                System.Windows.MessageBox.Show("Done!");
                startBtn.Content = "Start";

            }
        }

        int count = 0;
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = (e.ProgressPercentage/(double)count)*100;
            this.filesList.Items.Add(e.UserState.ToString());
            Console.WriteLine(e.UserState.ToString());
           // this.filesList.Items.
            

        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            this.count = Directory.GetFiles(foldername, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(Path.GetExtension(ext).ToLower())).Count();
            //Console.WriteLine(count);
            foreach (string imageFile in Directory.GetFiles(foldername, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(Path.GetExtension(ext).ToLower())))
            {
                System.Threading.Thread.Sleep(100);
                files.Add(imageFile);
                i++;

                this.bgWorker.ReportProgress(i, imageFile);
                if (this.bgWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
               

            
            
            
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
             StringBuilder supportedExtensions = new StringBuilder(50);

                if (jpgBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.jpg,");
                }
                if (pngBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.png,");
                }
                if (icoBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.ico,");
                }
                if (docxBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.docx,");
                }
                if (rawBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.raw,");
                }
                if (pdfBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.pdf,");
                }
                if (txtBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.txt,");
                }
                if (pptxBox.IsChecked == true)
                {
                    supportedExtensions.Append("*.pptx,");
                }
            supportedExtensions.Remove(supportedExtensions.Length-1, 1);// = ".ico";
            
            ext = supportedExtensions.ToString();
                if (!bgWorker.IsBusy)
                {
                    files.Clear();
                    filesList.Items.Clear();
                    progressBar.Value = 0;
                    this.bgWorker.RunWorkerAsync();
                    startBtn.Content = "Stop";
                }
                else
                {
                    bgWorker.CancelAsync();
                startBtn.Content = "Start";
            }
        }
        

        private void SelectallBox_Checked(object sender, RoutedEventArgs e)
        {
            this.jpgBox.IsChecked = true;
            this.pngBox.IsChecked = true;
            this.icoBox.IsChecked = true;
            this.rawBox.IsChecked = true;
            this.pdfBox.IsChecked = true;
            this.txtBox.IsChecked = true;
            this.docxBox.IsChecked = true;
            this.pptxBox.IsChecked = true;

            this.selectallBox.Content = "Deselect all";
            
        }

        private void SelectallBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.jpgBox.IsChecked = false;
            this.pngBox.IsChecked = false;
            this.icoBox.IsChecked = false;
            this.rawBox.IsChecked = false;
            this.pdfBox.IsChecked = false;
            this.txtBox.IsChecked = false;
            this.docxBox.IsChecked = false;
            this.pptxBox.IsChecked = false;

            this.selectallBox.Content = "Select all";
        }

        private void SelectfolderBtn_Click(object sender, RoutedEventArgs e)
        {
           var dialog = new FolderBrowserDialog();
           DialogResult result = dialog.ShowDialog();

            foldername = dialog.SelectedPath;
            directoryText.Content = foldername;



            //string[] filePaths = Directory.GetFiles(@foldername);

            
            //string[] filePaths = Directory.GetFiles(@foldername, "*.pdf", SearchOption.AllDirectories);

            //filesList.ItemsSource = filePaths;

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            files.Clear();
            filesList.Items.Clear();
            progressBar.Value = 0;
        }
    }
}