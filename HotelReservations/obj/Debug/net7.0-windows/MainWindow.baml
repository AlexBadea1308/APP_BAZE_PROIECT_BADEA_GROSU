   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:HotelReservations.ViewModelsHotelReservations.ViewModels  5   	   (    "HotelReservations.Views.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9vm*clr-namespace:HotelReservations.ViewModels     5   	     � Title$  Topolos Hotel App��5   	   $	��800��6#   $
��1000��60    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   ��5	      �� 5
   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 0,1&�6/   .��5      �  � Color$ #B3E5FC��6    � Offset$	 0.0Q�6+   � 5      $ #4FC3F7��6   $	 1.0Q�6+   5      .��5      � ��5   
   �� 5      $ 0.5,1&�6"   $ 0.5,0&�63   .��5      � $ Black��6   � 5      $ 	#FF1BCCD0��6   $ 1Q�61   5      .+�5   
   � $��Center=�6:   $��Center=�6U   y�6   �� *LoginGridVisibility+.+�5      �� 
�����@� �� CornerRadius$ 15��6,    �� Padding$ 80q�6>   $��
0,-85,12,0q�6K   ._�5      �� .+�5      �� $��/Images/logo.png��6   $	��154��66   $	��251��6C   $
��Fill=�6O   �� 5      $��Username��6    
���   �$��14(�6C   $��0,0,0,5q�6Q   �� 5      $��40��5      $��16(�6*   $��0,0,0,10q�68   $��5q�6J   ���#F44336$��1q�5    4   
���������5      �� *
Username+	 �� UpdateSourceTrigger$	 PropertyChanged=�.��5    [   
 J� InputBindings
 5!      �� 5"      
 �� Key$ Return��6)    �� Command 66   �� *LoginCommand+.��5#   1   5$      �� 5%      $��Password��6    
���   �$��14(�6C   $��0,0,0,5q�6Q   G� 5&      -   5        $��PasswordBox��5&   "   $��40��65   $��16(�6A   $��0,0,0,20q�6O   $��5q�5'   !   ���#F44336$��1q�6C   �� 5(      $��Login��6   ���#F44336
�������$��16(�5)   E   $
��Bold"�5*      $��40��6/   $	��100��6;   $��Center=�5+      
������ ��5(   -   �� *LoginCommand+5+   :   5,      5-      5.      � 51   
   y�6   �� *DashboardGridVisibility+.+�52      ]� .+�54      �� $
��Left=�6   $	��200��63   ���#37474F.+�55      �� $��/Images/logoMeniu.png��6   $��Uniform=�6;   $	��100��6M   $��	0,10,0,10q�56      $��Center=�6/   �� 57      $��Rooms��6   
������ 
�������$��5q�5:      
���   ���57   -   �� *OpenRoomsCommand+y�58      �� *RoomsMenuVisibility+5:   (   �� 5;      $��
Room Types��6   
������ 
�������$��5q�5>      
���   ���5;   2   �� *OpenRoomTypesCommand+y�5<      �� *RoomTypeMenuVisibility+5>   (   �� 5?      $��Users��6   
������ 
�������$��5q�5B      
���   ���5?   -   �� *OpenUsersCommand+y�5@      �� *UsersMenuVisibility+5B   (   �� 5C      $��Prices��6   
������ 
�������$��5q�5F      
���   ���5C   .   �� *OpenPricesCommand+y�5D      �� *PricesMenuVisibility+5F   (   �� 5G      $��Reservations��6   
������ 
�������$��5q�5J      
���   ���5G   4   �� *OpenReservationsCommand+y�5H      �� *ReservationsMenuVisibility+5J   (   �� 5K      $��Guest��6   
������ 
�������$��5q�5N      
���   ���5K   -   �� *OpenGuestsCommand+y�5L      �� *GuestsMenuVisibility+5N   (   �� 5O      $��Logout��6   
������ 
�������$��5q�5Q      
���   ���5O   .   �� *LogoutCommand+5Q   (   5R      � 5U      ��5V      �� 5W      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5X      � $ Black��6+   � 5Y      $ 	#FF1BCCD0��6+   $ 1Q�6=   5Z      .+�5\      �� $��Center=�6!   $��Center=�6>   .+�5]      �� 
�������$��1q�64   $ 10��5^   !   $ 5q�63   
���   �._�5_      �� $	��600��5`   $   $	��800��61   $��Uniform=�6=   ��5_   $   �� *CurrentImagePath+5`   =   5a      �� 5b       �� Orientation$ 
Horizontal=�6%   $��Center=�5c   %   $��10q�6B   .+�5d      �� $	��◀��6%   $��40��5e   %   $��40��60   $��18(�6<   ��5d   8   �� *PreviousImageCommand+5e   <   �� 5f      $	��▶��6%   $��40��5g   %   $��40��60   $��18(�6<   ��5f   8   �� *NextImageCommand+5g   <   5h      5i      5j      5k      5l      5m      5n      