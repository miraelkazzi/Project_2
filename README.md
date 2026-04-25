# Project_2

## Video_demo
https://drive.google.com/file/d/1CcjSOmz3VaFsm-zKSR0PQoqvn-ZDpNtu/view?usp=sharing

## Title: 
Mirage Protocol

## Controls
- WASD → Move  
- Mouse → Look  
- Left Click → Shoot  

## Enemy Types
- Starfish enemy  
- Chest enemy  
- Virus-like enemy  


## Weapons
- Weapon 1 → Basic weapon  
- Weapon 2 → Stronger weapon with higher damage and better bullets  

## Upgrades / Inventory System
- Health items  
- Shield protection  
- Ammo refill  

The upgrades are stored in an inventory system instead of being used immediately after purchase. The player can choose when to use each item, giving more control and strategy during gameplay.


## Features
- Enemy wave system  
- Shooting mechanics  
- Shop system  
- Inventory management  
- Keypad unlocking system  
- Sound system  
- UI (health bar, ammo, shield timer)  


## Known Bugs
- Enemies sometimes go slightly through walls.  
  I attempted multiple fixes including:
  - Using NavMesh Modifier to mark walls as not walkable  
  - Adjusting NavMesh obstacle avoidance  
  - Increasing agent radius  
  - Rebaking the NavMesh multiple times  
  Despite these efforts, the issue still occurs occasionally.

- Jumping stopped working unexpectedly.  
  The feature was originally part of the Starter Assets package. I tried debugging it through the Player Input System but could not identify the exact cause. Since jumping is not required for gameplay, it does not affect the experience significantly.


## What I Am Most Proud Of
I am most proud of the level design where I created multiple different rooms instead of keeping a single environment. I also really liked implementing the keypad system as it adds a sense of progression and interaction.

Additionally, I designed the inventory system so that upgrades are not used immediately after purchase. Instead, the player can store them and decide when to use them, which makes the gameplay feel more strategic and controlled.
