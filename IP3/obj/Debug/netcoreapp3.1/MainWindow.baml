   M S B A M L   `   `   `  ����>  :IP3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IP1IP1  5   	       IP1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:IP1     5   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)   .��5	      � ���	#FFE0E0E0.+�5
   
   �� -   5         ��Name$ image2��5
      $
��Left=�6    $	��200��6;   $��
839,58,0,0q�6H   $	��Top=�6\   $	��180��6t   �� 5   
   -   5        $ image1��5      $
��Left=�6    $	��442��6;   $��
178,58,0,0q�6H   $	��Top=�6\   $	��652��6t   �� 5   
   -   5        $ 	imageOrig��5      $
��Left=�6#   $	��193��6>   $��	10,60,0,0q�6K   $	��Top=�6^   $	��163��6v   �� 5   
   -   5         ��Name$ origText��5      $��Оригинал:��6"   $
��Left=�66   $��	65,20,0,0q�6Q   $	��Top=�6d   $��70��6|   �� 5   
   -   5        $ label1��5      $��Результат 1:��6    $
��Left=�67   $��
223,20,0,0q�6R   $	��Top=�6f   $	��142��6~   �� 5   
   -   5        $ label2��5      $��Результат 2:��6    $
��Left=�67   $��
908,10,0,0q�6R   $	��Top=�6f   $	��125��6~   �� 5   
   -   5         ��Name$ ButtonLoadImage��5      $/��)Загрузить изображение��6*   $
��Left=�6J   $��
10,264,0,0q�6e   $	��Top=�6�   $	��150��6�   $��31��6�   �� 5   
   -   5        $ Convert��5      $��Выполнить��6"   $
��Left=�66   $��1072,206,0,0q�6Q   $	��Top=�6g   $��89��6   $��22��6�   �� 5   
   -	   5        $ label��5      $��Время работы :��6   $
��Left=�68   $��1076,257,0,0q�6S   $	��Top=�6i   $��28��6�   $��84��6�   �� 5   
   -
   5        $ 	time1Text��5      $�� ��6#   $
��Left=�6.   $��259,300,0,0q�6I   $	��Top=�6^   $��70��6v   �� 5   
   -   5        $ 	time2Text��5      $�� ��6#   $
��Left=�6.   $��1165,262,0,0q�6I   $	��Top=�6_   $��70��6w   �� 5   
   -   5        $ qualityText��5      $��Качество :��6%   $
��Left=�6:   $��1086,283,0,0q�6U   $	��Top=�6k   $��70��6�   �� 5   
   -   5        $ 
qualityNum��5      $�� ��6$   $
��Left=�6/   $��1161,282,0,0q�6J   $	��Top=�6`   $��60��6x   �� 5   
   -   5        $ buttonSave1��5      $
��Save��6&   $
��Left=�65   $��267,515,0,0q�6P   $	��Top=�6e   6}   �� 5   
   -   5        $ buttonSave2��5      $
��Save��6&   $
��Left=�65   $��968,276,0,0q�6P   $	��Top=�6e   6}   	� 5   
   -   5         	�Name$ radioButtonCanny��5      $��Границы Кэнни��60   $
��Left=�6H   $��1072,58,0,0q�6c    	� 	GroupName$ Select��6x   $	��Top=�6�   $��16��6�   $	��114��6�   
������� d� 	IsChecked$
 TrueY�6�   	� 5   
   -   5        $ radioButtonHough��5      $4��.Фильтр Хаффа(Очень Долго)��60   $
��Left=�6T   $��1072,82,0,0q�6o   $ Select��6�   $	��Top=�6�   $��18��6�   $	��198��6�   �� 5   
   -   5        $ dblimits��5      $*��$Границы фильтрации:��6"   $
��Left=�6@   $��1129,23,0,0q�6[   $	��Top=�6p   $	��141��6�   $��30��6�   �� 5   
   -   5         ��Name$ textBoxLowDF��5      $
��Left=�6(   $��1181,58,0,0q�6C   $	��100��6X    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $��29��6�   $��19��6�   �� 5   
   -   5        $ textBoxHighDF��5      $
��Left=�6)   $��1215,58,0,0q�6D   $	��255��6Y   $
 Wrap=�6d   $	��Top=�6x   $��30��6�   $��19��6�   �� 5   
   -   5        $ buttonPushLeft1��5      $��ToOrig��6*   $
��Left=�6;   $��223,515,0,0q�6V   $	��Top=�6k   6�   �� 5   
   -   5        $ buttonPushLeft2��5      $��ToOrig��6*   $
��Left=�6;   $��908,276,0,0q�6V   $	��Top=�6k   6�   5       5!      