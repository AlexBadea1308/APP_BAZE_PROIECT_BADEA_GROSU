   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   ,    &HotelReservations.Windows.AddEditGuest   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  AddEditGuest��5   	   $	��450��6   $	��800��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $	��300 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��300 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   �� $��1��6   $��1��6"    �� Orientation$ Vertical=�62   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��30��6   $	��150��6$   $��Name: ��60   $��0,0,0,20q�6A   �� 5      -   5        $��NameTextBox��5   .   
���   �$��30��6A   $	��150��6M   $��Center=�6Y   $��0,0,0,15q�6{   ��5      ��  �� Path$
 Name� �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ 	LostFocus=�.��6\    =� ErrorTemplate 5      �� 5      .R�5      �� .+�5   "   �� �� 5    "   
���  ����6,   �� *[0].ErrorContent+6a   5!      5"      .��5#   0   5$      �� 5%      $��*��6   5&      �� 5'      $ 
Horizontal=�6   .+�5(      �� $��30��6   $	��150��6$   $��	Surname: ��60   $��0,0,0,20q�6D   �� 5)      -   5        $��SurnameTextBox��5)   .   
���   �$��30��6D   $	��150��6P   $��Center=�6\   $��0,0,0,15q�6~   ��5*      �� $ Surname�$
 True��$ 	LostFocus=�.��6_    5+      �� 5,      .R�5-      �� .+�5.   "   �� �� 5/   "   
���  ����6,   �� *[0].ErrorContent+6a   50      51      .��52   0   53      �� 54      $��*��6   55      �� 56      $ 
Horizontal=�6   .+�57      �� $��30��6   $	��150��6$   $��JMBG: ��60   $��0,0,0,20q�6A   �� 58      -   5        $��JMBGTextBox��58   .   
���   �$��30��6A   $	��150��6M   $��Center=�6Y   $��0,0,0,15q�6{   ��59      �� $
 JMBG�$
 True��$ 	LostFocus=�.��6\    5:      �� 5;      .R�5<      �� .+�5=   "   �� �� 5>   "   
���  ����6,   �� *[0].ErrorContent+6a   5?      5@      .��5A   0   5B      �� 5C      $��*��6   5D      5E      �� 5F   
   $��3��6   $��2��6"   $ 
Horizontal=�62   $��RightToLeft=�6K   .+�5G      �� -   5        $��
SaveButton��5G      $
��Save��6'   $��80��66   $��30��6A   $��5,0,5,0q�6M   6^   �� 5H      -   5        $��CancelButton��5H      $��Cancel��6)   $��80��6:   $��30��6E   6Q   5I      5K      5L      