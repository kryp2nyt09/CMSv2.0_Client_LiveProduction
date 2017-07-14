namespace CMS2.Client.Forms
{
    partial class PaymentSummaryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalReportViewer_PaymentSummary = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // crystalReportViewer_PaymentSummary
            // 
            this.crystalReportViewer_PaymentSummary.ActiveViewIndex = -1;
            this.crystalReportViewer_PaymentSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer_PaymentSummary.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer_PaymentSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer_PaymentSummary.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer_PaymentSummary.Name = "crystalReportViewer_PaymentSummary";
            this.crystalReportViewer_PaymentSummary.Size = new System.Drawing.Size(914, 376);
            this.crystalReportViewer_PaymentSummary.TabIndex = 0;
            // 
            // PaymentSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 376);
            this.Controls.Add(this.crystalReportViewer_PaymentSummary);
            this.Name = "PaymentSummaryForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "PaymentSummaryForm";
            this.ThemeName = "Office2010Black";
            this.Load += new System.EventHandler(this.PaymentSummaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer_PaymentSummary;
    }
}
