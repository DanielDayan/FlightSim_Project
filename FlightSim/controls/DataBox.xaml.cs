using FlightSim.controls;
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
    /// Interaction logic for DataBox.xaml
    /// </summary>
    public partial class DataBox : UserControl
    {

        DataBoxViewModel DataBox_vm;
        public DataBox()
        {
            InitializeComponent();
            DataBox_vm = new DataBoxViewModel(MainModel.Instance);
            DataContext = DataBox_vm;
        }

       
    }
}
