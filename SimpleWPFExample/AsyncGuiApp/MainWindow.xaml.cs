using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncGuiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Results = new ObservableCollection<string>();
            this.DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<string> Results { get; private set; }

        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {
            this.Results.Clear();
            using (var client = new CompositeService.CompositeServiceClient())
            {
                var results = client.GetQuotes(5);
                foreach (var result in results)
                    this.Results.Add(result);
            }
        }

        // How do we make this version asynchronous?
        private void ButtonAsync_Click(object sender, RoutedEventArgs e)
        {
            this.Results.Clear();
            using (var client = new CompositeService.CompositeServiceClient())
            {
                List<string> results = client.GetQuotes(5);
                foreach (var result in results)
                    this.Results.Add(result);
            }
        }
    }
}
