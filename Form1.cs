using Microsoft.Win32;

namespace SongAnalyser
{
    public partial class Form1 : Form
    {
        public Results ExistingResults;
        public static string path = "";
        public Form1()
        {
            InitializeComponent();
        }


        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            path = tbPath.Text;

            if(Directory.Exists(path))
            {
                if(ExistingResults == null)
                {
                    Results results = new(this);
                    results.Show();
                    Hide();
                }
                else
                {
                    ExistingResults.Show();
                    Hide();
                    ExistingResults.btnRefresh_Click(null, null);
                }
            }
            else
            {
                MessageBox.Show("Path does not exist");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey? steamKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Valve\\Steam");
            if(steamKey == null)
            {
                steamKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Valve\\Steam");
                if(steamKey == null)
                {
                    return;
                }
            }

            object? o = steamKey.GetValue("InstallPath");
            if(o == null)
            {
                return;
            }

            string? installPath = o as string; // where steam.exe is
            string vdfFile = installPath + "/steamapps/libraryfolders.vdf";
            if(!File.Exists(vdfFile))
            {
                return;
            }

            List<string> lines = new();
            string path = "";
            foreach(string line in File.ReadLines(vdfFile))
            {
                lines.Add(line);
                // we have beat saber
                if(line.Contains("\"620980\""))
                {
                    // traverse lines backwards
                    for(int i = lines.Count - 1; i >= 0; i--)
                    {
                        if (lines[i].Contains("path"))
                        {
                            // same as lines[i].Substring(11, lines[i].Length - 12)
                            path = lines[i][11..^1] + "/steamapps/common/Beat Saber/Beat Saber_Data/CustomLevels";
                            break;
                        }
                    }
                    break;
                }
            }

            tbPath.Text = path;
        }

        private void tbPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAnalyse_Click(sender, e);
            }
        }
    }
}