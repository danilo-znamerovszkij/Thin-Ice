   M S B A M L   `   `   `  ����C  ?Thin Ice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Thin_Ice.ViewThin_Ice.View  5   	       Thin_Ice.View.GameView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Thin_Ice.View     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	     � Title$  Thin Ice��5	   	    � 
ResizeMode$ NoResize=�5
   	   $	��550��5   	   $	��550��6    J� InputBindings 5      �� 5   
   
 �� Key$ Up��6    �� Command 6   �� *UpKeyPressedCommand+6H   �� 5   
   $ Right��6    6!   �� *RightKeyPressedCommand+6N   �� 5   
   $
 Down��6    6    �� *DownKeyPressedCommand+6L   �� 5   
   $
 Left��6    6    �� *LeftKeyPressedCommand+6L   �� 5   
   $ P��6    6   �� *PKeyPressedCommand+6F   �� 5   
   $ R��6    6   �� *RKeyPressedCommand+6F   �� 5   
   $ M��6    6   �� *MKeyPressedCommand+6F   �� 5   
   $ Enter��6    6!   �� *EnterKeyPressedCommand+6N   .��5      (� .��5   
   �� -   5        $��	GameBoard��5   +   
����·�$	��550��6<   $	��525��6H   .+�5      ��  K� ZIndex$ 3��5!      ��5      �� *GameLogic.Player.WidthPixels+ �� Mode$ OneWay=���5       �� *GameLogic.Player.HeightPixels+$ OneWay=���5#      �� 5$       �� ImageSource$D >pack://application:,,,/Thin Ice;component/Resources/player.png��6!   z�5&      V� 5'      .~�5(      T�  T� X 6-   �� *GameLogic.Player.XPosition+$ OneWay=�	 T� Y	 5)   -   �� *GameLogic.Player.YPosition+$ OneWay=�5*      5+      5-      �� 5/      $	��400��6   $	��350��50      $ 4��51      
�������
 �� Left$
 75��53      
 �� Top$ 75��54       J� Opacity 55      �� *GameLogic.IsOnPause+57      �� 59      $ 5��5:      "  WpfAnimatedGif.ImageBehavior   RepeatBehavior$ Forever��5<         AnimatedSource$J Dpack://application:,,,/Thin Ice;component/Resources/start_screen.gif��5=       5;      �� *GameLogic.MainMenuVisibility+5=      �� 5@      $	��550��6   $	��525��5A      $ 5��5B       5C      �� *$"GameLogic.CongratsScreenVisibility+��5E      �� 5F      $H Bpack://application:,,,/Thin Ice;component/Resources/end_screen.jpg��6!   5H      �� 5J       �� TextAlignment$ Center=�6P   $�� ��6g   $ 6��6o   
����  �$��Century Gothic)�5K   '   $��50(�5L      $��ExtraExpanded&�5M      $��Italic$�5N      $��	UltraBold"�5O       �� 
LineHeight$
 Auto��5Q       �� Padding$ 390,230,150,20q�5S       K� NumeralStyle$ OldStyle=�5T       K� SlashedZero .  5J      �� *$"GameLogic.CongratsScreenVisibility+�� 5W      �6   �� *GameLogic.Level+$ OneWay=�6F   �� 5X      6   �� 6)   65   �� 5Y      �6   �� *GameLogic.Score+$ OneWay=�6F   5Z      �� 5]      $ Center=�6@   $�� ��6W   $ 5��6_   
������$��Century Gothic)�5^   )   $��23(�5_      $��ExtraExpanded&�5`      $��	UltraBold"�5b      $
 Auto��5d      $ 150,115,150,20q�5e      $ OldStyle=�5g       .  5]      �� *GameLogic.IsOnPause+�� 5j      .�6   Exit pause menu (P)6+   �� 5k      6   �� 6)   65   �� 5l      6   �� 6)   65   �� 5m      .�6   Restart the game (R)6,   �� 5n      6   �� 6)   65   �� 5o      6   �� 6)   65   �� 5p      .�6   Back to main menu (M)6-   5q      �� 5r      $ 5��5s      $ Forever��5u      $A ;pack://application:,,,/Thin Ice;component/Resources/rip.gif��5v       5t      �� *GameLogic.IsLevelLost+5v      � 5y      .+�5{      �� $
��Left=�6G   $	��Top=�6b   ��6   �� *GameLogic.Board+��5|      �� 5}      .R�5~      ��  K� IsItemsHost . 6%   5      ��5�      �� 5�      .R�5�      �� $��2��5�   $   ��5�   (   �� *WidthPixels+$ OneWay=���5�   $   �� *HeightPixels+$ OneWay=���5�   "   �� 5�   &    61   �� *Image+$ OneWay=�5�      5�      ��5�      �� 5�      .�5�      ��  �� Property �@��Left �� Value 6<   �� *	XPosition+�� 5�       �@��Top 6;   �� *	YPosition+5�      5�      �� 5�      $ Right=�6   
������
�������$��Century Gothic)�5�   (   $��13(�5�      $��ExtraExpanded&�5�      $��	UltraBold"�5�      $
 Auto��5�      $ 400,2,150,2q�5�      $ OldStyle=�5�       . ��5�   2   �� *GameLogic.Score+ �� StringFormat$ 
SCORE: {0}��5�      �� 5�      $
 Left=�6   $��Pause(P)��61   
�������$��Century Gothic)�5�   (   $��15(�5�      $��Medium&�5�      $��Medium"�5�      $
 Auto��5�      $ 10,2,0,0q�5�      $ OldStyle=�5�       . 5�      �� 5�      $
 Left=�6   $��Press R to restart��61   
�������$��Century Gothic)�5�   (   $��15(�5�      $��Medium&�5�      $��Medium"�5�      $
 Auto��5�      $ 	150,2,0,0q�5�      $ OldStyle=�5�       .  5�      �� *GameLogic.IsLevelLost+5�      5�      5�      5�      5�      