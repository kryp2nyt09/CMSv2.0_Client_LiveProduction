using System;
using System.Windows.Forms;

namespace CMS2.Client
{
    public partial class Login : Telerik.WinControls.UI.RadForm
    {
        
        #region Properties

        public string username = "";
        public string password = "";
        Resizer rs = new Resizer();

        #endregion

        #region Constructors

        public Login()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Login_Load(object sender, EventArgs e)
        {
            this.txtUsername.Focus();
            CaptureCredentials();
            rs.FindAllControls(this);
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            rs.ResizeAllControls(this);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            CaptureCredentials();
            this.DialogResult = DialogResult.OK;
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }

        private void btnLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CaptureCredentials();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                CaptureCredentials();
                this.DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region Methods

        private void CaptureCredentials()
        {
            username = txtUsername.Text.Trim();
            password = txtPassword.Text.Trim();

        }


        #endregion

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void bntCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
