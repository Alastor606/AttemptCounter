   M S B A M L   `   `   `  ����E  ATryCounter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TryCounter.ViewsTryCounter.Views  5          TryCounter.Views.MainPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:TryCounter.Views     5        N� Title$  MainPage��5	       N� 
Background �	#FF313131.��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��50 �6   6,    � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      6   .+�5   
   �� 
������� �� TextAlignment$ Center=�6'   $��30(�6>   $
��Bold"�6L   Все разделы6k   �� 5   
   -   5        $��
FolderList��5      $��2��6   ���	#FF1B1B1B
�������$��Center=�6�   ��61   Z� ��5      �� 5      .R�5      �� .+�5      �� $��DemiBold"�6:   $��20(�6P   $ Center=�6^   $��Center=�6u   ��6$   �� *Name+6�   5      5      5      �� 5   
   $��1��6   $��1��6%    �� Orientation$ Vertical=�62   $��Bottom=�6I   .+�5      �� -   5        $
��0,20q�5      $��Bottom=�6$   $��30(�6?   $��35��6M   $��35��6Y   $
��Bold"�6d    � ToolTip$# Добавить раздел��6�   ��6�   Z� ��5       �� 5!       �� ImageSource$ /Resourse/plus.jpg��6!   5#      5$      5%      5&      