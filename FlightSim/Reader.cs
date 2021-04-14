using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlightSim
{
    class Reader
    {
       
       
        public Reader(){ }

        public ArrayList ReadCSV(String Path)
        {
            ArrayList arrayData = new ArrayList();
            string[] lines = System.IO.File.ReadAllLines(Path);
            lines = lines.Skip(1).ToArray();
            foreach (string line in lines)
            {
                ArrayList Float_Line = new ArrayList();
                string[] columns = line.Split(',');
                foreach (string column in columns)
                {
                    //Console.WriteLine(column);
                    Float_Line.Add(float.Parse(column, CultureInfo.InvariantCulture.NumberFormat));
                }
                arrayData.Add(Float_Line);
                //Console.WriteLine(" ");
            }
            //Console.WriteLine("Done");
            return arrayData;
        }
        public ArrayList ReadXML(String Path)
        {
            ArrayList colNames = new ArrayList();
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);
            XmlNodeList names = doc.GetElementsByTagName("name");
            int num_of_colms = names.Count / 2;
            for (int i = 0; i < num_of_colms; i++)
            {
                colNames.Add(names[i].InnerText);
                //Console.WriteLine(names[i].InnerText);
            }
            return colNames;
           
        }
     
    }

}
