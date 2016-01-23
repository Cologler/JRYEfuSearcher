using JryEfuSearcher.Models;
using System.Windows;

namespace JryEfuSearcher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public EfuFileManager EfuFileManager { get; } = new EfuFileManager();

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.EfuFileManager.Initialize();
        }
    }
}
