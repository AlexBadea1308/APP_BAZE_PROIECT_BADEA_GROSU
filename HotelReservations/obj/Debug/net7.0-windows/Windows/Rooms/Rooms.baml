   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   %    HotelReservations.Windows.Rooms   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Rooms��5   	   $	��800��6   $
��1000��6$    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      � ���#2C3E50 � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���#34495E.+�5      �� $��Room number:��6   $��Center=�6+   
�������6M   �� 5      -   5        $��RoomNumberSearchTextBox��5      $��24��65   $	��100��6A   $��3q�6M   $��Center=�6X   
����������#1C28336�   �� 5      $��
Room type:��6   $��Center=�6)   $��70��6K   
�������6V   �� 5      -   5        $��RoomTypeSearchTextBox��5      $��24��63   $	��100��6?   $��3q�6K   $��Center=�6V   
����������#1C28336�   5      c� 5   
   $��1��6   ���#2980B9.@�5       �� -   5        $��	AddButton��5       $	��Add��6&   
�������$��5,0,5,0q�6H   $��5,0,0,0q�6Z   
�������6�   �� 5!      -   5        $��
EditButton��5!      $
��Edit��6'   
�������$��5,0,5,0q�6J   $��5,0,0,0q�6\   
�������6�   �� 5"      -   5        $��DeleteButton��5"      $��Delete��6)   
�������$��5,0,5,0q�6N   $��5,0,0,0q�6`   
�������6�   5#      &   System.Windows.Controls.DataGrid  5&   
   -   5        $��RoomsDataGrid��5&   7      SelectionMode$ Single=�6   $��2��6*      
IsReadOnly$
 True��6L   ��5'      �� 5(       �� Color$ #1C2833��6"   ��5+      �� 5,      $ Black��6"   5/      50      51      