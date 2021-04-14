using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{
    class VideoViewModel : INotifyPropertyChanged
    {
        private MainModel model;
        int video_speed;
        int video_index;
        string path;
        private string xmlPath;
        int seconds;

        public event PropertyChangedEventHandler PropertyChanged;
        public VideoViewModel(MainModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                    
                };
        }
        public void updatePath(string path)
        {
            this.path = path;
            model.updatePath(path);
        }
        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public Client VM_Client()
        {
            return this.model.Client();
        }
        public int VM_VideoSpeed
        {
            get { return model.Speed; }
            set
            {
                video_speed = value;
                model.Speed = video_speed;
                //Console.WriteLine("speed has changed to {0} in model", model.Speed);
            }
        }
        public Boolean VM_Stop
        {
            get { return model.Stop; }
            set
            {
                model.Stop = true;
               // Console.WriteLine("video has stopped");
            }
        }
        public Boolean VM_Pause
        {
            get { return model.Pause; }
            set
            {
                model.Pause = true;
               // Console.WriteLine("video has Paused");
            }
        }
       
        public void VM_Play(string[] lines, int index)
        {
            model.play(lines,index);
        }
        public int VM_Index
        {
            get { return model.Index; }
            set
            {
                this.video_index = value;
                model.Index = video_index;
               
                 //Console.WriteLine("index has been updated");
            }
        }
      
        public int VM_FlightLength
        {
            get { return model.FlightLength; }
            set
            {
               // Console.WriteLine("inside View Model : {0}" ,value);
                model.FlightLength = value;
                
                // Console.WriteLine("index has been updated");
            }
        }
        public int VM_TotalTime
        {
            get { return model.TotalTime; }
            set
            {
                
                model.TotalTime = value;

            }
        }
        public string VM_CurrentTime
        {
            get
            {
                if(model.Stop == true)
                {
                    return "00:00:00";
                }
                float f = (float)model.Index /  ((float)model.FlightLength);
                float speed =  (float) ((float)video_speed / 100.00);
                if (speed != 0)
                    f = f * speed;
                TimeSpan time = TimeSpan.FromSeconds(f * model.TotalTime);
            
                return time.ToString(@"hh\:mm\:ss");
                
            }

        }

        public void updateXML(string xml_path)
        {
            this.xmlPath = xml_path;
            model.updateXML_Path(xml_path);
        }
    }
}
