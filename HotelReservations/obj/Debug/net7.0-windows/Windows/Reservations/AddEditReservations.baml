   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   3    -HotelReservations.Windows.AddEditReservations   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Add/Edit Reservations��5   	   $	��800��5	   	   $
��1000��6    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � ���#34495E � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $	��150 �6   �� 5      $��30 �6   .+�5   
   c� $��0��6   ���#2980B9.@�5      �� $��Rooms��6   $��Center=�6$   $��90��6F   
�������6Q   �� 5      $��
Room type:��6   $��Center=�6)   $��70��6K   
�������6V   �� 5      -   5        $��RoomTypeComboBox��5      $	��150��6/   $��30��6;   $��Center=�6G   ���#1ABC9C���#1ABC9C
���   �6�   �� 5      $��Start:��6   $��Center=�6%   $��90��6G   
�������6R   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ CheckStartDate��5      $��5,0,5,0q�61   $��Center=�6B   ���#1ABC9C
���   �6y   �� 5      $
��End:��6   $��90��6#   $��Center=�6.   
�������6P     5       -   5        $ CheckEndDate��5       $��5,0,5,0q�6/   $��Center=�6@   ���#1ABC9C
���   �6w   �� 5!      -   5        $��CheckAvailableRoomsButton��5!      $��Check available rooms��66   
�������$��5,0,5,0q�6j   $��	105,0,0,0q�6|   ���#3498DB
�������5"   R   5#      &   System.Windows.Controls.DataGrid  5&   
   -   5        $��RoomsDataGrid��5&   7      SelectionMode$ Single=�6   $��1��6*      
IsReadOnly$
 True��6L   ���#2C3E50
���   �6�   c� 5)   
   $��2��6   ���#2980B9.@�5*      �� $��Guests��6   $��90��6%   
�������60   �� 5+      -   5        $��AddGuestButton��5+      $	��Add��6+   
�������$��5,0,5,0q�6M   $��5,0,0,0q�6_   ���#3498DB
�������6�   �� 5,      -   5        $��EditGuestButton��5,      $
��Edit��6,   
�������$��5,0,5,0q�6O   $��5,0,0,0q�6a   ���#3498DB
�������6�   �� 5-      -   5        $��DeleteGuestButton��5-      $��Delete��6.   
�������$��5,0,5,0q�6S   $��5,0,0,0q�6e   ���#3498DB
�������6�   5.        51   
   -	   5          Name$ GuestsDataGrid��51   9   $ Extended=�6   $��3��6,   $
 True��6Q   ���#2C3E50
���   �   SelectionUnit$ FullRow=�6�   �� 55   
   $��5��6   	 �� Orientation$	 
Horizontal=�6"   $��RightToLeft=�6;   $��10q�6W   .+�56      �� -
   5        $��SaveBtn��56   )   
�������$
��Save��68   $��80��6G   $��30��6R   $��5,0,5,0q�6^   ���#27AE60
�������6�   �� 57      -   5        $��	CancelBtn��57   )   
�������$��Cancel��6:   $��80��6K   $��30��6V   ���#C0392B
�������6�   58      59      5:      