using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace JryEfuSearcher.Models
{
    public class EfuFile
    {
        public string Path { get; }

        public List<FileSystemRecord> FileSystemRecords { get; }

        public EfuFile(string path)
        {
            this.Path = path;
            this.FileSystemRecords = new List<FileSystemRecord>();
        }

        public bool IsExists() => File.Exists(this.Path);

        public bool Reload()
        {
            this.FileSystemRecords.Clear();
            if (!this.IsExists()) return false;

            try
            {
                using (var reader = File.OpenText(this.Path))
                {
                    var csv = new CsvReader(reader);
                    this.FileSystemRecords.AddRange(csv.GetRecords<FileSystemRecord>());
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}