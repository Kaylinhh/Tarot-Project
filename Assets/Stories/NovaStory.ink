INCLUDE 2d_api.ink
VAR drinkServed = ""

~ changeBg("Mountain")
Nova:Bienvenue !
~ show("Mei")
~ meetCharacter("Mei")
Mei:Bonjour...
~ gainAffinity("Mei", 1)

*Oui
*Non
- 
Bon bah d'accord.
// Point de pause clair
-> pause_for_barview

=== pause_for_barview ===
# PAUSE
~ changeScene("BarView")
-> barview_tutorial

=== barview_tutorial ===
Nova: Ceci est le début du tuto !
Nova: Puis la suite ! 
Nova: ceci est une ligne vide # PAUSE_MINIGAME

~ changeBg("Mountain")
Nova:Bonsoir !
~ show("Mei")
Mei:Salut...

*Oui2
*Non2
- 
Bon bah d'accord. C'est fini.
~ endDay()
~ changeScene("CardReading")
-> DONE