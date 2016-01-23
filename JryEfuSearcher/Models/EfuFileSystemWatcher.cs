using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JryEfuSearcher.Models
{
    public abstract class EfuFileSystemWatcher
    {
        private readonly FileSystemWatcher watcher;

        public EfuFileSystemWatcher(string path)
        {
            this.Path = path;
            this.watcher = new FileSystemWatcher(path);
            this.watcher.Created += this.Watcher_Changed;
            this.watcher.Changed += this.Watcher_Changed;
            this.watcher.Deleted += this.Watcher_Changed;
        }

        public virtual void StartWatch()
        {
            this.watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Task.Run(() =>
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Created:
                    case WatcherChangeTypes.Changed:
                    case WatcherChangeTypes.Deleted:
                        this.OnChanged(e.FullPath);
                        break;
                }
            });
        }

        public string Path { get; }

        public abstract IEnumerable<KeyValuePair<EfuFile, FileSystemRecord>> GetItems();

        protected abstract void OnChanged(string fullPath);
    }
}