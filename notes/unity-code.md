# MonoBehavior

- MonoBehaviour is the base class from which every Unity script derives. It contains the basic functionality that is needed for a script to work.
- When adding a script to a game object, it will only be the snapshot of the script at that time. If the script is changed, the changes will not be reflected in the game object. So it must be reset.

## Class Visibility

- `private` properties and classes are not visible in the _Unity Editor_, while `public` properties and classes are.
- Only serializable properties are visible in the Unity Editor.

# Event Handlers

- Event handlers are methods that are called when an event occurs.
  - e.g. Handling mouse clicks, player death, etc.

# Design Patterns

## Managers

- _Managers_ are scripts/objects that manage other game objects.
  - e.g. `GameManager` manages the game state, `AudioManager` manages the audio, etc.
- _GameManagers_ are the brains of the game. They manage the game state, and are responsible for starting and ending the game.

## Controllers

- _Controllers_ are scripts/objects that control other game objects.
  - e.g. `PlayerController` controls the player, `EnemyController` controls the enemies, etc.
- They should be used to control the behaviour of individual game objects, not shared game logic.

## State Pattern

- The _State Pattern_ is a design pattern that is used to manage the state of an object.
- It is used to change the behaviour of an object based on its state.
- The _Enter State_ method handles setting up the state, and the _Update State_ method is responsible for handling state on every update.

## Scriptable Objects

- _Scriptable Objects_ are assets that can be created in the Unity Editor to store data that can be shared between multiple game objects.
- They are useful for storing data that is not specific to a single game object.
  - e.g. Stats, items, etc.
- They're more efficient than using `MonoBehaviour` scripts, as they don't need to be attached to a game object.

# Callback Context

- The _Callback Context_ is a class that is used to pass data to the callback methods.
- Unity adds a variable called _action_ to the context parameter of input event handlers, they describe states such as:
  1. _Started_: The action has just started (When a button/binding is pressed).
  2. _Performed_: The action is being performed (When a button/binding is confirmed to be pressed).
  3. _Canceled_: The action has been canceled (When a button/binding has been released).
