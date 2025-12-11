# Architecture Overview

This is a high-level description of the project's main systems.

## Scenes
- Main Scene — gameplay scene containing player, enemies, UI, and level geometry.

## Important folders
- Assets/Scripts — contains gameplay scripts (PlayerController, Enemy, GameManager, UIManager).
- Assets/Animations — animated sprite assets and Animator Controllers.
- Assets/Art or 2D Dungeon PixelArt Tileset — tile and sprite graphics.

## Key scripts (expected)
- PlayerController.cs
  - Handles movement, input, jumping, and animation switching.
- GameManager.cs
  - Manages score, game state (running, paused, game over), and scene restarts.
- Coin.cs
  - Coin pickup logic, triggers score updates.
- Enemy.cs
  - Enemy behaviour, collision with player.

## Game flow
1. Player spawns in the scene.
2. Player collects coins — Coin.cs triggers score increment in GameManager.
3. Collisions with hazards or enemies lead to player death and GameManager triggers Game Over UI.
