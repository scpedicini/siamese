namespace Siamese
{
    partial class WndForm
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
            this.GrpConfig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnUnloadSnapshot = new System.Windows.Forms.Button();
            this.BtnUpdateSnapshot = new System.Windows.Forms.Button();
            this.BtnCompare = new System.Windows.Forms.Button();
            this.BtnPath = new System.Windows.Forms.Button();
            this.TextTargetPath = new System.Windows.Forms.TextBox();
            this.TextSnapshot = new System.Windows.Forms.TextBox();
            this.BtnLoadSnapshot = new System.Windows.Forms.Button();
            this.GrpResults = new System.Windows.Forms.GroupBox();
            this.DataGridResults = new System.Windows.Forms.DataGridView();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColExtension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColChangeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemFilterExt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemClearFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFiltersActive = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOverwriteLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOverwriteRight = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpConfig.SuspendLayout();
            this.GrpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridResults)).BeginInit();
            this.ResultsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpConfig
            // 
            this.GrpConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpConfig.Controls.Add(this.label2);
            this.GrpConfig.Controls.Add(this.label1);
            this.GrpConfig.Controls.Add(this.BtnUnloadSnapshot);
            this.GrpConfig.Controls.Add(this.BtnUpdateSnapshot);
            this.GrpConfig.Controls.Add(this.BtnCompare);
            this.GrpConfig.Controls.Add(this.BtnPath);
            this.GrpConfig.Controls.Add(this.TextTargetPath);
            this.GrpConfig.Controls.Add(this.TextSnapshot);
            this.GrpConfig.Controls.Add(this.BtnLoadSnapshot);
            this.GrpConfig.Location = new System.Drawing.Point(13, 13);
            this.GrpConfig.Name = "GrpConfig";
            this.GrpConfig.Size = new System.Drawing.Size(915, 194);
            this.GrpConfig.TabIndex = 0;
            this.GrpConfig.TabStop = false;
            this.GrpConfig.Text = "Configuration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Source Snapshot File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Target Folder";
            // 
            // BtnUnloadSnapshot
            // 
            this.BtnUnloadSnapshot.Location = new System.Drawing.Point(125, 75);
            this.BtnUnloadSnapshot.Name = "BtnUnloadSnapshot";
            this.BtnUnloadSnapshot.Size = new System.Drawing.Size(95, 23);
            this.BtnUnloadSnapshot.TabIndex = 6;
            this.BtnUnloadSnapshot.Text = "Unload Snapshot";
            this.BtnUnloadSnapshot.UseVisualStyleBackColor = true;
            this.BtnUnloadSnapshot.Click += new System.EventHandler(this.BtnUnloadSnapshot_Click);
            // 
            // BtnUpdateSnapshot
            // 
            this.BtnUpdateSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnUpdateSnapshot.Location = new System.Drawing.Point(696, 156);
            this.BtnUpdateSnapshot.Name = "BtnUpdateSnapshot";
            this.BtnUpdateSnapshot.Size = new System.Drawing.Size(95, 23);
            this.BtnUpdateSnapshot.TabIndex = 5;
            this.BtnUpdateSnapshot.Text = "Update";
            this.BtnUpdateSnapshot.UseVisualStyleBackColor = true;
            this.BtnUpdateSnapshot.Click += new System.EventHandler(this.BtnUpdateSnapshot_Click);
            // 
            // BtnCompare
            // 
            this.BtnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCompare.Location = new System.Drawing.Point(814, 156);
            this.BtnCompare.Name = "BtnCompare";
            this.BtnCompare.Size = new System.Drawing.Size(95, 23);
            this.BtnCompare.TabIndex = 4;
            this.BtnCompare.Text = "Run";
            this.BtnCompare.UseVisualStyleBackColor = true;
            this.BtnCompare.Click += new System.EventHandler(this.BtnCompare_Click);
            // 
            // BtnPath
            // 
            this.BtnPath.Location = new System.Drawing.Point(24, 156);
            this.BtnPath.Name = "BtnPath";
            this.BtnPath.Size = new System.Drawing.Size(95, 23);
            this.BtnPath.TabIndex = 3;
            this.BtnPath.Text = "Choose Path";
            this.BtnPath.UseVisualStyleBackColor = true;
            this.BtnPath.Click += new System.EventHandler(this.BtnPath_Click);
            // 
            // TextTargetPath
            // 
            this.TextTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTargetPath.Location = new System.Drawing.Point(24, 128);
            this.TextTargetPath.Name = "TextTargetPath";
            this.TextTargetPath.ReadOnly = true;
            this.TextTargetPath.Size = new System.Drawing.Size(885, 22);
            this.TextTargetPath.TabIndex = 2;
            // 
            // TextSnapshot
            // 
            this.TextSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextSnapshot.Location = new System.Drawing.Point(24, 47);
            this.TextSnapshot.Name = "TextSnapshot";
            this.TextSnapshot.ReadOnly = true;
            this.TextSnapshot.Size = new System.Drawing.Size(885, 22);
            this.TextSnapshot.TabIndex = 1;
            // 
            // BtnLoadSnapshot
            // 
            this.BtnLoadSnapshot.Location = new System.Drawing.Point(24, 75);
            this.BtnLoadSnapshot.Name = "BtnLoadSnapshot";
            this.BtnLoadSnapshot.Size = new System.Drawing.Size(95, 23);
            this.BtnLoadSnapshot.TabIndex = 0;
            this.BtnLoadSnapshot.Text = "Load";
            this.BtnLoadSnapshot.UseVisualStyleBackColor = true;
            this.BtnLoadSnapshot.Click += new System.EventHandler(this.BtnLoadSnapshot_Click);
            // 
            // GrpResults
            // 
            this.GrpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpResults.Controls.Add(this.DataGridResults);
            this.GrpResults.Location = new System.Drawing.Point(13, 213);
            this.GrpResults.Name = "GrpResults";
            this.GrpResults.Size = new System.Drawing.Size(915, 523);
            this.GrpResults.TabIndex = 1;
            this.GrpResults.TabStop = false;
            this.GrpResults.Text = "Results";
            // 
            // DataGridResults
            // 
            this.DataGridResults.AllowUserToAddRows = false;
            this.DataGridResults.AllowUserToDeleteRows = false;
            this.DataGridResults.AllowUserToOrderColumns = true;
            this.DataGridResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColExtension,
            this.ColDesc,
            this.ColChangeType});
            this.DataGridResults.Location = new System.Drawing.Point(24, 33);
            this.DataGridResults.Name = "DataGridResults";
            this.DataGridResults.ReadOnly = true;
            this.DataGridResults.Size = new System.Drawing.Size(885, 474);
            this.DataGridResults.TabIndex = 0;
            this.DataGridResults.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridResults_CellMouseDown);
            // 
            // ColName
            // 
            this.ColName.HeaderText = "Name";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            // 
            // ColExtension
            // 
            this.ColExtension.HeaderText = "Extension";
            this.ColExtension.Name = "ColExtension";
            this.ColExtension.ReadOnly = true;
            // 
            // ColDesc
            // 
            this.ColDesc.HeaderText = "Description";
            this.ColDesc.Name = "ColDesc";
            this.ColDesc.ReadOnly = true;
            // 
            // ColChangeType
            // 
            this.ColChangeType.HeaderText = "Change Type";
            this.ColChangeType.Name = "ColChangeType";
            this.ColChangeType.ReadOnly = true;
            // 
            // ResultsContextMenu
            // 
            this.ResultsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFilterExt,
            this.MenuItemClearFilter,
            this.MenuItemFiltersActive,
            this.MenuItemCopy,
            this.MenuItemOverwriteLeft,
            this.MenuItemOverwriteRight,
            this.MenuItemDeleteFile});
            this.ResultsContextMenu.Name = "ResultsContextMenu";
            this.ResultsContextMenu.Size = new System.Drawing.Size(181, 180);
            this.ResultsContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ResultsContextMenu_Opening);
            // 
            // MenuItemFilterExt
            // 
            this.MenuItemFilterExt.Name = "MenuItemFilterExt";
            this.MenuItemFilterExt.Size = new System.Drawing.Size(180, 22);
            this.MenuItemFilterExt.Text = "Filter Extension";
            this.MenuItemFilterExt.Click += new System.EventHandler(this.MenuItemFilterExt_Click);
            // 
            // MenuItemClearFilter
            // 
            this.MenuItemClearFilter.Name = "MenuItemClearFilter";
            this.MenuItemClearFilter.Size = new System.Drawing.Size(180, 22);
            this.MenuItemClearFilter.Text = "Remove Filters";
            this.MenuItemClearFilter.Click += new System.EventHandler(this.MenuItemClearFilter_Click);
            // 
            // MenuItemFiltersActive
            // 
            this.MenuItemFiltersActive.Checked = true;
            this.MenuItemFiltersActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemFiltersActive.Name = "MenuItemFiltersActive";
            this.MenuItemFiltersActive.Size = new System.Drawing.Size(180, 22);
            this.MenuItemFiltersActive.Text = "Filters Active";
            this.MenuItemFiltersActive.Click += new System.EventHandler(this.MenuItemFiltersActive_Click);
            // 
            // MenuItemCopy
            // 
            this.MenuItemCopy.Name = "MenuItemCopy";
            this.MenuItemCopy.Size = new System.Drawing.Size(180, 22);
            this.MenuItemCopy.Text = "Copy to other side";
            this.MenuItemCopy.Click += new System.EventHandler(this.MenuItemCopy_Click);
            // 
            // MenuItemOverwriteLeft
            // 
            this.MenuItemOverwriteLeft.Name = "MenuItemOverwriteLeft";
            this.MenuItemOverwriteLeft.Size = new System.Drawing.Size(180, 22);
            this.MenuItemOverwriteLeft.Text = "Overwrite left side";
            this.MenuItemOverwriteLeft.Click += new System.EventHandler(this.MenuItemOverwrite_Click);
            // 
            // MenuItemOverwriteRight
            // 
            this.MenuItemOverwriteRight.Name = "MenuItemOverwriteRight";
            this.MenuItemOverwriteRight.Size = new System.Drawing.Size(180, 22);
            this.MenuItemOverwriteRight.Text = "Overwrite right side";
            this.MenuItemOverwriteRight.Click += new System.EventHandler(this.MenuItemOverwrite_Click);
            // 
            // MenuItemDeleteFile
            // 
            this.MenuItemDeleteFile.Name = "MenuItemDeleteFile";
            this.MenuItemDeleteFile.Size = new System.Drawing.Size(180, 22);
            this.MenuItemDeleteFile.Text = "Delete File";
            this.MenuItemDeleteFile.Click += new System.EventHandler(this.MenuItemDeleteFile_Click);
            // 
            // WndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 760);
            this.Controls.Add(this.GrpResults);
            this.Controls.Add(this.GrpConfig);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WndForm";
            this.Text = "Siamese File Comparisons";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WndForm_FormClosing);
            this.Load += new System.EventHandler(this.WndForm_Load);
            this.GrpConfig.ResumeLayout(false);
            this.GrpConfig.PerformLayout();
            this.GrpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridResults)).EndInit();
            this.ResultsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpConfig;
        private System.Windows.Forms.Button BtnPath;
        private System.Windows.Forms.TextBox TextTargetPath;
        private System.Windows.Forms.TextBox TextSnapshot;
        private System.Windows.Forms.Button BtnLoadSnapshot;
        private System.Windows.Forms.GroupBox GrpResults;
        private System.Windows.Forms.DataGridView DataGridResults;
        private System.Windows.Forms.Button BtnCompare;
        private System.Windows.Forms.Button BtnUpdateSnapshot;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColExtension;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColChangeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnUnloadSnapshot;
        private System.Windows.Forms.ContextMenuStrip ResultsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFilterExt;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClearFilter;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFiltersActive;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOverwriteLeft;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOverwriteRight;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDeleteFile;
    }
}

