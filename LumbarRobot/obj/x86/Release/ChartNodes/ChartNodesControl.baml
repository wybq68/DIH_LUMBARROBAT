   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity   LumbarRobot.   (LumbarRobot.ChartNodes.ChartNodesControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     e aDynamicDataDisplay.Controls, Version=0.3.4703.0, Culture=neutral, PublicKeyToken=5b7d744a7263923fa ]DynamicDataDisplay.Maps, Version=0.3.4703.0, Culture=neutral, PublicKeyToken=5b7d744a7263923fd `DynamicDataDisplay.Markers, Version=0.3.4703.0, Culture=neutral, PublicKeyToken=5b7d744a7263923f\	 XDynamicDataDisplay, Version=0.3.4703.0, Culture=neutral, PublicKeyToken=5b7d744a7263923fCd34http://research.microsoft.com/DynamicDataDisplay/1.0    	 $	��680��$
��1024��  � 	Resources     HorizontalNormalBorderBrush �� Color  NormalBorderBrush  
GlyphBrush  ScrollBarPageButton �� 
TargetType �� Property �� Value �� 
TargetType  ScrollBarLineButtonTop ��Name �� CornerRadius �� ImageSource	 �� RelativeSource  ScrollBarLineButtonBot  ScrollBarLineButtonLeft  ScrollBarLineButtonRight  ScrollBarThumb
 �� 
StartPoint �� EndPoint � Color � Offset 	 VerticalScrollBar � RowDefinitions ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton 
 HorizontalScrollBar � ColumnDefinitions ]�Name �� Triggers P� Property P� Value  FavsScrollViewer ��Name �� Orientation �� HorizontalOffset � Value �� ScrollableWidth � Maximum �� ViewportWidth �� ViewportSize, �� %ComputedHorizontalScrollBarVisibility  �� VerticalOffset! �� ScrollableHeight" �� ViewportHeight*# �� #ComputedVerticalScrollBarVisibility��%�  &	        &	      &	 &     &	 6     &	 �     7  7  &	 &    7  7  &	 _    7  7  &	 �    7  7  &	 �    &		 
    7  7  7  7   7  7  &	
 �    7  7  7  7  7  7  '�� N
    7 
 7 	 &	 �    �� $ #efefef���� $ #efefef���� $
 #444���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� ���#efefef�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��8��  $��0q����� $ /images/up.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+	 �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��8��  $��0q����� $ /images/down.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+	 �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��42��8��  $��0q����� $ /images/left.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+	 �� *TemplatedParent+�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��42��8��  $��0q����� $ /images/right.png��._�F� $��Center=�$��Center=�8�� ���� *	Content+	 �� *TemplatedParent+�� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$ 38����  � ��$
 Auto����  � �� �� " n�.R��� ._�� .+��� ���� $
 0.5,0&�$ 0.5,1&�.��� $
 Gray��$	 0.7Q��� " ��.R��  �� $��23���� �� $��23��.+��� $��3��$��0��$ 0��
������ �� $��0��8��  $��23��$��ScrollBar.LineUpCommand��$	��Top=�]� $��
PART_Track��$��1����.  �� $��38��8�� $��ScrollBar.PageUpCommand�� n� 8�� 8��  �� $��38��8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��23��$��ScrollBar.LineDownCommand��$��Bottom=��� " ��.R�� $��35�� �� $��35���� �� $��35��.+��� $��3��$ 0�����#efefef�� $��0��8��  $��35��$��35��$��ScrollBar.LineLeftCommand��]� $ 
PART_Track��$��1����.   �� 8�� $��ScrollBar.PageLeftCommand�� n� 8�� $��0,1,0,1q�8�� ���� $ /images/bar1.png�� �� 8�� $ ��ScrollBar.PageRightCommand���� $��3��8�� $��35��$��35��$ ��ScrollBar.LineRightCommand���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 42����  � ��8   P�  �@��Orientation$ Vertical=�. ���  � ��$ 42����  � ��$
 Auto����  � ��8  �� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��  �� $
��Auto ���  �� �� $
��Auto �.+��� $��1���� $ PART_HorizontalScrollBar��$ 
Horizontal=�$��1��$��1��# z # z # z #y�z �� $��PART_VerticalScrollBar��$��0��$��1��# z  # z! # z" #y�z# $��
0,0,-0.5,0q�$��Right=�.��� ���#ececec$
��1024�� �� $��40 ��� .+��� $��0,0,0,2q����#999999$��2��$
��30,0q�._��� -   $ ��Name$$ txExitemName�����	#FF656565$��16(�$��Microsoft YaHei)�% �� IsHyphenationEnabled% . & �� TextAlignment$
& Left=�$
��Wrap=�$
��Left=�$
��10,8q����� *
ExItemName+�� $��Center=�$��Right=�.+��� -   $��	btnClosed��$��0,0,20,0q����� " ��.R��� $��cc��.���� $��/images/closed.png��$��30��$��30��� $��1��$	��918�� �� $	��120 ��� .+��  �� $��60 ��� $��60 � �� $	��30* ��� $
��120* ��� $	��30* ��� $
��120* ��� $	��30* ��� $
��120* ��� $	��30* ��� $
��120* �.+��� $
��Left=�$��Center=�.+��� -   ' ��Name$' imgred��$��/images/red.jpg��$��25��$��30���� $
��Left=�$��Center=�$��2��.+��� -   $' 	imgYellow��$��/images/yellow.jpg��$��25��$��30���� $
��Left=�$��Center=�$��4��.+��� -   $' 	imgOrange��$��/images/Magenta.jpg��$��25��$��30���� $
��Left=�$��Center=�$��2��$��1��.+��� -   $' imgspringgreen��$��/images/Brown.jpg��$��25��$��30��$
��0,-5q��� $
��Left=�$��Center=�$��1��.+��� -   $' 
imgdeepink��$��/images/Black.jpg��$��25��$��30��$
��0,-5q��� $
��Left=�$��Center=�$��1��.+��� -   $��CkGuided��$�� 被动运动模式��$��18(��� $
��Left=�$��Center=�$��3��.+��� -	   $��CkIsotonicA��$�� 等长运动模式��$��18(��� $
��Left=�$��Center=�$��5��.+��� -
   $��CkIsotonicB��$�� 协调性训练模式��$��18(��� $
��Left=�$��Center=�$��3��$��1��$
��0,16q�$	��214��$��2��.+��� -   $��CkFreeCounterWeight��$"�� 向心、离心运动模式��$��18(��� $
��Left=�$��Center=�$��1��$��1��.+��� -   $��CkFreeConstantResistance��$�� 等张运动模式��$��18(��� -   $��ColumnScroll����. $
��Auto=�$��1��$	��918�����#FFFFFF  ��%�   '��       7 	 �� " ��.���  � ��8    P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$	 100��P�  �@��Orientation$ Vertical=�. ���  � ��$ 40����  � ��$
 Auto��.��� -   $	��Mid��$
��1050�� �� $	��340 ��� $��10 ��� $	��340 ��� $��10 ��� $	��340 ��� $��10 �.+�� -   $��Grid_FW��$	��870��� -   $��
Grid_Power��$��2��$	��870��� -   $��Grid_C��$��4��$	��870��$y�Hidden=� �� $	��120 ��� .+��� $��/images/EgImg.png��$	��109��$	��198��8 	 2Microsoft.Research.DynamicDataDisplay.ChartPlotter  -   $��plotter2��$��1��(   MainHorizontalAxis( I 	 CMicrosoft.Research.DynamicDataDisplay.Charts.HorizontalDateTimeAxis  -   $��dateAxis��3 	 -Microsoft.Research.DynamicDataDisplay.Plotter)   Children.) D 	 >Microsoft.Research.DynamicDataDisplay.Charts.ClusteredBarChart  -   $��barChart��*   BarWidth$* 10Q�; 	 5Microsoft.Research.DynamicDataDisplay.PointsGraphBase+   
DataSource+ �� 