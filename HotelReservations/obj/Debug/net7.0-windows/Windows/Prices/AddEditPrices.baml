   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   -    'HotelReservations.Windows.AddEditPrices   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Add/Edit Prices��5   	   $	��800��6!   $
��1000��6.    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#34495E.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��200 �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5   
   �� $��1��6   $��1��6"    �� Orientation$ Vertical=�62   $��0,180,20,324q�6I   $��2��6_   .+�5       �� $ 
Horizontal=�6   .+�5!      �� $��30��6   $	��150��6$   $��Room type: ��60   
�������$��14(�6Y   $
��Bold"�6g   �� 5"      -   5        $��
RoomTypeCB��5"      $	��150��6-   $��40��69   $��Center=�6E   
���   ����#2C3E50
���   �$��14(�6�   $
��Bold"�6�    �� SelectedItem 6{   �� *
RoomType+ �� Mode$ TwoWay=�6�   5#      �� 5&      $ 
Horizontal=�6   .+�5'      �� $��30��6   $	��150��6$   $��Reservation type: ��60   
�������$��14(�6`   $
��Bold"�6n   �� 5(      -   5        $��ReservationTypeCB��5(      $	��150��64   $��40��6@   $��Center=�6L   
���   ����#2C3E50
���   �$��14(�6�   $
��Bold"�6�    6�   �� *ReservationType+6�   5)      �� 5,      $ 
Horizontal=�6   .+�5-      �� $��30��6   $	��150��6$   $��Price: ��60   
�������$��14(�6U   $
��Bold"�6c   �� 5.      -   5        $��PriceValueTextBox��5.      $��40��63   $	��150��6?   $��Center=�6K   
�������$��0,0,0,15q�6�   
�������
���   �$��14(�50   @   $
��Bold"�6N   ��5/      ��  �� Path$ 
PriceValue�	 �� ValidatesOnExceptions$
	 True��
 �� UpdateSourceTrigger$
 	LostFocus=�.��50   `    =� ErrorTemplate 51      �� 52      .R�53      �� .+�54   "   �� �� 55   "   
���  ��$
��Bold"�6_   ��6,   �� *[0].ErrorContent+6_   56      57      .��58   0   59      �� 5:      $��*��6   
���  ��$
��Bold"�65   5;      5<      �� 5?   
   $��2��6   $��1��6"   $ 
Horizontal=�62   $��RightToLeft=�6K   $��230,319,90,135q�6g   $��2��6   .+�5@      �� -   5        $��SaveBtn��5@   )   
�������$
��Save��68   $	��120��6G   $��40��6S   $��5,0,5,0q�6_   ���#2980B9
�������$��14(�5A   =   $
��Bold"�6K   $��2q�6]    J� Effect 5B      3  -System.Windows.Media.Effects.DropShadowEffect  5C         Color$ Black��6'      	Direction$	 315Q�65      ShadowDepth$ 5Q�6E      
BlurRadius$ 10Q�6U   5E      �� 5F      -   5        $��	CancelBtn��5F   )   
�������$��Cancel��6:   $	��120��6K   $��40��6W   ���#C0392B
�������$��14(�5G   =   $
��Bold"�6K   $��2q�6]    5H        5I      $ Black��6'   $	 315Q�65   $ 5Q�6E   $ 10Q�6U   5K      5L      5M      5N      