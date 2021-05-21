using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSplitter.GUI
{
    public partial class PartitionOperationForm : Form
    {
        private readonly PartitionExecutor executor;

        private bool canceled;
        private bool canClose;

        public PartitionOperationForm(PartitionExecutor executor)
        {
            InitializeComponent();
            this.executor = executor;
        }

        private async void PartitionOperationForm_Shown(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    executor.StatusChanged += OnOperationStatusChanged;
                    executor.Execute();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message, "Partition Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            executor.StatusChanged -= OnOperationStatusChanged;
            canClose = true;
            Close();
        }

        private void CancelOperation()
        {
            if (canceled) return;
            canceled = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Cancelling...";
            cancelButton.Enabled = false;
            executor.Cancel();
        }

        private void OnOperationStatusChanged(string status, float? percentageProgress)
        {
            Invoke(new Action(() =>
            {
                if (canceled)
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressLabel.Text = "Cancelling...";
                    return;
                }
                progressLabel.Text = status;
                if (percentageProgress.HasValue)
                {
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = (int)Math.Floor(
                        percentageProgress.Value * 100);
                }
                else
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                }
            }));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelOperation();
        }

        private void PartitionOperationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canceled) CancelOperation();
            if (!canClose) e.Cancel = true;
        }
    }
}
