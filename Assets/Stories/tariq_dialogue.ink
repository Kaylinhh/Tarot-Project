=== tariq_day1 ===

// === ARRIVAL (continuing from Mei's exit) ===

Tariq: We need to talk about the rent.

Nova: Alright, but I already paid the first two weeks, didn't I?

Tariq: Obviously. But I'm worried about the next one. 
Tariq: This (gestures at the bar with a grimace) isn't exactly… conventional.

Nova: Excuse you, do you not like the vibes? 
Nova: And I've had two customers already!

Tariq: Two customers? And it's already 8pm? 
(sarcastic) My, I guess you already made next month's rent then.

Nova: This is my first day! Of course it's slow. 
Nova: But people are gonna talk about it, and I'll get more and more customers.

Tariq: Leaving your fate -and my money- up to strangers? How smart of you. 
Tariq: (looks at the crystals) Do you intend to pray and cast spells also?

Nova: You will definitely get your rent on time. 
Nova: If you agreed to rent me this place, I'm pretty sure you believe in me in some sort of way. 
Nova: So how about letting me work my magic and relax? 
Nova: I'll make you a drink. It's on the house.

Tariq: (scoffs) Relax? You little… 
Tariq: (gives in) UGH fine. But make it quick. I'm busy.

// === TRANSITION TO BARVIEW ===
# PAUSE
~ changeScene("BarView")

// === NOVA MONOLOGUE (IN BARVIEW) ===
Nova: Okay, Tariq definitely seems like the fancy drink type. 
Nova: Probably judges people by their taste in alcohol. (pauses) 
Nova: But man, he's wound up tight… Maybe he needs to relax more than impress?

Nova: (prepares the shaker) # PAUSE_MINIGAME

// === BACK IN MAIN - REACTIONS ===
~ changeBg("Mountain")
~ show("Tariq")
{
- drinkServed == "Old Fashioned":
    Tariq: (twirls drink)  I guess you know how to read your customers well… 
    Tariq: Maybe this place isn't doomed after all.
    Nova: Thank… You?
    ~ gainAffinity("Tariq", 1)
    
- drinkServed == "Green Peace":
    Tariq: (stares at drink) What is this?
    Nova: Green Peace.
    Tariq: Do I look like someone who's into Green Peace?
    Nova: (exasperated) Just give it a go!
    Tariq: You should work on your cocktail names. (drinks reluctantly) 
    Tariq: … (relaxes visibly)
    Nova: How is it? Are you speechless because you have nothing mean to say anymore?
    Tariq: (rolls eyes) This isn't something I would usually drink.
    Nova: I know, that's why I made it for you.
    Tariq: It's good. I guess you're good.
    ~ gainAffinity("Tariq", 2)
    
- drinkServed == "Cosmopolitan":
    Tariq: (stares at drink suspiciously) What is this supposed to be? 
    Tariq: (takes a sip, face immediately sours) No. Absolutely not. 
    Tariq: (pushes glass away) I expected incompetence, but this is impressive in its own terrible way. 
    Tariq: Do better. Fast. For both our sakes.
    ~ gainAffinity("Tariq", -2)
    
- drinkServed == "Tequila Shot":
    Tariq: (doesn't even touch the drink) … Seriously? What am I, a teenager?
    ~ gainAffinity("Tariq", -2)
    
- else:
    Tariq: … (takes a sip, unimpressed) 
    Tariq: This is underwhelming. 
    Tariq: It could be worse, but it could be much, much better. 
    Tariq: So do better. Fast. For both our sakes.
}

// === UNSOLICITED ADVICE (GATHER) ===
-

Tariq: You know you can't survive by giving free drinks to everyone, right? (pauses) 
Tariq: Also you're not a therapist, you're a bartender. 
Tariq: I saw you with that girl earlier—too much time, too much care. 
Tariq: Be like that to every customer and you'll burn out before making rent.

Nova: … Are you… worried about me?

Tariq: (defensive) No. I don't care about you, I care about rent. There's a difference.

Nova: (hands up) Yes, of course.

Tariq: Alright, I need to go. I have actual work to do. (pauses, pulls out business card) 
Tariq: … Take this. Supplier contact. You'll need it.

Nova: Oh thank you! To be honest, I was buying from the supermarket… 
Nova: See, you can be nice!

Tariq: You were WHAT? I-  
Tariq: (rubs temples, gives up) Forget it. I'm not nice. This is self-preservation. 
Tariq: If you fail, I lose a tenant. I'll check in again. Try not to burn this place down.

~ meetCharacter("Tariq")
~ hide("Tariq")

-> after_tariq