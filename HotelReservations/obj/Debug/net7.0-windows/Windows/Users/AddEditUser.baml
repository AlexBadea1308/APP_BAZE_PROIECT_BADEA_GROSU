   M S B A M L   `   `   `  ����L  HHotelReservations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:HotelReservations.WindowsHotelReservations.Windows  5   	   +    %HotelReservations.Windows.AddEditUser   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:HotelReservations.Windows     5   	     � Title$  Add/Edit User��5   	   $	��800��6   $
��1000��6,    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#3498DB � 	Resources 5         ValidatableTextBox �� 
TargetType �� Property �� Value��%  &	        �� 5   
   " ��6+   .�5      ��  � ��$	 150��6&   �� 5       � ��$ 30��6'   �� 5       � ��$ Center=�69   �� 5       � ��$ 0,0,0,10q�6'   �� 5       � ��
 �   �6,   �� 5       � ��
 �����6+   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5       $��50 �6   �� 5!      $	��300 �6   �� 5"      $��* �6   �� 5#      $��90 �6   .+�5'   
   �� $��1��6   	 �� Orientation$	 Vertical=�6"   $��2��69   $��1,178,322,94q�6M   $��2��6c   $��1��6t   .+�5)      �� $	 
Horizontal=�6   $
��0,10q�62   .+�5*      �� $	��150��6   $��
User type:��6$   
�������69   �� 5+      -   5        $��
UserTypeCB��5+      $	��150��6-   $��Center=�69   
 �� SelectedItem
 6[   �� *
UserType+5,      �� 5/      $	 
Horizontal=�6   $
��0,10q�62   .+�50      �� $	��150��6   $��	Username:��6$   
�������68   �� 51      -   5        $��UsernameTextBox��51   F   #��[  6   ��6]   ��  �� Path$ Username� �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ 	LostFocus=�52      �� 55      $	 
Horizontal=�6   $
��0,10q�62   .+�56      �� $	��150��6   $��Name:��6$   
�������64   �� 57      -   5        $��NameTextBox��57   F   #��[  6   ��6Y   �� $
 Name�$
 True��$ 	LostFocus=�58      �� 5;      $	 
Horizontal=�6   $
��0,10q�62   .+�5<      �� $	��150��6   $��Surname:��6$   
�������67   �� 5=      -   5        $��SurnameTextBox��5=   F   #��[  6   ��6\   �� $ Surname�$
 True��$ 	LostFocus=�5>      �� 5A      $	 
Horizontal=�6   $
��0,10q�62   .+�5B      �� $	��150��6   $��	Password:��6$   
�������68   �� 5C      -   5        $��PasswordTextBox��5C   F   #��[  6   ��6]   �� $ Password�$
 True��$ 	LostFocus=�5D      �� 5G      $	 
Horizontal=�6   $
��0,10q�62   .+�5H      �� $	��150��6   $
��CNP:��6$   
�������63   �� 5I      -   5        $��
CNPTextBox��5I   F   #��[  6   ��6X   �� $	 CNP�$
 True��$ 	LostFocus=�5J      5K      �� 5N   
   $��3��6   $��2��6"   $	 
Horizontal=�62   $��RightToLeft=�6K   $��10q�6g   .+�5O      �� -   5        $��
SaveButton��5O      $
��Save��6'   $	��100��66   $��40��6B   ���#16A085
�������$��16(�6�   $
��Bold"�6�   �� 5P      -   5        $��CancelButton��5P      $��Cancel��6)   $	��100��6:   $��40��6F   ���#E74C3C
�������$��16(�6�   $
��Bold"�6�   $��10,0,0,0q�6�   5Q      5R      5S      