   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   J FAMW_Mathematics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AMW_MathematicsAMW_Mathematics 5   	   +   %AMW_Mathematics.Windows.UnitConverter   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   /localclr-namespace:AMW_Mathematics     5   	     � Title$  Unit Converter��5	   	   $��248.598��6    $��420.775��61    � 
ResizeMode$ CanMinimize=�6B   $��2q�5
   
      AccentColorBrush(   "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5   	    � WindowStartupLocation$ CenterScreen=�68   .��5      � .+�5   
   �� -   5         ��Name$ groupBox��5      $'��!                        	 Convert��6%   $��0q�6T   $��3q�6_   .��5      � .+�5      �� -   5         ��Name$ 
selectUnit��5      $
��Left=�6/   $��170,-33,0,0q�6J   $	��Top=�6_   $	��150��6w   $��10��6�   .@�5      �� $��Length��6$   �� 5      $
��Area��6#   �� 5      $
��Mass��6#   �� 5      $��Temperature��6#   �� 5      $
��Time��6#   �� 5      $��Velocity��6#   5      �� 5      -   5         ��Name$ label��5      $��form:��6'   $
��Left=�67   $��10,0,0,0q�6R   $	��Top=�6d    J� RenderTransformOrigin$ -0.025,0.607&�6|   �� 5      -   5        $ 
label_Copy��5      $	��to:��6,   $
��Left=�6:   $��	212,0,0,0q�6U   $	��Top=�6h   $ 0.524,0.119&�6�   �� 5      -   5        $ label_Copy1��5      $��Input:��6-   $
��Left=�6>   $��	10,69,0,0q�6Y   $	��Top=�6l   �� 5      -   5        $ label_Copy2��5      $��Output:��6-   $
��Left=�6?   $��
212,64,0,0q�6Z   $	��Top=�6n   �� 5      -   5        $ 	FromCombo��5      $
��Left=�6.   $��	10,26,0,0q�6I   $	��Top=�6\   $	��173��6t   �� 5      -   5        $ ToCombo��5      $
��Left=�6,   $��
212,26,0,0q�6G   $	��Top=�6[   $	��173��6s   6   �� 5      -	   5         ��Name$ InputBox��5      $
��Left=�6,   $��23��6G   $��	10,95,0,0q�6S   	 �� TextWrapping$
	 Wrap=�6f   $	��Top=�6z   $	��173��6�   �� 5      -
   5        $ 	OutputBox��5      $
��Left=�6-   $��23��6H   $��
212,94,0,0q�6T   $
	 Wrap=�6h   $	��Top=�6|   $	��173��6�   {�.  6�   �� 5      -   5        $ equal��5      $��=��6'   $
��Left=�63   $��
189,94,0,0q�6N   $	��Top=�6b   �� 5       -   5        
 ��Name$
 Convert��5       $��	calculate��6*   $
��Left=�6>     AccentedSquareButtonStyle#��[ 6Y   $��295,135,0,0q�6�   $	��Top=�6�   $	��100��6�   $��36��6�    �� 	IsDefault . 6�   5"      5#      5%      5&      