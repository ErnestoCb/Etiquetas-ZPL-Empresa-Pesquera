   M S B A M L   `   `   `  ����J  FEtiquetasBlumar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:EtiquetasBlumarEtiquetasBlumar  5   	        EtiquetasBlumar.tablaExcel   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:EtiquetasBlumar     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
tablaExcel��5	   	   $	��450��6   $	��800��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��133* �6   �� 5      $
��660* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dtGrid��5   ]      AutoGenerateColumns .    
IsReadOnly . $��Center=�6@   $��Center=�6k   $	��272��6�   $��63,47,73,100q�6�   $	��656��6�   $��2��6�   �� 5   
   -   5         ��Name$ 	btonExcel��5      $��Excel��6$   $
��Left=�64   $��337.5,360,0,0q�6O   $	��Top=�6f   $��66��6~   $��36��6�   $��1��6�   �� 5   
   -   5        $ 	btnVolver��5      $��Volver��6$   $
��Left=�65   $��457.5,376,0,0q�6P   $	��Top=�6g   $��75��6   $��1��6�   �� 5   
   -   5        $��txtFilePath��5      $��30��6   $��25,366,367,23q�61   $��10(�6H    J� Opacity$ 1Q�6V   $��2��6b   �� 5   
   -   5        $ 
printExcel��5      $��Imprimir��6%   $
��Left=�68   $��537.5,345,0,0q�6S   $	��Top=�6j   $��92��6�   $��32��6�   $��1��6�   �� 5   
   -   5         ��Name$ reimpresion��5      $��Reimpresión de Etiqueta��6(   $
��Left=�6J   $��537.5,396,0,0q�6e   $	��Top=�6|   $��1��6�   5      5      