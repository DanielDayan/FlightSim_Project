   M S B A M L   `   `   `  ����D  @FlightSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:FlightSim.controlsFlightSim.controls  5      %clr-namespace:FlightSim	FlightSim  6=   (    "FlightSim.controls.VideoController   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:FlightSim.controls     5      -	flightsimclr-namespace:FlightSim     6=   $��Stretch=�5      $��Stretch=�6=   .��5      �� $��	3,3,-3,65q�6   
�������$	��800��64   $��50��6@   .+�5
   
   �� -   5          ��Name$  ___upload_CSV_��5
      $��Upload CSV File��6)   $
��Left=�6C   $��19��6^   $	��Top=�6j   $	��112��6�   $��3��6�    �� Left$	 118��6�   
 �� Top$ 6��6�   �� 5   
   -   5        $  ___upload_XML_��5      $��Upload XML File��6)   $
��Left=�6C   $��19��6^   $	��Top=�6j   $	��112��6�   $��3��6�   $��2��6�   $	 235��6�   $ 6��6�   �� 5   
   -   5        $  Play_Button��5      $
��Play��6C   $
��Left=�6R   $��21��6m   $	��Top=�6y   $��80��6�   $��3��6�   $��1��6�   $	 366��6�   $ 6��6�   ��6'   �� *	VM_Play+6�   �� 5   
   -   5        $  Pause_button��5      $��Pause��6D   $
��Left=�6T   $��21��6o   $	��Top=�6{   $��80��6�   $��3��6�   $��1��6�   $	 462��6�   $ 6��6�   ��6'   �� *
VM_Pause+6�   �� 5   
   -   5        $  Stop_Button��5      $
��Stop��6C   $
��Left=�6R   $��21��6m   $	��Top=�6y   $��75��6�   $��3��6�   $��1��6�   $	 557��6�   $ 6��6�   ��6'   �� *	VM_Stop+6�   �� 5   
   -   5         ��Name$ Slider_��5      $
��Left=�5      $	��Top=�5      $	��614��5      $��2��6   $��18��63   $��2��6?    � Minimum$ 0Q�5       �� TickPlacement$ BottomRight=�5       �� TickFrequency$ 1Q�5       �� IsMoveToPointEnabled . $	 115��5   :   $ 32��6L   ���	#FFA09F9F � Maximum 5      �� *VM_FlightLength+	 � Value	 5      �� *
VM_Index+
 �� Mode$
 TwoWay=�5   \   �� 5   
   -   5         ��Name$ 
Play_Speed��5      $��1��6&   $
��Left=�6R   $��19��6m    �� TextWrapping$
 Wrap=�6y   $	��Top=�6�    �� TextAlignment$ Center=�6�   $��29��6�   $��2��6�    �� 	UndoLimit$ 3��6�    ^� InitialShowDelay$ 0��5       ^� ShowDuration$
 2000��5       ^� BetweenShowDelay$ 10000��5        � ToolTip$&  Enter a number and press ENTER .��5!      $��2��6D    J� RenderTransformOrigin$ 0.379,1.253&�6Q   $	 740��6u   $ 8��6�   �� 5"   
   $��
Play Speed��6   $
��Left=�6%   $	��Top=�6@   $��26��6X   $��70��6d   $��2��6o   $��3��6   $	 662��6�   $ 4��6�   �� 5$   
   -   5         ��Name$ 	Txt_block��5$      $
��Left=�6'   $��2��6B   $
��Wrap=�6O   $��00:00:00��6c   $	��Top=�6s   $ -0.441,0.725&�6�   $ 58��6�   $ 30��6�   �� 5&   
   $
��Left=�6   $��2��6/   $
��Wrap=�6<   $	��Top=�6p   $ 30��6�   $ 3��6�   ��6P   �� *VM_CurrentTime+6�   �� 5'   
   $��/��6   $
��Left=�6   $��2��67   $	��Top=�6D   $��35��6\   $��29��6h   $ 45��6s   $ 25��6�   5(      5)      