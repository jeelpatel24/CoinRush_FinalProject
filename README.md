# CoinRush

CoinRush is a 2D Unity arcade-style game where the player collects coins while avoiding obstacles and enemies. This repository contains the Unity project, assets, scenes, and settings required to open, run, and build the game.

## Quick demo
- Open the Unity project in a compatible Unity Editor version (see Installation).
- Open the main scene from Scenes/ (see Project Structure).
- Press Play to start testing locally.

## Highlights
- 2D arcade gameplay: collect coins, avoid hazards.
- Pixel art assets and animations included.
- Unity scene(s) and ProjectSettings configured.

## Features
- Collectible coins and scoring system.
- Player movement and animations.
- Basic enemy obstacles and collision logic.
- UI elements for score and restart.

## Zip distribution contents
When you distribute the game as a ZIP (for handouts, classroom submission, or builds), include these top-level files and folders:

- CoinRush.zip
  - README.md (this file)
  - LICENSE (project license)
  - Build/ (contains exported builds for target platforms, e.g., Windows, macOS)
  - Docs/ (documentation files — this repo's docs/ folder)
  - Assets/ (only if distributing the full Unity project; otherwise include source art under Licenses/ if required)
  - ProjectSettings/ (if distributing the full Unity project)
  - Scenes/ (if distributing the full Unity project)
  - Source/ (optional: extracted important source scripts if you want to include them separately)
  - ThirdPartyLicenses.md (list of asset licenses)

Tip: If you're shipping the built game only, include only Build/, README.md, LICENSE, and ThirdPartyLicenses.md in the ZIP.

## Installation (editor & runtime)
1. Install Unity Editor (recommended versions: 2020.3 LTS or 2021.3 LTS). Exact version used is often noted in ProjectSettings/ProjectVersion.txt.
2. Clone or extract the repository:
   - git clone https://github.com/jeelpatel24/CoinRush_FinalProject.git
   - or extract the ZIP that contains the project root (Assets, ProjectSettings, Packages).
3. Open Unity Hub → Add → select the project root folder.
4. Open the project and then open the main scene from Scenes/ to run in the Editor.

## How to build & prepare ZIP (short)
- In Unity Editor: File → Build Settings → choose platform → Build.
- Place generated builds into Build/ and include README.md, LICENSE, and ThirdPartyLicenses.md in the ZIP.
- For reproducible builds include a small BUILD_INFO.txt describing Unity version and build date.

More detailed build/package steps: see Docs/BUILD_AND_ZIP.md

## Controls
- Keyboard (default):
  - Move left/right: A / D or ← / →
  - Jump: Space or W / ↑
  - Pause / Resume: Esc
  - Restart: R
See Docs/CONTROLS.md for exact mapping and controller notes.

## Project Structure
- Assets/ — Unity assets and scripts
- Scenes/ — Unity scenes (main menu, gameplay)
- ProjectSettings/ — Unity project settings
- Animations/, 2D Dungeon PixelArt Tileset/ — art & tiles
- Docs/ — additional documentation (this distribution)
See Docs/ARCHITECTURE.md for a brief explanation of main scripts and important scene objects.

## Contributing
Contributions are welcome. Please follow the contributing guide in Docs/CONTRIBUTING.md. Typical workflow:
1. Fork
2. Create a feature branch
3. Add changes and tests
4. Open a pull request with a description of changes

## Known issues / TODO
- Optimization required for large scenes.
- Add mobile touch controls.
- More enemy types and level progression.

## Asset licenses & credits
- Pixel art and sprites: See Docs/ASSETS_LICENSES.md for details and attribution.
- Open-source packages (if any): list in ThirdPartyLicenses.md

## Contact
Project owner: jeelpatel24 (GitHub)
Repository: https://github.com/jeelpatel24/CoinRush_FinalProject

## License
Add your chosen license (MIT, CC BY, etc.) as a LICENSE file at repository root.

## 🎓 Course Information

**Course**: Game Development  
**Institution**: Conestoga college  
**Instructor**: Tenzin Zampa
**Semester**: Fall 2025  
**Project Weight**: 30% of final grade

---

## 👨‍💻 Author

👥 Authors / Team Members

Dhruv Jivani

Jeel Patel

Isha Patel

Aayushi Ravalji

Dilraj Singh

---

## 📄 License

This project is created for educational purposes as part of a university course.

---

## 🙏 Acknowledgments

- Unity Technologies for the game engine
- Course instructor for guidance
- Classmates for feedback and testing

