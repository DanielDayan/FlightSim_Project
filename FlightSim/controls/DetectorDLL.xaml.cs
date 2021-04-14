using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSim.controls
{
    /// <summary>
    /// Interaction logic for DetectorDLL.xaml
    /// </summary>
    public partial class DetectorDLL : System.Windows.Controls.UserControl
    {
        DetectorViewModel vm;

        public DetectorDLL()
        {
            vm = new DetectorViewModel(MainModel.Instance);
          
            InitializeComponent();
        }

        private void LinearSelected(object sender, RoutedEventArgs e)
        {
            if (vm.Is_CSV_uploaded())
            {
                
                anomaly_List.Items.Clear();
               List<AnomalyReport> list = vm.SelectedLinearDLL();
                foreach (var anomaly in list)
                {
                    string start = vm.convertLineIndexToTime(anomaly.Start);
                    string end = vm.convertLineIndexToTime(anomaly.End);

                    string desc = anomaly.Desc + "\n" + "at " + start + "-"  + end;
                    
                    anomaly_List.Items.Add(desc);         
                }
                vm.AddPointsToPlotModel(list);
            }
         
        }

        private void CircleSelected(object sender, RoutedEventArgs e)
        {
            if (vm.Is_CSV_uploaded())
            {

                anomaly_List.Items.Clear();
                List<AnomalyReport> list = vm.SelectedCircleDLL();
                foreach (var anomaly in list)
                {
                    string start = vm.convertLineIndexToTime(anomaly.Start);
                    string end = vm.convertLineIndexToTime(anomaly.End);

                    string desc = anomaly.Desc + "\n" + "at " + start + "-" + end;

                    anomaly_List.Items.Add(desc);
                }
                vm.AddPointsToPlotModel(list);
            }

        }

        private void LoadDllSelected(object sender, RoutedEventArgs e)
        {
            if (vm.Is_CSV_uploaded())
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();

                string dll_path = openFileDialog.FileName;
                anomaly_List.Items.Clear();
                try
                {
                    List<AnomalyReport> list = vm.SelectedUploadDLL(dll_path);
                    foreach (var anomaly in list)
                    {
                        string start = vm.convertLineIndexToTime(anomaly.Start);
                        string end = vm.convertLineIndexToTime(anomaly.End);

                        string desc = anomaly.Desc + "\n" + "at " + start + "-" + end;

                        anomaly_List.Items.Add(desc);
                    }
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Somthing went wrong...");
                }
           
            }
        }
    }
}
