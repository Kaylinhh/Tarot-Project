=== mei_day1 ===

~ show("Mei")

// === ARRIVAL ===
???: …

Nova: Hello, welcome!

???: (startles) Hi, um… Sorry, are you open?

Nova: Yes! Freshly open. You're my second customer!

???: Oh, so you're new… 
It might be too soon to ask but- how do you find the neighborhood so far?

Nova: It's really cool, I've only met nice people since I moved! 
Nova: I'm really happy to have opened my bar here! 
Nova: Do you live here?

???: Right, um. (fidgets) 
Actually, I saw this place for rent down the street…

Nova: (excited) 
Nova: Yes I see which one! Oh my god, you wanna open your own shop? 
Nova: We'd be neighbors! It could be fun! 
Nova: So what kind of place were you thinking about?

???: I don't know, I was just… (pause) 
I was thinking about some sort of… 
(whispers) esoteric shop maybe? 
I know it sounds stupid, right? 
Nevermind. You don't have to say anything.

Nova: Are you kidding? That sounds awesome! I LOVE the idea! 
Nova: I could get stuff from your place to freshen up the bar's decorations! 
Nova: I love switching up things from time to time.

???: (surprised) Oh, right… Your place does have some subtle esoteric elements. 
I love the big moon displayed here. And the small touch of plants and bottles. 
Maybe that's why I felt attracted to it.

Nova: I don't know much about it, but I love the vibes! 
Nova: You could totally teach me stuff!

???: Oh, I'm not good enough to teach anyone… 
I just find it fascinating. Comforting.

Nova: Well, if you're passionate about it, I think you should give it a go.

???: No, I don't know…
I don't know anything about businesses. 
And what would people think…
I mean, crystals and tarot aren't exactly… mainstream. 
What if nobody comes? 
What if I fail, what if- 
sorry, I'm spiraling. I do that sometimes.

Nova: Hey, listen. I'm new to this too. 
Nova: And yet I'm very happy about giving this a try! 
Nova: I feel so accomplished looking at my little cozy bar, and serving little drinks to make people happy. 
Nova: How about that? I'll whip you up one to make you feel better.

// === TRANSITION TO BARVIEW ===
# PAUSE
~ changeScene("BarView")

// === NOVA MONOLOGUE (IN BARVIEW) ===
Nova: She's scared, so she might want something comforting… 
Nova: But at the same time, I feel like she just needs a little boost.

Nova: (prepares the shaker) # PAUSE_MINIGAME

// === BACK IN MAIN - REACTIONS ===
~ changeBg("Mountain")
~ show("Mei")

{
- drinkServed == "Moonlight":
    ???: (sighs, relaxed) This is… really nice. 
    Exactly what I wanted. Thank you.
    ~ gainAffinity("Mei", 1)
    
- drinkServed == "Third Eye":
    ???: Oh. Not what I expected, but… I like it. A lot. 
    I feel… Awake? Is that weird? 
    It's not coffee, is it? (laughs) 
    Thank you, I think… I think I needed this.
    ~ gainAffinity("Mei", 2)
    
- drinkServed == "Tequila Shot":
    ???: Um, is this a prank by any chance?
    ~ gainAffinity("Mei", -2)
    
- drinkServed == "Old Fashioned" or drinkServed == "Cosmopolitan":
    ???: Um, I'm sorry but this isn't to my liking…
    But I'm sure other people would like it!
    ~ gainAffinity("Mei", -1)
    
- else:
    ???: Oh, um, it's… alright.
}

// === VULNERABLE MOMENT ===
-

???: To be honest…
I just FEEL like this shop is calling me. 
Like this spot is meant for me. 
It's just… I'm just scared.

// === NOVA'S RESPONSE CHOICES ===
* [Share your own experience]
    Nova: You know, I was scared too when I opened this place. Terrified, in fact. But I did anyway, and… Here we are.
    ???: (thoughtful) So even if you're scared… You just do it?
    Nova: Pretty much. The fear doesn't go away, but you learn to move with it.
    ???: (nods) I… I think I can try that.
    ~ gainAffinity("Mei", 1)
    
* [Validate her intuition]
    Nova: That feeling? That's your intuition talking. And from what I've learned… It's usually right.
    ???: (surprised) You… you think so?
    Nova: I do. Sometimes we know things before we can explain them.
    ???: (smiles) Yeah… yeah maybe you're right.
    ~ gainAffinity("Mei", 2)

* [Practical encouragement]
    Nova: Scared is normal. But you've thought about this. You found the space, you're here asking questions… Clearly that means something.
    ???: I guess… I have been doing research. And planning. 
    (smiles) I guess you're right.
    ~ gainAffinity("Mei", 1)

// === REALIZATION ===
-

???: Maybe I'm more ready than I think?

Nova: Sounds like it to me.

???: I keep waiting to feel… ready. 
But what if that never happens? 
What if I just have to… decide?

Nova: (smiles) Now you're getting it.

???: (nervous laugh) That's scary.

Nova: Yeah. But you're gonna do it anyway, aren't you?

???: … You know what? I think I am. 
I... I should try. This is something I want. Thank you.

Nova: I'm so happy to hear that. 
Nova: So what's your name, future neighbor? I'm Nova.

Mei: Oh, um right, I'm Mei… 
Mei: Nice to meet you, Nova. Thanks again.

~ meetCharacter("Mei")

// === TARIQ INTERRUPTS ===
// (door opens suddenly)
~ moveTo("Mei", "Right", 1)
~ show("Tariq")
~ moveTo("Tariq", "Left", 1)

Tariq: Nova? You here? We need to talk about- (notices Mei) 
Tariq: Oh. Didn't realize you were with someone.

Mei: (startles) Oh! Um, I was just, I was just leaving. 
Mei:Sorry, I didn't mean to take up so much of your time.

Nova: No, it's totally fine! That's what I'm here for! Don't worry about it, you okay?

Mei: Yes! Yes. Sorry. (pauses) 
Mei: And thank you again Nova. Really. I'll think about what you said. 
Mei: Bye. (smiles and leaves)

~ hide("Mei")
~ moveTo("Tariq", "Mid", 1)

Tariq: Friend of yours?

Nova: (sighs) She was a customer. You kinda scared her off.

Tariq: (shrugs) Didn't mean to. Anyway!

-> after_mei