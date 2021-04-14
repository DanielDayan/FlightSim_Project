using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace FlightSim
{
    class GraphViewModel : INotifyPropertyChanged

    {
        
        private PlotModel Graph;
        private PlotModel pearsonGraph;
        private LineSeries Line;
        private PlotModel PointsGraph;
        private MainModel model;
        private PlotModel CircleGraph;
        public event PropertyChangedEventHandler PropertyChanged;
        string ShowedGraph = null;
        string ShowdPearson = null;

        public GraphViewModel(MainModel model)
        {
            this.Graph = new PlotModel();
            this.pearsonGraph = new PlotModel();
            this.Line = new LineSeries();
            this.PointsGraph = new PlotModel();
            
            SetUpModel();
            
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
        public List<string> VM_Select
        {
            get
            {
                return model.Select;
            }

        }
        public PlotModel VM_GraphModel
        {
            get { return model.GraphModel; }
            set {
                this.Graph = value;
                  }
        }
        public PlotModel VM_PearsonGraph
        {
            get { return model.PearsonGraph; }
            set
            {
                this.pearsonGraph = value;
            }
        }
        public void ComboBoxChange(string option)
        {
            ShowedGraph = option;
            model.UpdateGraphVar = option;
        }
        public string VM_CurrentCorrVar { 
            get
            {
                this.ShowdPearson = model.UpdateCorrVar;
                return this.ShowdPearson;
            }
           
        }
        public LineSeries VM_LineSeries
        {
            get
            {
                return model.LineSeries;
            }
            set
            {
                this.Line = value;
            }

        }
        public PlotModel VM_PointsGraph
        {
            get
            {
                return model.PointsGraph;
            }
            set
            {
                this.PointsGraph = value;
            }

        }
      
        private void SetUpModel()
        {
            // graph
            var dateAxis1 = new LinearAxis();
            dateAxis1.Position = AxisPosition.Bottom;
            Graph.Axes.Add(dateAxis1);
            var valueAxis1 = new LinearAxis();
            valueAxis1.Position = AxisPosition.Top;
            Graph.Axes.Add(valueAxis1);
           
            // Pearson Graph
            var dateAxis2 = new LinearAxis();
            dateAxis2.Position = AxisPosition.Bottom;
            pearsonGraph.Axes.Add(dateAxis2);
            var valueAxis2 = new LinearAxis();
            valueAxis2.Position = AxisPosition.Left;
            pearsonGraph.Axes.Add(valueAxis2);

            // LinearRegGraph
           // var dateAxis3 = new LinearAxis();
            //dateAxis3.Position = AxisPosition.Bottom;
            //LinearRegGraph.Axes.Add(dateAxis3);
            //var valueAxis3 = new LinearAxis();
            //valueAxis3.Position = AxisPosition.Left;
            //LinearRegGraph.Axes.Add(valueAxis3);

            // PointsGraph
            var dateAxis4 = new LinearAxis();
            dateAxis4.Position = AxisPosition.Bottom;
            PointsGraph.Axes.Add(dateAxis4);
            var valueAxis4 = new LinearAxis();
            valueAxis4.Position = AxisPosition.Left;
            PointsGraph.Axes.Add(valueAxis4);


        }
       
    }
}
