# Unity 1v1 Turn-Based RPG System

2 clean, reusable and extendable scripts as a 2-player turn-based RPG combat template for Unity.

---

## 📦 Included Scripts

- TurnBasedBattleManager.cs
- RPGCharacter.cs

---

## ✨ Features

- 1v1 turn-based combat
- character stats and unique skill clean managment
- Energy system
- All stats and numbers are fully costumizable 
- Event-driven defeat detection
- Clean separation of combat and turn logic 

---

## ⚡ Energy System

- Maximum Energy: 100 (configurable)
- Energy gained per turn: 20 (configurable)
- Current possible actions (consume configurable energy costs):
  - Normal Attack (default 10)
  - Defend (default 20)
  - Unique Skill (default 50)

---

## 🧠 Unique Skill Design

The Unique Skill method is intentionally left empty, so that you can implement your own character skill logic :
extend RPGCharacter.cs by completing:

  public override bool UseUniqueSkill(RPGCharacter target)
  
This allows full customization without modifying the base system.

---

## ⏳ Turn Flow

1. Player turn starts
2. Player gains energy
3. 30-second timer begins
4. Player chooses an action:
   - Normal Attack
   - Defend
   - Unique Skill
5. Turn ends after valid action or timer expires.

---

## 🧠 Design Notes

- Energy capacity is separated from energy gained per turn.
- Action costs are configurable per character.
- Combat logic is decoupled from turn management.
- No UI dependency.
- Designed as a clean and easy gameplay foundation.

---

## 🚀 Possible Extensions

- AI-controlled opponents
- Skill cooldowns
- Online multiplayer adaptation

---

## 🛠 Unity Version

Tested in Unity6+ (should also work without problems in the newer versions)

---

## 📜 License

MIT
