using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Siamese
{
    public class FileSystemEnumerable : IEnumerable<FileSystemInfo>
    {
        // TODO Errors is recursive, we need to instead pass a collection that can aggregate the errors
        public List<string> Errors = new List<string>();

        private readonly DirectoryInfo _root;
        private readonly IList<string> _patterns;
        private readonly SearchOption _option;

        public FileSystemEnumerable(DirectoryInfo root, string pattern, SearchOption option)
        {
            _root = root;
            _patterns = new List<string> { pattern };
            _option = option;
        }

        public FileSystemEnumerable(DirectoryInfo root, IList<string> patterns, SearchOption option)
        {
            _root = root;
            _patterns = patterns;
            _option = option;
        }

        public IEnumerator<FileSystemInfo> GetEnumerator()
        {
            if (_root == null || !_root.Exists) yield break;

            IEnumerable<FileSystemInfo> matches = new List<FileSystemInfo>();
            try
            {
                foreach (var pattern in _patterns)
                {
                    matches = matches.Concat(_root.EnumerateDirectories(pattern, SearchOption.TopDirectoryOnly))
                                     .Concat(_root.EnumerateFiles(pattern, SearchOption.TopDirectoryOnly));
                }
            }
            catch (UnauthorizedAccessException)
            {
                Errors.Add($"Unable to access '{_root.FullName}'. Skipping...");
                yield break;
            }
            catch (PathTooLongException ptle)
            {
                Errors.Add($@"Could not process path '{_root.Parent.FullName}\{_root.Name}'.");
                yield break;
            }
            catch (System.IO.IOException e)
            {
                // "The symbolic link cannot be followed because its type is disabled."
                // "The specified network name is no longer available."
                Errors.Add($@"Could not process path (check SymlinkEvaluation rules)'{_root.Parent.FullName}\{_root.Name}'.");
                yield break;
            }

            foreach (var file in matches)
            {
                yield return file;
            }

            if (_option == SearchOption.AllDirectories)
            {
                foreach (var dir in _root.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    if (!dir.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        var fileSystemInfos = new FileSystemEnumerable(dir, _patterns, _option);
                        foreach (var match in fileSystemInfos)
                        {
                            yield return match;
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
