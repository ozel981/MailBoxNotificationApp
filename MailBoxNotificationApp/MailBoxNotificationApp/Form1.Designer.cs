
namespace MailBoxNotificationApp
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LogInPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.LogInButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ErrorMessageLabel = new System.Windows.Forms.Label();
            this.NotificationTimer = new System.Windows.Forms.Timer(this.components);
            this.MailBoxNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.MailBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogInPanel.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MailBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogInPanel
            // 
            this.LogInPanel.Controls.Add(this.tableLayoutPanel14);
            resources.ApplyResources(this.LogInPanel, "LogInPanel");
            this.LogInPanel.Name = "LogInPanel";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.tableLayoutPanel14, "tableLayoutPanel14");
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 1, 1);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            // 
            // tableLayoutPanel15
            // 
            resources.ApplyResources(this.tableLayoutPanel15, "tableLayoutPanel15");
            this.tableLayoutPanel15.Controls.Add(this.tableLayoutPanel16, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.ErrorMessageLabel, 0, 2);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            // 
            // tableLayoutPanel16
            // 
            resources.ApplyResources(this.tableLayoutPanel16, "tableLayoutPanel16");
            this.tableLayoutPanel16.Controls.Add(this.LogInButton, 1, 0);
            this.tableLayoutPanel16.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            // 
            // LogInButton
            // 
            resources.ApplyResources(this.LogInButton, "LogInButton");
            this.LogInButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LogInButton.Name = "LogInButton";
            this.LogInButton.UseVisualStyleBackColor = true;
            this.LogInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MailBoxNotificationApp.Properties.Resources.mailbox;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // ErrorMessageLabel
            // 
            resources.ApplyResources(this.ErrorMessageLabel, "ErrorMessageLabel");
            this.ErrorMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorMessageLabel.Name = "ErrorMessageLabel";
            // 
            // NotificationTimer
            // 
            this.NotificationTimer.Interval = 60000;
            this.NotificationTimer.Tick += new System.EventHandler(this.NotificationTimer_Tick);
            // 
            // MailBoxNotifyIcon
            // 
            this.MailBoxNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.MailBoxNotifyIcon.ContextMenuStrip = this.MailBoxContextMenuStrip;
            resources.ApplyResources(this.MailBoxNotifyIcon, "MailBoxNotifyIcon");
            // 
            // MailBoxContextMenuStrip
            // 
            this.MailBoxContextMenuStrip.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MailBoxContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MailBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.MailBoxContextMenuStrip.Name = "MailBoxContextMenuStrip";
            this.MailBoxContextMenuStrip.ShowImageMargin = false;
            resources.ApplyResources(this.MailBoxContextMenuStrip, "MailBoxContextMenuStrip");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LogInPanel);
            this.Name = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.LogInPanel.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MailBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LogInPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.Button LogInButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer NotificationTimer;
        private System.Windows.Forms.NotifyIcon MailBoxNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip MailBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Label ErrorMessageLabel;
    }
}
