   M S B A M L   `   `   `  ����  LumbarRobot*    $LumbarRobot.MyUserControl.SetControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          >�Name$  
setControl��5      .��5
      � $��67��6   $	��194��6    � RowDefinitions 5   
   �� 5      $��27 �6   �� 5      $��42 �6    � ColumnDefinitions 5   
   �� 5      �� 5      $��73 �6    �� 5      .+�5   
   � $��3��6   .+�5      �� -   5        $��tbTitle��5      $��Microsoft YaHei)�6b   ���	#FF656565$��16(�6�   ��6'   ��  �� ElementName$ 
setControl�� �� Path$ ContextText�6�   5      � 5   
   $��1��6   $��1��6   ��5      �� 5       j� Stretch$
 Fill=�6    �� ImageSource$ /images/txt_add.png��6,   .+�5      �� -   5        $��txtValue��5      $��黑体)�6\   $��16(�6l   ���#669900 �� IsHyphenationEnabled . $
��Bold"�6�    �� TextAlignment$ Center=�6�   $
��Wrap=�6�   $��
15,12,11,3q�6�   ��6(   �� $ 
setControl��$ Value�z�5      u� 5      	 u� Matrix$	 0.929,0,0,0.933,0,0x�6&   5      5       �� 5"   
   -   5        $��L_speed��5"      $��/images/reductionIcon.png��6   $��40��6E   $��47��6Q   
 � Cursor$

 Hand��6]   $
��Left=�6k   $��Center=�6�   
 � Tag$
 left��6  $��0,0,12,0q�6  $��1��6$  6k  �� 5$   
   -   5        $��R_speed��5$      $��/images/addIcon.png��6   $��40��6E   $��47��6Q   $

 Hand��6]   $
��Left=�6k   $��Center=�6�   $ right��6  $��12,0,0,0q�6  $��2��6$  $��1��64  6{  5&      5'      