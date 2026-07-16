# Grid Pulse

A grid-based mobile puzzle game developed in **Unity 6** as part of the **Grid Puzzle Challenge** technical assessment.
The project demonstrates clean architecture, deterministic gameplay logic, state management, ScriptableObject-driven level configuration, and a clear separation between game logic and UI presentation.

---

# Overview

Grid Pulse is a deterministic puzzle game where the player navigates a grid using **on-screen directional buttons**. During development, **keyboard controls (WASD / Arrow Keys)** are also supported for testing inside the Unity Editor.

The objective is to reach the Goal (`G`) before running out of moves while managing Energy and Crack cells strategically.

---

# Repository Information

| Item | Value |
|------|-------|
| **Branch** | `Task/Gameplay` |
| **Unity Version** | `6000.0.67f1` |
| **Main Scene** | `Assets/GridPuzzleChallenge/Scenes/Main.unity` |
| **APK** | https://github.com/kingsuk1996/GridPuzzleChallenge/releases/download/v1.0/GridPulse.apk |

# Gameplay Features

## Core Mechanics

- N × M Grid System
- Direction Button Controls
- Keyboard Support (Editor Only)
- Move Counter
- Undo System
- Restart System
- Win/Lose Conditions
- Status Feedback Messages
- Loading Screen

---

## Gameplay Hook

### Energy (E)

Collecting an Energy cell increases the available Pulse count.

### Crack (C)

Crack cells require one Pulse to break.

Breaking a Crack consumes one Pulse before allowing the player to move through it.

---

## Grid Symbols

| Symbol | Description |
|---------|-------------|
| P | Player |
| G | Goal |
| E | Energy |
| C | Crack |
| X | Wall |
| Empty | Walkable Cell |

---

# Project Architecture

The project follows a layered architecture with a clear separation between gameplay logic and presentation.

```
                    +--------------------+
                    |  Input Controller  |
                    +---------+----------+
                              |
                        Direction Event
                              |
                              ▼
                     +------------------+
                     |   GameManager    |
                     +--------+---------+
                              |
          +-------------------+-------------------+
          |                                       |
          ▼                                       ▼
+-------------------------+             +-----------------------+
|     MovementSystem      |             |   GameUIController    |
+------------+------------+             +-----------+-----------+
             |                                      |
             ▼                                      ▼
+-------------------------+              Unity UI / TMP Elements
|       GridData          |
+------------+------------+
             |
             ▼
+-------------------------+
|      GridManager        |
+------------+------------+
             |
             ▼
+-------------------------+
|       GridCell          |
+-------------------------+
```

---

# Functional Code Flow

```
Player Input
      │
      ▼
Input Controller
      │
      ▼
GameManager
      │
      ▼
MovementSystem
      │
      ▼
GridData Update
      │
      ▼
GridManager.RefreshGrid()
      │
      ▼
GridCell Rendering
      │
      ▼
UI Update
```

---

# Folder Structure

Assets
│
├── Art
├── Prefab
├── Scenes
├── Script
│   ├── Core
│   ├── Grid
│   ├── Input
│   ├── Manager
│   ├── Messages
│   ├── Presentation
│   ├── Scriptable
│   └── UndoSystem
│
├── ScriptableObjects
│   ├── Levels
│   ├── LevelDatabase
│   └── Config
│
├── Settings
├── TextMesh Pro
├── TutorialInfo

---

# Design Principles

The project was built following common software engineering practices:

- Separation of Concerns
- Single Responsibility Principle
- Event-driven Input
- Data-driven Level Design
- ScriptableObject Configuration
- Logical Grid independent from Rendering
- Modular Gameplay Systems

---

# Scriptable Objects

## LevelData

Stores level-specific configuration.

- Grid Width
- Grid Height
- Maximum Moves
- Maximum Pulse
- Cell Layout

---

## CellVisualDatabase

Stores visual configuration for each cell type.

- Background Color
- Label Text
- Label Color

---

# Implemented Systems

## Grid System

- Logical GridData model
- GridManager
- GridCell rendering
- Dynamic level loading

---

## Gameplay

- Player movement
- Wall collision
- Boundary validation
- Goal detection
- Energy collection
- Crack breaking
- Move limitation

---

## Undo System

- GameState snapshots
- Grid restoration
- Move restoration
- Pulse restoration

---

## UI System

- HUD
- Status messages
- Loading Screen
- Game Over Popup
- Restart
- Undo

---

# Win Condition

Reach the Goal before the move counter reaches zero.

---

# Lose Condition

Run out of available moves.

---

# Controls

## Mobile

- Up Button
- Down Button
- Left Button
- Right Button

## Unity Editor

- W / A / S / D
- Arrow Keys

---

# Git Commit History

The project was developed incrementally using atomic commits.

Example history:

```
feat: Initial project setup
feat: Scene setup
feat: setup grid system architecture
feat: add player movement, move counter and status feedback
feat: add move results, status messages and move counter
feat: Game over functionality implement and Restart level implement
feat: implement undo system with game state history
feat: making splash panel and game panel for UI enhancement
refactor: level database structure modify
feat: GridPulse namespace added to all scripts
feat: project settings update

```

---

# Build

**APK Download**

[Download GridPulse APK](https://github.com/kingsuk1996/GridPuzzleChallenge/releases/download/v1.0/GridPulse.apk)

# Unity Version

**Unity 6**

```
6000.0.67f1
```

# Author

**Kingsuk Guha**

Unity Game Developer
