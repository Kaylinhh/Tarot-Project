=== neo_day1 ===

// === ARRIVAL ===
~ show("Neo")
Neo: Hello? You open? I NEED your strongest drink, like now. I mean, please.
Nova: Well hello there! The strongest of the drinks coming right up. Did someone have a rough day?

~ face("Neo", "upset")
Neo: (laughs bitterly) Rough day? More like rough week! Rough month! Rough fucking life! 
Neo: I had a stable job- well I hated every second of it but hey, it paid the bills and now I don't even have THAT anymore! Got laid off! 

Nova: Oh… I'm so sorry to h-

Neo: (doesn't hear, continues ranting) And my girlfriend, I mean, ex-girlfriend now, we were together for THREE YEARS and she just… left. 
Neo: Said she wanted "more from life" or some shit. 
Neo: Like, sorry for being STABLE, I guess? 
Neo: I mean, what's wrong with her? 
Neo: (pause) Oh my god- what's wrong with me! 

~ face("Neo", "base")

Neo: I'm sorry all I do is ramble, please give me my drink and I'll shut up.

// === TRANSITION TO BARVIEW ===
# PAUSE
~ changeScene("BarView")

// === TUTORIAL (IN BARVIEW) ===
Nova: Strongest drink, huh? 
Nova: Is this really gonna help this poor man? 
Nova: He feels overwhelmed, he's on the verge of a panic attack...

Nova: Alrighty! A bit intense for a first customer, but we're gonna do just fine! 
Nova: To make a drink, we just have to drag and drop the ingredients in the shaker. 
Nova: Yes, some drinks aren't shaken, it doesn't matter. It's magic. 
Nova: We can check out the recipes we already know in our grimoire.

// TODO: Grimoire tutorial UI interaction here(or not)

Nova: (prepares the shaker) # PAUSE_MINIGAME

// === BACK IN MAIN - REACTIONS ===
~ changeBg("Mountain")
~ show("Neo")

// Check which drink was served
{
- drinkServed == "Tequila Shot":
    Nova: Know how it goes? Pour some salt on your hand, lick it, take the shot, bite the lime.
    ~ face("Neo", "happy")
    Neo: Damn, giving me the whole experience huh? 
    Neo: (takes the shot) Oooh, that's the thing. A bit small, but I definitely felt it go down. 
    Neo: (pause) Yeah that's what I wanted. To FEEL something, you know? Since everything's been numb lately.
    ~ gainAffinity("Neo", 2)
    
- drinkServed == "Phoenix Rising":
    Nova: I present you: the Phoenix Rising! The flames are just to show off. Let me put it out before you drink.
    Neo: (suspicious) Didn't ask for pretty, I asked for strong. (takes a sip reluctantly) 
    ~ face("Neo", "happy")
    Neo: Wait, this is actually pretty good.
    Nova: Thought you could use something to take the edge off.
    Neo: (takes another sip) 
    Neo: … "take the edge off" huh. (gets quiet) Right. Thank you.
    ~ gainAffinity("Neo", 1)
    
- drinkServed == "Cosmopolitan":
    ~ face("Neo", "upset")
    Neo: (takes a sip, grimaces) This is way too sweet. 
    Neo: (bitter laugh) Not made for me- just like my ex. Or my job. Or both. Whatever.
    ~ gainAffinity("Neo", -2)
    
- else:
    Neo: …Meh. (shrugs) Not what I wanted, but whatever.
}

// === VULNERABLE MOMENT (GATHER) ===
-
~ face("Neo", "base")
Neo: (stares at glass) You know what's fucked up? I feel… relieved. 
Neo: Like, everything's a disaster but I don't have to pretend anymore. Am I going insane?

// === NOVA'S RESPONSE CHOICES ===
* [Validate feelings]
    Nova: No, you're not insane. Those are huge changes you just described. Sometimes, relief and disaster can coexist, you know?
        ~ face("Neo", "happy")
    Neo: (caught off guard) Huh. Yeah… Yeah, you're right. 
    Neo: Didn't expect a stranger to… Or anyone for that matter to… 
    Neo: (pause) Thanks. I guess I needed to hear that.
    ~ gainAffinity("Neo", 2)

* [Offer optimism]
    Nova: Hey, you came here and you talked about it! It's already a step. Give yourself some credit.
    ~ face("Neo", "happy")
    Neo: (rolls eyes but smirks) What are you, a therapist? 
    Neo: … I mean, you are a bartender, so basically the same thing. 
    Neo: Alright, fine. One step, I'll take it.
    ~ gainAffinity("Neo", 1)

* [Keep it light]
    Nova: Well, at least you've found this amazing bar, am I right? (grins) Fresh start, fresh drinks.
    ~ face("Neo", "happy")
    Neo: (laughs) Okay, okay. "Fresh drinks" indeed. 
    Neo: (shakes head, smiling) You're ridiculous. But… Yeah, thanks for that.
    ~ gainAffinity("Neo", 1)

// === DEPARTURE ===
-

Neo: Alright, I'll leave you to… whatever bartenders do when they're not being therapists to drunk people. (pause) 
Neo: Oh, uh, I'm Neo by the way. In case you were wondering who just trauma dumped you. (awkward smile)

~ meetCharacter("Neo")

Nova: Nova. And hey, that's what the bar is for.

Neo: (nods) Nova. Cool name. 
Neo: I'll see you around, I guess.

~hide("Neo")

-> after_neo