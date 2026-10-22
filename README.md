# BeatAim

**A lightweight FPS aim trainer built with Unity.**

BeatAim is a first-person aim training game designed to help players practice their aiming skills in a customizable environment. Configure your settings, adjust the appearance of the game, and practice hitting targets in Freeplay.

## Features

- **Freeplay mode** — practice without a fixed session limit.
- **Customizable targets** — configure the maximum number of targets and their movement behavior.
- **Target movement** — adjust movement speed, direction, and bouncing behavior.
- **Target lifetime** — optionally make targets disappear after a configurable amount of time.
- **Customizable settings** — configure player name, mouse sensitivity, resolution, and fullscreen mode.
- **Visual customization** — change the colors of the crosshair, targets, floor, walls, and background.
- **Custom click sound** — choose a supported audio file or restore the default sound.
- **Pause menu** — pause gameplay, access settings, resume your session, or return to the main menu.

## Future Plans

The following features are planned for future development. Their implementation and release dates are not guaranteed.

- [ ] **Music Play Mode** — Practice your aim while playing music, with gameplay designed around the rhythm.
- [ ] **Ranked Mode** — Compete for scores and rankings, with the possibility of leaderboards and a competitive progression system.
- [ ] **1v1 Multiplayer** — Challenge another player in real-time aiming duels.

## Getting Started

There are two ways to try BeatAim:

1. Download a pre-built Windows version from the [Releases](../../releases) page.
2. Clone the repository and run the project through Unity.

### Download the game

1. Open the repository's **Releases** page.
2. Select the latest stable release.
3. Download the Windows build archive.
4. Extract the archive into a folder.
5. Run `BeatAim.exe`.

Keep the executable and all accompanying build files in the same folder structure. Do not run the executable directly from inside the compressed archive.

> BeatAim does not require Unity to be installed when using a pre-built Windows release.

## Requirements

### Running the game

- Windows 64-bit
- A mouse and keyboard
- at least 1 finger
- The extracted game files from a BeatAim release

### Running the source project

- Unity Hub
- The Unity Editor version specified in `ProjectSettings/ProjectVersion.txt`
- Git, or another tool capable of cloning Git repositories

## Build from Source

### 1. Clone the repository

Open a terminal and run:

```bash
git clone <REPOSITORY_URL>
cd BeatAim
```

Replace `<REPOSITORY_URL>` with the HTTPS or SSH URL of this repository.

Alternatively, use **Code → Download ZIP** on GitHub and extract the source code.

### 2. Open the project in Unity

1. Open Unity Hub.
2. Select **Add → Add project from disk**.
3. Select the cloned `BeatAim` project folder.
4. Install or select the Unity Editor version specified in `ProjectSettings/ProjectVersion.txt`.
5. Open the project and wait for Unity to import the assets and packages.

### 3. Run the game in the Editor

1. Open the main menu scene.
2. Make sure the required scenes are available in the project's build configuration.
3. Press **Play** in the Unity Editor.

If the project does not open correctly, check the Unity Console for missing packages, compilation errors, or missing assets.

## Build for Windows

To generate a standalone Windows build:

1. Open the project in Unity.
2. Open **File → Build Profiles** (or **Build Settings**, depending on your Unity version).
3. Select **Windows** as the target platform.
4. Select the **x86_64** architecture.
5. Add the required scenes to the build configuration and place the main menu scene first.
6. Ensure the game assets and required `StreamingAssets` files are included in the project.
7. Choose **Build** and select an output folder, such as `Builds/Windows`.
8. Wait for Unity to finish building the game.

The output folder contains the executable and the supporting files required to run the game.

**Important:** distribute the complete Windows build, not just the `.exe` file.

## Settings and Configuration

BeatAim provides several options to customize the experience:

| Setting | Description |
|---|---|
| Player name | Configure the name displayed by the game. |
| Mouse sensitivity | Adjust camera movement sensitivity. |
| Resolution | Choose a supported display resolution. |
| Fullscreen | Toggle fullscreen mode. |
| Click sound | Select a custom click sound or restore the default. |
| Crosshair color | Customize the aiming reticle. |
| Target color | Customize the targets. |
| Floor color | Customize the floor. |
| Wall color | Customize the walls. |
| Background color | Customize the background. |

Settings are saved locally on the player's machine.

## Freeplay

Freeplay lets you configure a practice session, including:

- Maximum number of targets.
- Target movement speed.
- Movement directions.
- Wall-bouncing behavior.
- Optional target lifetime.

Press `Esc` during gameplay to open the pause menu. From there, you can resume the session, open settings, or return to the main menu.

## Repository Structure

The project follows the standard Unity project layout.

```text
BeatAim/
├── Assets/
│   ├── Scenes/
│   ├── Scrpts/
│   └── StreamingAssets/
├── Packages/
├── ProjectSettings/
├── README.md
└── .gitignore
```

The exact contents may change as the project evolves. `Scrpts` reflects the current folder name in the project.

## Contributing

Contributions, bug reports, and suggestions are welcome.

1. Fork the repository.
2. Create a feature branch.
3. Make your changes and test them in Unity.
4. Commit your changes with a descriptive message.
5. Open a pull request describing the changes.

## Releases

Stable Windows builds are published on the [GitHub Releases](../../releases) page.

Each release should include the version number, a short summary of changes, and the complete Windows build archive.

## License

No license has been selected for BeatAim yet.

Until a license is added, the source code remains subject to applicable copyright laws. Public availability of this repository does not automatically grant permission to reuse, modify, or redistribute its contents.

---

**BeatAim — Practice your aim.**
