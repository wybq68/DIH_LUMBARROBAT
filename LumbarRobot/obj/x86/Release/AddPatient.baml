   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity   LumbarRobot   LumbarRobot.AddPatient   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $	��700��$
��1024��  � 	Resources     	ProButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� Opacity �� CornerRadius J� BitmapEffect	 O� Opacity
 O� GlowSize O� 	GlowColor �� RelativeSource �� Path �� 
StartPoint �� EndPoint � Offset � Color ��Name �� Triggers P� Property P� Value N� EnterActions	 W� To g� Duration �� 
TargetName �� TargetProperty	 �� To g� 	BeginTime N� ExitActions��%�
  &	        �� " ��.���  � ��
 �������  � �� �� " ��.R��� $
 back��$	 0.8Q�$ 3�� O� $		 0.7Q�$
 0Q� ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)����� $	 0,0&�$ 0,1.5&���� � $	 0.4Q� ��  �� *TemplatedParent+$1 +(Button.Background).(SolidColorBrush.Color)�._��� $
 fore��$��1q�$ 3�����#333333���� $	 0,0&�$	 0,1&���� � $ #333333��$	 0.5Q�._��� ._��� $ content��$��Center=�$��Center=�#��z��$��黑体)�$��16(�
�������$��UltraExpanded&� P�  �@J�IsMouseOver .  �� .���� .��W� $ 6X�$ 0:0:0.2D�$
 back��$< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ #99cc00X�$ 0:0:0.2X�$ 0:0:0.2D�$
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)� �� .���� .��W� $ 0:0:0.2D�$
 back��$< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ 0:0:0.2D�$
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�P�  � �� .  �� .���� .��W� $ 3X�$ 0:0:0.1D�$
 back��$< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ #99cc00X�$ 0:0:0.1D�$
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)� �� .���� .��W� $ 0:0:0.1D�$
 back��$< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ 0:0:0.1D�$
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�P�  � {� .  . ���  � �� �#333333 �� .���� .��W� $ 0X�$ 0:0:0.3D�$
 back��$< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)��� $ #333333X�$ 0:0:0.3D�$
 fore��$2 ,(Border.BorderBrush).(SolidColorBrush.Color)��� $ #333333X�$ 0:0:0.3D�$
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)� �� .���� .��W� $ 0:0:0.1D�$
 back��$< 6(Border.BitmapEffect).(OuterGlowBitmapEffect.GlowSize)�W� $ 0:0:0.1D�$ content��$F @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�W� $ 0:0:0.1D�$ content��$F @(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Opacity)�W� $ 0:0:0.1D�$ content��$H B(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Direction)��� $ 0:0:0.1D�$ content��$D >(ContentPresenter.BitmapEffect).(DropShadowBitmapEffect.Color)��� $ 0:0:0.1D�$
 fore��$2 ,(Border.BorderBrush).(SolidColorBrush.Color)��� $ 0:0:0.1D�$
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�.��� .+��� 
������� �� RadiusX$ 5�� �� RadiusY$ 5��$ 0Q�  J� Effect  3  -System.Windows.Media.Effects.DropShadowEffect  !   
BlurRadius$! 10Q�"   ShadowDepth$" 0Q��� 
�������$	��Top=�$��Center=�$
��1024��$	��400��$
��1024��$��1��$��1��$ 33,33,33,33��._�� ���#ececec# � RowDefinitions# �� $��6 ��� $	��60* ��� $	��60* ��� $	��60* ��� $
��124* ��� $	��70* �$ � ColumnDefinitions$ �� $
��170* ��� $
��201* ��� $
��168* ��� $
��141* ��� $
��209* ��� $
��131* �.+��� -   $��txtName��  PorductTextBox#���  
% � Tag$% 姓名��$��51��$	��197��$��1��$��1��& �� 	MaxLength$& 8��$��Center=�$
��Left=��� -   $��cboSex��$��1��$��2��$��51��$	��132��$��	22,3,14,5q�$��16(�$��Microsoft YaHei)����#000000.@��� $	��男��$% cboMail��' �� 
IsSelected' . �� $	��女��$% 	cboFeMale��(  "System.Windows.Controls.DatePicker  -   $��birthday��  CalendarStyle(   CalendarStyle#( [   ShortDatePickerStyle#��[ $��51��$	��197��$��2��$��1��$��Center=�$
��Left=��� -   $��	txtHeight��#���  $��51��$��87��$% 身高��$��2��$��2��$��Center=�$��Center=�$��	22,6,60,6q�$�� ��$& 3���� -   $ cm�����	#FF666666$��18(�$��SimHei)�) �� IsHyphenationEnabled) . * �� Left$	* 640��$ 0.988Q�+ �� TextAlignment$
+ Left=�$
��Wrap=�$��cm��$��2��$��2��$��Bottom=�$��Right=�$��0,0,36,6q��� -   $��	txtWeight��#���  $��51��$��87��$% 体重��$��2��$��3��$��Center=�$��Center=�$��2,6,52,6q�$�� ��$& 3���� -   $ Kg�����	#FF666666$��18(�$��SimHei)�) . $	* 640��$ 0.988Q�$
+ Left=�$
��Wrap=�$��Kg��$��2��$��3��$��Bottom=�$
��Left=�$��95,0,0,6q��� -   $��cboArm��$��2��$��4��$��51��$	��166��$��2,3,41,6q�$��16(�$��Microsoft YaHei)����#000000$y�	Collapsed=�.@��� $��左臂��' . $% LeftArm���� $��右臂��$% RightArm���� $��双臂��$% BothArm���� -	   $��txtPatientCarNo��#���  $& 18��$% 	病历号��$��3��$��1��$��2��$��51��$	��311��$��Center=�$
��Left=��� -
   $��cbodiagnoseTypeId��$��3��$��3��$��2��$��51��$	��311��$��Bottom=�$
��Left=�$��0,0,0,5q�$��16(�$��Microsoft YaHei)����#000000.@��� $��	腰背痛��' . �� $��其它���� -   $��txtNode��#���  $% 备注��$��4��$��1��$��5��$	��130��$	��679��$��Center=�$
��Left=��� -   $��
btnSaveDoc��$��保  存��$��5��#��[  $	��131��$��62��$��2���� -   $��	btnCancel��$��取  消��$��5��#��[  $	��131��$��62��$��3��