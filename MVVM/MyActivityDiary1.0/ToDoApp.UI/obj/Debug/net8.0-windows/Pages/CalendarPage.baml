   M S B A M L   `   `   `  ����E  AToDoApp.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ToDoApp.UI.ViewModelsToDoApp.UI.ViewModels  5      G CToDoApp.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO7clr-namespace:ToDoApp.Core.Models;assembly=ToDoApp.CoreToDoApp.Core.Models 5      #    ToDoApp.UI.Pages.CalendarPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      2vm#clr-namespace:ToDoApp.UI.ViewModels     5      Ilocal7clr-namespace:ToDoApp.Core.Models;assembly=ToDoApp.Core     5      .��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��28(�6/   $
��Bold"�6=   $��Center=�6O   $��0��6l   $��0,0,0,20q�6y   ��6   �� *	MonthName+6y   A� 5   
    A� Columns$ 7��6   $��1��6"   $��0,0,0,10q�6/   .+�5      �� $	��Pon��6   $
��Bold"�6#   $��Center=�65   �� 5      $��Wt��6   $
��Bold"�6"   $��Center=�64   �� 5      $	��Śr��6   $
��Bold"�6"   $��Center=�64   �� 5      $	��Czw��6   $
��Bold"�6#   $��Center=�65   �� 5      $��Pt��6   $
��Bold"�6"   $��Center=�64   �� 5      $	��Sob��6   $
��Bold"�6#   $��Center=�65   �� 5      $��Nd��6   $
��Bold"�6"   $��Center=�64   5      �� 5    
   $��2��6   ��6$   �� *CalendarDays+��5!      �� 5"      .R�5#      A� $ 7��6"   5$      ��5&      �� 5'       �� DataType%  ToDoApp.Core.Models.CalendarDay"  6   .R�5(      �� $��4q�6    �� Padding$ 10q�6(   
�������$��1q�6H   ��5)      �� 5*       �� 
TargetType" ��6$   .�5+   "   ��  �� Property � y� �� Value$ Visible=�6?    �� Triggers 5,   "   �� 5-   &    �� Binding 62   �� *	DayNumber+	 �� Value	 06P   .a�5.   *   ��  � y�$ 	Collapsed=�6G   5/   '   51      ._�54      �� .+�56      �� $
��Bold"�57      $��14(�58      $��Center=�59      ��56   (   �� *	DayNumber+59      �� 5<      $��10(�5=      
��� d �$��Center=�5?      ��5<   (   �� *Tasks.Count+
 �� StringFormat$
 Ilość zadań: {0}��5?      �� 5B      ��6+   �� *TasksSorted+��5C   "   �� 5D   &   .R�5E   *   �� $��10(�5F   $   
���   �$��CharacterEllipsis=�5H   $   ��5E   4   �� *Title+5H   $   5I   '   5K      5L      5M      5N      5P      �� 5Q   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $
��0,10q�6K   .+�5R      �� $	��◀��6   $��40��6G   ��6!   �� *GoPreviousCommand+6G   �� 5S      $��20(�66   $
��Bold"�6D   $
��20,0q�6V   $��Center=�6d   ��6   �� *DisplayMonth+6d   �� 5T      $	��▶��6   $��40��6C   ��6!   �� *GoNextCommand+6C   5U      5W      5X      