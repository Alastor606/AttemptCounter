   M S B A M L   `   `   `  ����E  ATryCounter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TryCounter.ViewsTryCounter.Views  5      !    TryCounter.Views.FolderPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:TryCounter.Views     5        N� Title$  
FolderPage��5	       N� 
Background �	#FF313131.��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��50 �6   6,   .+�5   
   �� -   5        $��
FolderName��5   N   $��25(�6    �� TextAlignment$ Center=�6    $��2��6`   ���	#FF212121
������� �� 	UndoLimit$ 30��6�   ��67   Z� 6�   �� 5   
   -   5        $��CountersList��5   6   $��1��6   
�������$��Center=�6]   ���	#FF1B1B1B��6   Z� ��5      �� 5      .R�5      �� .+�5      �� $��DemiBold"�6:   $��20(�6P    �� TextAlignment$ Center=�6^   $��Center=�6u   $	��250��6�   ��6$   �� *Name+6�   �� 5       �� Orientation$ 
Horizontal=�6%   .+�5      �� $��DemiBold"�6?   $��20(�6U   $ Center=�6c   $��Center=�6z   
�������$	��250��6�   ��6(   �� *Count+6�   5      5      5      5!      �� 5"   
   $��1��6   $��1��6%   $ Vertical=�62   $��Bottom=�6I   .+�5#      �� -   5        $��Bottom=�5#      $��30(�60   $��35��6>   $��35��6J   $
��Bold"�6U    � ToolTip$ 
Назад��6�   ��6t   Z� ��5$      �� 5%      	 �� ImageSource$	 /Resourse/Back.png��6!   5'      �� 5(      -   5        $
��0,20q�5(      $��Bottom=�6$   $��30(�6?   $��35��6M   $��35��6Y   $
��Bold"�6d   $% Добавить счетчик��6�   ��6�   Z� ��5)      �� 5*      $	 /Resourse/plus.jpg��6!   5,      5-      5/      50      