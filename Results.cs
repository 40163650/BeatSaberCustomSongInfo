using Newtonsoft.Json;
using System.Diagnostics;

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
        List<List<string>> resultsCopy = new();
        List<List<string>> filteredList = new();

        Font itemFont;

        public Results()
        {
            InitializeComponent();

            SetupColumns();

            itemFont = new(lvResults.Font, FontStyle.Regular);

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

            lvResults.View = View.Details;
            lvResults.LabelEdit = false;
            lvResults.AllowColumnReorder = false;
            lvResults.CheckBoxes = false;
            lvResults.FullRowSelect = true;
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

            foreach(string folder in Directory.GetDirectories(path))
            {
                string datfile = folder + "/info.dat";
                if(!File.Exists(datfile))
                {
                    datfile = folder + "/Info.dat";
                    if (!File.Exists(datfile))
                    {
                        break;
                    }
                }

                string text = File.ReadAllText(datfile);

                Song song = JsonConvert.DeserializeObject<Song>(text);

                ListViewItem lvItem = new(song._songName);
               // lvItem.SubItems.Add(song?._songSubName);
                lvItem.SubItems.Add(song._songAuthorName);
                lvItem.SubItems.Add(song._levelAuthorName);
                lvItem.SubItems.Add(Path.GetFileName(folder));

                lvItem.Font = itemFont;

                lvResults.Items.Add(lvItem);

                List<string> strings = new List<string> { song._songName, song._songAuthorName, song._levelAuthorName, Path.GetFileName(folder) };
                resultsCopy.Add(strings);
            }

            ResizeListView(sender, e);
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Called twice when resizing both width and height
        // Even occurs for file explorer, I wouldn't worry about it
        Point buttonRefreshLoc = new(25, 804); // Allocate once
        Point buttonOpenLoc = new(106, 804);
        private void Results_Resize(object sender, EventArgs e)
        {
            lvResults.Width = Width - 75;
            tbFilter.Width = lvResults.Width - 42;
            lvResults.Height = Height - 120;
            buttonRefreshLoc.Y = lvResults.Height + 48;
            buttonOpenLoc.Y = buttonRefreshLoc.Y;
            btnRefresh.Location = buttonRefreshLoc;
            btOpen.Location = buttonOpenLoc;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();
            Results_Load(sender, e);
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            string path = Form1.path;
            string folder = ""; 
            if(lvResults.SelectedItems.Count > 0)
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
                return new ListViewItem { Text = x[0], SubItems = { x[1], x[2], x[3] }, Font = itemFont };
            };

            lvResults.Items.AddRange(filteredList.Select(i => makeListViewItem(i)).ToArray());

            ResizeListView(sender, e);

            ResumeLayout();
        }
    }
}
