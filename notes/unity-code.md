# MonoBehavior

- MonoBehaviour is the base class from which every Unity script derives. It contains the basic functionality that is needed for a script to work.
- When adding a script to a game object, it will only be the snapshot of the script at that time. If the script is changed, the changes will not be reflected in the game object. So it must be reset.

# Class Visibility

- `private` properties and classes are not visible in the _Unity Editor_, while `public` properties and classes are.

# Input System

- The new input system is a package that can be installed from the package manager.
- It's more scalable than the old input system.
- It utilizes an Event Based System.
- Provides easy support for switching between input devices.

## Actions and Action Maps

- An action describes the behavior performed by the player.
  - e.g. Jump, Move, Shoot, Navigating a menu, etc.
- An action map is a group of actions.
