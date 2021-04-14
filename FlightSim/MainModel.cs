using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSim
{
    public struct FlyData
    {
        public FlyData(string x, int y, ArrayList d)
        {
            Name = x;
            ColIndex = y;
            Data = d;
        }

        public string Name { get; set; }
        public int ColIndex { get; set; }
        public ArrayList Data { get; set; }

    }
    class MainModel : INotifyPropertyChanged
    {
        private static MainModel instance = null;
        private static readonly object padlock = new object();
        // video controller
        Client client;
        volatile Boolean stop;
        volatile Boolean pause;
        int speed;
        int totalTime = 0;
         int index;
        int flightLength;
        string CSV_path;
        string XML_path;
        string[] lines;
        //joystick
        FlyData[] colms; // data of csv
        float aileron = 125;
        float elevator = 125;

       

        float rudder = 0;
        float throttle = 0;
        //Data box
        float altimeter;
        float airspeed;
        float heading;

       
        float yaw;
        float pitch;
        float roll;
        // Graphs
        GraphMaker pm;
        PlotModel graphModel;
        PlotModel pearsonModel;
        LineSeries lineSeries;
        PlotModel pointsModel;
        AnomalyDetector detector;
 
        public event PropertyChangedEventHandler PropertyChanged;

         MainModel(Client client)
         {
            this.detector = new AnomalyDetector();
            this.lines = null;
            this.flightLength = 1; // default
            this.index = 0;
            this.client = client;
            this.stop = false;
            this.pause = false;
            this.speed = 100; // default 10 lines per second
            this.CSV_path = null;
            this.colms = null;
            this.XML_path = null;
            
        }

      

        public static MainModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainModel(new Client());
                    }
                    return instance;
                }
            }
        }
        public void updatePath(string path)
        {
            this.CSV_path = path;
            this.detector.setDetectPath(path);
            //this.detector.DetectAnomalies(1);
        }
        public bool Is_CSV_uploaded()
        {
            return CSV_path != null;
        }
        public void connectToFG()
        {
            this.client.connect();
        }
        public void disconnectFromFG()
        {
            this.stop = true;
            this.client.disconnect();
        }
        public Client Client()
        {
            return this.client;
        }
        private void setJoystick_vars()
        {
            Reader reader = new Reader();
            ArrayList d = reader.ReadCSV(CSV_path);
            ArrayList names = reader.ReadXML(XML_path);
           
            colms = new FlyData[names.Count];
            List<string> temp_list = new List<string>();
            for (int i = 0; i < names.Count; i++)
            {
                temp_list.Add((string)names[i]);    
                ArrayList temp = new ArrayList();
                colms[i].Name = names[i].ToString();
                colms[i].ColIndex = i;
                for (int j = 0; j < d.Count; j++)
                {

                    ArrayList row = (ArrayList)d[j];
                    temp.Add(row[i]);
                }
                colms[i].Data = temp;
            }
            Select = temp_list;
            this.pm = new GraphMaker(this.colms, this.flightLength);
            this.pm.LearnCorrelation();
            

        }
        public void play(String[] lines, int idx)
        {
            if (this.lines == null)
            {
                this.lines = lines;
                setJoystick_vars();
               
            }
            this.stop = false;
            this.pause = false;
            int max = lines.Length - 1;
            new Thread(delegate ()
            {
                while (!stop)
                {
                    
                    this.client.sendLine(lines[this.index]);
                    //Console.WriteLine(this.index);
                    if (currentGraphVar != null)
                    {
                        GraphModel = pm.DrawGraph(index, speed, currentGraphVar, OxyColors.Blue);
                        PearsonGraph = pm.DrawPearsonGraph(index, speed, currentGraphVar);
                        UpdateCorrVar = pm.getCorlFromString(currentGraphVar);
                        // get regression line between vars
                        LineSeries = pm.LinearRegressionGraph(index, speed, currentGraphVar, currentCorrVar);
                         // draw points of two vars of last 30 seconds
                        PointsGraph = pm.DrawPoints(index,speed, currentGraphVar, currentCorrVar);
                        // add line series to graph
                        this.pointsModel.Series.Add(this.lineSeries);

                        //CircleGraph = pm.DrawCircle(index, speed, currentGraphVar, currentCorrVar);
                        // linear detector
                        if (detector.isSimpleDetectorRunning())
                            AddPointsToLinearGraph(this.detector.getLinearList());
                      
                        NotifyPropertyChanged("LineSeries");
                        
                    }
                    
                    // alireon
                    float alireon_value = (float)colms[0].Data[index] * 60;
                    Aileron = 125 + (alireon_value);
                    // elevator
                    float elevator_value = (float)colms[1].Data[index] * 60;
                    Elevator = 125 + (elevator_value);
                    // rudder
                    Rudder = (float)colms[2].Data[index];
                    //throttle
                    Throttle = (float)colms[6].Data[index];
                    // altimeter
                    Altimeter = (float)colms[24].Data[index];
                    //airspeed
                    AirSpeed = (float)colms[20].Data[index];
                    //airspeed
                    Heading = (float)colms[18].Data[index];
                    //airspeed
                    Pitch = (float)colms[17].Data[index];
                    //airspeed
                    Roll = (float)colms[16].Data[index];
                    //airspeed
                    Yaw = (float)colms[19].Data[index];
                    this.index = this.index + 1;
                    Index = this.index;
                    Thread.Sleep(this.speed);
                    if (this.index >= max)
                        Stop = true;
                    while (pause)
                    {
                        //Console.WriteLine("Pause = {0}", pause);
                        Thread.Sleep(100);
                        // press Resume
                    }

                }

            }).Start();
         
        }

        public void updateXML_Path(string xml_path)
        {
            this.XML_path = xml_path;
        }
        // VideoControll
        public void StopVideo()
        {
            this.stop = true;
        }
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                NotifyPropertyChanged("Speed");
            }
        }
        public Boolean Stop
        {
            get { return stop; }
            set
            {
                stop = true;
                NotifyPropertyChanged("Stop");
            }
        }
        public Boolean Pause
        {
            get { return pause; }
            set
            {
                pause = true;
                NotifyPropertyChanged("Pause");
            }
        }
        public int Index
        {
            get { return index; }
            set
            {
                this.index = value;
               // Console.WriteLine("index is {0} ", this.index);
                NotifyPropertyChanged("Index");
                NotifyPropertyChanged("CurrentTime");
            }   
        }
        public int FlightLength
        {
            get { return flightLength; }
            set
            {
                //Console.WriteLine("inside  Model : {0}", value);
                this.flightLength = value;
                NotifyPropertyChanged("FlightLength");
            }
        }

        //Joystick prop
        public float Aileron 
        { 
            get { return aileron; }
            set
            {
                aileron = value;
                //Console.WriteLine("Aileron changed in model");
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                //Console.WriteLine("Aileron changed in model");
                NotifyPropertyChanged("Elevator");
            }
        }
        public float Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                //Console.WriteLine("Aileron changed in model");
                NotifyPropertyChanged("Rudder");
            }
        }
        public float Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                //Console.WriteLine("Aileron changed in model");
                NotifyPropertyChanged("Throttle");
            }
        }
        // DataBox prop
        public int TotalTime
        {
            get {

                return flightLength / (speed / 10); }
            set { totalTime = value; }
        }
        public float Altimeter
        {
            get { return altimeter; }
            set 
            { 
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
           
        }
        public float AirSpeed
        {
            get { return airspeed; }
            set
            {
                airspeed = value;
                NotifyPropertyChanged("AirSpeed");
            }

        }
        public float Heading
        {
            get { return heading; }
            set
            {
                heading = value;
                NotifyPropertyChanged("Heading");
            }

        }
        public float Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }

        }
        public float Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }

        }
        public float Yaw
        {
            get { return yaw; }
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }

        }
        //Graphs props
        private List<string> select;
        public List<string> Select
        {
            get { return select; }
            set
            {
                select = value;
                NotifyPropertyChanged("Select");
            }

        }
        // plot model
        public PlotModel GraphModel
        {
            get { return this.graphModel; }
            set
            {
                graphModel = value;
                NotifyPropertyChanged("GraphModel");
            }

        }
        private string currentGraphVar = null;
        private string currentCorrVar = null;

        public string UpdateGraphVar
        {
            get { return currentGraphVar; }
            set { currentGraphVar = value; }
        }
        public string UpdateCorrVar
        {
            get { return currentCorrVar; }
            set
            {
                currentCorrVar = value;
                NotifyPropertyChanged("CurrentCorrVar");
            }
        }
        public PlotModel PearsonGraph
        {
            get { return this.pearsonModel; }
            set
            {
                pearsonModel = value;
                NotifyPropertyChanged("PearsonGraph");
            }

        }
        public LineSeries LineSeries
        {
            get { return this.lineSeries; }
            set
            {
                this.lineSeries = value;
                NotifyPropertyChanged("LineSeries");
            }

        }
        public PlotModel PointsGraph
        {
            get { return this.pointsModel; }
            set
            {
                pointsModel = value;
                NotifyPropertyChanged("PointsGraph");
            }

        }
        private PlotModel circleGraph;

        public PlotModel CircleGraph
        {
            get { return circleGraph; }
            set { circleGraph = value;
                NotifyPropertyChanged("CircleGraph");
            }
        }

        //AnomalyDetector
        public List<AnomalyReport> GetAnomaliesFromLinear()
        {
           return this.detector.getLinearList();
        }
        public void AddPointsToLinearGraph(List<AnomalyReport> list)
        {
         
            foreach (var ar in list)
            {
                string[] vars = ar.Desc.Split(' ');
                if (String.Compare(vars[0], currentGraphVar) == 0 || String.Compare(vars[1], currentGraphVar) == 0)
                {
                    if(Index >= ar.Start && Index <= ar.End)
                    {
                        int index1 = pm.getIndexFromName(vars[0]);
                        int index2 = pm.getIndexFromName(vars[1]);
                        List<DataPoint> ar_points = pm.GetPointsFromIndex(index, index1, speed / 10);
                        //this.pointsModel.Series.Add()
                        foreach (var point in ar_points)
                        {
                            var series = new LineSeries
                            {
                                Color = OxyColors.Red,
                                MarkerFill = OxyColors.Red,
                                MarkerStroke = OxyColors.Red,
                                MarkerType = MarkerType.Circle,
                                StrokeThickness = 0,
                                MarkerSize = 0.7,
                            };
                            series.Points.Add(point);
                            pointsModel.Series.Add(series);
                        }
                    }
                }
            }
            NotifyPropertyChanged("PointsGraph");
        }
        public void AddPointsToCircleGraph(List<AnomalyReport> list)
        {

            foreach (var ar in list)
            {
                string[] vars = ar.Desc.Split(' ');
                if (String.Compare(vars[0], currentGraphVar) == 0 || String.Compare(vars[1], currentGraphVar) == 0)
                {
                    if (Index >= ar.Start && Index <= ar.End)
                    {
                        int index1 = pm.getIndexFromName(vars[0]);
                        int index2 = pm.getIndexFromName(vars[1]);
                        List<DataPoint> ar_points = pm.GetPointsFromIndex(index, index1, speed / 10);
                        //this.pointsModel.Series.Add()
                        foreach (var point in ar_points)
                        {
                            var series = new LineSeries
                            {
                                Color = OxyColors.Red,
                                MarkerFill = OxyColors.Red,
                                MarkerStroke = OxyColors.Red,
                                MarkerType = MarkerType.Circle,
                                StrokeThickness = 0,
                                MarkerSize = 0.7,
                            };
                            series.Points.Add(point);
                            circleGraph.Series.Add(series);
                        }
                    }
                }
            }
            NotifyPropertyChanged("CircleGraph");
        }
        public List<AnomalyReport> GetAnomaliesFromCircle()
        {
            return detector.getCircleList();
        }
        public List<AnomalyReport> GetAnomaliesFromDLL(string dll_path)
        {
            return detector.GetAnomliesListFromDLL(dll_path);
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
