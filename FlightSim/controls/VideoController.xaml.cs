using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace FlightSim.controls
{
    /// <summary>
    /// Interaction logic for VideoController.xaml
    /// </summary>
    public partial class VideoController : System.Windows.Controls.UserControl
    {
        VideoViewModel vm;
        bool is_CSV_uploaded;
        string[] lines;
        string curr_path;
        string xml_path;
        int index;
        bool is_XML_uploaded;
        int length;
        
       // int seconds;
        


        public VideoController()
        {
            InitializeComponent();
            vm = new VideoViewModel(MainModel.Instance);
            is_CSV_uploaded = false;
            is_XML_uploaded = false;
            curr_path = null;
            index = 0;
            //seconds = 0;
            
            DataContext = vm;

        }
       

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Stopping");
            this.vm.VM_Stop = true;
            this.vm.VM_Index = 0;
            //this.index = 0;
            // vm.VM_Client().disconnect();
        }

        private void Pause_button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Pausing");
            this.vm.VM_Pause = true;
            
            
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
           // vm.UpdateIndex();
            if (!is_CSV_uploaded)
            {
                System.Windows.MessageBox.Show("Please upload CSV file", "Error");
                return;
            }
            if (!is_XML_uploaded)
            {
                System.Windows.MessageBox.Show("Please upload XML file", "Error");
                return;
            }
            
            if (!vm.VM_Client().is_connected)
                vm.VM_Client().connect();
           
          //  Console.WriteLine("Play video at {0}" , index);
            
            vm.VM_Play(this.lines, vm.VM_Index);
        }
        
        private void upload_CSV_Click(object sender, RoutedEventArgs e)
        {

            // upload csv

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            this.curr_path = openFileDialog.FileName;
            if (curr_path == null || curr_path.Length == 0)
                return;
            //Console.WriteLine(path);
            lines = File.ReadAllLines(curr_path);
            // skipe first line - names of features
            lines = lines.Skip(1).ToArray();
            vm.updatePath(curr_path);
            is_CSV_uploaded = true;
            this.length = lines.Length;
            //Console.WriteLine(Slider_.Maximum);
            vm.VM_FlightLength = this.length;
            Slider_.Maximum = this.length;
            //Console.WriteLine(Slider_.Maximum);
            int total = vm.VM_TotalTime;
           TimeSpan time = TimeSpan.FromSeconds(total);
            string str_time = time.ToString(@"hh\:mm\:ss");
            Txt_block.Text = str_time;

            //Reader reader = new Reader();
            //reader.ReadCSV(path);

        }

        private void Play_Speed_TextChanged(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string text = Play_Speed.Text;
           
            if (text.Length == 0)
                return;
            if (e.Key == Key.Return)
            {
                //Console.WriteLine("Text has changed {0}", text);
                double vspeed = Double.Parse(text);
                if (vspeed > 5)
                    vspeed = 5;
                else if (vspeed <= 0)
                    vspeed = 0.1;
                Play_Speed.Text = vspeed.ToString();
                int new_speed = (int)(100 / vspeed);

                vm.VM_VideoSpeed = new_speed;
            }

        }
        

        private void Slider__ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            vm.VM_Index = (int)e.NewValue; // move video to current time line of slider
            //Console.WriteLine("Slider Value : {0}", e.NewValue);
            
           
            
            //Txt_block.Text = new_index.ToString();
            
        }

        private void ___upload_XML__Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            this.xml_path = openFileDialog.FileName;
            if (xml_path == null || xml_path.Length == 0)
                return;
            //Console.WriteLine(path);
           
            vm.updateXML(xml_path);
            is_XML_uploaded = true;
            
            
            
        }
    }
}
