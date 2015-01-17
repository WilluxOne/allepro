namespace AllePro
{
  partial class MainForm
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
      this.components = new System.ComponentModel.Container();
      this.SearchButton = new System.Windows.Forms.Button();
      this.LoginButton = new System.Windows.Forms.Button();
      this.SearchQueryField = new System.Windows.Forms.TextBox();
      this.UserNameField = new System.Windows.Forms.TextBox();
      this.UserNameLabel = new System.Windows.Forms.Label();
      this.PasswordLabel = new System.Windows.Forms.Label();
      this.PasswordField = new System.Windows.Forms.TextBox();
      this.ClusterButton = new System.Windows.Forms.Button();
      this.LoginGroup = new System.Windows.Forms.GroupBox();
      this.SearchGroup = new System.Windows.Forms.GroupBox();
      this.SearchLabel = new System.Windows.Forms.Label();
      this.ClusteringGroup = new System.Windows.Forms.GroupBox();
      this.LoginWorker = new System.ComponentModel.BackgroundWorker();
      this.SearchWorker = new System.ComponentModel.BackgroundWorker();
      this.ClusterWorker = new System.ComponentModel.BackgroundWorker();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.StatusLabel = new System.Windows.Forms.Label();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.ClusterListView = new System.Windows.Forms.ListView();
      this.ClusterImages = new System.Windows.Forms.ImageList(this.components);
      this.LoginGroup.SuspendLayout();
      this.SearchGroup.SuspendLayout();
      this.ClusteringGroup.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      // 
      // SearchButton
      // 
      this.SearchButton.Enabled = false;
      this.SearchButton.Location = new System.Drawing.Point(119, 16);
      this.SearchButton.Name = "SearchButton";
      this.SearchButton.Size = new System.Drawing.Size(75, 23);
      this.SearchButton.TabIndex = 0;
      this.SearchButton.Text = "Search";
      this.SearchButton.UseVisualStyleBackColor = true;
      this.SearchButton.Click += new System.EventHandler(this.button1_Click);
      // 
      // LoginButton
      // 
      this.LoginButton.Location = new System.Drawing.Point(6, 102);
      this.LoginButton.Name = "LoginButton";
      this.LoginButton.Size = new System.Drawing.Size(75, 23);
      this.LoginButton.TabIndex = 1;
      this.LoginButton.Text = "Login";
      this.LoginButton.UseVisualStyleBackColor = true;
      this.LoginButton.Click += new System.EventHandler(this.button2_Click);
      // 
      // SearchQueryField
      // 
      this.SearchQueryField.Enabled = false;
      this.SearchQueryField.Location = new System.Drawing.Point(13, 19);
      this.SearchQueryField.Name = "SearchQueryField";
      this.SearchQueryField.Size = new System.Drawing.Size(100, 20);
      this.SearchQueryField.TabIndex = 2;
      this.SearchQueryField.Text = "otwieracz do konserw";
      // 
      // UserNameField
      // 
      this.UserNameField.Location = new System.Drawing.Point(6, 35);
      this.UserNameField.Name = "UserNameField";
      this.UserNameField.Size = new System.Drawing.Size(100, 20);
      this.UserNameField.TabIndex = 3;
      this.UserNameField.Text = "dulek000";
      // 
      // UserNameLabel
      // 
      this.UserNameLabel.AutoSize = true;
      this.UserNameLabel.Location = new System.Drawing.Point(6, 16);
      this.UserNameLabel.Name = "UserNameLabel";
      this.UserNameLabel.Size = new System.Drawing.Size(61, 13);
      this.UserNameLabel.TabIndex = 5;
      this.UserNameLabel.Text = "User name:";
      // 
      // PasswordLabel
      // 
      this.PasswordLabel.AutoSize = true;
      this.PasswordLabel.Location = new System.Drawing.Point(6, 59);
      this.PasswordLabel.Name = "PasswordLabel";
      this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
      this.PasswordLabel.TabIndex = 6;
      this.PasswordLabel.Text = "Password:";
      // 
      // PasswordField
      // 
      this.PasswordField.Location = new System.Drawing.Point(6, 76);
      this.PasswordField.Name = "PasswordField";
      this.PasswordField.PasswordChar = '*';
      this.PasswordField.Size = new System.Drawing.Size(100, 20);
      this.PasswordField.TabIndex = 7;
      this.PasswordField.Text = "Internet123Pro";
      // 
      // ClusterButton
      // 
      this.ClusterButton.Enabled = false;
      this.ClusterButton.Location = new System.Drawing.Point(64, 29);
      this.ClusterButton.Name = "ClusterButton";
      this.ClusterButton.Size = new System.Drawing.Size(75, 23);
      this.ClusterButton.TabIndex = 9;
      this.ClusterButton.Text = "Cluster";
      this.ClusterButton.UseVisualStyleBackColor = true;
      this.ClusterButton.Click += new System.EventHandler(this.button3_Click);
      // 
      // LoginGroup
      // 
      this.LoginGroup.Controls.Add(this.UserNameLabel);
      this.LoginGroup.Controls.Add(this.LoginButton);
      this.LoginGroup.Controls.Add(this.UserNameField);
      this.LoginGroup.Controls.Add(this.PasswordField);
      this.LoginGroup.Controls.Add(this.PasswordLabel);
      this.LoginGroup.Location = new System.Drawing.Point(10, 12);
      this.LoginGroup.Name = "LoginGroup";
      this.LoginGroup.Size = new System.Drawing.Size(115, 135);
      this.LoginGroup.TabIndex = 10;
      this.LoginGroup.TabStop = false;
      this.LoginGroup.Text = "Login data";
      // 
      // SearchGroup
      // 
      this.SearchGroup.Controls.Add(this.SearchLabel);
      this.SearchGroup.Controls.Add(this.SearchButton);
      this.SearchGroup.Controls.Add(this.SearchQueryField);
      this.SearchGroup.Location = new System.Drawing.Point(131, 12);
      this.SearchGroup.Name = "SearchGroup";
      this.SearchGroup.Size = new System.Drawing.Size(205, 60);
      this.SearchGroup.TabIndex = 11;
      this.SearchGroup.TabStop = false;
      this.SearchGroup.Text = "Search Allegro";
      // 
      // SearchLabel
      // 
      this.SearchLabel.AutoSize = true;
      this.SearchLabel.Location = new System.Drawing.Point(10, 42);
      this.SearchLabel.Name = "SearchLabel";
      this.SearchLabel.Size = new System.Drawing.Size(72, 13);
      this.SearchLabel.TabIndex = 16;
      this.SearchLabel.Text = "Search status";
      // 
      // ClusteringGroup
      // 
      this.ClusteringGroup.Controls.Add(this.ClusterButton);
      this.ClusteringGroup.Location = new System.Drawing.Point(131, 78);
      this.ClusteringGroup.Name = "ClusteringGroup";
      this.ClusteringGroup.Size = new System.Drawing.Size(205, 69);
      this.ClusteringGroup.TabIndex = 12;
      this.ClusteringGroup.TabStop = false;
      this.ClusteringGroup.Text = "Result clustering";
      // 
      // LoginWorker
      // 
      this.LoginWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoginWorker_DoWork);
      this.LoginWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoginWorker_RunWorkerCompleted);
      // 
      // SearchWorker
      // 
      this.SearchWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SearchWorker_DoWork);
      this.SearchWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SearchWorker_RunWorkerCompleted);
      // 
      // ClusterWorker
      // 
      this.ClusterWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClusterWorker_DoWork);
      this.ClusterWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ClusterWorker_RunWorkerCompleted);
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(10, 19);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(312, 23);
      this.progressBar1.TabIndex = 13;
      // 
      // StatusLabel
      // 
      this.StatusLabel.AutoSize = true;
      this.StatusLabel.Location = new System.Drawing.Point(142, 24);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(44, 13);
      this.StatusLabel.TabIndex = 14;
      this.StatusLabel.Text = "Nothing";
      this.StatusLabel.Visible = false;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.StatusLabel);
      this.groupBox4.Controls.Add(this.progressBar1);
      this.groupBox4.Location = new System.Drawing.Point(10, 153);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(327, 56);
      this.groupBox4.TabIndex = 15;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Operation status";
      // 
      // ClusterListView
      // 
      this.ClusterListView.Enabled = false;
      this.ClusterListView.Location = new System.Drawing.Point(10, 215);
      this.ClusterListView.MultiSelect = false;
      this.ClusterListView.Name = "ClusterListView";
      this.ClusterListView.Size = new System.Drawing.Size(327, 270);
      this.ClusterListView.TabIndex = 16;
      this.ClusterListView.TileSize = new System.Drawing.Size(128, 96);
      this.ClusterListView.UseCompatibleStateImageBehavior = false;
      this.ClusterListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClusterListView_MouseDoubleClick);
      // 
      // ClusterImages
      // 
      this.ClusterImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
      this.ClusterImages.ImageSize = new System.Drawing.Size(16, 16);
      this.ClusterImages.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(349, 497);
      this.Controls.Add(this.ClusterListView);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.ClusteringGroup);
      this.Controls.Add(this.SearchGroup);
      this.Controls.Add(this.LoginGroup);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.Text = "AllePro";
      this.LoginGroup.ResumeLayout(false);
      this.LoginGroup.PerformLayout();
      this.SearchGroup.ResumeLayout(false);
      this.SearchGroup.PerformLayout();
      this.ClusteringGroup.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button SearchButton;
    private System.Windows.Forms.Button LoginButton;
    private System.Windows.Forms.TextBox SearchQueryField;
    private System.Windows.Forms.TextBox UserNameField;
    private System.Windows.Forms.Label UserNameLabel;
    private System.Windows.Forms.Label PasswordLabel;
    private System.Windows.Forms.TextBox PasswordField;
    private System.Windows.Forms.Button ClusterButton;
    private System.Windows.Forms.GroupBox LoginGroup;
    private System.Windows.Forms.GroupBox SearchGroup;
    private System.Windows.Forms.GroupBox ClusteringGroup;
    private System.ComponentModel.BackgroundWorker LoginWorker;
    private System.ComponentModel.BackgroundWorker SearchWorker;
    private System.ComponentModel.BackgroundWorker ClusterWorker;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label StatusLabel;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Label SearchLabel;
    private System.Windows.Forms.ListView ClusterListView;
    private System.Windows.Forms.ImageList ClusterImages;
  }
}

