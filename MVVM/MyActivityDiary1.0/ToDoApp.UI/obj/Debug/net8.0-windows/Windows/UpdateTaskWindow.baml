   M S B A M L   `   `   `  ����E  AToDoApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ToDoApp.UI.UIHelpersToDoApp.UI.UIHelpers  5   	   )    #ToDoApp.UI.Windows.UpdateTaskWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5helper"clr-namespace:ToDoApp.UI.UIHelpers     5   	     � Title$  Aktualizuj Zadanie��5   	   $	��400��5	   	   $	��500��6   $	��400��5
   	   $	��500��6   $	��400��5   	   $	��500��6    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5      4   .ToDoApp.UI.UIHelpers.BoolToVisibilityConverter   BoolToVisibilityConverter8   2ToDoApp.UI.UIHelpers.TaskPriorityToPolishConverter ! TaskPriorityToPolishConverter��%:   &	        &	        5   
   6+     5   
   6/   5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5"   
   �� 5#      $	��150 �6   �� 5$      $��* �6   .+�5(   
   �� $��Tytuł:��6   $��0��6#   $��0��60   $��Center=�6@   �� 5)   
   -   5         ��Name$ TitleTextBox��5)   :   $��0��6P   $��1��6]   $��5q�6m   ��6   �� *TaskItemViewModelTitle+6m   �� 5,   
   $��Opis:��6   $��1��6!   $��0��6.   $��Center=�6>   �� 5-   
   -   5        $ DescriptionTextBox��5-   @   $��1��6\   $��1��6i   $��5q�6y   ��6   �� *TaskItemViewModelDescription+6y   �� 50   
   $��Data rozpoczęcia:��6   $��2��6-   $��0��6:   $��Center=�6J   (  "System.Windows.Controls.DatePicker  51   
   -   5          Name$ StartDatePicker��51      $��2��6b   $��1��6o   $��5q�6      SelectedDate 6.   �� *TaskItemViewModelStartDate+6   �� 54   
   $$��Czy dodać postęp do zadania?��6   $��3��6;   $��2��6H   $��5q�6\    d� 	IsChecked 55      �� *HasTaskProgress+�� 58   
   $��Data zakończenia:��6   $��4��6-   $��0��6:   $��Center=�6J   y�59      �� *HasTaskProgress+	 �� 	Converter#	 [    5:   
   -   5        $ FinishDatePicker��5:   J   $��4��6d   $��1��6q   $��5q�6�    6   �� *TaskItemViewModelFinishDate+y�5;      �� *HasTaskProgress+#	 [  �� 5>   
   $��Aktualny postęp:��6   $��5��6,   $��0��69   $��Center=�6I   y�5?      �� *HasTaskProgress+#	 [  �� 5@   
   -   5        $ CurrentProgressTextBox��5@   3   $��5��6S   $��1��6`   $��5q�6p   ��6   �� *CurrentProgress+y�5A      �� *HasTaskProgress+#	 [  �� 5C   
   $��Maksymalny postęp:��6   $��6��6.   $��0��6;   $��Center=�6K   y�5D      �� *HasTaskProgress+#	 [  �� 5E   
   -   5        $ MaxProgressTextBox��5E   /   $��6��6K   $��1��6X   $��5q�6h   ��6   �� *MaxProgress+y�5F      �� *HasTaskProgress+#	 [  �� 5I   
   $��Postęp opisowy:��6   $��7��6+   $��0��68   $��Center=�6H   y�5J      �� *HasTaskProgress+#	 [  �� 5K   
   -   5        $ ProgressStringTextBox��5K   6   $��7��6U   $��1��6b   $��5q�6r   ��6   �� *TaskStringProgress+y�5L      �� *HasTaskProgress+#	 [  �� 5N   
   $��
Priorytet:��6   $��8��6&   $��0��63   $��Center=�6C   �� 5O   
   $��8��6   $��1��6    $��5q�5R      $	��150��6   ��5P      �� *PriorityValues+
 �� SelectedItem
 5Q      �� *
Priority+ �� Mode$ TwoWay=���5S      �� 5T      .R�5U      �� ��6    �� #	 [ 5V      5X      �� 5[   
    �� Orientation$ 
Horizontal=�6   $��9��5\      $��2��5]      $��Right=�5^      $��0,20,0,0q�5_      .+�5`      �� -   5        $��
Aktualizuj��5`      $	��100��5b      $��35��5c      $��0,0,10,0q�5d      �� 5e      -	   5        $��Zamknij��5e      $	��100��5g      $��35��5h      5i      5j      5k      