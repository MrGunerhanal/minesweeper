   M S B A M L   `   `   `  ����F  Bminesweeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:minesweeperminesweeper  5	   	       minesweeper.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hi:http://schemas.microsoft.com/expression/2010/interactivity     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5   	   +localclr-namespace:minesweeper     5	   	     � Title$  BG's Awesome Minesweeper��5   	   $	��750��6*   $	��800��67    � 	Resources 5       	  Smile  Cry  Cool��%�   &	        &	 A     &	 �     �� 5   
   $��.\smile.png��6   $��50��63   $��50��6?   �� 5   
   $��	.\cry.png��6   $��50��6/   $��50��6;   �� 5   
   $��
.\Cool.jpg��6   $��50��61   $��50��6=   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��40* �6   �� 5      $
��383* �6   .+�5   
   �� -   5         ��Name$ MineCounter��5      $��0��6%   $��Ceria Lebaran)�61   $��25(�6L   $��270,68,415,592.5q�6Z   $��2��6t   �� 5   
   -   5        $ 
TimerLabel��5      $��0��6$   $��Ceria Lebaran)�60   $��25(�6K   $��430,68,240,592.5q�6Y   $��2��6s   �� 5   
   $��Minesweeper��6   $
��Left=�6&   $	��Top=�6A   $��78��6Y   $��Ceria Lebaran)�6d   $��16(�6   $��
337,12,0,0q�6�   �� 5   
   -   5        $
��Play��5      $
��Left=�6   $��60��68   $��
330,68,0,0q�6D   $	��Top=�6X   $��70��6p   $��Ceria Lebaran)�6{   $��15(�6�    J� RenderTransformOrigin$ 	0.929,0.5&�6�   $��2��6�   .��5      C�  C� ResourceKey$ Smile��6   5       � 5!   
   -   5         �Name$ 	sceneGrid��5!      $	��542��6"   $��15,79.5,0,0q�6/   $��1��6D   $	��Top=�6Q   $	��715��6i   5#      5$      5%      