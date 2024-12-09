# Multiplayer Game Project "Robot Fire"
- Work Title: Robot Fire
- Preview Date: 11/12/2024
- Release Date: - 

## Todo
- Distance Fog for Camera / Туман для камеры
- Server-Client Rpc Synchronization (NetCode for GameObjects) / Синхронизация RPC сервера и клиента
- In-game debug console / Внутриигровая консоль отладки
- Custom kinematic character controller (for base Character Movement) / Пользовательский кинематик контроллер персонажа 
  - Momentum-based movement / Движение на основе импульса
  - Snappy movement / Мгновенное движение
  - Collision Detection / Обнаружение столкновений


!Это прост идеи что можно было было сделать
# Game Mechanics / Игровые механики
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

## Robots Classes / Their Tasks:
- **Trooper** / Солдат (Двуногий робот похожий на человека): Может нести штурмовые винтовки и больше брони. Захват точек и тем самым обеспечение команды различными ресурсами. Цель: атака и захват новых территорий.
- **Builder** / Инженер (Четырехногий маленький робот-инженер): Может использовать спецоружие в одном слоте, может перетаскивать большие грузы (до 5 тележек) на большие расстояния. Назначение: укрепление территорий.
- **Engineer** / Строитель (Четырехногий большой робот-строитель): может нести только специальное оружие. Создает оружие, боеприпасы, запчасти, улучшения для роботов. Цель: улучшения роботов.

## Players in Game
- 24 / All - 12v12 Team
  - 16 / Troopers - 8v8
  - 4 / Engineers - 2v2
  - 4 / Builders - 2v2

## Weapons
1) Handgun / Пистолет
2) Assault Rifle / Штурмовая винтовка
3) SMG / Пистолет-пулемет
4) Shotgun / Дробовик

## Docking Station

The Docking Station is a base for each teams. Reproduce new robots each two minutes. In order to win, the enemy must destroy this Docking Station. The Docking Station can be repaired by engineers.

Док-станция — это база для каждой команды. Каждые две минуты воспроизводит новых роботов. Чтобы победить, противник должен уничтожить эту док-станцию. Док-станцию ​​могут ремонтировать инженеры.
