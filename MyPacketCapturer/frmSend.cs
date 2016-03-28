using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPacketCapturer
{
    public partial class frmSend : Form
    {
        public static int instantiations = 0;

        //**********Default constructor
        public frmSend()
        {
            InitializeComponent();
            instantiations++;
        }  //End frmSend


        //**********Openning the file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            openFileDialog1.Title = "Open Captured Packets";
            openFileDialog1.ShowDialog();

            //Check to see if a filename was given
            if (openFileDialog1.FileName != "")
            {
                txtPacket.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        } //End openToolStripMenuItem_Click

        //**********Saving the file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save the Captured Packets";
            saveFileDialog1.ShowDialog();

            //Check to see if a filename was given
            if (saveFileDialog1.FileName != "")
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, txtPacket.Text);
            }
        } //End saveToolStripMenuItem_Click


        //**********Sending out a packet (or packets)
        private void btnSend_Click(object sender, EventArgs e)
        {
            string stringBytes = "";
            //Get the hex values from the file
            foreach (string s in txtPacket.Lines){

                //Taking out the comments
                string[] noComments = s.Split('#');
                string s1 = noComments[0];
                stringBytes += s1 + Environment.NewLine;
            }  //End for

        //Extract the hex values into a string array
        string[] sBytes = stringBytes.Split(new string[] {"\n","\r\n"," ","\t"},
            StringSplitOptions.RemoveEmptyEntries);

        //Change the strings into bytes
        byte[] packet = new byte[sBytes.Length];
        try
        {
            int i = 0;
            foreach (string s in sBytes) { packet[i] = Convert.ToByte(s, 16); i++; }
        }
        catch (Exception exp) { MessageBox.Show("Hex values not correct - cannot convert to bytes"); return; }

        //Sending out the packet
        try
        {
            frmCapture.device.SendPacket(packet);
        }
        catch (Exception exp) { MessageBox.Show("Cannot properly send the byte values"); return; }


        }  //End btnSend


        //**********Form is closing - decrement the number of instatiations
        private void frmSend_FormClosed(object sender, FormClosedEventArgs e)
        {
            instantiations--;
        } //End frmSend_FormClosed


        //**********Clear the screen
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPacket.Clear();
        } //End clearToolStripMenuItem_Click
    }
}
