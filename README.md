# 🎮 Coin Rush - Unity Final Project

## 📖 Game Description
Coin Rush is a 2D platformer where players control a character to collect coins and reach the finish flag while avoiding obstacles.

**Genre**: 2D Platformer  
**Platform**: Windows PC (Android & WebGL coming soon)  
**Engine**: Unity 6  
**Language**: C#

---

## ✨ Features

### Part 1 (Completed)
- ✅ Player movement and jumping
- ✅ Coin collection system
- ✅ Score tracking
- ✅ Lives system (3 lives)
- ✅ Death zones and respawning
- ✅ Win and Game Over screens
- ✅ Main menu
- ✅ Screen boundaries
- ✅ Windows build

### Part 2 (In Progress)
- 🚧 Multiple levels
- 🚧 Moving enemies
- 🚧 Sound effects and music
- 🚧 Power-ups
- 🚧 Timer system
- 🚧 Particle effects

---

## 🎮 How to Play

### Controls
- **A / Left Arrow**: Move Left
- **D / Right Arrow**: Move Right
- **Space**: Jump
- **Escape**: Pause (Part 2)

### Objective
Collect all coins and reach the red flag to win!

---

## 🚀 How to Run

### Option 1: Play the Build
1. Download the latest release
2. Extract the ZIP file
3. Run `CoinRush.exe`

### Option 2: Open in Unity
1. Install Unity Hub and Unity 6
2. Clone this repository
3. Open project in Unity Hub
4. Open `MainMenu` or `Level1` scene
5. Click Play button

---

## 📁 Project Structure
```
CoinRush_FinalProject/
├── Assets/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   └── Level1.unity
│   ├── Scripts/
│   │   ├── PlayerController.cs
│   │   ├── GameManager.cs
│   │   ├── MenuManager.cs
│   │   ├── ScoreManager.cs
│   │   ├── DeathZone.cs
│   │   ├── EnemyPatrol.cs
│   │   ├── PowerUp.cs
│   │   ├── AudioManager.cs
│   │   └── PauseManager.cs
│   └── Sounds/ (Part 2)
├── Builds/
│   └── WindowsBuild/
└── README.md
```

---

## 🧪 Testing

All core features have been tested and verified:
- ✅ Player movement and jumping
- ✅ Coin collection and scoring
- ✅ Lives system and respawning
- ✅ Win/lose conditions
- ✅ UI updates
- ✅ Build stability

---

## 💻 Technical Details

**Unity Version**: Unity 6 (6000.0.X)  
**Scripting Backend**: Mono  
**API Compatibility**: .NET Standard 2.1  
**Target Platform**: Windows (x86_64)

---

## 📝 Development Log

### December 3, 2025
- Created basic player controller
- Implemented coin collection
- Added score system

### December 4, 2025
- Added main menu
- Implemented lives system
- Created game over screen
- Added screen boundaries

---

## 🎓 Course Information

**Course**: Game Development  
**Institution**: [Your University/College]  
**Instructor**: [Professor Name]  
**Semester**: Fall 2025  
**Project Weight**: 30% of final grade

---

## 👨‍💻 Author

**[Your Name]**  
Student ID: [Your ID]  
Email: [Your Email]

---

## 📄 License

This project is created for educational purposes as part of a university course.

---

## 🙏 Acknowledgments

- Unity Technologies for the game engine
- Course instructor for guidance
- Classmates for feedback and testing

---

**Last Updated**: December 3, 2025
```

3. **Edit** the template:
   - Replace `[Your Name]`, `[Your ID]`, `[Your Email]`
   - Replace `[Your University/College]` and `[Professor Name]`
   - Update dates if needed

4. **Save As**:
   - File name: `README.md`
   - Save as type: All Files
   - Location: Your Unity project root folder

---

## Step 4: Upload to GitHub using GitHub Desktop

### 4.1 Add Repository to GitHub Desktop

1. Open **GitHub Desktop**
2. **File** → **Add Local Repository**
3. Click **Choose...** button
4. Navigate to your Unity project folder (CoinRush_FinalProject)
5. Click **Select Folder**
6. If it says "This directory does not appear to be a Git repository":
   - Click **"create a repository"** link
   - Click **Create Repository**

### 4.2 Make Your First Commit

1. In GitHub Desktop, you'll see all your files listed
2. **Make sure these are INCLUDED** (checked):
   - ✅ Assets folder
   - ✅ ProjectSettings folder
   - ✅ .gitignore file
   - ✅ README.md file

3. **Make sure these are NOT included** (they should be grayed out because of .gitignore):
   - ❌ Library folder
   - ❌ Temp folder
   - ❌ Obj folder
   - ❌ .vs folder
   - ❌ .csproj and .sln files

4. At the bottom left:
   - **Summary**: `Initial commit - Core game mechanics`
   - **Description** (optional): 
```
   - Added player movement and jumping
   - Implemented coin collection system
   - Created score tracking
   - Added lives system and respawning
   - Built main menu and game over screen
   - Added screen boundaries
