using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSim.controls
{
      class  JoystickViewModel : INotifyPropertyChanged
    {
        private MainModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        float CAileron;
        float cElevator;
        float cRudder;
        float cThrottle;


        public JoystickViewModel(MainModel model)
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
        
        public float VM_Aileron 
        { 
            get 
            {
               // Console.WriteLine("Aileron in view model {0}" , model.Aileron);
                CAileron = model.Aileron;
                
                return model.Aileron;
            }
         
        }
        public float VM_Elevator
        {
            get
            {
               
                return model.Elevator;
            }

        }
        public float VM_Rudder
        {
            get
            {
                // Console.WriteLine("Aileron in view model {0}" , model.Aileron);
                cRudder = model.Rudder;

                return model.Rudder;
            }
           

        }
        public float VM_Throttle
        {
            get
            {
                // Console.WriteLine("Aileron in view model {0}" , model.Aileron);
                cThrottle = model.Throttle;

                return model.Throttle;
            }

        }


    }

}
