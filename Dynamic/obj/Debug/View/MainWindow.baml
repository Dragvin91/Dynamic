   M S B A M L   `   `   `  ����B  >Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Dynamic.ViewModelDynamic.ViewModel  5   
   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	       Dynamic.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Dynamic.ViewModel     5   
   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	     � Title$  Reading logs��5
   	   $
��auto��6   $
��auto��6,    � DataContext 5      +   %Dynamic.ViewModel.MainWindowViewModel  5   
    � 	Resources 5      &   System.Windows.Controls.DataGrid �� 
TargetType �� Triggers �� Binding �� Value �� Property �� Value��%�  '        '�� �     '�� 4    �� 5   
   "  6    5      �� 5       6   �� *result.Count+ 06?   .a�5      ��  � y�$ Hidden=�63   5      5      �� 5   
   " ��6    5      �� 5       6   �� *result.Count+ 06?   .a�5      ��  � y�$ Hidden=�63   5      5      �� 5   
   " ��6    5      �� 5       6   �� *
uids.Count+ 06=   .a�5       ��  � y�$ Hidden=�63   5!      5#      5$      .��5%      �� $
��auto��6   $
��auto��6   .+�5&   
   o� $
��Auto��6   $��20��6   $
��Left=�6)   $	��Top=�6D   .@�5'      m� $��Файл��6   $��20��6%   .@�5(      m� $��Открыть��6   	 m� Command	 6,   �� *OpenCommand+5)      5*      �� 5+   
   ��6   �� *uids+
 �� SelectedItem
 60   �� *
select_UID+.  (System.Windows.Interactivity.Interaction   Triggers 5,      /  )System.Windows.Interactivity.EventTrigger  5-         	EventName$ MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5.      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *DoubleClickCommand+   CommandParameter 6S   �� *SelectedItem+5/       �� View 51      �� 52      .P�53      �� $��Имя объекта��6Q    �� DisplayMemberBinding 6%   �� *
nameObject+6h   �� 54      $��Время старта��6V    6%   �� *start_check_uid+6n   �� 55      $#��Время остановки��6U    6%   �� *stop_check_uid+6p   �� 56      $!��Схема проверки��6S    6%   �� *active_cxema+6m   �� 57      $)��#Интервал измерения��6^    6%   �� *averaging_interval_time+6|   58      5:        5;   
   ��6   �� *result+�� 5<   
   $
��Left=�6   $��25��6,   ��68   �� *
Converting+.��6W   Создать Excel файл5>      �� 5?   
   -   5         ��Name$ ButExcel��5?   V   $
��Left=�6   $��25��6,   ��68   �� *	OpenExcel+.��6h   Открыть файл Excel5A      5B      5C      