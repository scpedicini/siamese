using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Siamese
{

    [Serializable]
    public class Snapfile
    {
        public long Size;
        public string RelName;

        public bool IsDirectory;

        public Snapfile(string relname, long size, bool isdirectory)
        {
            RelName = relname;
            Size = size;
            IsDirectory = isdirectory;
        }
    }

    public class Comparison
    {
        public enum Differences
        {
            SIZE,               // different file sizes 
            ONLY_LEFT_SIDE,     // exists in the source snapshot
            ONLY_RIGHT_SIDE,    // exists in the target snapshot
        }

        public Comparison(string filename, Differences diff, long sizediff, bool isdirectory)
        {
            Difference = diff;
            SizeDiff = sizediff;
            Filename = filename;
            IsDirectoryComparison = isdirectory;
        }

        public Differences Difference;

        public long SizeDiff; // target snapshot is +/- X bytes different than source

        public string Filename;

        public bool IsDirectoryComparison;

        public string Extension { get { return Filename.Substring(Filename.LastIndexOf('.') is var i && i >= 0 ? i + 1 : Filename.Length); } }


        public override string ToString()
        {
            if (Difference == Differences.ONLY_LEFT_SIDE)
                return $"{Filename} only exists in source snapshot";
            else if (Difference == Differences.ONLY_RIGHT_SIDE)
                return $"{Filename} only exists in target snapshot";
            else if (Difference == Differences.SIZE)
                return $"{Filename} in target is {Math.Abs(SizeDiff)} bytes {(SizeDiff >= 0 ? "larger" : "smaller")} than the source file";
            else
                return "No differences found";
        }


    }

    [Serializable]
    public class Snapshot
    {

        public string RootPath;

        private Dictionary<string, Snapfile> RelNameToSnapfile;

        public int Count => RelNameToSnapfile.Count;

        public Snapshot(string rootPath)
        {
            RootPath = rootPath;
            RelNameToSnapfile = new Dictionary<string, Snapfile>();
        }

        public void AddFile(string file, long size, bool isdirectory)
        {
            var dname = file.ToLowerInvariant();
            if (RelNameToSnapfile.ContainsKey(dname))
                RelNameToSnapfile[dname].Size = size;
            else
            {
                var snapfile = new Snapfile(file, size, isdirectory);
                RelNameToSnapfile[dname] = snapfile;
            }
        }

        public Snapfile FindRelSnapshot(string relname)
        {
            relname = relname.ToLowerInvariant();
            if (RelNameToSnapfile.ContainsKey(relname))
                return RelNameToSnapfile[relname];
            else
                return null;
        }

        /// <summary>
        /// Do not call this unless the file genuinely exists in this snapshot
        /// </summary>
        /// <param name="relname"></param>
        /// <returns></returns>
        public string GetAbsName(string relname)
        {
            relname = relname.ToLowerInvariant();
            //if (RelNameToSnapfile.ContainsKey(relname))
                return Path.Combine(RootPath, RelNameToSnapfile[relname].RelName); // preserve the case
            //else
              //  return null;
        }

        public string FabricateFile(string relname)
        {
            return Path.Combine(RootPath, relname);
        }

        public static Snapshot CreateFromPath(string rootPath, Func<bool> cancellationRequested = null)
        {
            rootPath = rootPath.ToLowerInvariant().Trim();

            var snapshot = new Snapshot(rootPath);

            var length = rootPath.EndsWith("\\") ? rootPath.Length : rootPath.Length + 1;

            var fse = new FileSystemEnumerable(new System.IO.DirectoryInfo(rootPath), "*.*", System.IO.SearchOption.AllDirectories);

            // we need to be including the paths as well



            foreach(var f in fse)
            {
                if (cancellationRequested != null && cancellationRequested())
                {
                    throw new TaskCanceledException("User cancelled creating serialized path analysis");
                }

                // both DirectoryInfo and FileInfo inherit from FileSystemInfo which is what the FileSystemEnumerable returns
                if( f as FileInfo is var finfo && finfo != null )
                {
                    if(finfo.FullName.Length > rootPath.Length)
                    {
                        snapshot.AddFile(finfo.FullName.Substring(length), finfo.Length, isdirectory: false);
                    }
                }
                else if(f as DirectoryInfo is var dinfo && dinfo != null)
                {
                    if (dinfo.FullName.Length > rootPath.Length)
                    {
                        snapshot.AddFile(dinfo.FullName.Substring(length), 0, isdirectory: true);
                    }
                }

            }

            return snapshot;
        }

        public void Serialize(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, this);
            }
        }

        public static Snapshot Deserialize(string filename)
        {
            Snapshot snapshot = null;
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                // Deserialize the hashtable from the file and assign the reference to the local variable.
                snapshot = (Snapshot)formatter.Deserialize(fs);
            }

            return snapshot;
        }

        public List<Comparison> CompareAgainst(Snapshot snapshot)
        {
            var src_snap = this;
            var tgt_snap = snapshot;

            var fileToSize = new Dictionary<string, long>();

            var src_names = src_snap.RelNameToSnapfile.Keys.ToList();
            var tgt_names = tgt_snap.RelNameToSnapfile.Keys.ToList();

            var cmn_names = src_names.Intersect(tgt_names);

            var src_only = src_names.Except(tgt_names);
            var tgt_only = tgt_names.Except(src_names);

            var comparisons = new List<Comparison>();

            foreach(var name in cmn_names)
            {
                // compare sizes
                if(src_snap.RelNameToSnapfile[name].Size != tgt_snap.RelNameToSnapfile[name].Size)
                {
                    comparisons.Add(new Comparison(name, Comparison.Differences.SIZE, tgt_snap.RelNameToSnapfile[name].Size - src_snap.RelNameToSnapfile[name].Size, isdirectory: false));
                }
            }

            comparisons.AddRange(src_only.Select(s => new Comparison(src_snap.RelNameToSnapfile[s].RelName, Comparison.Differences.ONLY_LEFT_SIDE, 0, src_snap.RelNameToSnapfile[s].IsDirectory)));
            comparisons.AddRange(tgt_only.Select(s => new Comparison(tgt_snap.RelNameToSnapfile[s].RelName, Comparison.Differences.ONLY_RIGHT_SIDE, 0, tgt_snap.RelNameToSnapfile[s].IsDirectory)));

            return comparisons;
        }

    }
}
