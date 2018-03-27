using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace login
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        KeyboardHook hook = new KeyboardHook();
        IntPtr pass = new IntPtr();

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(global::ModifierKeys.None, Keys.F11);//help menu
        //  hook.RegisterHotKey(global::ModifierKeys.None, Keys.F5);//change password
            hook.RegisterHotKey(global::ModifierKeys.None, Keys.F4);//clear applet
            hook.RegisterHotKey(global::ModifierKeys.None, Keys.F3);//login
            hook.RegisterHotKey(global::ModifierKeys.None, Keys.F2);//password
            hook.RegisterHotKey(global::ModifierKeys.None, Keys.F1);//username
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            int time = 4;
            int timer = 4;
            button1.Enabled = false;
            for (int x = 0; x < time; x++)
            {
                Thread.Sleep(1000);
                this.Text = "Starting in: " + (--timer) + " second(s).";
            }
            this.Text = "Login";
            button1.Enabled = true;

            SendKeys.SendWait(textBox1.Text);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(200);
            SendKeys.SendWait(textBox2.Text);
            SendKeys.SendWait("{ENTER}");
        }*/

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == Keys.F11)
            {
                MessageBox.Show(this, "Hotkeys\n\nF1: Copy selected username\nF2: Copy selected password\nF3: Login the RS applet\nF4: Clear the login screen\nF11: HotKey Menu", "HotKey Menu");
            }
         /*   else if (e.Key == Keys.F5)
            {
                this.Text = "Changing password...";
                SendKeys.SendWait(textBox2.Text);
                SendKeys.SendWait("{TAB}");
                Thread.Sleep(200);
                SendKeys.SendWait("lol1024");
                SendKeys.SendWait("{TAB}");
                Thread.Sleep(200);
                SendKeys.SendWait("lol1024");
                SendKeys.SendWait("{ENTER}");
                this.Text = "Login";
            }*/
            else if (e.Key == Keys.F4)
            {
                clear();
            } 
            else if (e.Key == Keys.F3)
            {
                SendKeys.SendWait(textBox1.Text);
                SendKeys.SendWait("{TAB}");
                Thread.Sleep(200);
                SendKeys.SendWait(textBox2.Text);
                SendKeys.SendWait("{ENTER}");
            }
            else if (e.Key == Keys.F2)
            {
              //  IntPtr pass = new IntPtr();
                SendCtrlC(pass);
                get("password");
            }
            else if (e.Key == Keys.F1)
            {
             //   IntPtr user = new IntPtr();
                SendCtrlC(pass);
                get("username");
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

       /* private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }*/
        private void get(String type)
        {
            if (type == "username")
            {
            //    textBox1.Text = "";
                textBox1.Text = Clipboard.GetText().Trim();
                if (textBox1.Text == "")
                    textBox1.Text = Clipboard.GetText().Trim();
            }
            else if (type == "password")
            {
             //   textBox2.Text = "";
                textBox2.Text = Clipboard.GetText().Trim();
                if (textBox2.Text == "")
                    textBox2.Text = Clipboard.GetText().Trim();
            }
        }
        private void SendCtrlC(IntPtr hWnd)
        {
            uint KEYEVENTF_KEYUP = 2;
            byte VK_CONTROL = 0x11;
            SetForegroundWindow(hWnd);
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event(0x43, 0, 0, 0); //Send the C key (43 is "C")
            keybd_event(0x43, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);// 'Left Control Up
        }
        private void clear()
        {
            SendKeys.SendWait("{TAB}");
            for (int i = 0; i < 20; i++)
            {
                SendKeys.SendWait("{BS}");
            }
            SendKeys.SendWait("{TAB}");
            for (int i = 0; i < 20; i++)
            {
                SendKeys.SendWait("{BS}");
            }
        }
    }
}
