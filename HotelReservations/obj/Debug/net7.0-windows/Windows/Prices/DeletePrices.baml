   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   ,    &HotelReservations.Windows.DeletePrices   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Delete Prices��5   	   $	��800��6   $
��1000��6,    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#3498DB.��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   �� $
��Left=�6   $	��Top=�60   $��98��6H   $��332,259,0,0q�6T   $��1��6i   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��30��6   $	��345��6$   $1��+Are you sure you want to delete this price?��60   $��Center=�6f   
�������$��16(�6�   5      �� 5      $ 
Horizontal=�6   $��Center=�62   $��10,20,10,10q�6O   .+�5      �� -   5        $��CancelButton��5   -   
���   �$��Cancel��6A   $	��100��6R   $��40��6^   ���#16A085
�������$��16(�6�   $
��Bold"�6�   $	��5,0q�6�   �� 5      -   5        $��DeleteButton��5   -   
���   �$��Delete��6A   $	��100��6R   $��40��6^   $��5,0,5,0q�6j   ���#E74C3C
�������$��16(�6�   $
��Bold"�6�   5      5      5       5!      