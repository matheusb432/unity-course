# Unity Code

Notes about unity C# code.

## Naming Conventions

Sources:

- [Naming and Code Style Tips](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity)
- [State Pattern Guide](https://unity.com/how-to/develop-modular-flexible-codebase-state-programming-pattern)

- Event delegates shouldn't have the `On` prefix. But the methods that raise them should.

```csharp
public class EventManager 
{
  public event Action DoorOpened; 
  public void OnDoorOpened() => DoorOpened?.Invoke();
}
```

- Controller instances shouldn't have the `Controller` suffix.

```csharp
public class GameManager 
{
  private PlayerController player;
}
```

## MonoBehavior

- MonoBehaviour is the base class from which every Unity script derives. It contains the basic functionality that is needed for a script to work.
- When adding a script to a game object, it will only be the snapshot of the script at that time. If the script is changed, the changes will not be reflected in the game object. So it must be reset.

## Class Visibility

- `private` properties and classes are not visible in the _Unity Editor_, while `public` properties and classes are.
- Only serializable properties are visible in the Unity Editor.

# Event Handlers

- Event handlers are methods that are called when an event occurs.
  - e.g. Handling mouse clicks, player death, etc.

- In Unity, the `UnityAction` delegate is used to handle events.

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

# Communicating Between Game Objects

- _Game Objects_ can communicate with each other using _Events_.
- _Events_ are used to notify other game objects when something happens.

## Observer / PubSub Pattern

- The _Observer Pattern_ is a design pattern that uses _Events_ to notify other game objects when something happens to promote loose coupling.
- It can be used to communicate between game objects without having to reference them directly via an event bus.

# Event Functions Lifecycle

- Unity calls a number of _Event Functions_ on start, update, and end of certain events.

The order of (some of) the _Event Functions_ is as follows:

1. `Awake()`: Called when the script is first loaded.
2. `Start()`: Called before the first frame update.
3. `OnEnable()`: Called if/when the script is enabled.
4. `Update()`: Called every frame.
5. `LateUpdate()`: Called after all `Update()` functions have been called.
6. `OnDisable()`: Called if/when the script is disabled.
7. `OnDestroy()`: Called when the script is destroyed.
