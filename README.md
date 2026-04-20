# Nova - A Bartending Visual Novel

<div align="justify">
A narrative-driven visual novel developed in Unity where players take on the role of Nova, a bartender serving unique cocktails to clients with distinct personalities. The game seamlessly blends branching narrative (powered by Ink) with interactive mixology gameplay.
</div>

## Play the Game

[**Play on Unity Play**](https://play.unity.com/en/games/70a1edc4-7bc9-49f0-a3d5-118592e5a4d0/nova-prototype)

---

## Features

### Narrative System
- **Branching dialogue** written in Ink with three unique characters
- **Dynamic character emotions** and expressions
- **Dialogue history** with character-specific coloring
- **Smooth scene transitions** between dialogue and gameplay phases

### Mixology Mini-Game
- **Drag & drop mechanics** to combine ingredients
- **Recipe discovery system** with visual feedback
- **Duplicate ingredient handling** for complex cocktails
- **Shake animation** with particle effects
- **Arcade mode** for free play without narrative

### Grimoire System
- **Double-page book interface** displaying character profiles and recipes
- **Progressive unlock system** as you meet characters and discover recipes
- **Generic architecture** allowing easy content expansion
- **Smooth page navigation** with visual polish

### UI/UX
- **End-of-day card reveal** showing characters encountered
- **Mouse-following tooltips** for ingredient information
- **Reactive UI updates** through event-driven architecture
- **Smooth animations** (bounce effects, fade transitions, shake effects)

### Audio
- **Background music playlist** with shuffle/loop options
- **Synchronized sound effects** (shaking, pouring, UI feedback)
- **Audio manager** with separate music and SFX channels

---

## Technical Highlights

### Architecture Patterns
- **Singleton pattern** for core managers (DataManager, AudioManager, DialogueHistory)
- **Observer pattern** with UnityEvents for decoupled communication
- **Generic interfaces** (`IPageFiller<T>`) for reusable UI systems
- **ScriptableObject runtime copies** to prevent persistent data modifications

### Advanced Systems
- **Additive scene management** with automatic context detection
- **Event-driven drag & drop** with dynamic Canvas reparenting
- **Coroutine-based animations** without Update() polling
- **State persistence** during scene transitions

### Code Quality
- **30+ scripts refactored** with comprehensive XML documentation
- **Encapsulation** with data protection (returning copies of internal lists)
- **Clean separation of concerns** (UI, logic, data)

---

## Key Learnings

This project allowed me to master:
- **Event systems** for component decoupling
- **Canvas & RectTransform manipulation** for complex UI
- **Coroutines** for smooth animations and timing
- **Generic programming** with Unity's limitations
- **ScriptableObject** architecture and runtime management
- **Scene flow management** with pause/resume logic

---

## Assets & Content

- **All visual assets** created by me (characters, backgrounds, UI elements)
- **Complete narrative** written in Ink
- **Three unique characters** with distinct personalities
- **Recipe system** with ingredient combinations

---

## Installation & Setup

1. Clone this repository
2. Open the project in Unity 2022.3 or later
3. Open the `MainMenu` scene
4. Press Play!

---


## Gameplay Loop

1. **Meet characters** through branching dialogue
2. **Create cocktails** in the Bar mini-game
3. **Progress relationships** based on choices and served drinks
4. **Review the day** with character card reveals


---

**If you enjoyed this project, feel free to star the repository!**
