using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FilePartitioner.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            string srcPath = @"C:\Users\h4dottd\Desktop\42063.zip";
            string dstDir = @"C:\Users\h4dottd\Desktop\42063\";
            PartitionExecutor executor = new PartitionExecutor(
                srcPath, dstDir, new GreedyPartitionStrategy(3));
            new PartitionOperationForm(executor).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\\Users\";
            dialog.IsFolderPicker = false;
            dialog.Filters.Add(new CommonFileDialogFilter("Zip Files", "*.zip"));
            dialog.ShowDialog();
        }
    }
}
