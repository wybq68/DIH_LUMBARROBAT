   M S B A M L   `   `   `  ����  LumbarRobot2    ,LumbarRobot.MyUserControl.MyMessageBox_Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$
  main��5        � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . $	��768��5   P   $
��1366��6]   ��5      Z�  � Title 5   
   �� *Title+��6#   �� *TitleFlowDirection+ � Triggers 5      :� 5	   
    :� RoutedEvent$ Window.Loaded��6   .[�5
      �� .��5      �� -   5         g�Name$ sbOpShow��5      .��5      W�  �� 
TargetName$
 main��5      	 �� TargetProperty$	 Opacity�5      
 W� From$
 0X�5      	 W� To$ 1X�6    g� Duration$ 0:0:0.15D�6%   5      5      5       � 	Resources 5         ContentStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� Opacity �� CornerRadius J� BitmapEffect O� Opacity O� GlowSize O� 	GlowColor �� RelativeSource �� Path �� 
StartPoint �� EndPoint � Offset � Color ��Name  �� Triggers! P� Property" P� Value# N� EnterActions	$ �� To% g� 	BeginTime& N� ExitActions��%	  &	        '�� {     �� 5   
   " ��6&   �5      �� 5       � �� 5      �� 5      " ��6*   5      5      5       �� 5#   
   " ��6   .�5$      ��  � ��
 �����6+   �� 5&       � �� 5'      �� 5)      " ��6&   .R�5+      �� $
 back��6!   $	 0.8Q�6/   $ 3��6=    5,      O� 5-   "   $	 0.7Q�68   $ 0Q�6F    6S   ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)���5/      �� 50   "   $	 0,0&�66   $ 0,1.5&�6G   ��52   *   � � 53   .   $	 0.4Q�6�    6;   ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)�6�   54   +   56   #   ._�59      �� $
 fore��6%   $��1q�63   $ 3��6G   ���#333333��5:   "   �� 5;   &   $	 0,0&�6:   $	 0,1&�6K   ��5=   .   � � 5>   2   $ #333333��6?   $	 0.5Q�6O   5?   /   5A   '   ._�5D   "   �� ._�5E   &   �� $ content��60   $��Center=�6A   $��Center=�6^   #��z��6y   $��黑体)�6�   $��16(�6�   
�������$��UltraExpanded&�6�   5F   '   5G   #   5H      5I        5K      P� 5M      ! �@J�IsMouseOver" . # 5N   "   �� 5O   &   .��5P   *   �� .��5Q   .   W� $ 6X�6>   $ 0:0:0.2D�6E   $
 back��6X   $<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6u   �� 5R   .   $$ #99cc00X�6=   $% 0:0:0.2X�6J   $ 0:0:0.2D�6^   $
 fore��6q   $U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6�   5S   +   5T   '   & 5V   "   �� 5W   &   .��5X   *   �� .��5Y   .   W� $ 0:0:0.2D�6>   $
 back��6Q   $<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6n   �� 5Z   .   $ 0:0:0.2D�6=   $
 fore��6P   $U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6m   5[   +   5\   '   5^      P� 5`      ! � ��" . # 5a   "   �� 5b   &   .��5c   *   �� .��5d   .   W� $ 3X�6>   $ 0:0:0.1D�6E   $
 back��6X   $<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6u   �� 5e   .   $$ #99cc00X�6=   $ 0:0:0.1D�6J   $
 fore��6]   $U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6z   5f   +   5g   '   & 5i   "   �� 5j   &   .��5k   *   �� .��5l   .   W� $ 0:0:0.1D�6>   $
 back��6Q   $<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6n   �� 5m   .   $ 0:0:0.1D�6=   $
 fore��6P   $U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6m   5n   +   5o   '   5q      P� 5s      ! � {�" .  . �5t   "   ��  � �� �#3333336?   # 5u   "   �� 5v   &   .��5w   *   �� .��5x   .   W� $ 0X�6>   $ 0:0:0.3D�6E   $
 back��6X   $<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6u   �� 5y   .   $$ #333333X�6=   $ 0:0:0.3D�6J   $
 fore��6]   $2	 ,(Border.BorderBrush).(SolidColorBrush.Color)�6z   �� 5z   .   $$ #333333X�6=   $ 0:0:0.3D�6J   $
 fore��6]   $U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6z   5{   +   5|   '   & 5~   "   �� 5   &   .��5�   *   �� .��5�   .   W� $ 0:0:0.1D�6>   $
 back��6Q   $<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6n   W� 5�   .   $ 0:0:0.1D�6>   $ content��6Q   $F	 @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�6q   W� 5�   .   $ 0:0:0.1D�6>   $ content��6Q   $F	 @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�6q   W� 5�   .   $ 0:0:0.1D�6>   $ content��6Q   $H	 B(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Direction)�6q   �� 5�   .   $ 0:0:0.1D�6=   $ content��6P   $D	 >(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Color)�6p   �� 5�   .   $ 0:0:0.1D�6=   $
 fore��6P   $2	 ,(Border.BorderBrush).(SolidColorBrush.Color)�6m   �� 5�   .   $ 0:0:0.1D�6=   $
 fore��6P   $U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6m   5�   +   5�   '   5�      5�      5�      5�      5�      .��5�      � ' � RowDefinitions' 5�   
   �� 5�      $
��135* �6   �� 5�      $
��215* �6   �� 5�      $
��233* �6   ( � ColumnDefinitions( 5�   
   �� 5�      $
��162* �6   �� 5�      $
��640* �6   �� 5�      $
��117* �6   .+�5�   
   � 
���   �$
 0.25Q�6"   $��3��61   6G   � 5�   
   
���   �$
 0.25Q�6"   $��1��61   $��2��6>   $��	0,0,572,0q�6R   6g   � 5�   
   
���   �$
 0.25Q�6"   $��1��61   $��1��6>   $��2��6N   $��	362,0,0,0q�6b   6w   � 5�   
   
���   �$
 0.25Q�6"   $��2��61   $��3��6>   6T   � 5�   
   $��1��6   $��1��6   $��	0,0,0,227q�6,   $��2��6?   .+�5�      � $��74,21,128,64q�6C   ��6   �� *ContentFlowDirection+��5�      �� 5�      ) �� ImageSource$) /images/bgMessageBox.png��6!   ' 5�      �� 5�      $��36 �6$   �� 5�      $��50 �6$   �� 5�      .+�5�      � ( 5�      �� 5�      �� 5�      .+�5�      �� -   5        $ lblTitle��5�       $
��Left=�6J   ���	#FF656565$��16(�5�   4   $��FZHTJW--GB1-0)�5�      $��Center=�5�      $��20,10q�68   ��5�   3   �� *Title+5�   8   �� 5�      -   5        $��	btnClosed��5�      $��1��6.   $��Right=�6>   $��0,0,20,0q�6Z   $y�Hidden=�6l   ��5�      �� 5�      " ��6.   .R�5�   "   �� $��cc��61   .��5�   &   �� $��/images/closed.png��6,   $��30��6H   $��30��6T   5�   #   5�      5�      5�      � 5�      $��1��6   $��72��6$   $	��Top=�60   $��2��6H   ( 5�      �� 5�      $	��160 �6+   �� 5�      .+�5�      �� -   5        $ lblMsg��5�       ���	#FF333333$��24(�5�   0   $��Microsoft YaHei)�5�      $
��Wrap=�5�      $
��Left=�5�      $��Bottom=�5�      $��1��5�      $	��437��6)   $��57��65   $��0,0,0,-8q�6A   ��5�   -   �� *	Message+5�   A   5�      �� 5�      -   5        $��btnOk��5�      $��确认��6&   $��58��6�   $	��126��6�   * �� Left$	* 438��6�   $��540,66,68,38q�6�   $��2��6�   y�63   �� *OkVisibility+��6W   �� *	OkCommand++ �� 	IsDefault+ 6u   �� *IsOkDefault+6�   5�      5�      5�      5�      