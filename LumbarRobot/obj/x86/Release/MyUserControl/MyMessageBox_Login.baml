   M S B A M L   `   `   `  ����   LumbarRobot2    ,LumbarRobot.MyUserControl.MyMessageBox_Login   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$
  main�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � AllowsTransparency . $	��768��$
��1366����Z�  � Title �� *Title+���� *TitleFlowDirection+ � Triggers :�  :� RoutedEvent$ Window.Loaded��.[��� .���� -    g�Name$ sbOpShow��.��W�  �� 
TargetName$
 main��	 �� TargetProperty$	 Opacity�
 W� From$
 0X�	 W� To$ 1X� g� Duration$ 0:0:0.15D� � 	Resources    ContentStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� Opacity �� CornerRadius J� BitmapEffect O� Opacity O� GlowSize O� 	GlowColor �� RelativeSource �� Path �� 
StartPoint �� EndPoint � Offset � Color ��Name  �� Triggers! P� Property" P� Value# N� EnterActions	$ �� To% g� 	BeginTime& N� ExitActions��%�
  &	        '�� )     �� " �����  � �� �� " ���� " ��.���  � ��
 �������  � �� �� " ��.R��� $
 back��$	 0.8Q�$ 3�� O� $	 0.7Q�$ 0Q� ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)����� $	 0,0&�$ 0,1.5&���� � $	 0.4Q� ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)�._��� $
 fore��$��1q�$ 3�����#333333���� $	 0,0&�$	 0,1&���� � $ #333333��$	 0.5Q�._��� ._��� $ content��$��Center=�$��Center=�#��z��$��黑体)�$��16(�
�������$��UltraExpanded&�  P� ! �@J�IsMouseOver" . # �� .���� .��W� $ 6X�$ 0:0:0.2D�$
 back��$<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $$ #99cc00X�$% 0:0:0.2X�$ 0:0:0.2D�$
 fore��$U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�& �� .���� .��W� $ 0:0:0.2D�$
 back��$<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ 0:0:0.2D�$
 fore��$U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�P� ! � ��" . # �� .���� .��W� $ 3X�$ 0:0:0.1D�$
 back��$<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $$ #99cc00X�$ 0:0:0.1D�$
 fore��$U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�& �� .���� .��W� $ 0:0:0.1D�$
 back��$<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ 0:0:0.1D�$
 fore��$U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�P� ! � {�" .  . ���  � �� �#333333# �� .���� .��W� $ 0X�$ 0:0:0.3D�$
 back��$<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $$ #333333X�$ 0:0:0.3D�$
 fore��$2	 ,(Border.BorderBrush).(SolidColorBrush.Color)��� $$ #333333X�$ 0:0:0.3D�$
 fore��$U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�& �� .���� .��W� $ 0:0:0.1D�$
 back��$<	 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�W� $ 0:0:0.1D�$ content��$F	 @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�W� $ 0:0:0.1D�$ content��$F	 @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�W� $ 0:0:0.1D�$ content��$H	 B(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Direction)��� $ 0:0:0.1D�$ content��$D	 >(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Color)��� $ 0:0:0.1D�$
 fore��$2	 ,(Border.BorderBrush).(SolidColorBrush.Color)��� $ 0:0:0.1D�$
 fore��$U	 O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�.��� ' � RowDefinitions' �� $
��135* ��� $
��215* ��� $
��233* �( � ColumnDefinitions( �� $
��162* ��� $
��640* ��� $
��117* �.+�� 
���   �$
 0.25Q�$��3��� 
���   �$
 0.25Q�$��1��$��2��$��	0,0,572,0q�� 
���   �$
 0.25Q�$��1��$��1��$��2��$��	362,0,0,0q�� 
���   �$
 0.25Q�$��2��$��3��� $��1��$��1��$��	0,0,0,227q�$��2��.+�� $��74,21,128,64q����� *ContentFlowDirection+���� ) �� ImageSource$) /images/bgMessageBox.png��' �� $��36 ��� $��50 ��� .+�� ( �� �� .+��� -   $ lblTitle��$
��Left=����	#FF656565$��16(�$��FZHTJW--GB1-0)�$��Center=�$��20,10q����� *Title+�� -   $��	btnClosed��$��1��$��Right=�$��0,0,20,0q�$y�Hidden=����� " ��.R��� $��cc��.���� $��/images/closed.png��$��30��$��30��� $��1��$��72��$	��Top=�$��2��( �� $	��160 ��� .+��� -   $ lblMsg�����	#FF333333$��24(�$��Microsoft YaHei)�$
��Wrap=�$
��Left=�$��Bottom=�$��1��$	��437��$��57��$��0,0,0,-8q����� *	Message+�� -   $��btnOk��$��确认��$��58��$	��126��* �� Left$	* 438��$��540,66,68,38q�$��2��y��� *OkVisibility+���� *	OkCommand++ �� 	IsDefault+ �� *IsOkDefault+