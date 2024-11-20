# Multiplayer Game Project "Robot Fire"
Work Title: Robot Fire
Age: 16+
Preview Date: 11/12/2024
Release Date: - 


## Todo
- Distance Fog for Camera
- Server-Client Rpc Synchronization
- In-game debug console.
- Custom kinematic character controller (for base Character Movement).
  - Momentum-based movement
  - Snappy movement
  - Collision Detection
- In-game runtime editor.



## Classes
- Class PlayerEssence
  - ConnectingToFreeRobot
  - StateMachine
  - InputForPlayerEssense
  - PlayerEssenceForEachPlayer(hasAuthority)


- Class RobotBase
  - Movement
  - Jump
  - InputForRobot
  - OwnershipToPlayer
  - BaseWeapon
  - BaseAdvancementsParts (Wearable rocket carriers, Radio jamming device or something like that (i.e. worn on a robot as a RobotComponent))


## Game Mechanics

### Player Controller

Players have two states: without a robot, controlling the robot.

Interface: 

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

## Robots (Classes / Their Tasks)

Standard Class (Robot (Non-Playing Class / Parent Class) ):

- Robots can move.
- Robots have different movement speeds.
- Robots have inventory slots (weapons, mods, cargo).
- Robots can carry loads with them: small boxes and boxes on themselves, trolleys on a trailer.
- Robots have several states: hibernation (there is no player), loading, active, inactive (with player), disabled (is dead).
- Robots can be repaired (using spare parts).
- The player can switch between free robots connected to the docking station.
- Robots can be rebuilt on the docking station, without the cost of spare parts.
- Each robot has a role that defines its characteristics.

From the roles of robots: Trooper, Builder, Engineer, Medic.

- Trooper - can carry assault rifles and more armor. Capturing points and thus providing the team with various resources. Purpose: attack and capture new territories.
- Builder - can use a special weapon in one slot, can drag large loads (up to 5 trolleys) over long distances. Purpose: logistics and strengthening of territories.
- Engineer - can only carry special weapons. Creates weapons, ammo, spare_parts, AdvancementsParts for robots. Goal: Improve the army of robots.


## Weapon Types:

1) Handguns
2) Rifles
3) Assault(Automatic)
- AK-74M
- M4 Carbine
4) Bolt-Action (Remington Model 700)
5) Shotguns
6) SMG
- UZI
- Vector  
- MP5K 
- MAC10
7) LMG
- M249

### Light Weapons
- HMG
- RPG
- ATGM
- MPADS

### Artillery Missiles

#### ABM (Anti-Ballistic Missile)

#### SSM (Surface-to-surface, ballistic missile)

### Artillery Missiles Launchers

#### Wheeled Missile Launcher like HIMARS

#### Multiple Landed Missile Launcher

#### Single Landed Missile Launcher

## Docking Station

The Docking Station is a base for each teams. Reproduce new robots each two minutes. In order to win, the enemy must destroy this Docking Station. The Docking Station can be repaired by medics/engineers.
