INCLUDE 2d_api.ink

~ changeBg("Mountain")
~ show("Nova")
Nova:Bienvenue !
~ meetCharacter("Nova")
~ meetCharacter("Ex")
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
# PAUSE
~ changeBg("Mountain")
~ show("Nova")
Nova:Bonsoir !
~ show("Mei")
Mei:Salut...

*Oui
*Non
- 
Bon bah d'accord. C'est fini.
~ endDay()
~ changeScene("CardReading")
-> DONE