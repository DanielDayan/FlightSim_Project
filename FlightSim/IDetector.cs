using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{
    public struct AnomalyReport
    {
        public AnomalyReport(string desc, int start, int end)
        {
            Desc = desc;
            Start = start;
            End = end;
        }

        public string Desc { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

    }
    interface IDetector
    {
        IDetector CreateDetector(); // return instance of IDetector
        void LearnNormal(string CSV_Learn_flight); // learn flight from learn flight csv
        void Detect(string CSV_Anomaly_flight); // Detect anomalies from anomaly flight csv
        int GetAnomlySize(); // get number of anomlioes detected
        AnomalyReport GetAnomalyByIndex(int index); // get anomaly by index
    }
}
