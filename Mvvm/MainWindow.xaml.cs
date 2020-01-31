using System.Windows;

namespace Mvvm
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext =
                new ApplicationViewModel(new DefaultDialogService(), new JsonFileService());
        }
    }
}