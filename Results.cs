using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using NAudio.Wave;
using NAudio.Vorbis;

namespace SongAnalyser
{
    // Used to deserialize json
    struct CustomData
    {
        public string[] _contributors;
        public string _customEnvironment;
        public string _customEnvironmentHash;
    }

    struct CustomDifficultyData
    {
        // Is this really necessary?
    }

    struct DifficultyBeatmap
    {
        public string _difficulty;
        public int _difficultyRank;
        public string _beatmapFilename;
        public int _noteJumpMovementSpeed;
        public float _noteJumpStartBeatOffset;
        public CustomDifficultyData _customData;
    }

    struct DifficultyBeatmapSet
    {
        public string _beatmapCharacteristicName;
        public DifficultyBeatmap[] _difficultyBeatmaps;
    }

    struct Song
    {
        public string _version;
        public string _songName;
        public string _songSubName;
        public string _songAuthorName;
        public string _levelAuthorName;

        public float _beatsPerMinute;
        public float _shuffle;
        public float _shufflePeriod;
        public float _previewStartTime;
        public float _previewDuration;

        public string _songFilename;
        public string _coverImageFilename;
        public string _environmentData;

        public CustomData customData;
    }

    public partial class Results : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int MessageBoxTimeout(IntPtr hWnd, String lpText, String lpCaption, uint uType, Int16 wLanguageId, Int32 dwMilliseconds);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        private bool playing = false;
        private VorbisWaveReader waveReader;
        private WaveOut waveOut;

        List<List<string>> resultsCopy = new();
        List<List<string>> filteredList = new();

        Font itemFont;

        Form1 MyParentForm;

        // Not sure how I feel about this more efficient resizing
        bool currentlyResizing = false;
        private const int WM_ENTERSIZEMOVE = 0x0231;
        private const int WM_EXITSIZEMOVE = 0x0232;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_ENTERSIZEMOVE:
                    currentlyResizing = true;
                    break;

                case WM_EXITSIZEMOVE:
                    currentlyResizing = false;
                    Results_Resize(null, null);
                    break;
            }
            base.WndProc(ref m);
        }


        private void lv_Click(object sender, MouseEventArgs e)
        {
            if (lvResults == null || lvResults.SelectedItems == null || lvResults.SelectedItems[0] == null || e == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                string text =                 lvResults.SelectedItems[0].SubItems[0].Text + "\n" +
                              "Song By: "   + lvResults.SelectedItems[0].SubItems[1].Text + "\n" +
                              "Map By: "    + lvResults.SelectedItems[0].SubItems[2].Text + "\n" +
                              "Map Link: "  + lvResults.SelectedItems[0].SubItems[4].Text;
                Clipboard.SetText(text);
                // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-messagebox
                uint uiFlags = 0x00000000 | 0x00040000 | 0x00001000 | 0x00000040;
                MessageBoxTimeout(GetForegroundWindow(), $"Data Copied", $"", uiFlags, 0, 400);
            }
        }

        public Results(Form1 form1)
        {
            InitializeComponent();

            MyParentForm = form1;

            SetupColumns();

            itemFont = new(lvResults.Font, FontStyle.Regular);

            lvResults.MouseClick += lv_Click;

            FormClosing += Results_FormClosing;
            Resize += Results_Resize;
        }

        public void SetupColumns()
        {
            lvResults.Font = new Font(lvResults.Font, FontStyle.Bold);

            // Width of -2 indicates auto-size.
            lvResults.Columns.Add("Song Name", -2, HorizontalAlignment.Left);
            // lvResults.Columns.Add("Song Subtitle", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Artist", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Mapper", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Folder Name", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Link", -2, HorizontalAlignment.Left);

            lvResults.View = View.Details;
            lvResults.LabelEdit = false;
            lvResults.AllowColumnReorder = false;
            lvResults.CheckBoxes = false;
            lvResults.FullRowSelect = true;
            lvResults.MultiSelect = false;
            lvResults.GridLines = true;
            lvResults.Sorting = SortOrder.Ascending;
        }

        public void ResizeListView(object sender, EventArgs e)
        {
            for (int i = 0; i < lvResults.Columns.Count; i++)
            {
                lvResults.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void Results_Load(object sender, EventArgs e)
        {
            resultsCopy.Clear();

            string path = Form1.path;

            if (!Directory.Exists(path))
            {
                return;
            }

            foreach (string folder in Directory.GetDirectories(path))
            {
                string datfile = folder + "/info.dat";
                if (!File.Exists(datfile))
                {
                    datfile = folder + "/Info.dat";
                    if (!File.Exists(datfile))
                    {
                        continue;
                    }
                }

                string text = File.ReadAllText(datfile);

                Song song = JsonConvert.DeserializeObject<Song>(text);

                string shortFolder = Path.GetFileName(folder);

                string link = " ";
                Regex rx = new Regex(@"[0-9A-Fa-f]+ ");
                if (rx.IsMatch(shortFolder))
                {
                    link = "https://beatsaver.com/maps/" + rx.Match(shortFolder);
                }

                // 273e5 (Bury The Light - Chained.Trap)
                int hyphenIndex = shortFolder.LastIndexOf("-") + 2;
                int bracketIndex = shortFolder.IndexOf(")");
                string mapperAlt = "";
                if (bracketIndex > 0 && hyphenIndex < bracketIndex)
                {
                    Console.WriteLine("BI: " + bracketIndex + " HI: " +  hyphenIndex);
                    mapperAlt = " (" + shortFolder.Substring(hyphenIndex, bracketIndex - hyphenIndex) + ")";
                }

                ListViewItem lvItem = new(song._songName);
                // lvItem.SubItems.Add(song?._songSubName);
                lvItem.SubItems.Add(song._songAuthorName);
                lvItem.SubItems.Add(song._levelAuthorName + mapperAlt);
                lvItem.SubItems.Add(shortFolder);
                lvItem.SubItems.Add(link);

                lvItem.Font = itemFont;

                lvResults.Items.Add(lvItem);

                List<string> strings = new List<string> { song._songName, song._songAuthorName, song._levelAuthorName, shortFolder, link };
                resultsCopy.Add(strings);
            }

            lbTracks.Text = lvResults.Items.Count.ToString() + " Tracks";

            ResizeListView(sender, e);
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Called twice when resizing both width and height
        // Even occurs for file explorer, I wouldn't worry about it
        Point buttonRefreshLoc = new(25, 815); // Allocate once
        Point buttonOpenLoc = new(106, 815);
        Point buttonPlayLoc = new(216, 815);
        Point buttonBackLoc = new(324, 815);
        Point labelTracksLoc = new(405, 819);
        private void Results_Resize(object sender, EventArgs e)
        {
            if (currentlyResizing) return;
            lvResults.Width = Width - 75;
            tbFilter.Width = lvResults.Width - 42;
            lvResults.Height = Height - 120;

            buttonRefreshLoc.Y = lvResults.Height + 49;
            buttonOpenLoc.Y    = buttonRefreshLoc.Y;
            buttonPlayLoc.Y    = buttonRefreshLoc.Y;
            buttonBackLoc.Y    = buttonRefreshLoc.Y;
            labelTracksLoc.Y   = buttonRefreshLoc.Y + 4;

            btnRefresh.Location = buttonRefreshLoc;
            btOpen.Location     = buttonOpenLoc;
            btnPlay.Location    = buttonPlayLoc;
            btn_Back.Location   = buttonBackLoc;
            lbTracks.Location   = labelTracksLoc;
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();
            Results_Load(sender, e);
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            string path = Form1.path;
            string folder = "";
            if (lvResults.SelectedItems.Count > 0)
            {
                folder = lvResults.SelectedItems[0].SubItems[3].Text;
            }

            string combined = Path.Combine(path, folder).Replace(@"\\", @"\");
            combined = combined.Replace(@"\\", @"\"); // Again
            combined = combined.Replace(@"/", @"\");

            Process.Start("explorer.exe", "\"" + combined + "\""); // This is very particular about the right slashes
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            filteredList = resultsCopy.Where(item => item.Any(s => s.Contains(tbFilter.Text, StringComparison.CurrentCultureIgnoreCase))).ToList();

            SuspendLayout();

            lvResults.Items.Clear();

            Func<List<string>, ListViewItem> makeListViewItem = x =>
            {
                return new ListViewItem { Text = x[0], SubItems = { x[1], x[2], x[3], x[4] }, Font = itemFont };
            };

            lvResults.Items.AddRange(filteredList.Select(i => makeListViewItem(i)).ToArray());

            ResizeListView(sender, e);

            ResumeLayout();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            MyParentForm.ExistingResults = this;
            MyParentForm.Show();
            this.Hide();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            waveReader.Dispose();
            waveOut.Dispose();
            btnPlay.Text = "Play Selected";
            playing = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (playing)
            {
                waveOut.Stop();
                return;
            }

            if (lvResults.SelectedItems.Count == 0)
            {
                return;
            }

            string path = Form1.path;
            string dir = lvResults.SelectedItems[0].SubItems[3].Text;
            dir = Path.Combine(path, dir).Replace(@"\\", @"\");
            dir = dir.Replace(@"\\", @"\"); // Again
            dir = dir.Replace(@"/", @"\");
            if (!Directory.Exists(dir))
            {
                return;
            }
            List<string> fileList = Directory.GetFiles(dir).ToList();

            // No need for complicated regex, this will be small
            foreach (string file in fileList)
            {
                if (file.EndsWith("ogg") || file.EndsWith("egg"))
                {
                    waveReader = new VorbisWaveReader(file);
                    waveOut = new WaveOut();
                    waveOut.Init(waveReader);
                    waveOut.Volume = 0.25f; // Actually sets the volume of the application
                    waveOut.Play();
                    waveOut.PlaybackStopped += OnPlaybackStopped;
                    playing = true;
                    btnPlay.Text = "Stop";
                    break; // Do not do this more than once
                }
            }
        }
    }
}
