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
            lvResults=new ListView();
            btnRefresh=new Button();
            tbFilter=new TextBox();
            label1=new Label();
            btOpen=new Button();
            btn_Back=new Button();
            lbTracks=new Label();
            btnPlay=new Button();
            SuspendLayout();
            // 
            // lvResults
            // 
            lvResults.Location=new Point(25, 41);
            lvResults.Name="lvResults";
            lvResults.Size=new Size(1207, 766);
            lvResults.TabIndex=0;
            lvResults.UseCompatibleStateImageBehavior=false;
            // 
            // btnRefresh
            // 
            btnRefresh.Location=new Point(25, 815);
            btnRefresh.Name="btnRefresh";
            btnRefresh.Size=new Size(75, 23);
            btnRefresh.TabIndex=1;
            btnRefresh.Text="Refresh";
            btnRefresh.UseVisualStyleBackColor=true;
            btnRefresh.Click+=btnRefresh_Click;
            // 
            // tbFilter
            // 
            tbFilter.Location=new Point(67, 12);
            tbFilter.Name="tbFilter";
            tbFilter.Size=new Size(1165, 23);
            tbFilter.TabIndex=2;
            tbFilter.TextChanged+=tbFilter_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize=true;
            label1.Location=new Point(25, 15);
            label1.Name="label1";
            label1.Size=new Size(36, 15);
            label1.TabIndex=3;
            label1.Text="Filter:";
            // 
            // btOpen
            // 
            btOpen.Location=new Point(106, 815);
            btOpen.Name="btOpen";
            btOpen.Size=new Size(104, 23);
            btOpen.TabIndex=4;
            btOpen.Text="Open Selected";
            btOpen.UseVisualStyleBackColor=true;
            btOpen.Click+=btOpen_Click;
            // 
            // btn_Back
            // 
            btn_Back.Location=new Point(324, 815);
            btn_Back.Name="btn_Back";
            btn_Back.Size=new Size(75, 23);
            btn_Back.TabIndex=5;
            btn_Back.Text="Back";
            btn_Back.UseVisualStyleBackColor=true;
            btn_Back.Click+=btn_Back_Click;
            // 
            // lbTracks
            // 
            lbTracks.AutoSize=true;
            lbTracks.Location=new Point(405, 819);
            lbTracks.Name="lbTracks";
            lbTracks.Size=new Size(39, 15);
            lbTracks.TabIndex=6;
            lbTracks.Text="Tracks";
            // 
            // btnPlay
            // 
            btnPlay.Location=new Point(216, 815);
            btnPlay.Name="btnPlay";
            btnPlay.Size=new Size(102, 23);
            btnPlay.TabIndex=7;
            btnPlay.Text="Play Selected";
            btnPlay.UseVisualStyleBackColor=true;
            btnPlay.Click+=btnPlay_Click;
            // 
            // Results
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(1266, 847);
            Controls.Add(btnPlay);
            Controls.Add(lbTracks);
            Controls.Add(btn_Back);
            Controls.Add(btOpen);
            Controls.Add(label1);
            Controls.Add(tbFilter);
            Controls.Add(btnRefresh);
            Controls.Add(lvResults);
            Name="Results";
            StartPosition=FormStartPosition.CenterScreen;
            Text="Song Analyser Results (Right click entry to copy data)";
            Load+=Results_Load;
            ResumeLayout(false);
            PerformLayout();
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