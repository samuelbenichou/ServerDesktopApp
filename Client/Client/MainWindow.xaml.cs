using Client.ViewModel;
using System;
using System.Collections.Generic;
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
using Client.Model;
using System.Timers;
using System.Windows.Threading;
using System.Data;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModelClass viewModel;
        DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModelClass();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Tick += new EventHandler(Datagrid_Loaded);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 20, 0); // Can change the interval of the timer
            dispatcherTimer.Start();

            //Execute immediately the timer
            DispatcherTimer_Tick(dispatcherTimer, new EventArgs());
        }

        /// <summary>
        /// Increase the size of the server and company columns in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Datagrid_Loaded(object sender, EventArgs e)
        {
            MyDataGrid.Columns[1].Width = new DataGridLength(3.8, DataGridLengthUnitType.Star);
            MyDataGrid.Columns[2].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
        }

        /// <summary>
        /// Event of the refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer_Tick(dispatcherTimer, new EventArgs());
            Datagrid_Loaded(MyDataGrid, new EventArgs());
        }

        /// <summary>
        /// Display the main window with the Servers status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            string lastUpdated = string.Empty;
            List<ServerStatus> serverStatus = viewModel.GetServerStatusList(out lastUpdated);
            MyDataGrid.ItemsSource = serverStatus;
            DateBox.Text = " " + lastUpdated;
        }

    }
}
