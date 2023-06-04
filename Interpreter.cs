using System;
using System.Windows.Input;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EZscript
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int key);

        string[] rawCodeList;
        string rawCode;

        string[] vars = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        bool start = false;

        bool update = false;
        int updateLine = 0;

        string button = "";

        string[] keyboardMap = {
          "", // [0]
          "", // [1]
          "", // [2]
          "CANCEL", // [3]
          "", // [4]
          "", // [5]
          "HELP", // [6]
          "", // [7]
          "BACK_SPACE", // [8]
          "TAB", // [9]
          "", // [10]
          "", // [11]
          "CLEAR", // [12]
          "ENTER", // [13]
          "ENTER_SPECIAL", // [14]
          "", // [15]
          "SHIFT", // [16]
          "CONTROL", // [17]
          "ALT", // [18]
          "PAUSE", // [19]
          "CAPS_LOCK", // [20]
          "KANA", // [21]
          "EISU", // [22]
          "JUNJA", // [23]
          "FINAL", // [24]
          "HANJA", // [25]
          "", // [26]
          "ESCAPE", // [27]
          "CONVERT", // [28]
          "NONCONVERT", // [29]
          "ACCEPT", // [30]
          "MODECHANGE", // [31]
          "SPACE", // [32]
          "PAGE_UP", // [33]
          "PAGE_DOWN", // [34]
          "END", // [35]
          "HOME", // [36]
          "LEFT", // [37]
          "UP", // [38]
          "RIGHT", // [39]
          "DOWN", // [40]
          "SELECT", // [41]
          "PRINT", // [42]
          "EXECUTE", // [43]
          "PRINTSCREEN", // [44]
          "INSERT", // [45]
          "DELETE", // [46]
          "", // [47]
          "0", // [48]
          "1", // [49]
          "2", // [50]
          "3", // [51]
          "4", // [52]
          "5", // [53]
          "6", // [54]
          "7", // [55]
          "8", // [56]
          "9", // [57]
          "COLON", // [58]
          "SEMICOLON", // [59]
          "LESS_THAN", // [60]
          "EQUALS", // [61]
          "GREATER_THAN", // [62]
          "QUESTION_MARK", // [63]
          "AT", // [64]
          "A", // [65]
          "B", // [66]
          "C", // [67]
          "D", // [68]
          "E", // [69]
          "F", // [70]
          "G", // [71]
          "H", // [72]
          "I", // [73]
          "J", // [74]
          "K", // [75]
          "L", // [76]
          "M", // [77]
          "N", // [78]
          "O", // [79]
          "P", // [80]
          "Q", // [81]
          "R", // [82]
          "S", // [83]
          "T", // [84]
          "U", // [85]
          "V", // [86]
          "W", // [87]
          "X", // [88]
          "Y", // [89]
          "Z", // [90]
          "OS_KEY", // [91] Windows Key (Windows) or Command Key (Mac)
          "", // [92]
          "CONTEXT_MENU", // [93]
          "", // [94]
          "SLEEP", // [95]
          "NUMPAD0", // [96]
          "NUMPAD1", // [97]
          "NUMPAD2", // [98]
          "NUMPAD3", // [99]
          "NUMPAD4", // [100]
          "NUMPAD5", // [101]
          "NUMPAD6", // [102]
          "NUMPAD7", // [103]
          "NUMPAD8", // [104]
          "NUMPAD9", // [105]
          "MULTIPLY", // [106]
          "ADD", // [107]
          "SEPARATOR", // [108]
          "SUBTRACT", // [109]
          "DECIMAL", // [110]
          "DIVIDE", // [111]
          "F1", // [112]
          "F2", // [113]
          "F3", // [114]
          "F4", // [115]
          "F5", // [116]
          "F6", // [117]
          "F7", // [118]
          "F8", // [119]
          "F9", // [120]
          "F10", // [121]
          "F11", // [122]
          "F12", // [123]
          "F13", // [124]
          "F14", // [125]
          "F15", // [126]
          "F16", // [127]
          "F17", // [128]
          "F18", // [129]
          "F19", // [130]
          "F20", // [131]
          "F21", // [132]
          "F22", // [133]
          "F23", // [134]
          "F24", // [135]
          "", // [136]
          "", // [137]
          "", // [138]
          "", // [139]
          "", // [140]
          "", // [141]
          "", // [142]
          "", // [143]
          "NUM_LOCK", // [144]
          "SCROLL_LOCK", // [145]
          "WIN_OEM_FJ_JISHO", // [146]
          "WIN_OEM_FJ_MASSHOU", // [147]
          "WIN_OEM_FJ_TOUROKU", // [148]
          "WIN_OEM_FJ_LOYA", // [149]
          "WIN_OEM_FJ_ROYA", // [150]
          "", // [151]
          "", // [152]
          "", // [153]
          "", // [154]
          "", // [155]
          "", // [156]
          "", // [157]
          "", // [158]
          "", // [159]
          "CIRCUMFLEX", // [160]
          "EXCLAMATION", // [161]
          "DOUBLE_QUOTE", // [162]
          "HASH", // [163]
          "DOLLAR", // [164]
          "PERCENT", // [165]
          "AMPERSAND", // [166]
          "UNDERSCORE", // [167]
          "OPEN_PAREN", // [168]
          "CLOSE_PAREN", // [169]
          "ASTERISK", // [170]
          "PLUS", // [171]
          "PIPE", // [172]
          "HYPHEN_MINUS", // [173]
          "OPEN_CURLY_BRACKET", // [174]
          "CLOSE_CURLY_BRACKET", // [175]
          "TILDE", // [176]
          "", // [177]
          "", // [178]
          "", // [179]
          "", // [180]
          "VOLUME_MUTE", // [181]
          "VOLUME_DOWN", // [182]
          "VOLUME_UP", // [183]
          "", // [184]
          "", // [185]
          "SEMICOLON", // [186]
          "EQUALS", // [187]
          "COMMA", // [188]
          "MINUS", // [189]
          "PERIOD", // [190]
          "SLASH", // [191]
          "BACK_QUOTE", // [192]
          "", // [193]
          "", // [194]
          "", // [195]
          "", // [196]
          "", // [197]
          "", // [198]
          "", // [199]
          "", // [200]
          "", // [201]
          "", // [202]
          "", // [203]
          "", // [204]
          "", // [205]
          "", // [206]
          "", // [207]
          "", // [208]
          "", // [209]
          "", // [210]
          "", // [211]
          "", // [212]
          "", // [213]
          "", // [214]
          "", // [215]
          "", // [216]
          "", // [217]
          "", // [218]
          "OPEN_BRACKET", // [219]
          "BACK_SLASH", // [220]
          "CLOSE_BRACKET", // [221]
          "QUOTE", // [222]
          "", // [223]
          "META", // [224]
          "ALTGR", // [225]
          "", // [226]
          "WIN_ICO_HELP", // [227]
          "WIN_ICO_00", // [228]
          "", // [229]
          "WIN_ICO_CLEAR", // [230]
          "", // [231]
          "", // [232]
          "WIN_OEM_RESET", // [233]
          "WIN_OEM_JUMP", // [234]
          "WIN_OEM_PA1", // [235]
          "WIN_OEM_PA2", // [236]
          "WIN_OEM_PA3", // [237]
          "WIN_OEM_WSCTRL", // [238]
          "WIN_OEM_CUSEL", // [239]
          "WIN_OEM_ATTN", // [240]
          "WIN_OEM_FINISH", // [241]
          "WIN_OEM_COPY", // [242]
          "WIN_OEM_AUTO", // [243]
          "WIN_OEM_ENLW", // [244]
          "WIN_OEM_BACKTAB", // [245]
          "ATTN", // [246]
          "CRSEL", // [247]
          "EXSEL", // [248]
          "EREOF", // [249]
          "PLAY", // [250]
          "ZOOM", // [251]
          "", // [252]
          "PA1", // [253]
          "WIN_OEM_CLEAR", // [254]
          "" // [255]
        };

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

                            Convert.ToInt32(rawCodeList[i].Split('=')[0].Split('r')[1].Trim(' ', '+')));
                        }
                        else if (rawCodeList[i].Contains("$"))
                        {
                            vars.SetValue(
                            vars[Convert.ToInt32(rawCodeList[i].Split('=')[1].Split(';')[0].Trim(' ', '$'))],
                            Convert.ToInt32(rawCodeList[i].Split('=')[0].Split('r')[1].Trim(' ')));
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
                    bool ifWork = false;
                    string ifA=  "";
                    string ifB = "";

                    int num = 0;

                    if (rawCodeList[i].Contains("input."))
                    {
                        button = rawCodeList[i].Split('.')[1].Split(')')[0];

                        for (int a = 0; a < keyboardMap.Length; a++)
                        {
                            if (keyboardMap[a] == button.ToUpper())
                            {
                                num = a++;
                                Console.WriteLine(num);
                            }
                        }

                        if ((GetAsyncKeyState(num) & 0x8000) > 0)
                        {
                            i++;
                            ifWork = true;
                            realCode();
                        }

                    }
                    else
                    {
                        ifA = rawCodeList[i].Split('(')[1].Split('=')[0].Trim(')', ' ');

                        if (ifA.Contains("$"))
                        {
                            ifA = Convert.ToString(vars[Convert.ToInt32(rawCodeList[i].Split('=')[0].Split('(')[1].Trim(';', '$', ' '))]);
                        }

                        ifB = rawCodeList[i].Split('(')[1].Split('=')[1].Trim(')', ' ');

                        if (ifB.Contains("$"))
                        {
                            ifB = Convert.ToString(vars[Convert.ToInt32(rawCodeList[i].Split('=')[1].Split(')')[0].Trim(';', '$', ' '))]);
                        }

                        if (ifA == ifB)
                        {
                            i++;
                            ifWork = true;
                            realCode();
                        }

                    }

                    if (ifWork == false)
                    {
                        i++;
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
