   M S B A M L   `   `   `  ����E  AToDoApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ToDoApp.UI.UIHelpersToDoApp.UI.UIHelpers  5      )    #ToDoApp.UI.Controls.WorkTaskControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5helper"clr-namespace:ToDoApp.UI.UIHelpers     5        � 	Resources  5
      4   .ToDoApp.UI.UIHelpers.BoolToVisibilityConverter   BoolToVisibilityConverter:   4ToDoApp.UI.UIHelpers.InvertBoolToVisibilityConverter # InvertBoolToVisibilityConverter��%:   &	        &	        5   
   6+     5   
   61   5      .��5      �� 
�������$��1q�6%    �� CornerRadius$ 6��69   $��5q�6J    �� Padding$ 10q�6U   ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5#      �� $��0��6   $��0��6$   $	��Top=�6�   $��0,0,10,0q�6�    d� 	IsChecked 64   �� *IsCompleted+y�6V   �� *
IsFinished+ �� 	Converter# [ 6�   �� 5$      $��0��6   $��1��6%   $
��Bold"�6L   $��14(�6^   ��65   �� *Title+6^   �� 5%      $��0��6   $��2��6%   
�������$��Italic$�6�   $��10,0,0,0q�6�   ��65   �� *	StartDate+ �� StringFormat$ ({0:dd.MM.yyyy})��6�   �� 5(      $��1��6   $��1��6%   $
��Wrap=�6R   
���OO/�$��0,4,0,0q�6�   ��65   �� *Description+6�   �� 5)      $��1��6   $��2��6"   $��Ukończ��5*      $	��100��5,      $��10,5,0,0q�5-      y�5+      �� *
IsFinished+# [ ��5.      �� *#!DataContext.FinishTaskItemCommand+ �� RelativeSource �� 	 �� AncestorType	 UserControl
 �� AncestorLevel$
 2����5/      �� �� 51      $��2��6   $��1��6&    �� Orientation$ 
Horizontal=�66   $��0,6,0,0q�6O   .+�53      �� 
���  ��$
��Bold"�54   %   $��0,0,15,0q�67   ��53      �� *PriorityAsText+54   7   �� 57      $ 
Horizontal=�6   y�66   �� *TaskProgress+# [  .+�58      �� $��0,0,15,0q�6`   ��6    �� *ProgressAsText+$ {0} jednostek��6`   �� 59      $��0,0,15,0q�6Z   ��6    �� *ProgressAsDaysLeft+$	 {0}��6Z   �� 5:      ��6    �� *ProgressString+y�5;      �� *HasTaskStringProgress+# [  5<      5=      �� 5?      $��3��6   $��2��6"   $��
Aktualizuj��5@      $	��100��5A      $��10,5,0,0q�5C      y�5B      �� *
IsFinished+# [ ��5D      �� *%#DataContext.OpenUpdateWindowCommand+ �� 	 UserControl$
 2����5E      �� 5F      5G      5H      