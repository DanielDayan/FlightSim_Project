using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{
    public struct Circle
    {
        public Circle(float x, float y, float r,string f1, string f2)
        {
            X = x;
            Y = y;
            Radius = r;
            Feature1 = f1;
            Feature2 = f2;

        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }

    }
 
    class AnomalyDetector
    {
       
        IntPtr ts;
        string csv_path;
        string detect_path = null;
        List<AnomalyReport> ar;
      
        private bool is_SimpleDetector;
        //IntPtr[] cf = null;
        Circle[] circles = null;
        public AnomalyDetector()
        {
            csv_path = "test.csv";
         
            IntPtr csv_learn_path = CreatestringWrapper();
            ar = new List<AnomalyReport>();
            for (int i = 0; i < csv_path.Length; i++)
            {
                char cur = csv_path[i];
                appendChar(csv_learn_path, cur);
            }
            this.ts = CreateTimeSeries(csv_learn_path);
            
            //Console.WriteLine(Get_CF_Size(HybDetector));
            // int size = Get_CF_Size(detector);

        }


        // 1 for simple 
        // 2 for hybrid
        public void DetectAnomalies(int detector_selected)
        {
            this.ar.Clear();
            IntPtr detector;
            switch(detector_selected)
            {
                case 1:
                    detector = createSimpleDetector();

                    break;
                case 2:
                    detector = createHybridDetector();
                    break;
                default:
                    detector = createSimpleDetector();
                    break;

            }
          

            InvokeLearnNormal(detector, this.ts);

           
            IntPtr csv_detect_path = CreatestringWrapper();
            for (int i = 0; i < this.detect_path.Length; i++)
            {
                char cur = this.detect_path[i];
                appendChar(csv_detect_path, cur);
            }
            
            IntPtr lts = CreateTimeSeries(csv_detect_path);
            MyDetectAnomalies(detector, lts);
            int size = Get_AR_Size(detector);
            if (size == 0)
            {
                this.ar = new List<AnomalyReport>();
                return;
            }
            IntPtr[] arw = new IntPtr[size];
            for (int i = 0; i < size; i++)
            {
                arw[i] = GetARWrapper(detector, i);
               
            }
            List<int> timesteps = new List<int>();
           // List<string> features = new List<string>();
            for (int i = 0; i < size; i++)
            {
                timesteps.Add(GetTimeStep(arw[i]));
            }
           
        

            for (int i = 0; i < size - 1; i++)
            {
                int start = i;
                int end = i;
                IntPtr descPTR = GetDescription(arw[i]);
                while (timesteps[i] + 1 == timesteps[i + 1])
                {
                    i++;
                    end++;
                    if (i == size - 1)
                        break;
                }
                int desc_len = len(descPTR);
                string desc = "";
                for (int j = 0; j < desc_len; j++)
                {
                    char c = getCharByIndex(descPTR, j);
                    desc += c.ToString();
                }
                string[] vars = desc.Split('#');
                //desc = "";
                desc = vars[0] + " " + vars[1];
                this.ar.Add(new AnomalyReport(desc, timesteps[start], timesteps[end]));
               
            }
           // Console.WriteLine("done");
        }

        public void setDetectPath(string path)
        {
            this.detect_path = path;
        }

        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr CreateTimeSeries(IntPtr sw);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr CreatestringWrapper();
       
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern int len(IntPtr str);

        [DllImport("AnomalyDetectionDll.dll")]
        public static extern char getCharByIndex(IntPtr str, int x);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern char appendChar(IntPtr str, char c);
        // simple detector
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr createSimpleDetector();

        [DllImport("AnomalyDetectionDll.dll")]
        public static extern void InvokeLearnNormal(IntPtr str, IntPtr ts);

        [DllImport("AnomalyDetectionDll.dll")]
        public static extern void MyDetectAnomalies(IntPtr str, IntPtr ts);


        [DllImport("AnomalyDetectionDll.dll")]
        public static extern int GetSize(IntPtr str);
        //CFW
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr GetCFWrapper(IntPtr detector, int idx);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern int Get_CF_Size(IntPtr detector);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr GetFeatureOne(IntPtr cfw, byte[] buf);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr GetFeatureTwo(IntPtr cfw, byte[] buf);

        // ARW
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr GetARWrapper(IntPtr detector, int idx);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern int Get_AR_Size(IntPtr detector);

        [DllImport("AnomalyDetectionDll.dll")]
        public static extern int GetTimeStep(IntPtr arw);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr GetDescription(IntPtr arw);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern IntPtr createHybridDetector();
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern float GetPointX(IntPtr cfw);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern float GetPointY(IntPtr cfw);
        [DllImport("AnomalyDetectionDll.dll")]
        public static extern float GetRadius(IntPtr cfw);


        public List<AnomalyReport> getLinearList()
        {
          
            is_SimpleDetector = true;

            DetectAnomalies(1);
            return this.ar;
        }
        public List<AnomalyReport> getList()
        {
            return this.ar;
        }
        public bool isSimpleDetectorRunning()
        {
            return this.is_SimpleDetector;
        }
        public List<AnomalyReport> getCircleList()
        {
           // if (!is_SimpleDetector)
             //   return ar;
            is_SimpleDetector = false;
            
            DetectAnomalies(2);
       
            return this.ar;
        }
        public Circle[] GetCircles()
        {
            return this.circles;
        }
        public void getCorrFeatures()
        {
            if (circles == null) 
            {
                IntPtr HybDetector = createHybridDetector();
                int size = Get_CF_Size(HybDetector);
                IntPtr [] cf  = new IntPtr[size];
                this.circles = new Circle[size];
                
                for (int i = 0; i < size; i++)
                {
                    cf[i]= GetCFWrapper(HybDetector, i);

                }
                char value = (char)0;
                for(int i = 0; i < size; i++)
                {
                    circles[i].X = GetPointX(cf[i]);
                    circles[i].Y = GetPointY(cf[i]);
                    circles[i].Radius = GetRadius(cf[i]);
                    byte[] buf1 = new byte[200];
                    byte[] buf2 = new byte[200];
                    GetFeatureOne(cf[i], buf1);
                    GetFeatureTwo(cf[i], buf2);

                    int len_f1 = System.Text.Encoding.ASCII.GetString(buf1).IndexOf(value);
                    int len_f2 = System.Text.Encoding.ASCII.GetString(buf2).IndexOf(value);
                    circles[i].Feature1 = System.Text.Encoding.ASCII.GetString(buf1, 0, len_f1);
                    circles[i].Feature2 = System.Text.Encoding.ASCII.GetString(buf2, 0, len_f2);
                    
                }
            }
                
        }

        public List<AnomalyReport> GetAnomliesListFromDLL(string dll_path)
        {
            object[] param = new object[1];
            // implement interface IDetector
            List<AnomalyReport> ar = new List<AnomalyReport>();
            Assembly asm = Assembly.Load(dll_path);
            Type myDetector = asm.GetType("IDetector");
            // create detector
            MethodInfo createDetector = myDetector.GetMethod("CreateDetector");
            object detector = Activator.CreateInstance(myDetector); // IDetector
            // Execute the method.
            createDetector.Invoke(detector, null);
            // learn normal
            MethodInfo Learn = myDetector.GetMethod("LearnNormal");
            param[0] = (object)this.csv_path;
            Learn.Invoke(detector, param);
            // Detect  Method
            MethodInfo Detect = myDetector.GetMethod("Detect");
            param[0] = (object)this.detect_path;
            Detect.Invoke(detector, param);
            // Get num of anomalies
            MethodInfo getSize = myDetector.GetMethod("GetAnomlySize");
            // Get anomaly by index method
            MethodInfo GetAnomaly = myDetector.GetMethod("GetAnomlyByIndex");
           
            object size = getSize.Invoke(detector, param);
            if (size is int)
            {
                for (int i = 0; i < (int)size; i++)
                {
                    param[0] = (object)i;
                    object anomaly = GetAnomaly.Invoke(detector, param);
                    ar.Add((AnomalyReport)anomaly);
                   
                }
            }
            return ar;
        }
    }
}
