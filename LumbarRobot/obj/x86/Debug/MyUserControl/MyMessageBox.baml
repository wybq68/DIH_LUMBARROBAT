   M S B A M L   `   `   `  ����F  BLumbarRobot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:LumbarRobot.MyUserControlLumbarRobot.MyUserControl  5   	   ,    &LumbarRobot.MyUserControl.MyMessageBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9local'clr-namespace:LumbarRobot.MyUserControl     5   	   -   5          �Name$
  main��5       $	��622��5   ?   $	��977��6L    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency .  � Left$	 307��5   #   
 � Top$ 13��6.    � ShowInTaskbar .  ��5      Z�  � Title 5   	   �� *Title+��6"   �� *TitleFlowDirection+ � Triggers 5      :� 5   
   	 :� RoutedEvent$	 Window.Loaded��6   .[�5      �� .��5      �� -   5        
 g�Name$
 sbOpShow��5      .��5      W�  �� 
TargetName$
 main��5       �� TargetProperty$ Opacity�5       W� From$ 0X�5      	 W� To$ 1X�6    g� Duration$ 0:0:0.15D�6%   5      5      5       � 	Resources 5         ContentStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� Opacity �� CornerRadius J� BitmapEffect O� Opacity O� GlowSize O� 	GlowColor �� RelativeSource �� Path �� 
StartPoint �� EndPoint  � Offset! � Color" ��Name# �� Triggers$ P� Property% P� Value& N� EnterActions	' �� To( g� 	BeginTime) N� ExitActions��%	  &	        '�� {     �� 5   
   " ��6&   �5      �� 5       � �� 5      �� 5      " ��6*   5       5"      5$      �� 5'   
   " ��6   .�5(      ��  � ��
 �����6+   �� 5*       � �� 5+      �� 5-      " ��6&   .R�5/      �� $
 back��6!   $	 0.8Q�6/   $ 3��6=    50      O� 51   "   $	 0.7Q�68   $ 0Q�6F    6S   ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)���53      �� 54   "   $	 0,0&�66   $ 0,1.5&�6G   ��56   *   � � 57   .   $	  0.4Q�6�   ! 6;   ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)�6�   58   +   5:   #   ._�5=      �� $
 fore��6%   $��1q�63   $ 3��6G   ���#333333��5>   "   �� 5?   &   $	 0,0&�6:   $	 0,1&�6K   ��5A   .   � � 5B   2   $! #333333��6?   $	  0.5Q�6O   5C   /   5E   '   ._�5H   "   �� ._�5I   &   �� $" content��60   $��Center=�6A   $��Center=�6^   #��z��6y   $��黑体)�6�   $��16(�6�   
�������$��UltraExpanded&�6�   5J   '   5K   #   5L      5M      # 5O      P� 5Q      $ �@J�IsMouseOver% . & 5R   "   �� 5S   &   .��5T   *   �� .��5U   .   W� $ 6X�6>   $ 0:0:0.2D�6E   $
 back��6X   $< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6u   �� 5V   .   $' #99cc00X�6=   $( 0:0:0.2X�6J   $ 0:0:0.2D�6^   $
 fore��6q   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6�   5W   +   5X   '   ) 5Z   "   �� 5[   &   .��5\   *   �� .��5]   .   W� $ 0:0:0.2D�6>   $
 back��6Q   $< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6n   �� 5^   .   $ 0:0:0.2D�6=   $
 fore��6P   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6m   5_   +   5`   '   5b      P� 5d      $ � ��% . & 5e   "   �� 5f   &   .��5g   *   �� .��5h   .   W� $ 3X�6>   $ 0:0:0.1D�6E   $
 back��6X   $< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6u   �� 5i   .   $' #99cc00X�6=   $ 0:0:0.1D�6J   $
 fore��6]   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6z   5j   +   5k   '   ) 5m   "   �� 5n   &   .��5o   *   �� .��5p   .   W� $ 0:0:0.1D�6>   $
 back��6Q   $< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6n   �� 5q   .   $ 0:0:0.1D�6=   $
 fore��6P   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6m   5r   +   5s   '   5u      P� 5w      $ � {�% .  . �5x   "   ��  � �� �#3333336?   & 5y   "   �� 5z   &   .��5{   *   �� .��5|   .   W� $ 0X�6>   $ 0:0:0.3D�6E   $
 back��6X   $< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6u   �� 5}   .   $' #333333X�6=   $ 0:0:0.3D�6J   $
 fore��6]   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6z   �� 5~   .   $' #333333X�6=   $ 0:0:0.3D�6J   $
 fore��6]   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6z   5   +   5�   '   ) 5�   "   �� 5�   &   .��5�   *   �� .��5�   .   W� $ 0:0:0.1D�6>   $
 back��6Q   $< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�6n   W� 5�   .   $ 0:0:0.1D�6>   $ content��6Q   $F @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�6q   W� 5�   .   $ 0:0:0.1D�6>   $ content��6Q   $F @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�6q   W� 5�   .   $ 0:0:0.1D�6>   $ content��6Q   $H B(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Direction)�6q   �� 5�   .   $ 0:0:0.1D�6=   $ content��6P   $D >(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Color)�6p   �� 5�   .   $ 0:0:0.1D�6=   $
 fore��6P   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6m   �� 5�   .   $ 0:0:0.1D�6=   $
 fore��6P   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6m   5�   +   5�   '   5�      5�      5�      5�      5�      .��5�      � * � RowDefinitions* 5�   
   �� 5�      $
��135* �6   �� 5�      $
��215* �6   �� 5�      $
��233* �6   + � ColumnDefinitions+ 5�   
   �� 5�      $
��162* �6   �� 5�      $
��640* �6   �� 5�      $
��117* �6   .+�5�   
   � 
���   �$
 0.25Q�6"   $��3��61   6G   � 5�   
   
���   �$
 0.25Q�6"   $��1��61   6@   � 5�   
   
���   �$
 0.25Q�6"   $��1��61   $��1��6>   $��2��6N   $��	619,0,0,0q�6b   6w   � 5�   
   
���   �$
 0.25Q�6"   $��2��61   $��3��6>   6T   � 5�   
   $��1��6   $��1��6   $��	0,0,1,227q�6,   $��2��6?   .+�5�      � $��0,0,0,6q�6C   ��6   �� *ContentFlowDirection+��5�      �� 5�      , �� ImageSource$, /images/bgMessageBox.png��6!   * 5�      �� 5�      $��36 �6$   �� 5�      $��50 �6$   �� 5�      .+�5�      � + 5�      �� 5�      �� 5�      .+�5�      �� -   5        $" lblTitle��5�       $
��Left=�6J   ���	#FF656565$��16(�5�   4   $��FZHTJW--GB1-0)�5�      $��Center=�5�      $��20,10q�68   ��5�   3   �� *Title+5�   8   �� 5�      -   5        $��	btnClosed��5�      $��1��6.   $��Right=�6>   $��0,0,20,0q�6Z   $y�Hidden=�6l   ��5�      �� 5�      " ��6.   .R�5�   "   �� $��cc��61   .��5�   &   �� $��/images/closed.png��6,   $��30��6H   $��30��6T   5�   #   5�      5�      5�      � 5�      $��1��6   $��72��6$   $	��Top=�60   $��2��6H   + 5�      �� 5�      $	��160 �6+   �� 5�      .+�5�      �� -   5        $" lblMsg��5�       ���	#FF333333$��16(�5�   0   $��Microsoft YaHei)�5�      $
��Wrap=�5�      $
��Left=�5�      $��Bottom=�5�      $��1��5�      $	��437��6)   $��57��65   $��0,0,0,-8q�6A   ��5�   -   �� *	Message+5�   A   5�      5�      5�      � 5�   
   -   5        - �Name$- ButtonsGrid��5�      $��1��6   $��	1,106,0,3q�61   $��1��6D   + 5�      �� 5�      �� 5�      �� 5�      .+�5�      �� -   5        $��btnOk��5�      $��2��6"   $��确认��62   $��58��6�   $	��126��6�   . �� Left$	. 438��6�   $��28,29,67,19q�6�   y�6?   �� *OkVisibility+��6c   �� *	OkCommand+/ �� 	IsDefault/ 6�   �� *IsOkDefault+6�   5�      5�      5�      