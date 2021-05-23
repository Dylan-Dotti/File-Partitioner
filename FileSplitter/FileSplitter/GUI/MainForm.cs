using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FileSplitter.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            string srcPath = @"C:\Users\h4dottd\Desktop\39760.zip";
            string dstDir = @"C:\Users\h4dottd\Desktop\FileSplitterDst";
            PartitionExecutor executor = new PartitionExecutor(
                srcPath, dstDir, new GreedyPartitionStrategy(5));
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
