namespace MarketSimulator
{
    partial class QuoteDataForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.symbolListView = new System.Windows.Forms.ListView();
            this.Symbol = new System.Windows.Forms.ColumnHeader();
            this.StartDate = new System.Windows.Forms.ColumnHeader();
            this.EndDate = new System.Windows.Forms.ColumnHeader();
            this.Count = new System.Windows.Forms.ColumnHeader();
            this.Splits = new System.Windows.Forms.ColumnHeader();
            this.MissingDays = new System.Windows.Forms.ColumnHeader();
            this.ExtraDays = new System.Windows.Forms.ColumnHeader();
            this.MaxConsecutiveMissingDays = new System.Windows.Forms.ColumnHeader();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 545);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(996, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.symbolListView);
            this.splitContainer1.Size = new System.Drawing.Size(996, 521);
            this.splitContainer1.SplitterDistance = 680;
            this.splitContainer1.TabIndex = 2;
            // 
            // symbolListView
            // 
            this.symbolListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Symbol,
            this.StartDate,
            this.EndDate,
            this.Count,
            this.Splits,
            this.MissingDays,
            this.ExtraDays,
            this.MaxConsecutiveMissingDays});
            this.symbolListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.symbolListView.GridLines = true;
            this.symbolListView.Location = new System.Drawing.Point(0, 0);
            this.symbolListView.Name = "symbolListView";
            this.symbolListView.Size = new System.Drawing.Size(680, 521);
            this.symbolListView.TabIndex = 0;
            this.symbolListView.UseCompatibleStateImageBehavior = false;
            this.symbolListView.View = System.Windows.Forms.View.Details;
            this.symbolListView.SelectedIndexChanged += new System.EventHandler(this.symbolListView_SelectedIndexChanged);
            // 
            // Symbol
            // 
            this.Symbol.Text = "Symbol";
            // 
            // StartDate
            // 
            this.StartDate.Text = "Start Date";
            // 
            // EndDate
            // 
            this.EndDate.Text = "End Date";
            // 
            // Count
            // 
            this.Count.Text = "Count";
            // 
            // Splits
            // 
            this.Splits.Text = "Splits";
            // 
            // MissingDays
            // 
            this.MissingDays.Text = "Missing Days";
            this.MissingDays.Width = 76;
            // 
            // ExtraDays
            // 
            this.ExtraDays.Text = "Extra Days";
            this.ExtraDays.Width = 76;
            // 
            // MaxConsecutiveMissingDays
            // 
            this.MaxConsecutiveMissingDays.Text = "MaxConsecutiveMissingDays";
            // 
            // QuoteDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 567);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Name = "QuoteDataForm";
            this.Text = "QuoteDataForm";
            this.Load += new System.EventHandler(this.QuoteDataForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView symbolListView;
        private System.Windows.Forms.ColumnHeader Symbol;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader EndDate;
        private System.Windows.Forms.ColumnHeader Count;
        private System.Windows.Forms.ColumnHeader Splits;
        private System.Windows.Forms.ColumnHeader MissingDays;
        private System.Windows.Forms.ColumnHeader ExtraDays;
        private System.Windows.Forms.ColumnHeader MaxConsecutiveMissingDays;
    }
}