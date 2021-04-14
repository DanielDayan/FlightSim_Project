using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{

    public struct CorrelatedFeatures
    {
        public CorrelatedFeatures(string x, string y)
        {
            Feature1 = x;
            Feature2 = y;
            
        }

        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
       

    }
    class GraphMaker
    {
        FlyData[] flyDatas;
        int size_of_row;
        int num_of_cols;
        
        List<CorrelatedFeatures> cf; // list of corrleated features
        public GraphMaker(FlyData[] fd, int size)
        {
            this.flyDatas = fd;
            this.size_of_row = size;
            this.num_of_cols = fd.Length;
            this.cf = new List<CorrelatedFeatures>();
        }
        // get list of points at flydata index from start to start + flow(number of points - num of lines per second)
        public List<DataPoint> GetPointsFromIndex(int start ,int index, int flow)
        {
            List<DataPoint> dp_list = new List<DataPoint>();
            ArrayList arr = flyDatas[index].Data;

          

            for (int i = start; i < flow + start; i++) 
            {
               
                if (i < size_of_row)
                    dp_list.Add(new DataPoint(i, (float)arr[i]));
                
            }
            return dp_list;
        }

        public PlotModel DrawGraph(int current_time, int speed, string Name,  OxyColor color)
        {
            int index = getIndexFromName(Name);
            List<DataPoint> list = GetPointsFromIndex(current_time ,index, speed / 10);
            
            var line = new LineSeries();
            line.Points.AddRange(list);
            line.Color = color;
            PlotModel pm = new PlotModel();
            
            pm.Series.Add(line);
           

            // pm.LegendTitle = Name;
            //pm.LegendPosition = LegendPosition.TopLeft;
            //pm.LegendPlacement = LegendPlacement.Outside;
            //pm.LegendTitleFontSize = 10;
            //pm.LegendTextColor = color;
            return pm;
        }
        public PlotModel DrawPearsonGraph(int current_time, int speed, string Name)
        {
            string cor_featur_name = getCorlFromString(Name);
            return DrawGraph(current_time, speed, cor_featur_name, OxyColors.Red);
        }
        public int getIndexFromName(string name)
        {
            for (int i = 0; i < flyDatas.Length; i++)
            {
                if (String.Compare(name, flyDatas[i].Name) == 0) 
                    return flyDatas[i].ColIndex;

            }
            return 0;
        }
        private float Avg (ArrayList x)
        {
            float sum = 0;
            int size = x.Count;
            for (int i = 0; i < size; sum += (float)x[i], i++) ;
            return sum / size;
        }
        private float Var(ArrayList x)
        {
            float av = Avg(x);
            float sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += (float)x[i] * (float)x[i];
            }
            return sum / x.Count - av * av;
        }
        private float Cov(ArrayList x, ArrayList y)
        {
            int size = x.Count;
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                float a = (float)x[i];
                float b = (float)y[i];
                sum = sum + (a * b);
            }
            sum /= size;

            return sum - Avg(x) * Avg(y);
        }


        // returns the Pearson correlation coefficient of X and Y
        private float Pearson(ArrayList x, ArrayList y)
        {
            int size = x.Count;
            return (float)(Cov(x, y) / (Math.Sqrt(Var(x)) * Math.Sqrt(Var(y))));
        }
        public void LearnCorrelation()
        {
            
            for (int i = 0; i < this.num_of_cols; i++)
            {
                ArrayList tempX = this.flyDatas[i].Data;
                float maxCorl = 0;
                int j_maxCorl = 0;
                for (int j = 0; j < this.num_of_cols; j++)
                {
                    if (j != i)
                    {

                        ArrayList tempY = this.flyDatas[j].Data;
                        float corl = Math.Abs(Pearson(tempX, tempY));
                        if (corl > maxCorl)
                        {
                            maxCorl = corl;
                            j_maxCorl = j;
                        }
                    }
                }
                string f1 = this.flyDatas[i].Name; 
                string f2 = this.flyDatas[j_maxCorl].Name;

                this.cf.Add(new CorrelatedFeatures(f1, f2));

            }
        }
        public string getCorlFromString(string feature)
        {
            foreach (var item in this.cf)
            {
                if (String.Compare(item.Feature1, feature) == 0)
                    return item.Feature2;
            }
            return null;
        }
       
        public PlotModel DrawPoints(int current_time, int speed ,string f1, string f2)
        {
            PlotModel p = new PlotModel();
           
            int index1 = getIndexFromName(f1); // get col index of feature1
            int index2 = getIndexFromName(f2); // get col index of feature2
            int start = current_time - (30 * (speed / 10));
            if (start < 0)
                start = 0;
            List<DataPoint> dp_list = new List<DataPoint>();
            ArrayList arr1 = flyDatas[index1].Data;
            ArrayList arr2 = flyDatas[index2].Data;
            
            for (int i = start; i < current_time; i++)
            {

                if (i < size_of_row)
                    dp_list.Add(new DataPoint((float)arr1[i] , (float)arr2[i]));

            }
            //Console.WriteLine(dp_list.Count);
            // List<DataPoint> list1 = GetPointsFromIndex(start, index1, current_time - start);
            //List<DataPoint> list2 = GetPointsFromIndex(start, index2, current_time - start);

            foreach (var item in dp_list)
            {
                var series = new ScatterSeries
                {
                  
                    MarkerFill = OxyColors.Black,
                    MarkerStroke = OxyColors.Black,
                    MarkerType = MarkerType.Circle,
                 
                    MarkerSize = 0.5,
                };
                //series.Points.Add(item);
                series.Points.Add(new ScatterPoint(item.X, item.Y));
                p.Series.Add(series);
            }

            // Console.WriteLine(list1.Count);
            //Define the x axis
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;

            //Define the y axis
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            //Disable the axis zoom
           // yAxis.IsZoomEnabled = false;
            p.Axes.Add(xAxis);
            p.Axes.Add(yAxis);
            p.ZoomAllAxes(10);
            return p;
        }


       
        public LineSeries LinearRegressionGraph(int current_time, int speed, string f1, string f2)
        {
            int index1 = getIndexFromName(f1);
            int index2 = getIndexFromName(f2);
            // int start = current_time - (30 * (speed / 10));
            //  if (start < 0)
            //   start = 0;
            ArrayList x = flyDatas[index1].Data;
            ArrayList y = flyDatas[index2].Data;
            LineSeries ls = Linear_reg(x, y);

            // plot model for the line reg
           // PlotModel m = new PlotModel();
           // m.Series.Add(ls);
            
            return ls;
        }
        // find linear reg of 2 corrleated feature (from points)
        LineSeries Linear_reg(ArrayList x, ArrayList y)
        {
           
            float a = Cov(x, y) / Var(x);
            float b = Avg(y) - a * (Avg(x));
           
            Line reg_line = new Line(a, b);

            // find max x value and mix x value of list1
            float max_x = (float)x[0];
            float min_x = max_x;
            float max_y, min_y;
            foreach (var v in x)
            {
                float x_value = (float)v;
              
                if (x_value > max_x)
                    max_x = x_value;
                if (x_value < min_x)
                    min_x = x_value;
            }
           
            min_y = reg_line.f(min_x);
            max_y = reg_line.f(max_x);
            LineSeries ls = new LineSeries
            {
                Color = OxyColors.Green,
                StrokeThickness = 1
            };
         
            ls.Points.Add(new DataPoint(min_x, min_y));
            ls.Points.Add(new DataPoint(max_x, max_y));
            return ls;
        }

         public class Line
         {
            float a, b;
            
            public Line(float a, float b)
            {
                this.a = a;
                this.b = b;
            }
            public float f(float x)
            {
                return a * x + b;
            }
         }
    }
    
}
