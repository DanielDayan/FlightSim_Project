using FlightSim.controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSim
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        JoystickViewModel joy_vm;
        public Joystick()
        {
            InitializeComponent();
            joy_vm = new JoystickViewModel(MainModel.Instance);
            DataContext = joy_vm;
   
        }
        


    }
}
