using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JryEfuSearcher.Models
{
    public class EfuFileManager
    {
        private const string ConfigPath = "watcher.config";

        public EfuFileManager()
        {
            this.EfuFiles = new List<EfuFileSystemWatcher>();
            
        }

        public List<EfuFileSystemWatcher> EfuFiles { get; }

        public void Initialize()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    using (var reader = File.OpenText(ConfigPath))
                    {
                        do
                        {
                            var line = reader.ReadLine();
                            if (line == null) break;
                            if (File.Exists(line))
                            {
                                this.EfuFiles.Add(new EfuFileWatcher(line));
                            }
                            else if (Directory.Exists(line))
                            {
                                this.EfuFiles.Add(new EfuDirectoryWatcher(line));
                            }
                        } while (true);
                    }
                }
                catch
                {
                    // ignored
                }

                this.StartWatch();
            }
        }

        public void StartWatch()
        {
            foreach (var watcher in this.EfuFiles)
            {
                watcher.StartWatch();
            }
        }

        public IEnumerable<KeyValuePair<EfuFile, FileSystemRecord>> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return this.EfuFiles.SelectMany(z => z.GetItems())
                .Where(z => z.Value.Filename.ToLower().Contains(keyword))
                .Take(200);
        }
    }
}