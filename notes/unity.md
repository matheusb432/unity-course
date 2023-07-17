# Unity

Unity is a cross-platform game engine that allows you to create games 2D, 3D, VR, AR and other types of games.

# Rendering Pipelines

- The rendering pipeline is the process of taking a 3D scene and rendering it to a 2D screen.

## Built-in Rendering Pipeline

- The _Built-in Rendering Pipeline_ is the default rendering pipeline that Unity uses.
- It is a single-pass forward rendering pipeline, meaning that it renders everything in one pass.

## Universal Rendering Pipeline (URP)

- The _URP_ is a multi-pass rendering pipeline that is optimized for mobile and other platforms.
- It's highly customizable, performant and is adequate for low to medium graphics.

## High-Definition Rendering Pipeline (HDRP)

- The _HDRP_ is the most powerful pipeline, designed for AAA games and high-end graphics.
- More complex and realistic lighting, shadows, reflections, etc.
- May not be compatible with some assets.

# Scenes

- A scene can be thought of as a level in a game. It contains all the objects, assets, etc. that make up that level.
- Scenes can be loaded and unloaded at runtime.

# Game Window

- The _Game Window_ is the window that shows the game running, it shows the game from the player's perspective.

# Game Objects

- A _Game Object_ is an object in the game, it can be anything from a character to a light source.
- It can have scripts attached to it, which are used to control the object's behaviour.

# Assets

- An _Asset_ is a file that contains data that can be used in a game.
- e.g. a _Model_ or a _Texture_.

## Materials

- A _Material_ is a collection of settings that determine how an object is rendered.
- It can be thought of as the "paint" that is applied to an object.

## Prefabs

- A _Prefab_ (Prefabrication) is a template for a _Game Object_, it can be used to create multiple instances of the same object.

## Models & Meshes

- A _Model_ is a 3D object, it can be imported into Unity from a 3D modelling program.
- A _Mesh_ is a collection of vertices, edges and faces that make up a 3D object.
- _Meshes_ are used to render _Models_.

## Forward Vector

- The _Forward Vector_ is the direction that the object is facing.
- In 3D, a common pattern is that the forward vector should be the blue axis.

# Components

- A _Component_ is a piece of functionality that can be added to a _Game Object_, it can be thought of as a building block.
- As with _MonoBehaviors_, all components are C# classes that derive from the `Component` class.
- e.g. a `Rigidbody` component to give the object physics.
- _Component Composition_ is the process of adding multiple components to a game object to give it the functionality that you want, composing game objects from multiple, reusable components.

## Transform Component

- The _Transform Component_ is a component that is automatically added to every game object, it contains information about the object's position, rotation and scale.
