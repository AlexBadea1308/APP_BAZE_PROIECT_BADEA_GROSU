   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   /    )HotelReservations.Windows.AddEditRoomType   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Add/Edit Room Type��5   	   $	��800��6$   $
��1000��61    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#34495E.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��200 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   �� $��1��6   $��1��6"    �� Orientation$ Vertical=�62   $��2��6I   $��5,184,345,300q�6]   $��2��6t   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��30��6   $	��150��6$   $��New Room Type: ��60   
�������$��14(�6]   $
��Bold"�6k   �� 5      -   5        $��RoomTypeTextBox��5   .   
�������$��30��6E   $	��150��6Q   $��Center=�6]   
�������
���   �$��14(�5    @   $
��Bold"�6N   ��5      ��  �� Path$
 Name� �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ 	LostFocus=�.��5    `   	 =� ErrorTemplate	 5!      �� 5"      .R�5#      �� .+�5$   "   �� �� 5%   "   
���  ��$
��Bold"�6_   ��6,   �� *[0].ErrorContent+6_   5&      5'      .��5(   0   5)      �� 5*      $��*��6   
���  ��$
��Bold"�65   5+      5,      �� 5/   
   $��2��6   $��1��6"   $ 
Horizontal=�62   $��RightToLeft=�6K   $��2��6g   $��170,249,130,145q�6{   .+�50      �� -   5        $��SaveBtn��50   )   
�������$
��Save��68   $	��120��6G   $��40��6S   $��5,0,5,0q�6_   ���#2980B9
�������$��14(�51   =   $
��Bold"�6K   $��2q�6]   
 J� Effect
 52      3  -System.Windows.Media.Effects.DropShadowEffect  53         Color$ Black��6'      	Direction$	 315Q�65      ShadowDepth$ 5Q�6E      
BlurRadius$ 10Q�6U   55      �� 56      -   5        $��	CancelBtn��56   )   
�������$��Cancel��6:   $	��120��6K   $��40��6W   ���#C0392B
�������$��14(�57   =   $
��Bold"�6K   $��2q�6]   
 58        59      $ Black��6'   $	 315Q�65   $ 5Q�6E   $ 10Q�6U   5;      5<      5=      5>      