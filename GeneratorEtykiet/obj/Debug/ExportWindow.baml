   M S B A M L   `   `   `  ����K  GGeneratorEtykiet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GeneratorEtykietGeneratorEtykiet  5   	   #    GeneratorEtykiet.ExportWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GeneratorEtykiet     5   	     � Title$  MANAGER ETYKIET��5   	   $	��600��6!   $	��800��6.    � Icon 5
      �� 5   
    �� 	UriSource$ Images/icon.ico?�6   .��5      � -   5         �Name$ mainGrid��5      
����ح� � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��10,10,10,10q�6   
������� 5      �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��30 �6    �� 5      $��10 �6    �� 5      $��* �6    .+�5      �� $��1��6   
����ح�6;   �� 5      -   5        $��Dodaj etykietę��5      $��16(�6.   6<   � 5!      -   5        $ ___No_Name_��5!      $��2��6(    5"      �� 5#      $��* �6'    5%      �� 5&      $��* �6$   .+�5)      �� $
��Auto=�6   .��5*      �� .+�5+      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5+   #   $��0��65   $��16(�6E   $��1��6S   $��2��6`      AutoGenerateColumns .     Columns 5,      4  .System.Windows.Controls.DataGridTemplateColumn  5-   "   ,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 5* 69   
   Header$
 Nazwa��6D      CellTemplate 5.   &   �� 5/   *   .R�50   .   �� -   5        ��50   7   �� *ListaElementow+ �� SelectedItem 6^   �� *WybranyElement+6�   51   +   53   #     54   "   $
	 1.5* 69   $
 Słoje��6F    55   &   �� 56   *   .R�57   .   �� -   5        ��57   7   �� *Sloje+ 6U   �� *WybranySloj+6�   58   +   5:   #   0  *System.Windows.Controls.DataGridTextColumn  5;   "   $	 * 65   $
 Ilość��6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6N   �� *Ilosc+  5<   "   $
  ��69   $	 30 6C    5=   &   �� 5>   *   .R�5?   .   �� -   5        $
��Bold"�5?   5   $��X��6G   
���  ��$��2q�6i   
���  ��
�������6�   5@   +   5B   #   5D      5E      5F      5H      5I      � 5J   
   $��1��6   $��10,10,10,10q�6    5K      �� 5L      $��* �6#    5N      �� 5O      $��30 �6    �� 5P      $��* �6    �� 5Q      $��30 �6    .+�5U      �� -   5         ��Name$
 LOGS��5U      $��1��6$   .��61   Console logs...6B   �� 5V      -	   5        $��3��5V      $��16(�6"   .��6G   	Eksportuj6R   5W      5X      5Y      