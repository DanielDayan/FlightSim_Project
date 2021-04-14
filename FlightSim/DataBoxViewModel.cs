using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{
    class DataBoxViewModel : INotifyPropertyChanged

    {
        private MainModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        

        public DataBoxViewModel(MainModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                    //Console.WriteLine("VM_" + e.PropertyName + " {0} " , VM_Aileron);

                };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public float VM_Altimeter
        {
            get
            {
                return model.Altimeter;
            }

        }
        public float VM_AirSpeed
        {
            get
            {
                return model.AirSpeed;
            }

        }
        public float VM_Heading
        {
            get
            {
                return model.Heading;
            }

        }
        public float VM_Pitch
        {
            get
            {
                return model.Pitch;
            }

        }
        public float VM_Roll
        {
            get
            {
                return model.Roll;
            }

        }
        public float VM_Yaw
        {
            get
            {
                return model.Yaw;
            }

        }

    }
}
