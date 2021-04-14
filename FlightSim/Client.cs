using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSim
{
    class Client
    {
        TcpClient tcpClient;
        NetworkStream nwStream;
        public  Boolean is_connected = false;
        public Client()
        {
        }

       public void connect()    
        {
            try
            {
                this.tcpClient = new TcpClient("127.0.0.1", 5400);
                this.nwStream = this.tcpClient.GetStream();
                is_connected = true;
            }
            catch
            {
                Console.WriteLine("Run FlightGear");
            }
        }
        public void disconnect()
        {
            if (nwStream != null)
                nwStream.Close();
            this.tcpClient.Close();
            is_connected = false;
        }
       
      
        public void sendLine(string line)
        {
            String CLRF = "\r\n";
            if (tcpClient == null)
            {
                MessageBox.Show("Connect to FlightGear !");
                return;
            }
            NetworkStream nwStream = this.tcpClient.GetStream();
            string send_line = String.Concat(line, CLRF);
            //Console.WriteLine(send_line);
            byte[] buffer = Encoding.ASCII.GetBytes(send_line);
            nwStream.Write(buffer, 0, buffer.Length);
            nwStream.Flush();
           
        }
    }
}
