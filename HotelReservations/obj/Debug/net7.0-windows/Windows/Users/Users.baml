   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   %    HotelReservations.Windows.Users   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Users��5   	   $	��800��6   $
��1000��6$    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#3498DB.��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��70 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��	10,0,10,0q�6V   .+�5      �� $��	Username:��6   
�������$��16(�6;   $��0,0,10,0q�6I   $��Center=�6[   �� 5      -   5        $��UsernameSearchTextBox��5      $	��200��63   $��30��6?   $��Center=�6K   $��14(�6m   $��0,0,10,0q�5      6(   5      c� 5   
   $��1��6   ���#2980B9$��10q�64   $��Center=�6A   .@�5      �� -   5        $��	AddButton��5      $	��Add��6&   $	��100��6L   $��40��6X   $��14(�5       
����������#1ABC9C$��5,0,0,0q�6K   �� 5!      -   5        $��
EditButton��5!      $
��Edit��6'   $	��100��6O   $��40��6[   $��14(�5"      
����������#F39C12$��5,0,0,0q�6K   �� 5#      -   5        $��DeleteButton��5#      $��Delete��6)   $	��100��6U   $��40��6a   $��14(�5$      
����������#E74C3C$��5,0,0,0q�6K   5%      &   System.Windows.Controls.DataGrid  5(   
   -   5        $��UsersDataGrid��5(       $��2��6      SelectionMode$ Single=�65      
IsReadOnly$
 True��6L      AutoGenerateColumns$
 True��5)      
�������
���   �$��10q�5*   9   $��Stretch=�6E   $��Stretch=�6a   $��14(�5+      5,      5-      