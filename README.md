# Multiplayer Game Project "Robot Fire"
- Work Title: Robot Fire
- Age: 16+
- Preview Date: 11/12/2024
- Release Date: - 

## Todo
- Distance Fog for Camera
- Server-Client Rpc Synchronization
- In-game debug console.
- Custom kinematic character controller (for base Character Movement).
  - Momentum-based movement
  - Snappy movement
  - Collision Detection
- In-game runtime editor.


# Game Mechanics
## Resources 
- `gun_bullets`
- `projectiles` for such weapons
- `spare_parts` (Robot Components)
- `armor_plates` (Robot Components)
- `in-game_resources`:
  - `gold` (for motherboards_of_robots),
  - `silver` (for electronics),
  - `platinum` (for radioelectronics),
  - `iron` (for spare_parts or armor_plates).

## Classes
- Class Robot
	- DualRiped Human-like
	- QuadRiped Robot Small
	- QuadRiped Robot Big

## Robots (Classes / Their Tasks)
Standard Class (Robot (Non-Playing Class / Parent Class) ):
Robots can move.
Robots have different movement speeds.
Robots have inventory slots (weapons, mods, cargo).
Robots can carry loads with them: small boxes and boxes on themselves, trolleys on a trailer.
Robots can be repaired (using spare parts).
The player can switch between free robots connected to the docking station.
Robots can be rebuilt on the docking station, without the cost of spare parts.
Each robot has a role that defines its characteristics.

From the roles of robots: **Trooper**, **Builder**, **Engineer**.
- **Trooper**: can carry assault rifles and more armor. Capturing points and thus providing the team with various resources. Purpose: attack and capture new territories.
- **Builder**: can use a special weapon in one slot, can drag large loads (up to 5 trolleys) over long distances. Purpose: logistics and strengthening of territories.
- **Engineer**: can only carry special weapons. Creates weapons, ammo, spare parts, improvements for robots. Goal: Improve the army of robots.


### Soldier (Trooper)
Analog of Troop Soldier
### Builder
Big Quad-Ripped Digger Bot
- Can Build
- Can Repair Walls
- Can Walk on Walls
- Heavy
### Engineer
Small Quad-Ripped Engineer Bot works on Robots & Turrets & Other Tools
Three branches of development:
- Mechanics (mechanical parts): weapons, logistics, armor. 
- Electronics (electronic parts): interface, robot repair, robot creation. 
- Radio engineering (radio hacking): turning off the robot, capturing the robot, radio interference.

## Players in Game
- 24 / All - 12v12 Team
  - 16 / Troopers - 8v8
  - 4 / Engineers - 2v2
  - 4 / Builders - 2v2

## Weapons
1) Handgun
2) Assault Rifle
3) SMG
4) Shotgun

## Docking Station

The Docking Station is a base for each teams. Reproduce new robots each two minutes. In order to win, the enemy must destroy this Docking Station. The Docking Station can be repaired by medics/engineers.
