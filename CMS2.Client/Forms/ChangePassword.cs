using System;
using System.Windows.Forms;

namespace CMS2.Client
{
    public partial class ChangePassword : Form
    {
        public string oldPassword = "";
        public string newPassword1 = "";
        public String newPassword2="";

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CaptureInput();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void CaptureInput()
        {
            oldPassword = txtOldPassword.Text.Trim();
            newPassword1 = txtNewPassword1.Text.Trim();
            newPassword2 = txtNewPassword1.Text.Trim();
        }

        private void txtOldPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewPassword1.Focus();
            }
        }

        private void txtNewPassword1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewPassword2.Focus();
            }
        }

        private void txtNewPassword2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void btnSave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CaptureInput();
            }
        }
    }
}
