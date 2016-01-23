using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JryEfuSearcher.Models
{
    public class EfuDirectoryWatcher : EfuFileSystemWatcher
    {
        public EfuDirectoryWatcher(string path)
            : base(path)
        {
            this.EfuFiles = new List<EfuFile>();
        }

        public List<EfuFile> EfuFiles { get; }

        public override IEnumerable<KeyValuePair<EfuFile, FileSystemRecord>> GetItems()
            => this.EfuFiles.SelectMany(z => z.FileSystemRecords.Select(
                x => new KeyValuePair<EfuFile, FileSystemRecord>(z, x)));

        protected override void OnChanged(string fullPath)
        {
            if (!fullPath.ToLower().EndsWith(".efu")) return;

            var file = this.EfuFiles.Find(z => z.Path == fullPath);
            if (file == null)
            {
                file = new EfuFile(fullPath);
                this.EfuFiles.Add(file);
            }
            file.Reload();
        }

        public override void StartWatch()
        {
            this.EfuFiles.AddRange(Directory.GetFiles(this.Path).Select(z => new EfuFile(z)));
            foreach (var efuFile in this.EfuFiles)
            {
                efuFile.Reload();
            }
            base.StartWatch();
        }
    }
}