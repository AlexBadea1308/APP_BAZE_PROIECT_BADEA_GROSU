   M S B A M L   `   `   `  ����  HotelReservations,    &HotelReservations.Windows.Reservations   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Reservations��5   	   $	��800��6   $
��1000��6+    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6   .��5      � ���#2C3E50 � RowDefinitions 5   
   �� 5      $��30 �6   �� 5	      $��30 �6   �� 5
      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   ���#34495E.+�5      �� $��Room number:��6   $��Center=�6+   $��90��6M   
�������6X   �� 5      -   5        $��RoomNumberSearchTextBox��5      $��24��65   $	��100��6A   $��3q�6M   $��Center=�6X   
����������#1C28335   >   �� 5      $��Start date:��6   $��Center=�6*   $��70��6L   
�������6W   �� 5      -   5        $��StartDateSearchTextBox��5      $��24��64   $	��100��6@   $��3q�6L   $��Center=�6W   
����������#1C28335   >   �� 5      $��	End date:��6   $��Center=�6(   $��70��6J   
�������6U   �� 5      -   5        $��EndDateSearchTextBox��5      $��24��62   $	��100��6>   $��3q�6J   $��Center=�6U   
����������#1C28335   >   5      c� 5   
   $��1��6   ���#2980B9.@�5      �� -   5        $��AddReservationButton��5      $	��Add��61   
�������$��5,0,5,0q�6S   $��5,0,0,0q�6e   
�������5   (   �� 5      -   5        $��DeleteReservationButton��5      $��Delete��64   
�������$��5,0,5,0q�6Y   $��5,0,0,0q�6k   
�������5   (   �� 5       -   5        $��FinishReservationButton��5       $��FINISH RESERVATION��64   
��� � �
�������
�������$��5,0,5,0q�5!      $��5,0,0,0q�6'   68   5"      &   System.Windows.Controls.DataGrid  5%   
   -   5        $��ReservationsDataGrid��5%   7      SelectionMode$ Single=�6   $��2��6*      
IsReadOnly$
 True��6S   ���#1C2833
���   ����#34495E$��10q�5&   r   ��6I   �� *ReservationsList+5'      5)      5*      