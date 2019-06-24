using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;

namespace FontGen_iOS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                txbFontName.Text = "";
                txbProfileUUID.Text = "";
                txbFPayloadUUID.Text = "";
                txbPPayloadUUID.Text = "";
                return;
            }
            Font selectedFont = (Font)listFont.Items[index];



            txbFontName.Text = selectedFont.fileName;
            txbProfileUUID.Text = selectedFont.profileUUID;
            txbFPayloadUUID.Text = selectedFont.fontPayloadUUID;
            txbPPayloadUUID.Text = selectedFont.profilePayloadUUID;

        }
        private void ListFont_Drop(object sender, System.Windows.DragEventArgs e)
        {
            foreach (var s in (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false))
            {
                if (!System.IO.Directory.Exists(s))
                {
                    string[] dirSeperators = s.Split('.');
                    string ext = dirSeperators.Last();

                    if (ext == "otf" || ext == "ttf")
                    {
                        Font item = new Font(s);
                        listFont.Items.Add(item);
                    }
                }
            }


            if (listFont.Items.Count >= 1)
            {
                listFont.SelectedIndex = listFont.Items.Count - 1;
                listFont.Focus();
            }
        }

        private void BtnAddFont_Click(object sender, RoutedEventArgs e)
        {

            // Create OpenFileDialog             
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();               // Set filter for file extension and default file extension             

            dlg.Filter = "Fonts (*.otf;*.ttf)|*.otf;*.ttf|" + "All files (*.*)|*.*";
            dlg.Multiselect = true;
            dlg.Title = "Choose Font Files";

            if (dlg.ShowDialog() == true)
            {
                foreach (string name in dlg.FileNames)
                {
                    Font item = new Font(name);
                    listFont.Items.Add(item);
                }

                if (listFont.Items.Count >= 1) {
                    listFont.SelectedIndex = listFont.Items.Count - 1;
                    listFont.Focus();
                }

            }

        }

        private void BtnRemoveFont_Click(object sender, RoutedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);

            if (index < 0)
            {
                return;
            }

            listFont.Items.RemoveAt(index);

            if (index == listFont.Items.Count)
            {
                index--;
            }

            listFont.SelectedIndex = index;
            listFont.Focus();
        }

        private void BtnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            listFont.Items.Clear();
        }


        private void TxbFontName_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
            ((Font)listFont.Items[index]).fileName = txbFontName.Text;
        }

        private void TxbProfileUUID_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
           ((Font)listFont.Items[index]).profileUUID = txbProfileUUID.Text;
        }

        private void TxbFPayloadUUID_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
           ((Font)listFont.Items[index]).fontPayloadUUID = txbFPayloadUUID.Text;
        }

        private void TxbPPayloadUUID_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
           ((Font)listFont.Items[index]).profilePayloadUUID = txbPPayloadUUID.Text;
        }

        private void BtnRevert_Click(object sender, RoutedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }

           txbFontName.Text = ((Font)listFont.Items[index]).fixedFileName;

        }

        private void BtnGenerateProfileUUID_Click(object sender, RoutedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
            txbProfileUUID.Text = System.Guid.NewGuid().ToString();

        }
        private void BtnGenerateFPayloadUUID_Click(object sender, RoutedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
            txbFPayloadUUID.Text = System.Guid.NewGuid().ToString();
        }

        private void BtnGeneratePPayloadUUID_Click(object sender, RoutedEventArgs e)
        {
            int index = listFont.Items.IndexOf(listFont.SelectedItem);
            if (index < 0)
            {
                return;
            }
            txbPPayloadUUID.Text = System.Guid.NewGuid().ToString();
        }

        BackgroundWorker generatorWorker = null;
        private async void Generator_DoWork(object sender, DoWorkEventArgs e)
        {
            ItemCollection collection = ((Tuple<ItemCollection, string>)e.Argument).Item1;
            string path = ((Tuple<ItemCollection, string>)e.Argument).Item2;

            string H1 = Properties.Resources.ResourceManager.GetString("H1");
            string H2 = Properties.Resources.ResourceManager.GetString("H2");
            string H3 = Properties.Resources.ResourceManager.GetString("H3");
            string H4 = Properties.Resources.ResourceManager.GetString("H4");

            for (int i = 0; i < collection.Count; i++)
            {
                Font font = (Font)collection[i];
                string output = "";

                Byte[] bytes = System.IO.File.ReadAllBytes(font.filePath);
                String fontString = Convert.ToBase64String(bytes);

                output = H1 + "\n" + fontString + "\n" + H2 +
                    "\n<string>" + font.fileName + "</string>\n" + H3.Replace("%FontPayloadUUID%", font.fontPayloadUUID).Replace("%FONTNAME%", font.fileName) +
                    "\n<string>" + font.profileUUID + "</string>\n" + H4.Replace("%ProfilePayloadUUID%", font.profilePayloadUUID);

                using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(path, font.fixedFileName + ".mobileconfig")))
                {
                    try
                    {
                        outputFile.Write(output);
                    }
                    catch (Exception err) {
                        i = collection.Count;
                        System.Windows.MessageBox.Show(err.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                generatorWorker.ReportProgress((i + 1) * 100 / collection.Count);
            }
        }

        string outputPath = "";
        private void Generator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) {
                if (System.Windows.MessageBox.Show("Done! Do you want to open the output folder?\n完成！是否打开输出文件夹？", "Complete", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(outputPath);
                }
                else
                {

                }
                generatorWorker = null;
                setControls(true);
            }
        }

        private void setControls(bool status) {
            btnAddFont.IsEnabled = status;
            btnRemoveAll.IsEnabled = status;
            btnRemoveFont.IsEnabled = status;
            btnStart.IsEnabled = status;
            btnGenerateFPayloadUUID.IsEnabled = status;
            btnGeneratePPayloadUUID.IsEnabled = status;
            btnGenerateProfileUUID.IsEnabled = status;
            btnRevert.IsEnabled = status;
            btnAbout.IsEnabled = status;
            txbFontName.IsEnabled = status;
            txbFPayloadUUID.IsEnabled = status;
            txbPPayloadUUID.IsEnabled = status;
            txbProfileUUID.IsEnabled = status;
            listFont.IsEnabled = status;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {

            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                outputPath = fbd.SelectedPath;
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    setControls(false);
                    generatorWorker = new BackgroundWorker();
                    Tuple<ItemCollection, string> arguments = new Tuple<ItemCollection, string>(listFont.Items,fbd.SelectedPath);
                    generatorWorker.DoWork += Generator_DoWork;
                    generatorWorker.ProgressChanged += Generator_ProgressChanged;
                    generatorWorker.WorkerReportsProgress = true;
                    generatorWorker.RunWorkerAsync(arguments);
                }
            }
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            About box = new About();
            box.ShowDialog();
        }
    }
}
