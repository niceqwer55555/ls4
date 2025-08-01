using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LeagueSandbox_LAN_Server_Launcher
{
    public partial class Form1 : Form
    {
        private bool maximized = false;
        public static List<Form2> red = new List<Form2>();
        public static List<Form2> blue = new List<Form2>();
        public static int redPlayerCount = 0;
        public static int bluePlayerCount = 0;
        public static int playerCount = 0;
        public static int playerId = 0;
        public static List<int> idList = new List<int>();
        public static List<int[]> redMapping = new List<int[]>();
        public static List<int[]> blueMapping = new List<int[]>();
        public static bool testingMode = false;

        private int progressValue = 100;
        private bool debug = true;
        private string contentPath;
        private bool cheatsEnabled = false;
        private bool manacostsEnabled = true;
        private bool cooldownsEnabled = true;
        private bool minionSpawnsEnabled = true;
        private int isBot; // To store the value from ls4lan.ini

        public Form1()
        {
            InitializeComponent();

            // Ensure ls4lan.ini is created and initialized
            IniFile ini = new IniFile();

            // Read content path from INI file
            contentPath = ini.Read("Settings", "ContentPath", "../../../../Content");
            textBox1.Text = contentPath;

            // Read IsBot setting from INI file
            isBot = int.Parse(ini.Read("Settings", "IsBot", "0"));

            Console.WriteLine($"Content path read from INI: {contentPath}");
            Console.WriteLine($"IsBot setting from INI: {isBot}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void setProgess(int value)
        {
            float maximumWidth = panel6.Width / 100;
            if ((maximumWidth * 100 - value * maximumWidth) >= 0)
            {
                progress.Width = Convert.ToInt32(maximumWidth * (100 - value));
                button3.Width = Convert.ToInt32(value * maximumWidth);
                button3.TextAlign = ContentAlignment.MiddleCenter;
                progressValue = value;
            }
            this.Invalidate();
        }

        bool launchDisabled = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (!launchDisabled)
            {
                launchDisabled = true;
                setProgess(30);
                button3.Text = "构建玩家...";
                Game game = prepareBuild();
                string buffer = buildJson(game);
                Console.WriteLine(buffer);
                setProgess(85);
                button3.Text = "写入Json...";

                // 获取或设置 GameInfo.json 路径
                IniFile ini = new IniFile();
                string jsonPath = ini.Read("Settings", "GameInfoFilePath", "Settings\\GameInfo.json");

                try
                {
                    File.WriteAllText(jsonPath, buffer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("无法找到目录：\n" + ex.Message, "错误 - 无法找到目录", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                setProgess(95);
                button3.Text = "启动服务器...";
                // 获取 GameServerConsole.exe 路径
                string gameServerPath = ini.Read("Settings", "GameServerPath", "GameServerConsole.exe");
                try
                {
                    Process.Start(gameServerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("无法启动服务器：\n" + ex.Message, "错误 - 无法启动服务器", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                setProgess(100);
                button3.Text = "启动服务器！";
                launchDisabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            maximizeTrigger();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point LeftDown;
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            LeftDown.X = e.X;
            LeftDown.Y = e.Y;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = MousePosition;
                p.Offset(-LeftDown.X, -LeftDown.Y);
                Location = p;
            }
            if (maximized & e.Button == MouseButtons.Left)
            {
                button5.Text = "口";
                this.WindowState = FormWindowState.Normal;
                maximized = !maximized;
                setProgess(progressValue);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            LeftDown.X = e.X;
            LeftDown.Y = e.Y;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = MousePosition;
                p.Offset(-LeftDown.X, -LeftDown.Y);
                Location = p;
            }
            if (maximized & e.Button == MouseButtons.Left)
            {
                button5.Text = "口";
                this.WindowState = FormWindowState.Normal;
                maximized = !maximized;
                setProgess(progressValue);
            }
        }

        private void panel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            maximizeTrigger();
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            maximizeTrigger();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add before use
            playerCount++;
            redPlayerCount++;
            if (redPlayerCount < 6)
            {
                // Determine playerId based on IsBot setting
                if (redPlayerCount == 1)
                {
                    playerId = isBot == 1 ? 1 : 1;
                }
                else
                {
                    if (isBot == 1)
                    {
                        playerId = -1;
                    }
                    else
                    {
                        playerId = idList.Count == 0 ? 1 : idList[idList.Count - 1] + 1;
                        while (playerId == 0)
                        {
                            playerId++;
                        }
                    }
                }

                // Used to link listboxes to tabpage indexes
                int[] mapping = { redPlayerCount - 1, playerCount };
                redMapping.Add(mapping);
                listBox1.Items.Add("Player" + Convert.ToString(playerId));
                // New player instance
                Form2 form = new Form2(playerId, "Player" + Convert.ToString(playerId), "RED", tabControl1, listBox1, redMapping[redMapping.Count - 1], isBot);
                // New tabpage to hold the instance
                TabPage neu = new TabPage();
                // Instance initilization
                neu.BackColor = Color.Maroon;
                form.Dock = DockStyle.Fill;
                form.TopLevel = false;
                form.WindowState = FormWindowState.Maximized;
                // Add to the UI Display
                red.Add(form);
                neu.Controls.Add(red[redPlayerCount - 1]);
                tabControl1.TabPages.Add(neu);
                red[redPlayerCount - 1].Show();
                tabControl1.SelectedIndex = playerCount;
                idList.Add(playerId);
                listBox1.SelectedIndex = redPlayerCount - 1;
            }
            else
            {
                playerCount--;
                redPlayerCount--;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playerCount++;
            bluePlayerCount++;
            if (bluePlayerCount < 6)
            {
                // Determine playerId based on IsBot setting
                if (bluePlayerCount == 1)
                {
                    playerId = isBot == 1 ? 1 : 1;
                }
                else
                {
                    if (isBot == 1)
                    {
                        playerId = -1;
                    }
                    else
                    {
                        playerId = idList.Count == 0 ? 1 : idList[idList.Count - 1] + 1;
                        while (playerId == 0)
                        {
                            playerId++;
                        }
                    }
                }

                int[] mapping = { bluePlayerCount - 1, playerCount };
                blueMapping.Add(mapping);
                listBox2.Items.Add("Player" + Convert.ToString(playerId));
                Form2 form = new Form2(playerId, "Player" + Convert.ToString(playerId), "BLUE", tabControl1, listBox2, blueMapping[blueMapping.Count - 1], isBot);
                TabPage neu = new TabPage();
                neu.BackColor = Color.MidnightBlue;
                form.Dock = DockStyle.Fill;
                form.TopLevel = false;
                form.WindowState = FormWindowState.Maximized;
                blue.Add(form);
                neu.Controls.Add(blue[bluePlayerCount - 1]);
                tabControl1.TabPages.Add(neu);
                blue[bluePlayerCount - 1].Show();
                tabControl1.SelectedIndex = playerCount;
                idList.Add(playerId);
                listBox2.SelectedIndex = bluePlayerCount - 1;
            }
            else
            {
                playerCount--;
                bluePlayerCount--;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int[] mapping in redMapping)
            {
                if (listBox1.SelectedIndex == mapping[0])
                {
                    if (!(listBox2.SelectedIndex < 0))
                        listBox2.SelectedIndex = -1;
                    tabControl1.SelectedIndex = mapping[1];
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int[] mapping in blueMapping)
            {
                if (listBox2.SelectedIndex == mapping[0])
                {
                    if (!(listBox1.SelectedIndex < 0))
                        listBox1.SelectedIndex = -1;
                    tabControl1.SelectedIndex = mapping[1];
                }
            }
        }

        private void maximizeTrigger()
        {
            if (maximized)
            {
                button5.Text = "口";
                this.WindowState = FormWindowState.Normal;
                maximized = !maximized;
            }
            else
            {
                button5.Text = "▽";
                this.WindowState = FormWindowState.Maximized;
                maximized = !maximized;
            }
            setProgess(progressValue);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (debug) return;
            // Check if GameServerConsole.exe exists
            IniFile ini = new IniFile();
            string gameServerPath = ini.Read("Settings", "GameServerPath", "GameServerConsole.exe");
            if (!File.Exists(gameServerPath) || !Directory.Exists("Settings\\"))
            {
                MessageBox.Show("未找到服务器文件，请将本程序至于GameServerConsole.exe同级目录！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private Game prepareBuild()
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < redPlayerCount; i++)
            {
                Form2 builder = tabControl1.TabPages[redMapping[i][1]].Controls[0] as Form2;
                players.Add(builder.buildPlayer());
            }
            for (int i = 0; i < bluePlayerCount; i++)
            {
                Form2 builder = tabControl1.TabPages[blueMapping[i][1]].Controls[0] as Form2;
                players.Add(builder.buildPlayer());
            }
            setProgess(50);
            button3.Text = "建立数据结构...";
            return new Game(players, manacostsEnabled, cooldownsEnabled, cheatsEnabled, minionSpawnsEnabled, contentPath);
        }

        private string buildJson(Game game)
        {
            setProgess(75);
            button3.Text = "构建Json...";
            return JsonConvert.SerializeObject(game, Formatting.Indented);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            contentPath = textBox1.Text;
            textBox1.ForeColor = Color.WhiteSmoke;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cheatsEnabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            manacostsEnabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            cooldownsEnabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            minionSpawnsEnabled = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            label7.Visible = checkBox5.Checked;
            checkBox5.ForeColor =
                !checkBox5.Checked ?
                Color.PaleGreen :
                Color.OrangeRed;
            testingMode = checkBox5.Checked;
            for (int i = 0; i < redPlayerCount; i++)
            {
                Form2 cache = tabControl1.TabPages[redMapping[i][1]].Controls[0] as Form2;
                cache.updateTestMode();
            }
            for (int i = 0; i < bluePlayerCount; i++)
            {
                Form2 cache = tabControl1.TabPages[blueMapping[i][1]].Controls[0] as Form2;
                cache.updateTestMode();
            }
        }
    }
}
