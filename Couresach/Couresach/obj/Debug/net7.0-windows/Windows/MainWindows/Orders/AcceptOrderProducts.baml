   M S B A M L   `   `   `  ����D  @Couresach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Couresach.WindowsCouresach.Windows  5   	   +    %Couresach.Windows.AcceptOrderProducts   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   1localclr-namespace:Couresach.Windows     5   	     � Title$'  !Подтвердите заказ��5   	   $	��300��6#   $	��400��60    � 
ResizeMode$ NoResize=�6<   
����ح�.��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      .+�5   
   �� $��Center=�6   $��Center=�6/   $��SemiBold"�6L   $��26(�6b   
Заказ6w   �� 5   
   $��1��6   .+�5      �� $"Добавьте описание:6,   �� 5      -   5         ��Name$ DescriptionOrder��5   A    �� TextWrapping$
 Wrap=�6   $��60��6*   $��5q�66   6]   5      �� 5   
   -   5        $��2��5      $��30��6   $	��100��6*   $��	0 0 200 0q�66   .��6d   Заказать!6o   �� 5   
   -   5        $��2��5      $��30��6   $	��100��6*   $��	200 0 0 0q�66   .��6e   Отмена :(6p   5      5      