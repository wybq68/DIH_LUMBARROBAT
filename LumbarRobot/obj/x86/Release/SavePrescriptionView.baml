   M S B A M L   `   `   `  ����   LumbarRobot&     LumbarRobot.SavePrescriptionView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $	��768��$
��1366��  � 	Resources     	ProButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� Opacity �� CornerRadius J� BitmapEffect	 O� Opacity
 O� GlowSize O� 	GlowColor �� RelativeSource �� Path �� 
StartPoint �� EndPoint � Offset � Color ��Name �� Triggers P� Property P� Value N� EnterActions	 W� To g� Duration �� 
TargetName �� TargetProperty	 �� To g� 	BeginTime N� ExitActions  ContentCenterStyle  DataGridStyle1&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow*  $System.Windows.Controls.DataGridCell �� Triggers  DataGridStyle �� Binding  �� Mode! �� Value" J� SnapsToDevicePixels  HorizontalNormalBorderBrush# �� Color  NormalBorderBrush  
GlyphBrush  ScrollBarPageButton  ScrollBarLineButtonTop$ �� ImageSource 	 ScrollBarLineButtonBot 
 ScrollBarLineButtonLeft  ScrollBarLineButtonRight  ScrollBarThumb  ScrollBarHorizontalThumb  VerticalScrollBar% � RowDefinitions& ]� DecreaseRepeatButton' ]� Thumb( ]� IncreaseRepeatButton  HorizontalScrollBar) � ColumnDefinitions* ]�Name  FavsScrollViewer+ ��Name, �� Orientation- �� HorizontalOffset. � Value/ �� ScrollableWidth0 � Maximum1 �� ViewportWidth2 �� ViewportSize,3 �� %ComputedHorizontalScrollBarVisibility4 �� VerticalOffset5 �� ScrollableHeight6 �� ViewportHeight*7 �� #ComputedVerticalScrollBarVisibility��%	$  &	        &	 �
    &	 �
    &	 �    &	     &	 '    &	 :    &	 J    &	     7  7  &		 :    7  7  &	
 s    7  7  &	 �    7  7  &	 �    &	     &	 D    7  7  7  7  7  7 	 &	 �    7 
 7  7  7  7  7  '�� Z     7  7  &	 �!    �� " ��.���  � ��
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
 fore��$U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)��� " ��.���  � ��$ Center=���  � ��$ Center=��� "  .���  � ��$ 0q���  � �� �#FFFFFF��  �@ AutoGenerateColumns .  ��  �@ GridLinesVisibility$
 None=���  �@ ColumnHeaderHeight$ 33Q���  �@ CanUserDeleteRows .  ��  �@ CanUserReorderColumns .  ��  �@ CanUserResizeColumns .  ��  �@ CanUserAddRows .  ��  �@��AlternationCount$ 2����  �@ ColumnHeaderStyle �� "  .���  � �� �#333333��  � ��$ 14(���  � ��$ 50����  � ��$ Microsoft YaHei)���  � �� �#d6d6d6��  � ��$ 1q���  � ��$ 60����  � ��$ Center=���  � ��$ Center=���  �@ RowStyle �� "  .���  � ��$ 40����  � ��$ Center=���  � ��$ 14(���  � �� �#666666��  � �� �#FFFFFF��  � ��$ Center=���  �@��TextAlignment$ Center=���  �@ 	CellStyle �� "  .���  � �� �#d6d6d6��  � ��$ 1,0,1,2q���  � ��$ Center=���  � ��$ Center=���  �@��TextAlignment$ Center=� P�  �@ 
IsSelected . . ���  � �� �#669900��  �@ RowHeaderWidth$ 0Q��� "  .���  � ��$ 0q���  �@ AutoGenerateColumns .  ��  �@ GridLinesVisibility$
 None=���  �@ ColumnHeaderHeight$ 33Q���  �@ CanUserDeleteRows .  ��  �@ CanUserReorderColumns .  ��  �@ CanUserResizeColumns .  ��  �@ CanUserAddRows .  ��  �@��AlternationCount$ 2����  �@ ColumnHeaderStyle �� "  .���  � ��$ 0q���  � �� �#333333��  � ��$ 14(���  � ��$ 50����  � ��$ Microsoft YaHei)���  � ��$ 60����  � �� �#d6d6d6��  � ��$ 1q���  � ��$ Center=���  � �� �#cccccc ��  ��  �� $  PreviousData=�! Z� .a���  � ��$ 1,1,1,1q���  �@ RowStyle �� "  .���  � ��$ 40����  � ��$ Center=���  � ��$ 14(���  � �� �#666666��  � �� �#FFFFFF��  � ��$ Center=���  �@��TextAlignment$ Center=���  �@ 	CellStyle �� "  .���  � ��$ 0q���  � �� �#d6d6d6��  � ��$ 0,0,1,1q���  � ��$ Center=���  �@��TextAlignment$ Center=���  � �� �� "  .R��� #��z��#��z��#��z��" . ._��� $��Center=� P�  �@ 
IsSelected . . ���  � �� �#669900��  ��  �� $  PreviousData=�! Z� .a���  � ��$ 1,0,1,1q���  �@ RowHeaderWidth$ 0Q��� $# #efefef���� $# #efefef���� $
# #444���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� ���#efefef�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��8��  $��0q����� $$ /images/up.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��8��  $��0q����� $$ /images/down.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��42��8��  $��0q����� $$ /images/left.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��42��8��  $��0q����� $$ /images/right.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+ �� *TemplatedParent+�� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� " n�.R��� ._�� .+��� ���� $ 0.5,0&�$ 0.5,1&�.��� $
 Gray��$	 0.7Q��� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$
 Auto����  � ��$ 38����  � �� �� " n�.R��� ._�� .+��� ���� $ 0.5,0&�$ 0.5,1&�.��� $
 Gray��$	 0.7Q��� " ��.R�� % �� $��23���� �� $��23��.+��� $��3��$��0��$ 0��
������ �� $��0��8��  $��23��$��ScrollBar.LineUpCommand��$	��Top=�]� $��
PART_Track��$��1����. & �� $��38��8�� $��ScrollBar.PageUpCommand��' n� 8�� 8�� ( �� $��38��8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��23��$��ScrollBar.LineDownCommand��$��Bottom=��� " ��.R�� $��35��) �� $��35���� �� $��35��.+��� $��3��$ 0�����#efefef�� $��0��8��  $��35��$��35��$��ScrollBar.LineLeftCommand��]� $* 
PART_Track��$��1����.  & �� 8�� $��ScrollBar.PageLeftCommand��' n� 8�� 8�� ( �� 8�� $ ��ScrollBar.PageRightCommand���� $��3��8�� $��23��$��35��$ ��ScrollBar.LineRightCommand���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 42����  � ��8   P�  �@��Orientation$ Vertical=�. ���  � ��$ 42����  � ��$
 Auto����  � ��8  �� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R�� ) �� $
��Auto ��� % �� �� $
��Auto �.+��� $��1���� $+ PART_HorizontalScrollBar��$, 
Horizontal=�$��1��$��1��#. z- #0 z/ #2 z1 #y�z3 �� $��PART_VerticalScrollBar��$��0��$��1��#. z4 #0 z5 #2 z6 #y�z7 $��
0,0,-0.5,0q�$��Right=�.��� % �� �� $	��700 ��� .+�� 
���   �$
 0.45Q�� 
���   �$
 0.45Q�$��2��� $��1��) �� �� $
��1200 ��� .+�� 
���   �$
 0.45Q�� 
���   �$
 0.45Q�$��2��� $��1��% �� $��42 ��� .+�� ���� $$ /images/bgdetail.png��.+��� $��Center=�$
��Left=�$	��0,6q�$	��100��.+��� -   $��btnSavePrescription��#���   $��确 定��$
��10,0q�$��30��$��80��$y�Visible=��� $��Center=�$��Right=�.+��� -   $��	btnClosed��$��0,0,20,0q����� " ��.R��� $��cc��.���� $��/images/closed.png��$��30��$��30��� $��1��.+�  -   $��gvDetail��#��[ 8   SelectionMode$8 Single=�$
��Auto������ *Entity+9   Columns9 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn:   ElementStyle#: [ ,  &System.Windows.Controls.DataGridColumn;   CanUserSort; .  <   Header$< 治疗名称��=   Width5  /System.Windows.Controls.DataGridLengthConverter$	= 170 >   
IsReadOnly> . ?   Binding? �� *
ActionName+  #: [ ; .  $< 模式��$	= 105 > . ? �� *
ModeName+  #: [ ; .  $< 力量��$	= 100 > . ? �� *PForce+  #: [ ; .  $< 速度��$	= 100 > . ? �� *Speed+  #: [ ; .  $< 最大角度��$	= 100 > . ? �� *
MaxAngle+  #: [ ; .  $< 最小角度��$	= 100 > . ? �� *
MinAngle+  #: [ ; .  $< 分组��$	= 100 > . ? �� *PGroup+  #: [ ; .  $< 应训次数��$	= 100 > . ? �� *Times+  #: [ ; .  $< 初始位置��$	= 100 > . ? �� *LastLocation+4	  .System.Windows.Controls.DataGridTemplateColumn	  $< 编辑操作��$= 90 @ 	  CellTemplate@ �� .R��� -   #��[  $��60��$��20������ $
 Edit�
A � TagA �� *Id+.��修改	  $< 删除操作��$= 90 @ �� .R��� -   #��[  $��60��$��20������ $	 Del�A �� *Id+.��删除