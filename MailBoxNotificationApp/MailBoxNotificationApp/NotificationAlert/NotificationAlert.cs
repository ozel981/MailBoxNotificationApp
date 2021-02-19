
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MailBoxNotificationApp
{

    public partial class NotificationAlert : Form
    {
        public string Message { get; set; }

        private AlertAction action;
        private (int x, int y) position;
        private int X { get => position.x; set { position.x = value; } }
        private int Y { get => position.y; set { position.y = value; } }
        private bool noticed = false;
        private NotifyIconManager notifyIconManager;

        public enum AlertAction
        {
            wait,
            start,
            close
        }

        public NotificationAlert(string message, NotifyIconManager notifyIconManager)
        {
            InitForm(message, Color.FromKnownColor(KnownColor.Highlight), notifyIconManager);
        }
        public NotificationAlert(string message, Color color, NotifyIconManager notifyIconManager)
        {
            InitForm(message, color, notifyIconManager);
        }

        private void InitForm(string message, Color color, NotifyIconManager notifyIconManager)
        {
            InitializeComponent();
            TopMost = true;
            Message = message;
            SetBackgroundColor(color);
            this.notifyIconManager = notifyIconManager;
        }

        public void SetBackgroundColor(Color color)
        {
            BackColor = color;
            CloseButton.BackColor = color;
        }

        public void ShowAlert()
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;

            for (int i = 0; i < 7; i++)
            {
                if (AlertIndexIsAvailable(i))
                {
                    DisplayAlert(i);
                    break;
                }
            }
        }

        private void DisplayAlert(int index)
        {
            Name = "AlertNr" + index.ToString();
            X = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
            Y = Screen.PrimaryScreen.WorkingArea.Height - (Height + 4) * (index + 1);
            Location = new Point(X, Y);

            MessageLabel.Text = Message;

            Show();
            action = AlertAction.start;

            AlerTimer.Interval = 1;
            AlerTimer.Start();
        }

        private bool AlertIndexIsAvailable(int index)
        {
            string formName = "AlertNr" + index.ToString();
            return ((NotificationAlert)Application.OpenForms[formName]) == null;
        }

        private void MessageLabel_Click(object sender, EventArgs e)
        {
            noticed = true;
            System.Diagnostics.Process.Start("https://mailboxnet.azurewebsites.net/");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            noticed = true;
            AlerTimer.Interval = 1;
            action = AlertAction.close;
        }

        private void AlerTimer_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case AlertAction.wait:
                    {
                        WaitAction();
                    }
                    break;
                case AlertAction.start:
                    {
                        StartAction();
                    }
                    break;
                case AlertAction.close:
                    {
                        CloseAction();
                    }
                    break;
            }
        }

        private void WaitAction()
        {
            AlerTimer.Interval = 5000;
            action = AlertAction.close;
        }

        private void StartAction()
        {
            AlerTimer.Interval = 1;
            Opacity += 0.1;
            if (X < Location.X)
            {
                Left--;
            }
            else
            {
                if (Opacity == 1.0)
                {
                    action = AlertAction.wait;
                }
            }
        }

        private void CloseAction()
        {
            AlerTimer.Interval = 1;
            Opacity -= 0.1;

            Left -= 3;
            if (Opacity == 0.0)
            {
                if (!noticed)
                {
                    notifyIconManager.AddNotify(Message);
                }
                Close();
            }
        }
    }
}
