using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Siamese
{
    public partial class WndForm : Form
    {
        BackgroundWorker Worker;

        string SnapshotFile { get { return TextSnapshot.Text.Trim(); } set { TextSnapshot.Text = value; } }

        string TargetFolder { get { return TextTargetPath.Text.ToLowerInvariant().Trim(); } set { TextTargetPath.Text = value; } }

        Snapshot SourceSnapshot;

        Snapshot TargetSnapshot;

        HashSet<string> FilteredExtensions;

        

        public WndForm()
        {
            InitializeComponent();

            DataGridResults.CellMouseDoubleClick += DataGridResults_CellMouseDoubleClick;
            DoubleBuffered = true;
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
               .SetValue(DataGridResults, true, null);


            FilteredExtensions = new HashSet<string>();

            //DataGridResults.DataTable

            

        }

        private void DataGridResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var relname = DataGridResults.Rows[e.RowIndex].Cells[0].Value as string;

            var chtype = (Comparison.Differences)Enum.Parse(typeof(Comparison.Differences), (string)DataGridResults.Rows[e.RowIndex].Cells[3].Value);

            var srcfile = Path.Combine(SourceSnapshot.RootPath, relname);
            var tgtfile = Path.Combine(TargetSnapshot.RootPath, relname);

            if (chtype == Comparison.Differences.SIZE)
            {

                var exefile = Properties.Settings.Default.CompareTool;
                if (File.Exists(exefile))
                {
                    IOLibrary.RunProgram(exefile, "\"" + srcfile + "\" \"" + tgtfile + "\"");
                }

            }
            else
            {
                var selfile = chtype == Comparison.Differences.ONLY_LEFT_SIDE ? srcfile : tgtfile;

                // explorer.exe /select,"C:\Folder\subfolder\file.txt"

                IOLibrary.RunProgram("explorer.exe", $"/select,\"{selfile}\"");


            }
        }

        private void BtnLoadSnapshot_Click(object sender, EventArgs e)
        {
            var prevfile = TextSnapshot.Text.Trim();


            using (var fd = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "Snapshot files (*.snp)|*.snp|All files (*.*)|*.*"
            })
            {

                if (File.Exists(prevfile))
                {
                    fd.InitialDirectory = Path.GetDirectoryName(prevfile);
                    fd.FileName = prevfile;
                }


                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    // at this point we need to queue up the snapshot
                    SourceSnapshot = Snapshot.Deserialize(fd.FileName);
                    SnapshotFile = fd.FileName;
                }
            }
        }

        private void BtnCompare_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TargetFolder))
            {
                if(SourceSnapshot != null)
                {
                    Cursor = Cursors.WaitCursor;

                    DataGridResults.DataSource = null;
                    //DataGridResults.Rows.Clear();

                    TargetSnapshot = Snapshot.CreateFromPath(TargetFolder);
                    var comparisons = SourceSnapshot.CompareAgainst(TargetSnapshot);

                    DataGridResults.SuspendLayout();

                    var dt = new DataTable();
                    
                    dt.Columns.AddRange(DataGridResults.Columns.Cast<DataGridViewColumn>().Select(c =>  new DataColumn(c.Name)).ToArray());
                   
                    foreach (var c in comparisons)
                    {
                        dt.Rows.Add(c.Filename, c.IsDirectoryComparison ? "DIRECTORY" : c.Extension, c.ToString(), c.Difference);
                        
                        //DataGridViewRow row = (DataGridViewRow)DataGridResults.RowTemplate.Clone();
                        //row.CreateCells(DataGridResults, c.Filename, c.Extension, c.ToString(), c.Difference);
                        //DataGridResults.Rows.Add(row);
                    }

                    DataGridResults.AutoGenerateColumns = false;
                    BindingSource bs = new BindingSource { DataSource = dt };

                    foreach (DataGridViewColumn col in DataGridResults.Columns)
                    {
                        col.DataPropertyName = col.Name;
                    }

                    DataGridResults.DataSource = bs;
                    Refilter();

                    DataGridResults.ResumeLayout();

                    Cursor = Cursors.Default;

                    MessageBox.Show(this, "Analysis complete.", this.Text);
                }
                else if (MessageBox.Show(this, "You have not selected a snapshot file, do you want to create one now?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataGridResults.Rows.Clear();

                    using (var fd = new SaveFileDialog { Filter = "Snapshot files (*.snp)|*.snp|All files (*.*)|*.*" })
                    {

                        if (fd.ShowDialog(this) == DialogResult.OK)
                        {
                            SnapshotFile = fd.FileName;
                            SourceSnapshot = Snapshot.CreateFromPath(TargetFolder);
                            SourceSnapshot.Serialize(fd.FileName);
                            MessageBox.Show(this, $"Snapshot file created from {TargetFolder}, total files examined: {SourceSnapshot.Count}", this.Text);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "You need to select a target folder", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    TargetFolder = fbd.SelectedPath;

                }
            }
        }

        private void BtnUpdateSnapshot_Click(object sender, EventArgs e)
        {
            if(TargetSnapshot != null && MessageBox.Show(this, $"Updating will replace snapshot {SnapshotFile} with the newly generated one. Are you sure?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                SourceSnapshot = TargetSnapshot;
                SourceSnapshot.Serialize(SnapshotFile);
                TargetSnapshot = null;
            }
        }

        private void WndForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SourceSnapshotFile = SnapshotFile;
            Properties.Settings.Default.TargetFolder = TargetFolder;
            
            var sc = new System.Collections.Specialized.StringCollection();
            sc.AddRange(FilteredExtensions.ToArray());
            Properties.Settings.Default.FilteredExts = sc;

            Properties.Settings.Default.Save();
        }

        private void WndForm_Load(object sender, EventArgs e)
        {

            var snapshotfile = Properties.Settings.Default.SourceSnapshotFile;
            var targetfolder = Properties.Settings.Default.TargetFolder;


            if (Properties.Settings.Default.FilteredExts is var fe && fe != null)
            {
                foreach (var ext in fe)
                {
                    FilteredExtensions.Add(ext);
                }
            }

            if (!string.IsNullOrWhiteSpace(snapshotfile) && File.Exists(snapshotfile))
            {
                SourceSnapshot = Snapshot.Deserialize(snapshotfile);
                SnapshotFile = snapshotfile;
            }

            if (!string.IsNullOrWhiteSpace(targetfolder))
                TargetFolder = targetfolder;

        }

        private void BtnUnloadSnapshot_Click(object sender, EventArgs e)
        {
            SourceSnapshot = null;
            SnapshotFile = string.Empty;
        }

        void Refilter()
        {
            // go through list and filter???
            if (FilteredExtensions.Count > 0 && MenuItemFiltersActive.Checked)
            {
                var filter = "ColExtension NOT IN (" + string.Join(", ", FilteredExtensions.Select(f => $"'{f}'")) + ")";
                ((BindingSource)DataGridResults.DataSource).Filter = filter;

                GrpResults.Text = "Results (filtering is active)";
            }
            else
            {
                ((BindingSource)DataGridResults.DataSource).RemoveFilter();
                GrpResults.Text = "Results (filtering is inactive)";
            }

        }

        private void MenuItemFilterExt_Click(object sender, EventArgs e)
        {
            var ext = DataGridResults.CurrentCell.OwningRow.Cells[1].Value as string;
            FilteredExtensions.Add(ext.Trim().ToLowerInvariant());
            Refilter();


        }

        private void ResultsContextMenu_Opening(object sender, CancelEventArgs e)
        {
            foreach(DataGridViewRow row in DataGridResults.SelectedRows)
            {

            }
        }


        private void DataGridResults_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                MenuItemFilterExt.Enabled = true;
                MenuItemClearFilter.Enabled = true;
                MenuItemFiltersActive.Enabled = true;
                MenuItemOverwriteLeft.Enabled = MenuItemOverwriteRight.Enabled = MenuItemCopy.Enabled = false;

                if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    MenuItemCopy.Enabled = false;
                    MenuItemFilterExt.Enabled = false;
                }
                // Ignore if a column or row header is clicked
                else
                {

                    if (DataGridResults.SelectedCells.Count == 1)
                    {
                        DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                        DataGridResults.CurrentCell = clickedCell;  // Select the clicked cell, for instance
                    }

                    if (DataGridResults.SelectedCells.Cast<DataGridViewCell>().All(vc => ((Comparison.Differences)Enum.Parse(typeof(Comparison.Differences), (string)vc.OwningRow.Cells[3].Value)) > 0))
                    {
                        MenuItemCopy.Enabled = true;
                    }

                    if (DataGridResults.SelectedCells.Cast<DataGridViewCell>().All(vc => ((Comparison.Differences)Enum.Parse(typeof(Comparison.Differences), (string)vc.OwningRow.Cells[3].Value)) == Comparison.Differences.SIZE))
                    {
                        MenuItemOverwriteLeft.Enabled = MenuItemOverwriteRight.Enabled = true;
                    }

                }

                // Get mouse position relative to the vehicles grid
                var relativeMousePosition = DataGridResults.PointToClient(Cursor.Position);

                // Show the context menu
                this.ResultsContextMenu.Show(DataGridResults, relativeMousePosition);
            }
        }

        private void MenuItemClearFilter_Click(object sender, EventArgs e)
        {
            FilteredExtensions.Clear();
            Refilter();
        }

        private void MenuItemFiltersActive_Click(object sender, EventArgs e)
        {
            MenuItemFiltersActive.Checked = !MenuItemFiltersActive.Checked;
            Refilter();
        }

        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            //var rows = DataGridResults.SelectedCells.Cast<DataGridViewCell>().Select(c => c.OwningRow);
            var rows = DataGridResults.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex).Distinct().Select(c => DataGridResults.Rows[c]).ToList();
            Debug.Assert(rows.All(r => r.Index >= 0));
            var boundrows = new List<DataRow>();
            DataTable dt = ((DataTable)((BindingSource)DataGridResults.DataSource).DataSource);

            foreach (DataGridViewRow row in rows)
            {
                var compdiff = (Comparison.Differences)Enum.Parse(typeof(Comparison.Differences), (string)row.Cells[3].Value);
                var relfile = (string)row.Cells[0].Value;


                var _srcsnap = compdiff == Comparison.Differences.ONLY_LEFT_SIDE ? SourceSnapshot : TargetSnapshot;
                var _tgtsnap = compdiff == Comparison.Differences.ONLY_LEFT_SIDE ?  TargetSnapshot: SourceSnapshot;

                var _srcTrueRelName = _srcsnap.FindRelSnapshot(relfile).RelName;

                if (_srcsnap.FindRelSnapshot(relfile).IsDirectory)
                {
                    // be really fucking careful since this is not going to create directories that are different on one side to the other!!!!!
                    //Directory.CreateDirectory(_tgtsnap.GetAbsName( _srcsnap.FindRelFile(relfile).RelName));

                    Directory.CreateDirectory(_tgtsnap.FabricateFile(_srcTrueRelName));

                    

                }
                else
                {
                    // this file transfer may still require a "directory creation"
                    //var srcfile = compdiff == Comparison.Differences.ONLY_LEFT_SIDE ? SourceSnapshot.GetAbsName(relfile) : TargetSnapshot.GetAbsName(relfile);
                    //var tgtfile = compdiff == Comparison.Differences.ONLY_LEFT_SIDE ? Path.Combine(TargetSnapshot.RootPath, relfile) : Path.Combine(SourceSnapshot.RootPath, relfile);

                    var srcfile = _srcsnap.GetAbsName(relfile);
                    var tgtfile = _tgtsnap.FabricateFile(_srcTrueRelName);


                    var basepath = Path.GetDirectoryName(tgtfile);
                    if (!Directory.Exists(basepath)) { Directory.CreateDirectory(basepath); }

                    File.Copy(srcfile, tgtfile, false);
                }

                
                var dr = ((DataRowView)row.DataBoundItem).Row;
                boundrows.Add(dr);
                
            }

            foreach (var br in boundrows)
                dt.Rows.Remove(br);

        }

        private void MenuItemOverwrite_Click(object sender, EventArgs e)
        {
            if( sender is ToolStripItem t && t != null)
            {
                var msgsrcpart = t.Name == MenuItemOverwriteRight.Name ? SourceSnapshot.RootPath : TargetSnapshot.RootPath;
                var msgtgtpart = t.Name == MenuItemOverwriteRight.Name ? TargetSnapshot.RootPath : SourceSnapshot.RootPath;

                if (MessageBox.Show(this, $"Files from {msgsrcpart} will overwrite {msgtgtpart}. Are you sure?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    DataTable dt = ((DataTable)((BindingSource)DataGridResults.DataSource).DataSource);

                    var boundrows = new List<DataRow>();
                    var rows = DataGridResults.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex).Distinct().Select(c => DataGridResults.Rows[c]);
                    Debug.Assert(rows.All(r => r.Index >= 0));

                    foreach (DataGridViewRow row in rows)
                    {
                        var compdiff = (Comparison.Differences)Enum.Parse(typeof(Comparison.Differences), (string)row.Cells[3].Value);
                        var relfile = (string)row.Cells[0].Value;

                        var srcsnap = t.Name == MenuItemOverwriteRight.Name ? SourceSnapshot : TargetSnapshot;
                        var tgtsnap = t.Name == MenuItemOverwriteRight.Name ? TargetSnapshot : SourceSnapshot;

                        var srcfile = srcsnap.GetAbsName(relfile);
                        var tgtfile = tgtsnap.FabricateFile(srcsnap.FindRelSnapshot(relfile).RelName);

                        File.Copy(srcfile, tgtfile, overwrite: true);

                        var dr = ((DataRowView)row.DataBoundItem).Row;
                        boundrows.Add(dr);
                    }

                    foreach (var br in boundrows)
                        dt.Rows.Remove(br);
                }
                
            }
        }

        private void MenuItemDeleteFile_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem t && t != null)
            {
                var msgsrcpart = t.Name == MenuItemOverwriteRight.Name ? SourceSnapshot.RootPath : TargetSnapshot.RootPath;
                var msgtgtpart = t.Name == MenuItemOverwriteRight.Name ? TargetSnapshot.RootPath : SourceSnapshot.RootPath;

                var boundrows = new List<DataRow>();
                var rows = DataGridResults.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex).Distinct().Select(c => DataGridResults.Rows[c]);
                Debug.Assert(rows.All(r => r.Index >= 0));

                if (MessageBox.Show(this, $"{rows.Count()} directories and subsequent files will be deleted. Are you sure?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        var compdiff = (Comparison.Differences)Enum.Parse(typeof(Comparison.Differences), (string)row.Cells[3].Value);
                        var relfile = (string)row.Cells[0].Value;
                        var set = new List<string>();

                        if (compdiff == Comparison.Differences.ONLY_LEFT_SIDE || compdiff == Comparison.Differences.SIZE) set.Add(SourceSnapshot.GetAbsName(relfile));
                        else if (compdiff == Comparison.Differences.ONLY_RIGHT_SIDE || compdiff == Comparison.Differences.SIZE) set.Add(TargetSnapshot.GetAbsName(relfile));

                        bool error = false;
                        foreach (var s in set)
                        {
                            try
                            {
                                if (File.Exists(s))
                                {
                                    if(new FileInfo(s) is var fs && fs.Attributes.HasFlag(FileAttributes.ReadOnly) || fs.Attributes.HasFlag(FileAttributes.Hidden) )
                                        File.SetAttributes(s, FileAttributes.Normal);
                                    File.Delete(s);
                                }
                                else if(Directory.Exists(s))
                                {
                                    Directory.Delete(s, recursive: true);
                                }
                            }
                            catch(Exception ex)
                            {
                                error = true;
                            }
                        }

                        var dr = ((DataRowView)row.DataBoundItem).Row;
                        if (!error)
                            boundrows.Add(dr);
                        else
                            row.DefaultCellStyle.BackColor = Color.Red;

                    }
                    
                    DataTable dt = ((DataTable)((BindingSource)DataGridResults.DataSource).DataSource);
                    foreach (var br in boundrows)
                        dt.Rows.Remove(br);
                }

            }
        }
    }
}
