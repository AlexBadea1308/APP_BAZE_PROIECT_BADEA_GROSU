   M S B A M L   `   `   `  ����  HotelReservations"    HotelReservations.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Topolos Hotel App��5   	   $	��800��6#   $
��1000��60    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 0,1&�6/   .��5	      �  � Color$ #B3E5FC��6    � Offset$	 0.0Q�6+   � 5
      $ #4FC3F7��6   $	 1.0Q�6+   5      .��5      � ��5   
   �� 5      $ 0.5,1&�6"   $ 0.5,0&�63   .��5      � $ Black��6   � 5      $ 	#FF1BCCD0��6   $ 1Q�61   5      .+�5   
   � -   5        $��	LoginGrid��5      $��Center=�6    $��Center=�6;   .+�5      �� 
�����@� �� CornerRadius$ 15��6,    �� Padding$ 80q�6>   $��
0,-85,12,0q�6K   ._�5      �� .+�5      �� -   5        	 ��Name$
	 Logo��5      $��/Images/logo.png��6*   $	��154��6D   $	��251��6Q   $
��Fill=�6]   �� 5      $��Username��6    
���   �$��14(�6C   $��0,0,0,5q�6Q   �� 5      -   5        
 ��Name$
 UsernameTextBox��5      $��40��67   $��16(�6C   $��0,0,0,10q�6Q   $��5q�6c   ���#F44336$��1q�6�   
�������6�   �� 5      $��Password��6    
���   �$��14(�6C   $��0,0,0,5q�6Q   G� 5      -   5         G�Name$ PasswordBox��5   "   $��40��67   $��16(�6C   $��0,0,0,20q�6Q   $��5q�6c   ���#F44336$��1q�6�   �� 5      -   5        $��Login��5      ���#F44336
�������$��16(�6o   $
��Bold"�6}   $��40��6�   $	��100��6�   $��Center=�6�   
������ 6�   5       5!      5"      � 5%   
   -   5        $��DashboardGrid��5%      $y�Hidden=�6$   .+�5&      ]� .+�5(      �� $
��Left=�6   $	��200��63   ���#37474F.+�5)      �� $��/Images/logoMeniu.png��6   $��Uniform=�6;   $	��100��6M   $��	0,10,0,10q�6Z   $��Center=�6m   �� 5*      -   5        $��RoomsMenuItem��5*   -   $��Rooms��6   
������ 
�������$��5q�6�   
���   �6�   �� 5+      -   5        $��RoomTypeMenuItem��5+   2   $��
Room Types��6   
������ 
�������$��5q�6�   
���   �6�   �� 5,      -	   5        $��UsersMenuItem��5,   -   $��Users��6   
������ 
�������$��5q�6�   
���   �6�   �� 5-      -
   5        $��PricesMenuItem��5-   .   $��Prices��6   
������ 
�������$��5q�6�   
���   �6�   �� 5.      -   5        $��ReservationsMenuItem��5.   4   $��Reservations��6   
������ 
�������$��5q�6�   
���   �6�   �� 5/      -   5        $��
GuestsItem��5/   -   $��Guest��6   
������ 
�������$��5q�6�   
���   �6�   �� 50      -   5        $��LogoutMenuItem��50   .   $��Logout��6   
������ 
�������$��5q�6�   
���   �6�   51      � 54      ��55      �� 56      $ 0.5,1&�6.   $ 0.5,0&�6?   .��57      � $ Black��6+   � 58      $ 	#FF1BCCD0��6+   $ 1Q�6=   59      .+�5;      �� $��Center=�6!   $��Center=�6>   .+�5=      �� 
�������$��1q�64   $ 10��6H   $ 5q�6Z   
���   �._�5>      �� -   5        $	 CarouselImage��5>   $   $	��600��6;   $	��800��6H   $��Uniform=�6T   5?      �� 5@       �� Orientation$ 
Horizontal=�6%   $��Center=�6>   $��10q�6[   .+�5A      �� -   5        $	��◀��5A   %   $��40��6T   $��40��6_   $��18(�6k   �� 5B      -   5        $	��▶��5B   %   $��40��6P   $��40��6[   $��18(�6g   5C      5D      5E      5F      5G      5H      5I      