   M S B A M L   `   `   `  ����J  FEtiquetasBlumar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:EtiquetasBlumarEtiquetasBlumar  5   	        EtiquetasBlumar.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:EtiquetasBlumar     5   	   M ITAlex.WPF.Controls, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null@talex4http://schemas.talex-soft.com/2010/xaml/presentation  5   	     � Title$  Etiquetas Blumar��5	   	   $	��600��6"   $	��850��6/   .��5
      � .+�5   
   �� $��Etiqueta Blumar��6   $
��Left=�6*   $��
380,20,0,0q�6E   $	��Top=�6Y   $��53��6q   $	��217��6}   $
��20pt(�6�   �� 5   
   $��Material��6   $
��Left=�6#   $��
380,70,0,0q�6>   $	��Top=�6R   $
��15pt(�6j   �� 5   
   -   5         ��Name$ cbxMaterial��5      $
��Left=�6(   $��
520,75,0,0q�6C   $	��Top=�6W   $	��200��6o    �� SelectedValuePath$	 Tag��6{   .@�5      �� $��Seleccione Material...��6   
 � Tag$ 0��6<   $
��Left=�6D   $	��198��6_   5      �� 5   
   $
��Lote��6   $
��Left=�6   $��380,105,0,0q�6:   $	��Top=�6O   $
��15pt(�6g   �� 5   
   -   5         ��Name$ txtLote��5       �� 	MaxLength$ 6��6#   $
��Left=�61   $��23��6L   $��520,110,0,0q�6X    �� TextWrapping$
 Wrap=�6m   $�� ��6�   $	��Top=�6�   $	��200��6�   6�   �� 5   
   $��Porción��6   $
��Left=�6"   $��380,175,0,0q�6=   $	��Top=�6R   $
��15pt(�6j   �� 5   
   -   5        $ 
txtPorcion��5      $ 2��6&   $
��Left=�64   $��23��6O   $��520,180,0,0q�6[   $
 Wrap=�6p   $�� ��6�   $	��Top=�6�   $	��200��6�   6�   �� 5   
   $��Correlativo��6   $
��Left=�6&   $��380,210,0,0q�6A   $	��Top=�6V   $
��15pt(�6n   �� 5   
   -   5        $ txtCorrelativo��5      $ 3��6*   $
��Left=�68   $��23��6S   $��520,215,0,0q�6_   $
 Wrap=�6t   $�� ��6�   $	��Top=�6�   $	��200��6�   6�   �� 5   
   $��Fecha��6   $
��Left=�6    $��380,140,0,0q�6;   $	��Top=�6P   $
��15pt(�6h   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ fechaPicker��5      $
��Left=�6*   $��520,145,0,0q�6E   $	��Top=�6Z   $	��200��6r   6~   �� 5   
   $��Envase��6   $
��Left=�6!   $��380,245,0,0q�6<   $	��Top=�6Q   $
��15pt(�6i   �� 5   
   -   5        $ 	cbxEnvase��5      $
��Left=�6&   $��520,255,0,0q�6A   $	��Top=�6V   $	��200��6n   $	 Tag��6z   .@�5      �� $ 7000003��6   $��JUMBO_1250KG��6)   $
��Left=�6@   $	��198��6[   �� 5      $ 7000004��6   $��ESLINGA 40X25 KG��6)   $
��Left=�6D   $	��198��6_   �� 5      $ 7000010��6   $��JUMBO_650KG��6)   $
��Left=�6?   $	��198��6Z   �� 5      $ 7000006��6   $��	SACO 25KG��6)   $
��Left=�6=   $	��198��6X   �� 5      $ 7000022��6   $��Eslinga 50x25 Kg��6)   $
��Left=�6D   $	��198��6_   5      �� 5    
   $��Origen��6   $
��Left=�6!   $��380,280,0,0q�6<   $	��Top=�6Q   $
��15pt(�6i   �� 5!   
   -   5        $ cbxOrigenes��5!      $
��Left=�6(   $��520,290,0,0q�6C   $	��Top=�6X   $	��200��6p   $	 Tag��6|   .@�5"      �� $ 0��6   $��Recursos��6#   $
��Left=�66   $	��198��6Q   �� 5#      $ 1��6   $��Harina��6#   $
��Left=�64   $	��198��6O   5$      �� 5%   
   $
��Tipo��6   $
��Left=�6   $��380,315,0,0q�6:   $	��Top=�6O   $
��15pt(�6g   �� 5&   
   -   5        $ cbxTipos��5&      $
��Left=�6%   $��520,325,0,0q�6@   $	��Top=�6U   $	��200��6m   $	 Tag��6y   .@�5'      �� $ 4��6   $��Producción��6#   $
��Left=�68   $	��198��6S   �� 5(      $ 2��6   $��Repaso��6#   $
��Left=�64   $	��198��6O   �� 5)      $ 3��6   $��Mezcla��6#   $
��Left=�64   $	��198��6O   �� 5*      $ 1��6   $��Rechazo��6#   $
��Left=�65   $	��198��6P   5+      �� 5,   
   $��Kilos��6   $
��Left=�6    $��380,350,0,0q�6;   $	��Top=�6P   $
��15pt(�6h   &   TAlex.WPF.Controls.NumericUpDown  5-   
   -	   5          Name$ cantToneladas��5-      $
��Left=�65   $��520,360,0,0q�6P   $	��Top=�6e   $��23��6}   $	��200��6�   	   Maximum$		 999~�6�   �� 5.   
   $��Centro��6   $
��Left=�6!   $��380,380,0,0q�6<   $	��Top=�6Q   $
��15pt(�6i   �� 5/   
   -
   5        $ comboCentro��5/      
 J� IsHitTestVisible
 .  |�.   �� 
IsEditable .  $	 Tag��6f   $
��Left=�6~   $��520,390,0,0q�6�   $	��Top=�6�   $	��200��6�   6�   �� 50   
   $��N° Etiquetas��6   $
��Left=�6'   $��380,410,0,0q�6B   $	��Top=�6W   $
��15pt(�6o     51   
   -   5        $ cantEtiquetas��51      $
��Left=�65   $��520,420,0,0q�6P   $	��Top=�6e   $��23��6}   $��60��6�   $		 999~�6�   6�   �� 52   
   $��Copias��6   $
��Left=�6!   $��585,410,0,0q�6<   $	��Top=�6Q   $
��15pt(�6i     53   
   -   5        $ copiasEtiquetas��53   )      Value$ 1~�6   $
��Left=�6B   $��660,420,0,0q�6]   $	��Top=�6r   $��23��6�   $��60��6�   $		 999~�6�   �� 55   
   -   5         ��Name$ btnImprimir��55      $��Imprimir��6&   $
��Left=�69   $��560,455,0,0q�6T   $	��Top=�6i   $	��160��6�   $��30��6�   6�   �� 56   
   -   5         ��Name$ imgLogo��56      $
��Left=�6!   $	��170��6<   $��	62,37,0,0q�6I   $	��Top=�6\   $	��230��6t   $
��Fill=�6�   �� 57   
   
���   �$��1q�6%   $
��Left=�69   $	��153��6T   $��
67,314,0,0q�6a   $	��Top=�6u   $	��250��6�   �� 58   
   -   5         ��Name$ lblLote��58      $��000000��6!   $
��Left=�62   $��
65,315,0,0q�6M   $	��Top=�6a   $
��15pt(�6y   �� 59   
   $��-��6   $
��Left=�6   $��135,315,0,0q�67   $	��Top=�6L   $
��15pt(�6d   �� 5:   
   -   5        $ lblCorrelativo��5:      $	��000��6(   $
��Left=�66   $��145,315,0,0q�6Q   $	��Top=�6f   $
��15pt(�6~   �� 5;   
   $��-��6   $
��Left=�6   $��180,315,0,0q�67   $	��Top=�6L   $
��15pt(�6d   �� 5<   
   -   5        $ 
lblPorcion��5<      $��00��6$   $
��Left=�61   $��195,315,0,0q�6L   $	��Top=�6a   $
��15pt(�6y   �� 5=   
   $��/��6   $
��Left=�6   $��225,315,0,0q�67   $	��Top=�6L   $
��15pt(�6d   �� 5>   
   -   5        $ 
lblJuliano��5>      $	��000��6$   $
��Left=�62   $��235,315,0,0q�6M   $	��Top=�6b   $
��15pt(�6z   �� 5?   
   $��-��6   $
��Left=�6   $��275,315,0,0q�67   $	��Top=�6L   $
��15pt(�6d   �� 5@   
   -   5        $ lblAño��5@      $��00��6    $
��Left=�6-   $��285,315,0,0q�6H   $	��Top=�6]   $
��15pt(�6u   �� 5A   
   -   5        $ 	imgQrCode��5A      $
��Left=�6#   $	��100��6>   $��135,352,0,0q�6K   $	��Top=�6`   $	��110��6x   �� 5B   
   -   5        $ btnAdmin��5B      $
��Left=�6#   $��380,447,0,0q�6>   
�������$��0q�6f   #��[" �6z   $	��Top=�6�   $��50��6�   .��5C      �� .+�5D      �� $��img/administrador.png��6   5E      5F      �� 5G   
   -   5        $ 
btnRefresh��5G      $
��Left=�6%   $��285,260,0,0q�6@   
�������$��0q�6h   #��[" �6|   $	��Top=�6�   $��50��6�   .��5H      �� .+�5I      �� $��img/refreshBlue.png��6   5J      5K      �� 5L   
   -   5         ��Name$ reimpresion��5L      $��Reimpresión de etiqueta��6(   $��13(�6J   $
��Left=�6X   $��560,492,0,0q�6s   $	��Top=�6�   �� 5M   
   -   5        $ 
lblRechazo��5M      $y�Hidden=�6$   $�� ��68   $
��Left=�6C   $��
86,460,0,0q�6^   $	��Top=�6r   $��49��6�   $��54��6�   �� 5N   
   -   5        $ testeo��5N      $��Version de Desarrollo��6    $y�Hidden=�6@   $
��Left=�6T   $��	10,11,0,0q�6o   $	��Top=�6�    J� RenderTransformOrigin$ -1.723,0.511&�6�   G� 5O   
   -   5         G�Name$ adminPassword��5O       G� 	MaxLength$ 8��6-   $y�Visible=�6;    G� PasswordChar$ *��6P   
����  �$
��Left=�6w   $��446,467,0,0q�6�   $	��Top=�6�   $��99��6�   6�   �� 5P   
   -   5        $ unlockCentroCkb��5P      $y�Hidden=�6,   $��Desbloquear��6@   $��13(�6V   $
��Left=�6d   $��446,417,0,0q�6   $	��Top=�6�   6�   �� 5Q   
   -   5        $ btnTest��5Q      $y�Hidden=�6"   $
��Test��66   $
��Left=�6E   $��	10,37,0,0q�6`   $	��Top=�6s   $��47��6�   $��25��6�   5S      5T      