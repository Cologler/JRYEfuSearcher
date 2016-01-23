using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using Jasily.ComponentModel;

namespace JryEfuSearcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }

    public class MainViewModel : JasilyViewModel
    {
        public MainViewModel()
        {
            this.FilterText = string.Empty;
        }

        private string filterText;

        public ObservableCollection<FileSystemRecordViewModel> Items { get; }
            = new ObservableCollection<FileSystemRecordViewModel>();

        public string FilterText
        {
            get { return this.filterText; }
            set
            {
                if (this.SetPropertyRef(ref this.filterText, value))
                    this.BeginDelayFilter();
            }
        }

        public async void BeginDelayFilter()
        {
            var text = this.FilterText ?? string.Empty;
            await Task.Delay(400);
            if (text == this.filterText)
            {
                this.Items.Reset(((App) Application.Current).EfuFileManager
                    .Search(text.Trim())
                    .Select(z => new FileSystemRecordViewModel(z)));
            }
        }
    }
}
