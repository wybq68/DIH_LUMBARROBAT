   M S B A M L   `   `   `  ����   LumbarRobot"    LumbarRobot.FitResultControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $	��750��$
��1090��  � 	Resources     	ProButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� Opacity �� CornerRadius J� BitmapEffect	 O� Opacity
 O� GlowSize O� 	GlowColor �� RelativeSource �� Path �� 
StartPoint �� EndPoint � Offset � Color ��Name �� Triggers P� Property P� Value N� EnterActions	 W� To g� Duration �� 
TargetName �� TargetProperty	 �� To g� 	BeginTime N� ExitActions �� 	Resources �� Color  �� Opacity-  'System.Windows.Input.KeyboardNavigation!   TabNavigation  ControlLightColor" � RowDefinitions# ��Name  ControlMediumColor  ContentCenterStyle  DataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow*  $System.Windows.Controls.DataGridCell$ �� Triggers  HorizontalNormalBorderBrush  NormalBorderBrush  
GlyphBrush  ScrollBarPageButton 	 ScrollBarLineButtonTop% �� ImageSource 
 ScrollBarLineButtonBot  ScrollBarLineButtonLeft  ScrollBarLineButtonRight  ScrollBarThumb  VerticalScrollBar& ]� DecreaseRepeatButton' ]� Thumb( ]� IncreaseRepeatButton  HorizontalScrollBar) � ColumnDefinitions* ]�Name  FavsScrollViewer+ ��Name, �� Orientation- �� HorizontalOffset. � Value/ �� ScrollableWidth0 � Maximum1 �� ViewportWidth2 �� ViewportSize,3 �� %ComputedHorizontalScrollBarVisibility4 �� VerticalOffset5 �� ScrollableHeight6 �� ViewportHeight*7 �� #ComputedVerticalScrollBarVisibility��%�  &	        '�� �
    'Z� P    &	 �    &	     &	 �    &	 �    &	 �    &	 �    &		 �    7  7  &	
 �    7  7  &	     7  7  &	 H    7  7  &	 �    &	 �    7 	 7  7  7  7  7 
 &	 B    7  7  7  7  7  7  '�� �    7  7  &	 0    �� " ��.���  � ��
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
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)��� " �� ���� $ #eeeeee��$  0Q�(��       * SystemColors.HighlightBrushKey+)�� $ #eeeeee��$  0Q�(��       *SystemColors.ControlBrushKey+)�� " Z�.���  � ��# � ����  � ��# � ����  �@�FocusVisualStyle Z� ��  � �� �� " Z�.R��� #��z��#��z��|�.  ._�� $! Local=����� # �  " �� $
��Auto ��� $��* ��� $
��Auto �.+��� $# PART_ContentHost��$��1����. $
��Auto=���. ���� $ 0.5,1&�$ 0.5,0&�.��� # �  $ 0Q�� # �  $ 1Q��� " ��.���  � ��$ Center=���  � ��$ Center=��� "  .���  � ��$ 0q���  � �� �#FFFFFF��  �@ AutoGenerateColumns .  ��  �@ GridLinesVisibility$
 None=���  �@ ColumnHeaderHeight$ 33Q���  �@ CanUserDeleteRows .  ��  �@ CanUserReorderColumns .  ��  �@ CanUserResizeColumns .  ��  �@ CanUserAddRows .  ��  �@��AlternationCount$ 2����  �@ ColumnHeaderStyle �� "  .���  � �� �#333333��  � ��$ 14(���  � ��$ Microsoft YaHei)���  � �� �#cccccc��  � �� �#d6d6d6��  � ��$ 1q���  � ��$ 60����  � ��$ Center=���  � ��$ Center=���  �@ RowStyle �� "  .���  � ��$ 40����  � ��$ Center=���  � ��$ 14(���  � �� �#666666��  � �� �#FFFFFF��  � ��$ Center=���  �@��TextAlignment$ Center=���  �@ 	CellStyle �� "  .���  � �� �#d6d6d6��  � ��$ 1,0,1,2q���  � ��$ Center=���  � ��$ Center=���  �@��TextAlignment$ Center=�$ P�  �@ 
IsSelected . . ���  � �� �#669900��  �@ RowHeaderWidth$ 0Q��� $ #efefef���� $ #efefef���� $
 #444���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� ���#efefef�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��8��  $��0q����� $% /images/up.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��8��  $��0q����� $% /images/down.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��35��8��  $��0q����� $% /images/left.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��42��8��  $��0q����� $% /images/right.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� " n�.R��� ._�� .+��� ���� $ 0.5,0&�$ 0.5,1&�.��� $
 Gray��$	 0.7Q��� " ��.R�� " �� $��23���� �� $��23��.+��� $��3��$��0��$ 0��
������ �� $��0��8��  $��23��$��ScrollBar.LineUpCommand��$	��Top=�]� $��
PART_Track��$��1����. & �� $��38��8�� $��ScrollBar.PageUpCommand��' n� 8�� 8�� ( �� $��38��8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��23��$��ScrollBar.LineDownCommand��$��Bottom=��� " ��.R�� $��35��) �� $��35���� �� $��35��.+��� $��3��$ 0�����#efefef�� $��0��8��  $��23��$��35��$��ScrollBar.LineLeftCommand��]� $* 
PART_Track��$��1����.  & �� 8�� $��ScrollBar.PageLeftCommand��' n� 8�� $��0,1,0,1q�8�� ���� $% /images/bar1.png��( �� 8�� $ ��ScrollBar.PageRightCommand���� $��3��8�� $��35��$��35��$ ��ScrollBar.LineRightCommand���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . $ P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 42����  � ��8   P�  �@��Orientation$ Vertical=�. ���  � ��$ 42����  � ��$
 Auto����  � ��8  �� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R�� ) �� $
��Auto ��� " �� �� $
��Auto �.+��� $��1���� $+ PART_HorizontalScrollBar��$, 
Horizontal=�$��1��$��1��#. z- #0 z/ #2 z1 #y�z3 �� $��PART_VerticalScrollBar��$��0��$��1��#. z4 #0 z5 #2 z6 #y�z7 $��
0,0,-0.5,0q�$��Right=�.��� 
�������" �� �� $	��100 �.+�� ) �� $	��390 ��� " �� $��35 ��� .+��� $��Center=�$
��20,0q�.+��� -   $ 评测记录�����	#FF000000$��16(�$��Microsoft YaHei)�8 �� IsHyphenationEnabled8 . 9 �� TextAlignment$
9 Left=�$
��Wrap=�$��评测记录���� $��1q����#d9d9d9$��1��$
��6800��$
��Auto��$	��330��$��
10,1,10,10q�._�� $
��Left=�.+�  -   $��gvFit��$��1��$��1��#���  ����   ��%�   '��       7  �� " ��.���  � ��8   $ P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$	 100��P�  �@��Orientation$ Vertical=�. ���  � ��$ 40����  � ��$
 Auto��:   Columns: 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn;   Width5  /System.Windows.Controls.DataGridLengthConverter$; 80 <   Header< �� -   $��全选��$��18(�
���   �= d� 	IsChecked$
= TrueY�>   CellTemplate> �� .R��� -   $��18(�$��25��$��Center=�= �� *	IsChecked+? �� Mode$? TwoWay=�0	  *System.Windows.Controls.DataGridTextColumn	  1
  +System.Windows.Controls.DataGridBoundColumn@ 
  ElementStyle#@ [ $< 编号��A   
IsReadOnlyA . $; 60 B 
  BindingB �� *
DayIndex+	  $< 日期��A . $	; 165 #@ [ B �� *	DayTime+� $��1��$��1��.+�� " �� $��60 ��� $��2 ��� �� $	��200 �.+��� ���#cecece.+��� -   $��cbAvg��$��仅显示均值��$��36��$
��Left=�$��	10,13,0,0q�
���   �  -   $��gvInfo��$��2��#���  ���� : 	  #@ [ $< 评测模式��A . $	; 125 B �� *
ActionName+	  #@ [ $< 	爆发力��A . $; 95 B �� *MaxV+	  #@ [ $< 耐力��A . $; 95 B �� *	LastValue+	  #@ [ $< 疲劳指数��A . $; 95 B �� *FatigueIndex+	  #@ [ $< 评测时间��A . $	; 155 B �� *EvaluteDetailDate+  $< 操作��$; 90 > �� .R��� -   #��[  $��60��$��20������ $
 Edit�
C � TagC �� *EvaluteDetailId+.��删除�� -   $��spImage��$��3���� -	   $��cbMode��$��50��$
��Left=�$��	173,5,0,0q�$	��Top=�$	��216��$��1��$��1��.@��� $��全部��$C 0���� $��前屈活动度测试��$C 3���� $��后伸活动度测试��$C 4���� $$��旋转（左）活动度测试��$C 1���� $$��旋转（右）活动度测试��$C 2���� $��前屈力量测试��$C 7���� $��后伸力量测试��$C 8���� $!��旋转（左）力量测试��$C 5���� $!��旋转（右）力量测试��$C 6��� -
   D �Name$D 
SetVidicon�����	#B0FFFFFF$y�	Collapsed=�$��2��$��2��$	��750��$
��1000��E K� ZIndex$	E 100��.+��� -   $ Loading��$ 5��$��40��
���   �$��1q�
�������$	��350��F J� EffectF 3  -System.Windows.Media.Effects.DropShadowEffect  G   Color$G Black��._�� $	��350��$��35��) �� $��35 ��� .+��    	 brushH j� StretchI j� 
AlignmentXJ j� 
AlignmentYK L� DrawingL � BrushM � GeometryN @� RadiusXO @� RadiusY��%^   &	       L� $
H None=�$I Center=�$	J Top=�K � 
L �   �M @� $N 2Q�$O 5Q�.+��� -   P ��Name$	P r01��#��[ $	 0.2Q�Q J� RenderTransformOrigin$Q 0.5,0.5&�z��� R �� Angle$R 0Q��� -   $	P r02��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $R 30Q��� -   $	P r03��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $R 60Q��� -   $	P r04��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $R 90Q��� -   $	P r05��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 120Q��� -   $	P r06��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 150Q��� -   $	P r07��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 180Q��� -   $	P r08��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 210Q��� -   $	P r09��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 240Q��� -   $	P r10��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 270Q��� -   $	P r11��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 300Q��� -   $	P r12��#��[ $	 0.2Q�$Q 0.5,0.5&�z��� $	R 330Q�S � TriggersS :� T :� RoutedEvent$T Grid.Loaded��.[��� .���� U g� RepeatBehavior$U Forever��.��W� $	 r01��$ Opacity�V g� AutoReverseV . $ 0:0:0.08333D�$ 0:0:0.00000X�$ 0X�W� $	 r02��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.08333X�$ 0X�W� $	 r03��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.16666X�$ 0X�W� $	 r04��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.24999X�$ 0X�W� $	 r05��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.33332X�$ 0X�W� $	 r06��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.41665X�$ 0X�W� $	 r07��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.49998X�$ 0X�W� $	 r08��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.58331X�$ 0X�W� $	 r09��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.66664X�$ 0X�W� $	 r10��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.74997X�$ 0X�W� $	 r11��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.83330X�$ 0X�W� $	 r12��$ Opacity�V . $ 0:0:0.08333D�$ 0:0:0.91663X�$ 0X�� $��1��.+��� -   $��tbPrompt��$��Center=�$��Center=�$	��0,7q�设备homing...�� -   $��btnSave��$��1��$��保存处方��#��[  $	��132��$��62��$��354,0,514,0q��� -   $��btnExcel��$��1��$��导出Excel��#��[  $	��132��$��62��$��501,0,367,0q��� -   $��btnPrint��$��1��$��打印预览��#��[  $	��132��$��62��$��583,0,155,0q��� -   $��	btnCancel��$��1��$��返  回��#��[  $	��132��$��62��$��	729,0,9,0q�