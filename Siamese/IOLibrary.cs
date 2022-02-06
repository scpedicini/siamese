using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Siamese
{
    public class IOLibrary
    {
        public static void RunProgram(string exe, string args)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = exe;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.Start();
            }
        }

        /*
        static void DoIt()
        {
            string infile = @"C:\temp\legion_data.csv";
            string outfile = null; //@"C:\temp\legion_data.csv";

            string tgtpath = @"D:\data";

            var compareSets = !String.IsNullOrWhiteSpace(infile);

            var results = new List<string>();


            Dictionary<string, long> fileToSize_In = null;

            if (compareSets)
            {
                fileToSize_In = JsonConvert.DeserializeObject<Dictionary<string, long>>(File.ReadAllText(infile, Encoding.UTF8));
            }

            var fileToSize = new Dictionary<string, long>();

            var fsenum = new FileSystemEnumerable(new DirectoryInfo(tgtpath), "*.*", SearchOption.AllDirectories);
            foreach (var file in fsenum)
            {
                var name = file.FullName.ToLowerInvariant().Replace(@"d:\", @"c:\");
                long length = file is FileInfo ? ((FileInfo)file).Length : 0;
                fileToSize.Add(name, length);


                // does this file exist in the infile set?
                if (compareSets && fileToSize_In.ContainsKey(name) && fileToSize[name] != fileToSize_In[name])
                {
                    results.Add($"{name} - In Size: {fileToSize_In[name]}, Output Size: {fileToSize[name]}");
                }
            }

            if (compareSets)
            {
                var infile_list = fileToSize_In.Keys.ToList();
                var outfile_list = fileToSize.Keys.ToList();

                infile_list.Except(outfile_list).ToList().ForEach(i => results.Add($"{i} is unique to input set"));
                outfile_list.Except(infile_list).ToList().ForEach(i => results.Add($"{i} is unique to output set"));

            }

            Console.WriteLine($"File total: {fileToSize.Count}");

            Console.WriteLine("Errors: ");
            Console.WriteLine(string.Join(Environment.NewLine, fsenum.Errors));

            if (compareSets)
            {
                Console.WriteLine("Differences:");
                Console.WriteLine(string.Join(Environment.NewLine, results));
            }

            if (!string.IsNullOrWhiteSpace(outfile))
            {
                //SerializeToXML(fileToSize, outfile);
                var jsonstring = JsonConvert.SerializeObject(fileToSize);
                File.WriteAllText(outfile, jsonstring, Encoding.UTF8);
            }


        }
        */


    }
}
