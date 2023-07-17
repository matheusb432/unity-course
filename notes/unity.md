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

# 3D and 2D

- 3D Games can contain 2D elements, such as UI elements (e.g. health bar, text prompts, etc.).

## Forward Vector

- The _Forward Vector_ is the direction that the object is facing.
- In 3D, a common pattern is that the forward vector should be the blue axis.
