   M S B A M L   `   `   `  ����G  CPacketRouter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PacketRouterPacketRouter  5      #    PacketRouter.TunnerController   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PacketRouter     5      $��Stretch=�5	      $��Stretch=�5
      .��5      � $��Stretch=�6   $��Stretch=�6)   .+�5   
   �� 
���   �$��2q�6%   $��Stretch=�69   $��5,5,5,5q�6W   $��Stretch=�6h   ._�5      � $��Stretch=�6   $��Stretch=�61   .+�5      �� -   5          ��Name$  netIntfACombo��5      $
��Left=�62   $��	10,10,0,0q�6M   $	��Top=�6`   $	��150��6x   �� 5      -   5        $  netIntfBCombo��5      $��Right=�62   $��	0,10,10,0q�6N   $	��Top=�6a   $	��150��6y   �� 5      -   5         ��Name$ startChannelButton��5      $��START��65   $��Center=�6E   $��
-50,0,0,10q�6b   $��Bottom=�6v   $��40��6�   6�   �� 5      -   5        $ stopChannelButton��5      $
��STOP��64   $��Center=�6C   $��	50,0,0,10q�6`   $��Bottom=�6s   $��40��6�   6�   �� 5      -   5         ��Name$ 
channAPort��5      $
��Left=�6.   $��22��6I   $��	10,0,0,10q�6U    �� TextWrapping$
 Wrap=�6h   $��45000��6|   $��Bottom=�6�   $��50��6�    �� TextAlignment$
 Left=�6�   �� 5      -   5        $ 
channBPort��5      $��Right=�6.   $��22��6J   $��	0,0,10,10q�6V   $
 Wrap=�6i   $��45001��6}   $��Bottom=�6�   $��50��6�   $ Right=�6�   �� 5      -   5        $ channelStatusA��5      {�.  $��Center=�6D   $��22��6a   $��
-70,10,0,0q�6m   $
 Wrap=�6�   $��NC��6�   $	��Top=�6�   $��60��6�   $ Center=�6�   $
��Bold"�6�   
���  ��6�   �� 5      -   5        $ channelStatusB��5      {�.  $��Center=�6D   $��22��6a   $��	70,10,0,0q�6m   $
 Wrap=�6�   $��NC��6�   $	��Top=�6�   $��60��6�   $ Center=�6�   $
��Bold"�6�   
���  ��6�   �� 5      -	   5        $  netAddressesA��5      $
��Left=�62   $��
10,-30,0,0q�6M   $��Center=�6a   $	��150��6|   �� 5      -
   5        $  netAddressesB��5      $��Right=�62   $��
0,-30,10,0q�6N   $��Center=�6b   $	��150��6}   �� 5      -   5        $ remoteEndpointAText��5      $
��Left=�67   $��22��6R   $��	10,30,0,0q�6^   $
 Wrap=�6q   $��Non Connesso��6�   $��Center=�6�   $	��150��6�   {�.  $
 Left=�6�   �� 5      -   5        $ remoteEndpointBText��5      $��Right=�67   $��22��6S   $��	0,30,10,0q�6_   $
 Wrap=�6r   $��Non Connesso��6�   $��Center=�6�   $	��150��6�   {�.  $ Right=�6�   5      5      5      5      