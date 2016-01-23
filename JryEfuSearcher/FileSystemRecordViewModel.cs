using Jasily.ComponentModel;
using JryEfuSearcher.Models;
using System.Collections.Generic;
using System.IO;

namespace JryEfuSearcher
{
    public class FileSystemRecordViewModel : JasilyViewModel<FileSystemRecord>
    {
        public FileSystemRecordViewModel(KeyValuePair<EfuFile, FileSystemRecord> kvp)
            : base(kvp.Value)
        {
            this.EfuFileName = Path.GetFileName(kvp.Key.Path);
        }

        public string FileName
        {
            get
            {
                var name = Path.GetFileName(this.Source.Filename);
                return name.Length == 0 ? this.Source.Filename : name;
            }
        }

        public string FullPath => this.Source.Filename;

        public string EfuFileName { get; private set; }
    }
}