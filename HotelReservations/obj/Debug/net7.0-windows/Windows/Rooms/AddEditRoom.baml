   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   +    %HotelReservations.Windows.AddEditRoom   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Add/Edit Room��5   	   $	��800��6   $
��1000��6,    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#34495E.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��200 �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5   
   �� $��1��6   $��1��6"    �� Orientation$ Vertical=�62   $��0,100,20,404q�6I   $��2��6_   .+�5       �� $ 
Horizontal=�6   .+�5!      �� $��30��6   $	��150��6$   $��Room Number: ��60   $��0,0,0,15q�6H   
�������$��14(�6m   $
��Bold"�6{   �� 5"      -   5        $��RoomNumberTextBox��5"   .   
�������$��40��6G   $	��150��6S   $��Center=�6_   $��0,0,0,15q�6�   
�������
���   �$��14(�5$   @   $
��Bold"�6N   ��5#      ��  �� Path$ 
RoomNumber� �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ 	LostFocus=�.��5$   `   	 =� ErrorTemplate	 5%      �� 5&      .R�5'      �� .+�5(   "   �� �� 5)   "   
���  ��$
��Bold"�6_   ��6,   �� *[0].ErrorContent+6_   5*      5+      .��5,   0   5-      �� 5.      $��*��6   
���  ��$
��Bold"�65   5/      �� 52      $ 
Horizontal=�6   .+�53      �� $��30��6   $	��150��6$   $��Has minibar?��60   
�������$��14(�6Z   $
��Bold"�6h   �� 54      -   5        $��HasMiniBarCheckBox��54   /   
�������$��Center=�6I   
�������
 d� 	IsChecked
 6d   �� $ 
HasMiniBar�6�   55      �� 58      $ 
Horizontal=�6   .+�59      �� $��30��6   $	��150��6$   $��Has TV?��60   
�������$��14(�6U   $
��Bold"�6c   �� 5:      -   5        $��HasTvCheckBox��5:   /   
�������$��Center=�6D   
�������
 6_   �� $ HasTV�6�   5;      �� 5>      $ 
Horizontal=�6   .+�5?      �� $��30��6   $	��150��6$   $��
Room type:��60   
�������$��14(�6X   $
��Bold"�6f   �� 5@      -   5        $��RoomTypeComboBox��5@      $	��150��63   $��40��6?   $��Center=�6K   
���   ����#2C3E50
���   �$��14(�6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�    �� SelectedItem 6�   �� *
RoomType+ �� Mode$ TwoWay=� J� Effect 5A      3  -System.Windows.Media.Effects.DropShadowEffect  5B         Color$ Black��6+      	Direction$	 315Q�69      ShadowDepth$ 5Q�6I      
BlurRadius$ 10Q�6Y   5D      5E      5F      �� 5I   
   $��2��6   $��1��6"   $ 
Horizontal=�62   $��RightToLeft=�6K   $��275,234,100,165q�6g   $��2��6�   .+�5J      �� -   5        $��SaveBtn��5J   )   
�������$
��Save��68   $	��120��6G   $��40��6S   $��5,0,5,0q�6_   ���#2980B9
�������$��14(�5K   =   $
��Bold"�6K   $��2q�6]    5L        5M      $ Black��6'   $	 315Q�65   $ 5Q�6E   $ 10Q�6U   5O      �� 5P      -   5        $��	CancelBtn��5P   )   
�������$��Cancel��6:   $	��120��6K   $��40��6W   ���#C0392B
�������$��14(�5Q   =   $
��Bold"�6K   $��2q�6]    5R        5S      $ Black��6'   $	 315Q�65   $ 5Q�6E   $ 10Q�6U   5U      5V      5W      5X      