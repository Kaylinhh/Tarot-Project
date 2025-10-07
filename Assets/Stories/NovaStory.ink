INCLUDE 2d_api.ink

~ changeBg("Mountain")
~ meetCharacter("Nova")
~ show("Nova")
Nova:Bienvenue !
~ meetCharacter("Mei")
~ show("Mei")
Mei:Bonjour...

*Oui
*Non
- 
Bon bah d'accord.
~ changeScene("BarView")
A
A
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