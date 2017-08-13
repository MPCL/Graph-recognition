using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Date: 2017/8/13 
 * Name : Graphical Finder
 * Auther : Masoud Pourghaffar (MPCL5)
 */ 

namespace Graphical
{
    public partial class FrmMain : Form
    {
        // some codes for movale form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public FrmMain()
        {
            InitializeComponent();
        }

        private void say_false()
        {
            lblresualt.BackColor = Color.Red;
            lblresualt.Text = "Not graphical";
        }

        private void say_true()
        {
            lblresualt.BackColor = Color.LimeGreen;
            lblresualt.Text = "Is graphical";
        }

        private bool check(string ctext)
        {
            // found from there: https://stackoverflow.com/questions/1818611/how-to-check-if-a-particular-character-exists-within-a-character-array
            if (ctext.IndexOf('-') != -1)
                return true;
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            if (check(txtDeg.Text))
                say_false();
            else
            {
                string sorteddeg = Sort(',' + txtDeg.Text, ',');
                while (true)
                {
                    var M = sorteddeg.Split(',');
                    if (M[1] == "0")
                    {
                        say_true();
                        break;
                    }
                    sorteddeg = "";
                    for(int i=2;i<=(int.Parse(M[1])+1);i++)
                    {
                        try
                        {
                            sorteddeg = sorteddeg + ',' + (int.Parse(M[i]) - 1).ToString();
                        }
                        catch
                        {
                            say_false();
                            return;
                        }
                    }
                    if (check(sorteddeg))
                    {
                        say_false();
                        break;
                    }
                    sorteddeg=Sort(sorteddeg,',');
                    //MessageBox.Show(sorteddeg);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //for exit programe
            Environment.Exit(1);
        }

        // A void for sort numbers
        private string Sort(string Deg,char ch)
        {
            string result="";

            for (int i = 1; ; i++)
            {
                try
                {
                    int lol = int.Parse(maximum(Deg, ch));
                        for(int n=1;n<=count(Deg,lol.ToString(),lol.ToString().Length);n++)
                        {
                            result = result +ch+ maximum(Deg, ch);
                        }
                    Deg=Deg.Replace(','+maximum(Deg, ch),"");

                }
                catch{break; }
            }
            return result;
        }

        // A void for count any thing
        private int count(string X,string l,int cc)
        {
            int L=0;
            for(int i=0;i<X.Length;i++)
            {
                try
                {
                    string y = X.Substring(i, cc);
                    if (y == l)
                        L++;
                }
                catch { break; }
            }
            return L;           
        }

        // A void for return maximum
        private string maximum(string X, char ch)
        {
            var slice = X.Split(ch);
            int max = int.Parse(slice[1]);
            for (int i = 2; i <= count(X, ch.ToString(), 1); i++)
            {
                if (int.Parse(slice[i]) > max)
                {
                    MessageBox.Show(slice[i]);
                    max = int.Parse(slice[i]);
                }
            }
            return max.ToString();
        }

        //make form moveable
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
