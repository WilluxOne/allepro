namespace AllePro
{
  partial class ClusterViewer
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
      this.ClusterItemsList = new System.Windows.Forms.ListView();
      this.ListImages = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // ClusterItemsList
      // 
      this.ClusterItemsList.LargeImageList = this.ListImages;
      this.ClusterItemsList.Location = new System.Drawing.Point(0, 0);
      this.ClusterItemsList.MultiSelect = false;
      this.ClusterItemsList.Name = "ClusterItemsList";
      this.ClusterItemsList.Size = new System.Drawing.Size(400, 350);
      this.ClusterItemsList.TabIndex = 0;
      this.ClusterItemsList.TileSize = new System.Drawing.Size(128, 96);
      this.ClusterItemsList.UseCompatibleStateImageBehavior = false;
      // 
      // ListImages
      // 
      this.ListImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
      this.ListImages.ImageSize = new System.Drawing.Size(128, 96);
      this.ListImages.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // ClusterViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(399, 349);
      this.Controls.Add(this.ClusterItemsList);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.Name = "ClusterViewer";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Cluster Viewer";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView ClusterItemsList;
    private System.Windows.Forms.ImageList ListImages;
  }
}