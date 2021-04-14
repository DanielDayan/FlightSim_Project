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

namespace FlightSim.controls
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {
        GraphViewModel graph_vm;
        private bool handle = true;
        
        public Graphs()
        {
            InitializeComponent();
            graph_vm = new GraphViewModel(MainModel.Instance);
            DataContext = graph_vm;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }
        private void Handle()
        {
            if (Var_select.SelectedItem == null)
                return;
            string option = Var_select.SelectedItem.ToString();

            //Console.WriteLine(option);
            graph_vm.ComboBoxChange(option);
        }
    }
}
