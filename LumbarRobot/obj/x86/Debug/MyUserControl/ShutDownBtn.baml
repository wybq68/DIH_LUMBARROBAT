   M S B A M L   `   `   `  ����  LumbarRobot+    %LumbarRobot.MyUserControl.ShutDownBtn   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5	         ProShutdownBtn �� 
TargetType �� Property �� Value �� 
TargetType �� Orientation ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName��%�  &	        �� 5   
   " ��6'   .�5      ��  � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 
Horizontal=�6%   .+�5      � $��71��6#   $��76��6/   .+�5   "   �� $ imageNoChecked��6(   $��/images/Shutdown_0.png��6@   $��Center=�6`   $��Center=�6}   $��35��6�   $��32��6�   �� 5   "   $ imageChecked��6(   $��/images/Shutdown_1.png��6>   $��Center=�6^   $��Center=�6{   $��35��6�   $��32��6�   5      �� 5      #��z��6$   #��z��6H   #��z��6n   5       5      P� 5       � ��	 . . �5   "   �� $
 imageChecked��6)    � y�$ Visible=�6Y   �� 5   "   $
 imageNoChecked��6)    � y�$ Hidden=�6[   5      P� 5       � ��	 .  . �5    "   �� $
 imageChecked��6)    � y�$ Hidden=�6Y   �� 5!   "   $
 imageNoChecked��6)    � y�$ Visible=�6[   5"      5$      5&      5'      5(      .��5)      � .+�5*   
   �� -   5        $��btnShutDown��5*      $��71��6$   $��76��60    � Cursor$
 Hand��6;   
 � Tag$ index��6I   #���   6|   ��6U   �� *ShunDownBtnCommand+6�   5+      5,      