using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using BreadthFirstSearch.Task;

namespace BreadthFirstSearch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CheckerField StartField { set; get; }
        public CheckerField FinalField { set; get; }
        private Bfs _bfs;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _bfs = new Bfs();
            _bfs.Run();
            StartField = _bfs.StartField;
            CheckerGrid.DataContext = StartField;
            FinalField = _bfs.FinalField;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartField.InitPath();
            _bfs.Path = StartField.Path;
            _bfs.InitPath();
            _bfs.Step();
            StartField.InitStartFieldCells();
            MessageBox.Show("Шлях був знайдений. Довжина=" + _bfs.Path.Count);
        }
        
        private void Pause()
        {
            CheckerGrid.Dispatcher.Invoke(DispatcherPriority.Background,
                new Action(delegate() { CheckerGrid.UpdateLayout(); }));
            Thread.Sleep(300);
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var move in _bfs.Path)
            {
                _bfs.PathHistory.Add(move.ToString());
                move.Transfer();

                CheckerGrid.DataContext = null;
                CheckerGrid.DataContext = StartField;
                ResultRichTextBox.Document.Blocks.Clear();
                ResultRichTextBox.Document.Blocks.Add(new Paragraph(new Run(_bfs.GetPath)));
                Pause();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            StartField.InitStartFieldCells();
            _bfs.StartField = StartField;
            _bfs.PathHistory.Clear();
            CheckerGrid.DataContext = null;
            CheckerGrid.DataContext = StartField;
            ResultRichTextBox.Document.Blocks.Clear();
        }
    }
}
