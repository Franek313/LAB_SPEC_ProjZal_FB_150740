   M S B A M L   `   `   `  ���� K  GGeneratorEtykiet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GeneratorEtykietGeneratorEtykiet  !    GeneratorEtykiet.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:GeneratorEtykiet       � Title$  Generator Etykiet��$	��700��$
��1650��.��� -    �Name$ interfaceGrid�� � ColumnDefinitions �� $��* ��� $
��1595 ��� $��* � � RowDefinitions �� $��* ��� $	��550 ��� $��* ��� $	��100 �.+��� $��0��$��1��
����ح��� $��2��$��1��
����ح��� $��0��$��0��$��3��
����ح��� $��0��$��2��$��3��
����ح�� -   $ 	imageGrid��$��1��$��1�� �� $��* � �� $��* �.+��� $ ��Images/pusta_etykieta2.png��$
��Auto��$
��Auto��$��0��$��0��$��2���� -    ��Name$ BarCode��$��0��$��0��$��1219,248,53,40q��� -    ��Name$ Kod_kreskowy�� �� TextAlignment$ Center=� �� 	MaxLength$ 13��$��0��$��0��$��0q�$��30(�$��1203,475,37,32q�
������ $��Tu wpisz kod kreskowy...���� -   $ Nazwa��$��0��$��DemiBold"� �� TextWrapping$
 Wrap=�$��0��$��0q�$��60(�
������ $��31,58,725,318q�.��Tęczowa Półka�� -   $ Opis_PL��$��0��$��0��$��0q�$��40(�$ WrapWithOverflow=�$��31,246,834,165q�
������ .��3 półkowa�� -   $ 
Dimensions��$��0��$��0��$��0q�$��28(�$��31,385,834,119q�
������ .��=;wysokość 116,5cm, szerokość 120cm, głębokość 31,5cm�� -   $ MadeIn��$��0��$��0��$��0q�$ Right=�$��25(�$��712,460,433,54q�
������ .��Wyprodukowano w Polsce�� -	   $ Box_X_Dimension��$��0��$��0��$��0q�$��40(�$ Center=�$��889,58,467,432q�
������ .��33cm�� -
   $ Box_Y_Dimension��$��0��$��0��$��0q�$��40(�$ Center=�$��759,186,524,298q�
������ 	 J� RenderTransformOrigin$	 0.5,0.5&�.��z�V� .~��� �� �� 
 �� Angle$	
 -90Q�T� .��123cm�� -   $ Box_Z_Dimension��$��0��$��0��$��0q�$��32(�$ Center=�$��998,180,293,332q�
������ $	 0.5,0.5&�.��z�V� .~��� ��  �� AngleX$ 30Q��� $
 90Q�T� .��11cm�� -   $ Weight��$��0��$��0��$��0q�$��70(�$ Center=�$��889,275,467,175q�
������ .��13,5kg�� -   $ Symbol��$��0��$��0��$��0q�$��20(�$ Center=�$��886,385,467,124q�
������ .��3_SHELVE_120_H�� -   $	 Kod��$��0��$��0��$��0q�$��25(�$ Right=�
������ $��NOCODE��$��1203,0,37,0q�$��40��$	��Top=�� -   $ UIGrid��$��0��$��3��$��3�� �� $��* ��� $��* ��� $��* ��� $��* � �� $��* �.+��� -   $��20(�$��0��.��ANULUJ�� -   $	 CMD��$��20(�$��1��.��ANULUJ�� -   $��20(�$��2��.��EKSPORTUJ JAKO PNG�� -   $��20(�$��3��.��ZAPISZ SZABLON