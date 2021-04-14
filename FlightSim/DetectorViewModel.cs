using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{
    class DetectorViewModel : INotifyPropertyChanged
    {
        private MainModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public DetectorViewModel(MainModel model)
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

        public bool Is_CSV_uploaded()
        {
            return model.Is_CSV_uploaded();
        }
        public List<AnomalyReport> SelectedLinearDLL()
        {
            return model.GetAnomaliesFromLinear();
        }

        public void AddPointsToPlotModel(List<AnomalyReport> list)
        {
            if (model.Stop || model.Pause)
                model.AddPointsToLinearGraph(list);
           // model.AddPointsToLinearGraph(list);
        }
        public string convertLineIndexToTime(int line)
        {
            float f = (float)line / ((float)model.FlightLength);
            float speed = (float)((float)model.Speed / 100.00);
            if (speed != 0)
                f = f * speed;
            TimeSpan time = TimeSpan.FromSeconds(f * model.TotalTime);

            return time.ToString(@"hh\:mm\:ss");
        }

        public List<AnomalyReport> SelectedCircleDLL()
        {
            return model.GetAnomaliesFromCircle();
        }

        public List<AnomalyReport> SelectedUploadDLL(string dll_path)
        {
            return model.GetAnomaliesFromDLL(dll_path);
        }
    }
}
