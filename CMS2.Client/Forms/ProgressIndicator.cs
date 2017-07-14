using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CMS2.Client
{
    public partial class ProgressIndicator : Form
    {
        public ProgressIndicator(string title, string progressText, DoWorkEventHandler worker)
        {

            InitializeComponent();
            workerProgress.WorkerReportsProgress = true;
            workerProgress.WorkerSupportsCancellation = true;

            this.Text = title;
            lblProgressText.Text = progressText;
            workerProgress.DoWork += worker;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (workerProgress.IsBusy)
            {
                btnCancel.Enabled = false;
                lblProgressText.Text = "Cancelling ...";
                workerProgress.CancelAsync();
            }
        }

        private void ProgressIndicator_Load(object sender, EventArgs e)
        {
            workerProgress.RunWorkerAsync();
        }

        private void workerProgress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void workerProgress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
