   M S B A M L   `   `   `  ���� F  BAudioWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AudioWpfAppAudioWpfApp      AudioWpfApp.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:AudioWpfApp     -     � Title$$  Musiikin metadata-automaattori��$	��800��$
��1200�� J� 	AllowDrop$
 True�� � Icon$ Icon/appicon_icon.ico�� � 	Resources  �� 
TargetType �� Property �� Value��%�  '��       '�� ?     '�� �     '�� �     '��     '�� H    �� " ��.���  � ��$ 15(���  � ��$ SemiBold"��� " ��.���  � ��$ 14(���  � ��$ Normal"���  � ��$ 2q��� " ��.���  � ��$ 14(���  � ��$ Normal"��� " ��.���  � ��$ 14(���  � ��$ Normal"��� " ��.���  � ��$ 14(���  � ��$ Normal"��� " ��.���  � ��$ 13(���  � ��$ Normal"�.��]� $
��True��.+�� $��20q�$	��Top=� � ColumnDefinitions �� $
��0.5* ��� $��* ��� $��* � � RowDefinitions �� $
��0.3* ��� $��* �.+�� $��0��$��0��$��0 0 0 20q� �� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   	 ��Name$	 CustomIDTextBlock��$��Kartuntatunnus��$��Center=�$��0��$��0��$��3q��� -   
 ��Name$
 CustomIDTextBox��$	��200��$��1��$��0���� -   $#	 CustomIDErrorMessageTextBlock��$��2��$��0��
���  ��$	��200��$��30��$
��Wrap=�$��12(�$��Normal"��� -   $	 TrackNumberTextBlock��$��	Uranumero��$��Center=�$��3��$��0��$��3q��� -   $
 TrackNumberTextBox��$	��200��$��4��$��0��� $��0��$��1��$��
20 0 20 20q� �� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $	 AlbumNameTextBlock��$��Albumin nimi��$��Center=�$��0��$��0��$��3q��� -   $
 AlbumNameTextBox��$��1��$��0���� -	   $$	 AlbumNameErrorMessageTextBlock��$��2��$��0��
���  ��$	��200��$��30��$
��Wrap=�$��12(�$��Normal"��� -
   $	 TrackNameTextBlock��$��Kappaleen nimi��$��Center=�$��3��$��0��$��3q��� -   $
 TrackNameTextBox��$��4��$��0��� $��0��$��2��$��	20 0 0 20q� ��%W   '��       �� " ��.���  � ��$ 5q���  � ��$ 5q���  � ��$ 70�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto �.+��� $��Kieli��$��0��$��0��$��3q��� -   $
 LanguageTextBox��$	��120��$��75��$��1��$��0��$��0 0 10 0q� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=� �� 
IsReadOnly$
 True��.�� � ContextMenu �� .@�m� -   $��Poista��.��� $��1��$��1�� �� $��* ��� $��* �.+��� -    ��Name$ suomiButton��$��suomi��$��0��$��13(��� -   $ englantiButton��$��englanti��$��1��$��13(�� $��1��$��2�� �� $��* ��� $��* �.+��� -   $ ruotsiButton��$��ruotsi��$��0��$��13(��� -   $ koreaButton��$��korea��$��1��$��13(�� $��1��$��3�� �� $��* ��� $��* �.+��� -   $ espanjaButton��$��saame��$��0��$��13(��� -    ��Name$ otherLanguagesComboBox��$��1��$��5q� �� 
IsEditable$
 True�� �� IsTextSearchEnabled$
 True��.@��� $��espanja���� $��islanti���� $��italia���� $��japani���� $��kiina���� $��norja���� $��	portugali���� $��ranska���� $��saksa���� $��sanaton���� $��tanska���� $��tunnistamaton���� $��ukraina���� $��venäjä���� $
��viro���� $��0��$��1��.+��  �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $	 CatalogueNumberTextBlock��$��Kataloginumero��$��Center=�$��0��$��0��$��3 20 3 3q��� -   $
 CatalogueNumberTextBox��$	��200��$��1��$��0��$��0 0 0 15q��� -   $	 ISRCTextBlock��$
��ISRC��$��Center=�$��2��$��0��$��3q��� -   $
 ISRCTextBox��$	��200��$��3��$��0��$��0 0 0 15q��� -   $	 RecordingYearTextBlock��$��Äänitysvuosi��$��Center=�$��4��$��0��$��3q��� -   $
 RecordingYearTextBox��$	��200��$��5��$��0��$��0 0 0 15q��� -   $	 PublicationYearTextBlock��$��Julkaisuvuosi��$��Center=�$��6��$��0��$��3q��� -   $
 PublicationYearTextBox��$	��200��$��7��$��0��$��0 0 0 15q��� -   $	 RecordLabelTextBlock��$��Levy-yhtiö��$��Center=�$��8��$��0��$��3q��� -   $
 RecordLabelTextBox��$	��200��$��75��$��9��$��0��$
 Wrap=�$
 Auto=�$��0 0 0 15q��� -    ��Name$ FinnishContentCheckBox��$��Fennica��$��10��$��0��$��3q��� -   $ YleMasterCheckBox��$��
Yle master��$��11��$��0��$��3q��� $��1��$��1��$��
20,20,20,0q�.+�� $��0,0,0,10q� �� $
��Auto ��� $
��Auto �.+��� $��Esiintyjät��$��Center=��� -    $��+Lisää esiintyjä��$��10,0,0,0q�$��1��$
��10 0q�&   System.Windows.Controls.DataGrid  -!     Name$ PerformerDataGrid��   CanUserAddRows$ False��   AutoGenerateColumns$ False��$	��200��   HeadersVisibility$ Column=�$��0,0,0,40q�$
 True��   RowStyle �� )  #System.Windows.Controls.DataGridRow"  .�-"    =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � ��
 ���@���  � ��$ 1,1,1,2q���  � ��
 �   ���  � ��$ 5,0,0,0q���  � ��$ SemiBold"���  � ��$ 13(�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Nimi��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PerformerName+  $ Soitin��$ 3*  �� *PerformerInstrument+4  .System.Windows.Controls.DataGridTemplateColumn  $  ��$ 1*    CellTemplate �� .R��� -#   $��Poista��� $��0,0,0,10q� �� $
��Auto ��� $
��Auto �.+��� $��Tekijät��$��Center=��� -$   $��+Lisää tekijä��$��10,0,0,0q�$��1��$
��10 0q�  -%   $ AuthorDataGrid��$ False��$ False��$	��200��$ Column=�$
 True�� �� "  .�-&    *	  $System.Windows.Controls.DataGridCell��%�   '        '	  �     �� "  .���  � ��
 ���@���  � ��$ 1,1,1,2q���  � ��
 �   ���  � ��$ 5,0,0,0q���  � ��$ SemiBold"���  � ��$ 13(��� " 	 .���  � ��$ 13(���  � ��$ Regular"�   $
 Nimi��$ 3*  �� *
AuthorName+  $ Rooli��$ 3*  �� *
AuthorRole+  $  ��$ 1*  �� .R��� -'   $��Poista���� $��2��$��1��$��	20 0 0 0 q� ��%,   '��       �� " ��.���  � ��$ 2q�.+��  �� $
��Auto ��� $��* ��� $��* ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��$��0��$��0 0 0 20q� �� �� �� ��  �� �� .+��� $��
Kokoonpano��$��0��$��0��$��3q��� -(   $
 EnsembleTextBox��$	��125��$��75��$��1��$��0��$��0,0,10,0q�$
 Wrap=�$
 Auto=�$
 True��.�� �� .@�m� -)   $��Poista��.��� $��1��$��1��$��0 0 10 0q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��* ��� $��* ��� $��* �.+��� -*   $ KMMinusButton��
 � Tag$ KM��$��-��$��0��$��0��$��5q�$��20���� -+   $ KMButton��$ KM��$��KM��$��0��$��1��$	��7 5q�$��40���� -,   $ KMPlusButton��$ KM��$��+��$��0��$��2��$��5q�$��20���� --   $ KNMinusButton��$ KN��$��-��$��1��$��0��$��5q�$��20���� -.   $ KNButton��$ KN��$��KN��$��1��$��1��$	��7 5q�$��40���� -/   $ KNPlusButton��$ KN��$��+��$��1��$��2��$��5q�$��20���� -0   $ KSMinusButton��$ KS��$��-��$��2��$��0��$��5q�$��0 10 0 0q�$��20���� -1   $ KSButton��$ KS��$��KS��$��2��$��1��$	��7 5q�$��0 10 0 0q�$��40���� -2   $ KSPlusButton��$ KS��$��+��$��2��$��2��$��5q�$��0 10 0 0q�$��20��� $��1��$��2��$��0 0 10 0q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��* ��� $��* ��� $��* �.+��� -3   $ KQMinusButton��$ KQ��$��-��$��0��$��1��$��5q�$��20���� -4   $ KQButton��$ KQ��$��KQ��$��0��$��2��$	��7 5q�$��40���� -5   $ KQPlusButton��$ KQ��$��+��$��0��$��3��$��5q�$��20���� -6   $ KVMinusButton��$ KV��$��-��$��1��$��1��$��5q�$��20���� -7   $ KVButton��$ KV��$��KV��$��1��$��2��$	��7 5q�$��40���� -8   $ KVPlusButton��$ KV��$��+��$��1��$��3��$��5q�$��20���� -9   $ KZMinusButton��$ KZ��$��-��$��2��$��1��$��5q�$��0 10 0 0q�$��20���� -:   $ KZButton��$ KZ��$��KZ��$��2��$��2��$	��7 5q�$��0 10 0 0q�$��40���� -;   $ KZPlusButton��$ KZ��$��+��$��2��$��3��$��5q�$��0 10 0 0q�$��20��� $��3��$��3�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��* ��� $��* ��� $��* �.+��� -<   $ KIButton��$��KI��$��2��$��2��$	��7 5q�$��0 10 0 0q�$��70��� $��4��$��0�� ��%W   '��       �� " ��.���  � ��$ 5q���  � ��$ 5q���  � ��$ 70�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto �.+��� $
��Laji��$��0��$��0��$��3q��� -=   $
 GenreTextBox��$	��125��$��75��$��1��$��0��$��0,0,10,0q�$
 Wrap=�$
 Auto=�$
 True��.�� �� .@�m� ->   $��Poista��.��� $��1��$��1��$
��Auto�� �� $
��Auto ��� $
��Auto �.+��� -?   $ 	L4AButton��$	��L4A��$��0��$��13(��� -@   $ 	L5AButton��$	��L5A��$��1��$��13(�� $��5��$��2�� �� $
��Auto ��� $
��Auto �.+��� -A   $ 	L6CButton��$	��L6C��$��0��$��13(��� -B   $ 	L6VButton��$	��L6V��$��1��$��13(�� $��5��$��3�� �� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $��* �.+��� -C   $ 	L9EButton��$	��L9E��$��13(�$��0��$��35���� -D   $ 	L9TButton��$	��L9T��$��13(�$��1��$��35���� -E   $ otherGenresComboBox��$��1��$��5q�$
 True��$
 True��� $��5��$��0�� �� $��* ���  �� �� �� �� �� �� �� .+��� $��Äänitysmaa��$��Center=�$��0��$��0��$��3 20 3 3q��� -F   $ RecordingCountryComboBox��$��1��$��0��$��4��$��0 3 3 0q�$
 True��$��Stretch=�$	��430��.@��� $��
Alankomaat���� $��
Argentiina���� $��	Australia���� $��Brasilia���� $J��DEnglanti (ja Kanaalin ym. saaret; Iso-Britannian valtiollinen koodi)���� $��Espanja���� $��Etelä-Korea���� $6��0Irlanti (tasavalta ja erittelemätön saarialue)���� $��Islanti���� $��Japani���� $��Kanada���� $��Meksiko���� $��Nigeria���� $��Norja���� $��Ranska���� $��Ruotsi���� $��Saksa���� $��Suomi���� $��Tanska���� $H��BVenäjä (myös nykyisen Venäjän federaation valtiollinen koodi)���� $��Yhdysvallat���� -G   $' !RecordingCountryEstimatedCheckBox��$��Äänitysmaa arvioitu��$��2��$��0��$��	0 10 3 15q��� -H   $	 KeywordTextBlock��$��	Asiasanat��$��Center=�$��3��$��0��$��3q��� -I   $
 KeywordTextBox��$��4��$��2��$��0��$��0 0 0 15q��� -J   $	 CultureTextBlock��$��	Kulttuuri��$��Center=�$��5��$��0��$��3q��� -K   $
 KulttuuriTextBox��$��6��$��2��$��0��$��0 0 0 15q�� $��Bottom=�$��
10 0 10 10q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� -L   $ CreateJSONButton��$��Luo JSON��$	��5,0q�$��0��$
��20 5q�$��40���� -M   $ ClearButton��$��	Tyhjennä��$	��5,0q�$��Right=�$��2��$��40��$
��20 5q�
���  ��