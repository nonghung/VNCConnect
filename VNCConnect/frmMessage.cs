using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VNCConnect
{
    public partial class frmMessage : Form
    {

        public frmMessage()
        {
            InitializeComponent();
           
        }
        public DialogResult ShowMessage(string Msg, string Caption, MessageBoxButtons MsgBoxButton)
        {
            lbMess.Text = Msg;
            Text = Caption;
         
            ButtonStatus(MsgBoxButton);
            return ShowDialog();
        }
        public DialogResult ShowMessage(string Msg)
        {
            lbMess.Text = Msg;
            Text = "Confirmation";
           
            return ShowDialog();
        }
        public DialogResult ShowTextbox(string Msg, MessageBoxButtons MsgBoxButton)
        {
            textBox1.Text = Msg;
            textBox1.Visible = true;
            Text = "Confirmation";

            ButtonStatus(MsgBoxButton);
            return ShowDialog();
        }
        public DialogResult ShowMessage(string Msg, MessageBoxButtons MsgBoxButton)
        {
            lbMess.Text = Msg;
            Text = "Confirmation";
          
            ButtonStatus(MsgBoxButton);
            return ShowDialog();
        }
        private void ButtonStatus(MessageBoxButtons MsgBoxButton)
        {
            switch (MsgBoxButton)
            {
                case MessageBoxButtons.OK:
                   btOK.Visible = true;
                    break;
                case MessageBoxButtons.YesNo:
                    btYes.Visible = true;
                    btNo.Visible = true;
                    break;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {

        }
    }
}
