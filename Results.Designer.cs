namespace SongAnalyser
{
    partial class Results
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvResults = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btOpen = new System.Windows.Forms.Button();
            this.btn_Back = new System.Windows.Forms.Button();
            this.lbTracks = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvResults
            // 
            this.lvResults.Location = new System.Drawing.Point(25, 41);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(1207, 766);
            this.lvResults.TabIndex = 0;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(25, 815);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(67, 12);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(1165, 23);
            this.tbFilter.TabIndex = 2;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter:";
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(106, 815);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(104, 23);
            this.btOpen.TabIndex = 4;
            this.btOpen.Text = "Open Selected";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // btn_Back
            // 
            this.btn_Back.Location = new System.Drawing.Point(324, 815);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 23);
            this.btn_Back.TabIndex = 5;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // lbTracks
            // 
            this.lbTracks.AutoSize = true;
            this.lbTracks.Location = new System.Drawing.Point(405, 819);
            this.lbTracks.Name = "lbTracks";
            this.lbTracks.Size = new System.Drawing.Size(39, 15);
            this.lbTracks.TabIndex = 6;
            this.lbTracks.Text = "Tracks";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(216, 815);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(102, 23);
            this.btnPlay.TabIndex = 7;
            this.btnPlay.Text = "Play Selected";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 847);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lbTracks);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lvResults);
            this.Name = "Results";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Song Analyser Results";
            this.Load += new System.EventHandler(this.Results_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView lvResults;
        private Button btnRefresh;
        private TextBox tbFilter;
        private Label label1;
        private Button btOpen;
        private Button btn_Back;
        private Label lbTracks;
        private Button btnPlay;
    }
}