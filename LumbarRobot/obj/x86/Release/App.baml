   M S B A M L   `   `   `  ����   LumbarRobot    LumbarRobot.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       �� 	Resources   �� MergedDictionaries �� Source   
NormalPage �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  
TitleStyle  CalendarStyle&   System.Windows.Controls.Calendar  CalendarItemStyle1  CalendarDayButton1  CalendarButton1 �� EndPoint	 �� 
StartPoint
 � Color � Offset �Name5  /System.Windows.Controls.Primitives.CalendarItem  Name   CalendarItemStyle  ShortDatePickerStyle(  "System.Windows.Controls.DatePicker �� 	Resources:  4System.Windows.Controls.Primitives.DatePickerTextBox  CtrlTextBackground ��Name �� Triggers P� Property P� Value  CtrlDisabledBackground �� Padding'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName � 	Resources 	 Key �� Color   Transitions%  System.Windows.VisualTransition   GeneratedDuration	    To	! �� To" g� 	BeginTime# �� KeyTime$ �� Value% O� KeyTime& O� Value' � ColumnDefinitions( � RowDefinitions) ��Name* J� Opacity+ J� RenderTransformOrigin, �� Stretch- ��Name 
 DropDownButtonTemplate.  Name/ J� IsHitTestVisible0 �� RadiusY1 �� RadiusX2 �Name3 � AllowsTransparency4 � 	StaysOpen5 � PlacementTarget6 �� ElementName:	  4System.Windows.Controls.Primitives.CalendarDayButton7 :� RoutedEvent7
  1System.Windows.Controls.Primitives.CalendarButton8 � 	Resources9 � Cursor: F�Name; ��Name  PreviousButtonTemplate  HeaderButtonTemplate  NextButtonTemplate< ��Name  DisabledColor= �� Triggers> �� 
TargetName? �� Value@ �� BindingA �� RelativeSourceB �� AncestorType  CalendarControlTemplate1C �� Bullet��  �� $" Resources\MenuItemStyle.xaml?��� $( "Resources\DocumentViewerStyle.xaml?��� $ Resources\ComboBox.xaml?��� $ Resources\TextBox.xaml?��� $! Resources\ListBoxStyle.xaml?�%N/  &	        &	 �     &	 �    &	 �    7 
 &	     &	 V    &	 �    7  7  7  7  7  &	 �(    '�� �(    '	� �+    �� " N�.���  �@N�FontSize$ 20(���  �@N�
Foreground �	#FF3E3A39��  �@N�
Background
 ���� ��  �@N�
FontFamily$ Microsoft YaHei)���  �@N�Template �� " N�.R��� $ 6��$��0q�
������ ._��� .u��� �� " ��.���  � �� �	#FF898989��  � ��$ 25(���  � ��$ Microsoft YaHei)���  � ��$
 Left=���  � ��$	 Top=���  � ��$ 	80,24,0,0q��� "  .���  �@ CalendarItemStyle# �  ��  �@ CalendarDayButtonStyle# �  ��  �@ CalendarButtonStyle# �  ��  � �� �	#FF333333��  � ��$ 25(���  � �� �� $ 0.5,1&�$	 0.5,0&�.��� $
 	#FFE4EAF0��$ 0Q�� $
 	#FFECF0F4��$
 0.16Q�� $
 	#FFFCFCFD��$
 0.16Q�� $
 	#FFFFFFFF��$ 1Q���  � �� �� $ 0.5,1&�$	 0.5,0&�.��� $
 #ececec��$	 0.1Q�� $
 #c9c8c8��$ 0.375Q�� $
 #c9c8c8��$ 0.375Q�� $
 #ececec��$	 0.1Q���  � ��$ 2q���  � �� �� "  .R�� $ 	PART_Root��$��Center=�$	��286��$	��286��.+�  $ PART_CalendarItem��#��z��#��z��#��z��#��z $	��286��$	��270��$	��Top=�$��Center=��� "   ���� '        "  .���  � ��# �  ��  � ��$ 0q���  �@��
IsReadOnly . ��  � �� �� 
dptext��"  .R�� .+�� $ WatermarkContent��#��z��#��z��.+��� $ PART_ContentHost��$��0q�#��z��#��z�� P�  � {� .  . ���  � ��# �  .���  � ��$ 16(���  � ��$ Center=���  � ��$ 0,0,0,0q���  � ��$ 40����  � ��$	 150����  � ��$	 198����  � ��$ Center=���  � �� �#434343��  �@ IsTodayHighlighted . ��  �@ SelectedDateFormat$ Short=���  � �� �#FFFFFF��  � ��$ 2q���  � �� �� $
 .5,0&�$
	 .5,1&�.��� $
 #c9c8c8��$	 0.5Q�� $
 #ececec��$	 0.1Q���  � ��$ 2,2,0,0q���  � ��$ Stretch=���  � �� �� "  .R��� $	��145��$��30��#��z��#��z��#��z��# z��   CommonStates��.   
Normal��  Disabled��. �� .��W� $ 0D�$ 1X�$ Opacity�$ PART_DisabledVisual��._�� $ 	PART_Root��#��z��#��z�� ���� DisabledBrush	 $ 	#A5FFFFFF���� DropDownButtonTemplate	 " ��.R��    CommonStates��   $ 0D�  $ 0:0:0.1D�$  	MouseOver��  $ 0:0:0.1D�$  Pressed��.   
Normal��  	MouseOver��. �� .���� $ 0D�$! 	#FF448DCAX�$1 +(Border.Background).(SolidColorBrush.Color)�$ 
Background���� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[3].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#7FFFFFFF���� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#CCFFFFFF���� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#F2FFFFFF��  Pressed��. �� .���� $" 0X�$ 00:00:00.001D�$1 +(Border.Background).(SolidColorBrush.Color)�$ 
Background��.g��� $# 0��$$ 	#FF448DCA��U� $" 0X�$ 00:00:00.001D�$ (UIElement.Opacity)�$ 	Highlight��.\��� $% 0��$& 1Q��� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#EAFFFFFF���� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#C6FFFFFF���� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[3].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#6BFFFFFF���� $" 0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�$ BackgroundGradient��.g��� $# 0��$$ 	#F4FFFFFF��  Disabled��.+�� ���	#11FFFFFF$��LeftToRight=�$��Center=�$��35��$��0q�$��Center=�$��32��' �� $	��20* ��� $	��20* ��� $	��20* ��� $	��20* �( �� $	��23* ��� $	��19* ��� $	��19* ��� $	��19* �.+��� $) 	Highlight��$y�	Collapsed=����	#FF45D6FA$��2q�$��4��$ 0,0,1,1��$��-1q�$* 0Q�$��0��$��4���� $) 
Background��$y�	Collapsed=����	#FFFFFFFF$��2q����	#FF1F3B53$��4��$ .5��$��0,-1,0,0q�$* 1Q�$��1��$��3���� $) BackgroundGradient��$y�	Collapsed=����	#BF000000$��2q�$��4��$ .5��$��0,-1,0,0q�$* 1Q�$��1��$��3������ $
 .7,1&�$
	 .7,0&�.��� $
 	#FFFFFFFF��$ 0Q�� $
 	#F9FFFFFF��$ 0.375Q�� $
 	#E5FFFFFF��$ 0.625Q�� $
 	#C6FFFFFF��$ 1Q�F� $y�	Collapsed=�$��4��$��0������p�[� 4ZX�k� @Q�F� 4ZX�k� @џF� {2� �u� {2� �u�  )� �*� &"@pg� �*� &"@� � ��*�"@��� yh���#@h/��@�#@,� j����"@Sn#� l�H@�/"@��� ����k�!@�[� 4ZX�k� @p�� 4ZX�k� @Q�L4ZX�k� @Q�L�:��"@QH � �:��"@�H � ��� �U+� L�� ����E0@�=� ��� �=� ��	�=� �0ݒ� 5N=� ����� �� �b� �� ��� Ӧ� ��� �u"9��Up0@�� "9��Up0@S�n� "9��Up0@�$� Ӹ� ��� H~� �� {�� �\�� �� ��� [�� �� [�� �y�[�� ���I� ��� Ӭ�J"� sc,� ~T�~O0@c,� Ӷ�� c,� @�� �P� � � /�� ��� �� 	���	#FF2F2F2F$��Center=�$��4,3,4,3q�$��1��$��3��$+ 0.5,0.5&�$
, Fill=�$��Center=�A� $y�	Collapsed=�$��4�����	#FFFFFFFF$��Center=�$��3��$��0��$��Center=�$��3���� $) DisabledVisual��$y�	Collapsed=����	#B2FFFFFF$��1q�$��4��$ 	0,0,.5,.5��$* 0Q�$��0��$��4���� $��/images/datepickerpic.png��$��4��$��4��$��33��$��35��' �� �� $
��Auto �$��39��.+��� $- PART_Button��$��1��#��z��|�.  $
��Left=�$��3,0,3,0q�$��0��8��  $	��Top=�$��33��$��35��  $. PART_TextBox��$y�Visible=�$��0q����#909090���#909090$��0��#��z��|�.  $��Stretch=�$��0��$��Center=�$��Stretch=�� $ PART_DisabledVisual��$��2��$��0��/ .  $* 0Q�$��0��' �� �� $
��Auto �$��42��.+��� $y�	Collapsed=�$��0�����	#A5FFFFFF$0 1��$1 1��$��0��$��Stretch=��� $y�	Collapsed=�$��1�����	#A5FFFFFF$��35��$��0,0,0,0q�$0 1��$��0��$1 1��$��35��� $2 
PART_Popup��3 . $��Bottom=�4 .  5 �� $6 PART_TextBox���� " 	  :� $7 CalendarDayButton.Click��.���  � ��$ 16(��� " 
  :� $7 CalendarButton.Click��.���  � ��$ 16(���  � �� �#8dd44a��  � �� �#dedede�� "  .���  � ��$ 16(���  � ��$ 0,3,0,3q���  � �� �� "  8 ���� (��       **(CalendarItem.DayTitleTemplateResourceKey+).R��� ���	#FF333333$
��Bold"�$��16(�$��Microsoft YaHei)�$��Center=�$��0,6,0,6q�$��Center=����� .R�� $ 	PART_Root�� ���� DisabledColor	 $ 	#A5FFFFFF��   CommonStates��.   
Normal��  Disabled��. �� .��W� $ 0D�$ 1X�$ Opacity�$ PART_DisabledVisual��.+��� #��z��#��z��#��z��$ 1��$��Center=�$��Center=�$	��256��$	��286��._��� ���	#FFFFFFFF$��2q�$ 1��$��Center=�$��Center=�$	��256��$	��286��._�� $��Center=�$	��Top=�$	��266��$	��286�� ���� PreviousButtonTemplate	 " ��.R�� $
9 Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$! 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��. �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��.+��� ���	#11E5EBF1$* 1Q�$
, Fill=�� .+�F� $
: path��"����0u����0uh�W��<�h�	���	#FF333333$
��Left=�$��10��$��	14,-6,0,0q�$
, Fill=�$��Center=�$��6���� NextButtonTemplate	 " ��.R�� $
9 Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$! 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��. �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��.+��� ���	#11E5EBF1$* 1Q�$
, Fill=�� .+�F� $
: path��"����xT��!��xT���S�� 	���	#FF333333$��Right=�$��10��$��	0,-6,14,0q�$
, Fill=�$��Center=�$��6���� HeaderButtonTemplate	 " ��.R�� $
9 Hand��   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ 0D�$! 	#FF73A9D8X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ buttonContent��  Disabled��. �� .��W� $ 0D�$ .5X�$ Opacity�$ buttonContent��.+��� $; buttonContent��#��z��#��z�����	#FF333333#��z��$��1,4,1,9q�#��z��' �� $
��Auto �$��90���� $
��Auto �$	��100���� $
��Auto �$	��112��( �� $��42 ��� $��* �.+��� $- PART_PreviousButton��$��0��|�.  $
��Left=�$��20��$��0��8��  $��28��$��Center=�$��0,12,0,0q��� $- PART_HeaderButton��$��1��$
��Bold"�|�.  $��15(�$��Center=�$��0��8�� $��Center=�$��0,10,0,0q��� $- PART_NextButton��$��2��|�.  $��Right=�$��20��$��0��8�� $��28��$��	0,12,10,0q�� $ PART_MonthView��$
��Auto��$	��286��$��3��$��Center=�$��-8,3,6,6q�$��1��$y�Visible=�' �� $
��Auto �$��40���� $
��Auto �$��40���� $
��Auto �$��40���� $
��Auto �$��40���� $
��Auto �$��40���� $
��Auto �$��40���� $
��Auto �$��40��( �� $
��Auto �$��30���� $
��Auto �$��30���� $
��Auto �$��30���� $
��Auto �$��30���� $
��Auto �$��30���� $
��Auto �$��30���� $
��Auto �$��30��� $ PART_YearView��$��3��$��Center=�$��-8,3,7,6q�$��1��$y�Hidden=�' �� $
��Auto �$��70���� $
��Auto �$��70���� $
��Auto �$��70���� $
��Auto �$��70��( �� $
��Auto �$��70���� $
��Auto �$��70���� $
��Auto �$��70���� $< PART_DisabledVisual��8�� $* 0Q�$0 2��$1 2��$
, Fill=�8�� $��1��$y�	Collapsed=�= P�  � {� .  . ��� $> PART_DisabledVisual�� � y�$ Visible=��� $
? Year��@ �� *DisplayMode+A �� *FindAncestor+"B  .a��� $> PART_MonthView�� � y�$ Hidden=��� $> PART_YearView�� � y�$ Visible=��� $? Decade��@ �� *DisplayMode+A �� *FindAncestor+"B  .a��� $> PART_MonthView�� � y�$ Hidden=��� $> PART_YearView�� � y�$ Visible=��� "  .R�  $ PART_CalendarItem��#��z��#��z��#��z��#��z �� ProCheckBox��" ��.���  �@�OverridesDefaultStyle . ��  �@J�SnapsToDevicePixels . ��  � �� �	#FFA1A1A1��  � ��$ 18(���  � ��$ Microsoft YaHei)���  � �� �� " ��.R��� $��LeftToRight=�$��Center=�C �� $) bd��$��2q����#808080$��24��$��24��$��Center=����� $		 0,0&�$	 1,1&�.��� $
 White��$ 1Q�._�F� $: cp��$��
-7,-9,-8,0q�$��27��$��29�����#669900._��� $	��8,2q�= P�  �@d�	IsChecked$
 TrueY�. ��� $> cp�� � ��� ���� H�� Ӏ� �� ��' @h� d;�O�@�<� �)S �� ��` 0�� h�k ��� ��v �*� ffffff @F����x0@؈� P��x�� 9z (D�h� @�h0 �@��h�B (uhG� ��� p^��@�� �j�X:c 8���� H�� �� ProRadioButton��" 	�.���  �@�OverridesDefaultStyle . ��  �@J�SnapsToDevicePixels . ��  � �� �	#FFA1A1A1��  � ��$ 18(���  � ��$ Microsoft YaHei)���  � �� �� " 	�.R��� $��LeftToRight=�$��Center=�C �� $) bd��$��2q����#808080$��24��$��24��$��Center=����� $		 0,0&�$	 1,1&�.��� $
 White��$ 1Q�._�F� $: cp��$��
-7,-9,-8,0q�$��27��$��29�����#669900._��� $	��8,2q�= P�  �@d�	IsChecked$
 TrueY�. ��� $> cp�� � ��� ���� H�� Ӏ� �� ��' @h� d;�O�@�<� �)S �� ��` 0�� h�k ��� ��v �*� ffffff @F����x0@؈� P��x�� 9z (D�h� @�h0 �@��h�B (uhG� ��� p^��@�� �j�X:c 8���� H�� 