# MonoBehavior

- MonoBehaviour is the base class from which every Unity script derives. It contains the basic functionality that is needed for a script to work.
- When adding a script to a game object, it will only be the snapshot of the script at that time. If the script is changed, the changes will not be reflected in the game object. So it must be reset.

# Class Visibility

- `private` properties and classes are not visible in the _Unity Editor_, while `public` properties and classes are.
- Only serializable properties are visible in the Unity Editor.

# Event Handlers

- Event handlers are methods that are called when an event occurs.
  - e.g. Handling mouse clicks, player death, etc.

# Managers

- _Managers_ are scripts/objects that manage other game objects.
  - e.g. `GameManager` manages the game state, `AudioManager` manages the audio, etc.
- _GameManagers_ are the brains of the game. They manage the game state, and are responsible for starting and ending the game.

# Controllers

- _Controllers_ are scripts/objects that control other game objects.
  - e.g. `PlayerController` controls the player, `EnemyController` controls the enemies, etc.
- They should be used to control the behaviour of individual game objects, not shared game logic.
