   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  J FAMW_Mathematics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AMW_MathematicsAMW_Mathematics ,   &AMW_Mathematics.Windows.TriangleSolver   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     /localclr-namespace:AMW_Mathematics       � Title$  AMW Mathematics��$��538.831��$	��392�� � 
ResizeMode$ CanMinimize=�$��2q�   AccentColorBrush(   "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �    � WindowStartupLocation$ CenterScreen=�.��� $��0,0,0,3q�.+�� -    �Name$ 
TriangeImg��$��10,10,10,318q�$��Stretch=��� -   $��290,470,0,0q�$��3��$��	Calculate��$	��Top=� J� Opacity$	 0.6Q�$
��Left=�$��84�� �� 	IsDefault . �� -   $��10,470,287,0q�$��3��$��Clear��$	��Top=�$	 0.6Q��� -    ��Name$ SolvingValuesBox��$P��J            Sides                                        Angles           ��$
��Left=�$	��364��$��
10,299,0,0q�$	��162��$	��Top=�.��� $��0,0,0,-2q�.+��� -    ��Name$ TypeSolving��$
��Left=�$��102,-32,0,0q�$	��Top=�$��73��.@��� $��Sides���� $��Angles��� -   $ TypeSolvingGrid��{�.  .+��� ���	#FFF4F4F5$	��125��$��	198,0,0,0q�
���   �$	��Top=�$
��Left=�$��1���� -   	 ��Name$	 ColorLabelC��$��c��$��59,8,0,0q����	#FF41B1E1
�������$��30��$	��Top=�
 J� RenderTransformOrigin$
 	0.474,0.5&�$
��Left=�$��19���� -   $	 ColorLabelB��$��b��$��	59,46,0,0q����	#FF41B1E1
�������$��30��$	��Top=�$
��Left=�$��19���� -	   $	 ColorLabelA��$��a��$��	59,85,0,0q����	#FF41B1E1
�������$��30��$	��Top=�$
��Left=�$��19���� -
    ��Name$ TValuec��$
��Left=�$��30��$��78,8,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��97���� -   $ TValuea��$
��Left=�$��30��$��	78,46,0,0q�$
 Wrap=�$	��Top=�$��97���� -   $ TValueb��$
��Left=�$��30��$��	78,85,0,0q�$
 Wrap=�$	��Top=�$��97��$
 	0.629,1.5&��� ���	#FFF4F4F5$	��125��$��36,0,0,0q�
���   �$	��Top=�$
��Left=�$��1���� -   $ AngleC��$
��Left=�$��30��$��	239,8,0,0q�$
 Wrap=�$	��Top=�$��97���� -   $ AngleB��$
��Left=�$��30��$��
239,46,0,0q�$
 Wrap=�$	��Top=�$��97��$
 0.443,1.462&��� -   $ AngleA��$
��Left=�$��30��$��
239,85,0,0q�$
 Wrap=�$��97��$	��Top=�$
 0.495,0.423&��� -   $	 ColorLabelGama��$��γ��$��	220,8,0,0q����	#FF41B1E1
�������$��30��$	��Top=�$
��Left=�$��19��$
 0.421,0.462&��� -   $	 ColorLabelBeta��$��β��$��
220,46,0,0q����	#FF41B1E1
�������$��30��$	��Top=�$
��Left=�$��19���� -   $	 ColorLabelAlfa��$��α��$��
220,85,0,0q����	#FF41B1E1
�������$
 0.491,0.474&�$��30��$	��Top=�$
��Left=�$��19���� $	��x²��$��0,64,0,0q�$��13��$	��Top=�$
��Left=�$��26����  � ToolTip$* $Square root of a number. e.g. sqr(4)��$
��Left=�$��	0,34,0,66q�$��26��$��18��$	��√��$��12(��� $��π��$��	0,5,326,0q�$��4��$	��Top=�$��Right=�$��26���� $
��xⁿ��$��0,94,0,0q�$��13��$	��Top=�$
��Left=�$��26���� -   $ groupBox_Copy��$
��Show��$
��Left=�$	��364��$��
10,168,0,0q�$	��126��$	��Top=�.���  � ColumnDefinitions �� $
��215* ��� $
��137* �.+��� -   $ SolveTriangelProperties��{�.  $
��Left=�$��103,-32,0,0q�$	��Top=�$	��166��$��2��.@��� $��Rules used to calculate���� $��Triangle Type���� $��Altitudes and area���� -    ��Name$ ShowSolverBox��$��2��