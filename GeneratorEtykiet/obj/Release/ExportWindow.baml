   M S B A M L   `   `   `  ���� K  GGeneratorEtykiet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GeneratorEtykietGeneratorEtykiet  #    GeneratorEtykiet.ExportWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:GeneratorEtykiet       � Title$  MANAGER ETYKIET��$	��600��$	��800�� � Icon ��  �� 	UriSource$ Images/icon.ico?�.��� -    �Name$ mainGrid��
����ح� � ColumnDefinitions �� $��3* ��� $��* �.+�� $��0��$��10,10,10,10q�
������� �� $��* � � RowDefinitions �� $��30 ��� $��10 ��� $��* �.+��� $��1��
����ح��� -   $��Dodaj etykietę��$��16(�� -   $ ___No_Name_��$��2�� �� $��* � �� $��* �.+��� $
��Auto=�.���� .+�&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$��0��$��16(�$��1��$��2��   AutoGenerateColumns .     Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 5* 
   Header$
 Nazwa��   CellTemplate �� .R��� -   ���� *ListaElementow+ �� SelectedItem �� *WybranyElement+  $
	 1.5* $
 Słoje�� �� .R��� -   ���� *Sloje+ �� *WybranySloj+0  *System.Windows.Controls.DataGridTextColumn  $	 * $
 Ilość��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Ilosc+  $
  ��$	 30  �� .R��� -   $
��Bold"�$��X��
���  ��$��2q�
���  ��
�������� $��1��$��10,10,10,10q� �� $��* � �� $��30 ��� $��* ��� $��30 �.+��� -    ��Name$
 LOGS��$��1��.��Console logs...�� -	   $��3��$��16(�.��	Eksportuj