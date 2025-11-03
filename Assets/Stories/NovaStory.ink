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