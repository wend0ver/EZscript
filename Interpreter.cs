using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EZscript
{
    public partial class Form1 : Form
    {

        string[] rawCodeList;
        string rawCode;

        string[] vars = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        bool start = false;

        bool update = false;
        int updateLine = 0;

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

            if (this.richTextBox1.Text.Trim('"').EndsWith(".txt"))
            {
                string fileLocation = this.richTextBox1.Text;
                fileLocation.Replace("\\", "/");
                fileLocation = fileLocation.Trim('"');
                rawCode = File.ReadAllText(fileLocation);
                rawCodeList = rawCode.Split(';');
                this.richTextBox1.Visible = false;
                this.button1.Visible = false;
                this.label1.Visible = false;
                start = true;
                runCode();
            }
            else
            {
                this.richTextBox1.Text = "File must be a .txt file type";
            }

            

        }

        void runCode()
        {


            for (int i = updateLine; i < rawCodeList.Length; i++)
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

                        if (rawCodeList[i].Split('=')[1].Trim(';',' ').Contains('$'))
                        {
                            this.Text = Convert.ToString(vars[Convert.ToInt32(rawCodeList[i].Split('=')[1].Trim(';').Trim('$',' '))]);
                        }
                        else
                        {
                            this.Text = rawCodeList[i].Split('=')[1].Trim(';');
                        }

                    }

                    // Window Render

                    if (rawCodeList[i].Contains("window.render"))
                    {

                        int posX;
                        int posY;
                        int sizeX;
                        int sizeY;

                        if (rawCodeList[i].Split('=')[1].Split(',')[0].Trim(' ').Contains('$'))
                        {
                            posX = Convert.ToInt32(vars[int.Parse(rawCodeList[i].Split('=')[1].Split(',')[0].Trim(' ','$'))]);
                        }
                        else
                        {
                            posX = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[0].Trim(' '));
                        }
                        ///////////////////////////
                        if (rawCodeList[i].Split('=')[1].Split(',')[1].Trim(' ').Contains('$'))
                        {
                            posY = Convert.ToInt32(vars[int.Parse(rawCodeList[i].Split('=')[1].Split(',')[1].Trim(' ', '$'))]);
                        }
                        else
                        {
                            posY = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[1].Trim(' '));
                        }
                        ///////////////////////////
                        if (rawCodeList[i].Split('=')[1].Split(',')[2].Trim(' ').Contains('$'))
                        {
                            sizeX = Convert.ToInt32(vars[int.Parse(rawCodeList[i].Split('=')[1].Split(',')[2].Trim(' ', '$'))]);
                        }
                        else
                        {
                            sizeX = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[2].Trim(' '));
                        }
                        ///////////////////////////
                        if (rawCodeList[i].Split('=')[1].Split(',')[3].Trim(' ').Contains('$'))
                        {
                            sizeY = Convert.ToInt32(vars[int.Parse(rawCodeList[i].Split('=')[1].Split(',')[3].Trim(' ', '$'))]);
                        }
                        else
                        {
                            sizeY = int.Parse(rawCodeList[i].Split('=')[1].Split(',')[3].Trim(' '));
                        }

                        Graphics g = this.CreateGraphics();

                        g.FillRectangle(Brushes.Black, new Rectangle(posX + (this.Size.Width / 2) - (sizeX / 2), -posY + (this.Size.Height / 2) - (sizeY / 2), sizeX, sizeY));

                    }

                    // Window Clear

                    if (rawCodeList[i].Contains("window.clear"))
                    {
                        Graphics g = this.CreateGraphics();

                        g.Clear(Color.White);

                    }



                    // Update
                    if (rawCodeList[i].Contains("func update"))
                    {
                        if (update == false)
                        {
                            update = true;
                            updateLine = i + 1;
                        }
                    }



                    // Var

                    if (rawCodeList[i].Contains("var"))
                    {
                        if (rawCodeList[i].Contains("+="))
                        {
                            vars.SetValue(
                            Convert.ToString(

                            Convert.ToInt32(vars[Convert.ToInt32(rawCodeList[i].Split('+')[0].Split('r')[1].Trim(' '))]) +  
                            Convert.ToInt32(rawCodeList[i].Split('=')[1].Split(';')[0].Trim(' '))),

                            Convert.ToInt32(rawCodeList[i].Split('=')[0].Split('r')[1].Trim(' ','+')));
                        }
                        else
                        {
                            vars.SetValue(
                            Convert.ToString(rawCodeList[i].Split('=')[1].Split(';')[0].Trim(' ')),
                            Convert.ToInt32(rawCodeList[i].Split('=')[0].Split('r')[1].Trim(' ')));
                        }

                    }



                }


                // If
                if (rawCodeList[i].Contains("if"))
                {

                    string ifA = rawCodeList[i].Split('(')[1].Split('=')[0].Trim(')', ' ');

                    if (ifA.Contains("$"))
                    {
                        ifA = Convert.ToString(vars[Convert.ToInt32(rawCodeList[i].Split('=')[0].Split('(')[1].Trim(';','$', ' '))]);
                    }

                    string ifB = rawCodeList[i].Split('(')[1].Split('=')[1].Trim(')', ' ');

                    if (ifB.Contains("$"))
                    {
                        ifB = Convert.ToString(vars[Convert.ToInt32(rawCodeList[i].Split('=')[1].Split(')')[0].Trim(';', '$', ' '))]);  
                    }

                    i++;

                    if (ifA == ifB)
                    {
                        realCode();
                    }


                }

                
            }
 

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (start == true && update == true)
            {
                runCode();
            }
        }
    }
}
