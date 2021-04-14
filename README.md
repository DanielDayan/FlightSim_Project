# FlightSimulator



# FlightSim project
## 







## Features

- Fly with FlightGear simulator throught the world .
- Pause , Resume , Change video speed and more via VideoController bar.
- See online updated data within the flight via graphs.
- Learn about correlated features and detect anomalies within the flight.
- Jump to the points of the time of the anomaly and study them.







## Installation

Flight simulator app for dekstop , which connected to FlightGear server
In order to run the project please do the following steps first :

 1) Download Flight Gear simulator from  https://www.flightgear.org/download/
 2) after downloaded , run FG simulator
 3) go to Setting - > Additionl Settings and copy : 
 > --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
 4) press Fly!
 5) download source code from github
 6) run FlightSim.exe 
 path : FlightSim\bin\Debug\FlightSim.exe
 8) upload CSV file  , and XML file via button (see examples for csv and xml)
 9) Press Play in order to FLY !
 
 NOTICE : we assumed CSV files comes with first line name of the features .

for running example you can use follwing files provided insise main branch git: 

* anomaly_flight.csv - upload from inside the program
* playback_small.xml - for xml file
* test.csv - for use of Learn method in the detector(dont have to uplooad this)

## Plugins

Flight is currently extended with the following plugins.
In order to load your own written plugins,  implement IDetector.cs interface 
upload your own dll at runtime via "Upload you own DLL detector" option.
for more information look source code .


| Plugin | README |
| ------ | ------ |
| AnomalyDetectionDll |https://github.com/DanielDayan/FlightSimulator_Project/FlightSim/Plugins/README.md |
| OxyPlot | https://github.com/oxyplot/oxyplot/blob/develop/README.md


## Development

Want to write your own DLL detector ? 

Implement IDetector interface that has the method : 

First Tab:

```sh
LearnNormal(string csv_path)
Detect(string csv_path)
GetAnomlySize(); 
AnomalyReport GetAnomalyByIndex(int idx)
```
Store your anomlies detected with a class / struct called AnomalyReport that has fields :
```sh
Second Tab:
Desc
Start
End
```

# UML Diagram 

![alt text](https://github.com/DanielDayan/FlightSimulator/blob/66573838ca54c620fc323d2e0addd80421d36695/UML.png)

# Video 



that all for now see you in next version update !

* Current Version 1.0 








