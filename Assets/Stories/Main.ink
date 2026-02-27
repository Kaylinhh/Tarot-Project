INCLUDE 2d_api.ink
INCLUDE neo_dialogue.ink
INCLUDE mei_dialogue.ink
INCLUDE tariq_dialogue.ink

VAR drinkServed = ""

// === INTRO DAY 1 ===
~ changeBg("Mountain")

Nova: Welcome to the Moonlit Pour!
Nova: (looks around nervously) First day open… I hope someone shows up soon.

// === NEO (TOWER) ===
-> neo_day1

=== after_neo === 

// === MEI (HIGH PRIESTESS) ===
-> mei_day1

=== after_mei === 

// === TARIQ (MAGICIAN) ===
-> tariq_day1

=== after_tariq === 
-> end_of_day

// === END OF DAY ===
=== end_of_day ===
Nova: What a day… Time to close up.

~ endDay()
~ changeScene("CardReading")

-> DONE