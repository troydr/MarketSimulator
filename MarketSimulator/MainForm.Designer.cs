namespace MarketSimulator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.BottomStatusStrip = new System.Windows.Forms.StatusStrip();
            this.Vertical = new System.Windows.Forms.SplitContainer();
            this.TopMenuStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.Vertical.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.OptionsMenuItem,
            this.HelpMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Size = new System.Drawing.Size(797, 24);
            this.TopMenuStrip.TabIndex = 0;
            this.TopMenuStrip.Text = "TopMenuStrip";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            this.FileMenuItem.Click += new System.EventHandler(this.FileMenuItem_Click);
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.Name = "OptionsMenuItem";
            this.OptionsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.OptionsMenuItem.Text = "Options";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpMenuItem.Text = "Help";
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripTextBox1});
            this.TopToolStrip.Location = new System.Drawing.Point(0, 24);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(797, 25);
            this.TopToolStrip.TabIndex = 1;
            this.TopToolStrip.Text = "TopToolStrip";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // BottomStatusStrip
            // 
            this.BottomStatusStrip.Location = new System.Drawing.Point(0, 544);
            this.BottomStatusStrip.Name = "BottomStatusStrip";
            this.BottomStatusStrip.Size = new System.Drawing.Size(797, 22);
            this.BottomStatusStrip.TabIndex = 2;
            this.BottomStatusStrip.Text = "BottomStatusStrip";
            // 
            // Vertical
            // 
            this.Vertical.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Vertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Vertical.Location = new System.Drawing.Point(0, 49);
            this.Vertical.Name = "Vertical";
            this.Vertical.Size = new System.Drawing.Size(797, 495);
            this.Vertical.SplitterDistance = 264;
            this.Vertical.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 566);
            this.Controls.Add(this.Vertical);
            this.Controls.Add(this.BottomStatusStrip);
            this.Controls.Add(this.TopToolStrip);
            this.Controls.Add(this.TopMenuStrip);
            this.MainMenuStrip = this.TopMenuStrip;
            this.Name = "MainForm";
            this.Text = "Market Simulator";
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.Vertical.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStrip TopToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.StatusStrip BottomStatusStrip;
        private System.Windows.Forms.SplitContainer Vertical;
    }
}

