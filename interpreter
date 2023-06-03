using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EZscript
{
    public partial class Form1 : Form
    {

        string[] rawCodeList;
        string rawCode;
        string[] rawCodeLines;
        


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            
        }

        public void button1_Click(object sender, EventArgs e)
        {

            if (this.richTextBox1.Text.EndsWith(".txt"))
            {
                string fileLocation = this.richTextBox1.Text;
                fileLocation.Replace("\\", "/");
                rawCode = File.ReadAllText(fileLocation);
                rawCodeList = rawCode.Split(';');
                rawCodeLines = File.ReadAllLines(fileLocation);
                this.richTextBox1.Visible = false;
                this.button1.Visible = false;
                this.label1.Visible = false;

                runCode();
            }
            else
            {
                this.richTextBox1.Text = "File must be a .txt file type";
            }

            

        }

        void runCode()
        {

            for (int i = 0; i < rawCodeList.Length; i++)
            {

                realCode();

                void realCode()
                {

                    // Window Size

                    if (rawCodeList[i].Contains("window.size"))
                    {

                        this.MinimumSize = new Size(0, 0);
                        this.MaximumSize = new Size(0, 0);

                        this.Height = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[0]);
                        this.Width = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[1]);

                        this.MinimumSize = this.Size;
                        this.MaximumSize = this.Size;
                    }


                    // Window Name
                    if (rawCodeList[i].Contains("window.name"))
                    {

                        this.Text = rawCodeList[i].Split('=')[1].Trim(';');

                    }

                    // Window Render

                    if (rawCodeList[i].Contains("window.render"))
                    {

                        int posX = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[0].Trim(' '));
                        int posY = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[1].Trim(' '));
                        int sizeX = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[2].Trim(' '));
                        int sizeY = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[3].Trim(' '));

                        Graphics g = this.CreateGraphics();

                        g.FillRectangle(Brushes.Black, new Rectangle(posX + (this.Size.Width / 2) - (sizeX / 2), -posY + (this.Size.Height / 2) - (sizeY / 2), sizeX, sizeY));

                    }

                }


                // If
                if (rawCodeList[i].Contains("if"))
                {

                    string ifA = rawCodeList[i].Split('(')[1].Split('=')[0].Trim(')', ' ');
                    string ifB = rawCodeList[i].Split('(')[1].Split('=')[1].Trim(')', ' ');

                    i++;

                    if (ifA == ifB)
                    {
                        realCode();
                    }
                    else
                    {
                        Console.WriteLine("false");
                    }


                }
            }





        }


    }
}
