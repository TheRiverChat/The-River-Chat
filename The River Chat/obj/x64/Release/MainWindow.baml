   M S B A M L   `   `   `  ���� I  EThe River Chat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:The_River_ChatThe_River_Chat      The_River_Chat.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:The_River_Chat     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  	The River��$	��450��$	��800��$	��450��$	��800�� � 	Resources ��%    .��� .+���  �� Orientation$ 
Horizontal=�$��Stretch=�$��Stretch=�.+��� -    ��Name$ 	listwleft��$
��Left=�$	��401��$��	10,10,0,0q�$	��Top=�$	��124��
���   �$	��0.5q� �� View �� .P��� �� $��Stretch=�$��1��$��10q�
���   �]� ��. $��	155 5 5 5q�.+��� $ 
Horizontal=�$��Center=�$��Center=�$	��Top=�.+��� -    ��Name$ chatname��$��	Chat-Name��$	��Top=�$��16(��� -    ��Name$ connect_btn�� $   MaterialDesignRaisedAccentButton#��[  $	��100��$��	250,0,0,0q� � ToolTip$5 /Resource name: MaterialDesignRaisedAccentButton�����	#FF00EAEA���	#FF0095EA.��	Connect�� -   $ ECButton��
������ $��0q�
������ $��10 0 0 0q�.��� .+�'  !MaterialDesignThemes.Wpf.PackIcon  -     Name$ ECIcon_Open_Warning��	   Kind$	 LockOpenAlert=�$��Center=�$��Center=�$��30��$��30��
���  ��$y�Visible=�  -   $ ECIcon_Open��$	 LockOpen=�$��Center=�$��Center=�$��30��$��30��
��� ���$y�Hidden=�  -   $ ECIcon_Locked��$	 	LockCheck=�$��Center=�$��Center=�$��30��$��30��
��� � �$y�Hidden=�]� $��Bottom=�$��Stretch=���. $��Bottom=�$��0 0 0 10q�.+��� -   
 ��Name$
 text_tosend��$��35�� �� TextWrapping$
 Wrap=�$�� ��$	��460��$��0q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Type text...���� -	   $ add_file��$��40��$��40��
������ 
������ $��0q�$��Right=�.��  $	 	Paperclip=�
������ 
���   �$��30��$��30��$��Center=�$��Center=��� -
   $ send_btn��#��[  $	��100��$��35��$	��Top=�$��Right=�$5 /Resource name: MaterialDesignRaisedAccentButton�����	#FF00EAEA���	#FF0095EA.��SEND�� -    ��Name$ sv��
���   �$��1q�$	��Top=�$��155 45 5 60q�.���� -    ��Name$ st��$ Vertical=�� -    �Name$ nw_ms��$	��300��$��30��$��Bottom=�$��Center=�$��
100,0,0,60q� � Cursor$
 Hand��$y�Hidden=�.+��� $��1q�$��0��$��2�� �� CornerRadius$ 	20 20 0 0��
������ 
����� ��� $��New messages��$��20(�$��Center=�$��Center=�]� -    ]�Name$ DisplayNameDockPanel����. $��Stretch=�$��Stretch=�$y�Hidden=�����  �� Color$ Black�� �� Opacity$	 0.4Q�.+��� $��Center=�$��Center=�$��Bottom=�$	��200��$	��270��
�������$��1q�$ 20�� J� Opacity$ 1Q�._��� $ Vertical=�$��Center=�$��Center=�$��10q�.+��� $��What's your display-name?��$��Center=�$��Center=�
���   �$��20(�$��verdana)�$��Normal$��� -   $
 	dname_box��$��
30 30 30 0q� * &MaterialDesignOutlinedTextFieldTextBox#��[ $	��Top=�$��15(� �� AcceptsReturn .  $
 Wrap=�$ Display-Name���� -   $ dname�� " MaterialDesignRaisedDarkButton#��[ $��35��+  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 5��$��
90 30 90 0q�$7 1MaterialDesignRaisedDarkButton with Round Corners�����	#FF00EAEA���	#FF0095EA.���� $
��Okay��
���   �]� -   $ EncryptCodeDockPanel����. $��Stretch=�$��Stretch=�$y�Hidden=����� $ Black��$	 0.4Q�.+��� $��Center=�$��Center=�$��Bottom=�$	��200��$	��270��
�������$��1q�$ 20��$ 1Q�._��� $ Vertical=�$��Center=�$��Center=�$��10q�.+��� $0��*Type any 8 char and 8 numbers (255 is max)��$��Center=�$��Center=�
���   �$��20(�$��verdana)�$��Normal$��� -   $ 
char_panel��$ 
Horizontal=�$��Center=�.+��� $��Chars: ��$��verdana)�
���   �$��Bottom=�$��0 0 0 2q�$��16(��� -   $
 	charbox_1�� % !MaterialDesignFloatingHintTextBox#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=� �� TextAlignment$ Center=�$��20���� -   $
 	charbox_2��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� -   $
 	charbox_3��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� -   $
 	charbox_4��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� -   $
 	charbox_5��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� -   $
 	charbox_6��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� -   $
 	charbox_7��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� -   $
 	charbox_8��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ Center=�$��20���� $��1��$��
50 20 50 0q�
���   ��� -   $ 	num_panel��$ 
Horizontal=�$��Center=�.+��� $��	Numbers: ��$��verdana)�
���   �$��Bottom=�$��0 0 0 2q�$��16(��� -   $
 numbox_1��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=� �� 	MaxLength$ 3��$ Center=�$��20���� -   $
 numbox_2��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -   $
 numbox_3��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -   $
 numbox_4��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -    $
 numbox_5��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -!   $
 numbox_6��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -"   $
 numbox_7��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -#   $
 numbox_8��#��[ $�� ��$��8 0 8 0q�$��Bottom=�$��Center=�$ 3��$ Center=�$��20���� -$   $ SetEncryptKeys��#��[ $��35��$ 5��$��
90 30 90 0q�$7 1MaterialDesignRaisedDarkButton with Round Corners�����	#FF00EAEA���	#FF0095EA.���� $
��Okay��
���   �