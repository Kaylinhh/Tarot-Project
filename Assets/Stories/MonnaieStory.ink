INCLUDE 2d_api.ink
VAR monnaie = 10
~ changeBg("Mountain")
Once upon {monnaie} time... 
~ show("Kub")

Kaylinh:Ma phrase préférée.
Kub:Ma deuxième phrase préférée.
Phrase de contexte.
 * There were two choices.
    La suite de l'{~histoire|aventure|autre chose}.
        * * Encore un autre choix.
-> chapitre1        
        * * Et un autre choix.
        -> chapitre1.sous1
 * There were four lines of content.
        * * Et un autre. 
        * * Et encore un autre...

- They lived happily ever after.
    -> END
    
=== chapitre1
~ monnaie -= 1
{monnaie==8 : -> sous1}
~ show("Kaylinh")
Je dis {monnaie} trucs.
    + Un {&histoire|aventure|autre chose} de choix.
    -> chapitre1
    + {monnaie<10} grab
    ~monnaiegrab(3)
    -> chapitre1
-> END
= sous1
~ fadeBg("Swamp", 2.0)
~ moveTo("Kaylinh", "Left", 2)
~ flipX("Kaylinh")
~ face("Kaylinh", "happy")
Je dis des sous trucs.
-> END

=== function monnaiegrab(grab)
~monnaie += grab
EXTERNAL monnaiegrab(grab) 
