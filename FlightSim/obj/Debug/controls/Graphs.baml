   M S B A M L   `   `   `  ����D  @FlightSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:FlightSim.controlsFlightSim.controls  5      %clr-namespace:FlightSim	FlightSim  6=       FlightSim.controls.Graphs   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +oxyhttp://oxyplot.codeplex.com     5      2local clr-namespace:FlightSim.controls     5      -	flightsimclr-namespace:FlightSim     6=   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0!oxy1http://oxyplot.org/wpf  6g   $��300.8��5      $��515.2��6.   .��5	      � .+�5
   
   � $��0,10,0.2,-0.2q�6     J� RenderTransformOrigin$  
0.728,0.64&�6&    � RowDefinitions 5       � ColumnDefinitions 5      �� 5      $	��43* �6#   �� 5      $	��60* �6#   .+�5        OxyPlot.Wpf.PlotView  -   5          Name$ Plot1��5      $��32,58,43.2,130q�6c   }�. $	��140��6�   $��102.4��6�   ��6+   Z�    Model 6B   �� *VM_GraphModel+6�     5      -   5        $ Pearson��5      $��185,59,190.2,130q�6f   }�. $��2��6�   ��6-   Z�  6C   �� *VM_PearsonGraph+6�     5      -   5        $ 	LinearReg��5      $��125.8,59,34.2,130q�6Q   $��1��6�   $	��140��6�    6/   �� *VM_PointsGraph+��6l   Z� 6�   5      �� 5#   
   -   5         ��Name$ 
Var_select��5#      $
��Left=�6�   $	��Top=�6�   $	��169��6�   $��	10,18,0,0q�6�   $��14(�6�   ��6'   �� *	VM_Select+ �� Mode$ OneWay=�6�   �� 5'   
   $
��Left=�6   $
��Wrap=�6/   $	��Top=�6C   $��8,0,0,0q�6[   $��Chose Variable ��6l   $��33��6�   $	��144��6�   �� 5(   
   $
��Left=�6   $��73��60   $��	191,0,0,0q�6<   $	��Top=�6O   $	��144��6g   .+�5)      �� $��Correlative variable:��6   $��25��64   $��4,0,-7,0q�6@   �� 5*      $
��Left=�6   $��51��63   $��	9,0,-10,0q�6?   $
��Wrap=�6R   $	��145��6�   
���  ��$��10(�6�   ��6f   �� *VM_CurrentCorrVar+6�   5+      �� 5-   
   $��Linear Regression Graph��6   $��337,3,41.2,254.8q�63   6Q   5.      5/      