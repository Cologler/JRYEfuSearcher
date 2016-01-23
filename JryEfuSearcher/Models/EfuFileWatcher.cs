using System.Collections.Generic;
using System.Linq;

namespace JryEfuSearcher.Models
{
    public class EfuFileWatcher : EfuFileSystemWatcher
    {
        public EfuFileWatcher(string path)
            : base(path)
        {
            this.EfuFile = new EfuFile(path);
        }

        public EfuFile EfuFile { get; }

        public override IEnumerable<KeyValuePair<EfuFile, FileSystemRecord>> GetItems()
            => this.EfuFile.FileSystemRecords.Select(z => new KeyValuePair<EfuFile, FileSystemRecord>(this.EfuFile, z));

        protected override void OnChanged(string fullPath)
        {
            this.EfuFile.Reload();
        }

        public override void StartWatch()
        {
            this.EfuFile.Reload();
            base.StartWatch();
        }
    }
}