# Subnautica Enhancer

Subnautica Enhancer is a mod for the game *Subnautica* that provides enhanced gameplay features including improved item tracking, object rendering (ESP), and useful game helpers to make exploring the underwater world more convenient and engaging.

## Features

- **ESP (Extra-Sensory Perception)**: Displays resource nodes and creature eggs on the game GUI to help players locate valuable items easily.
- **Collectors**: Automatically track certain types of objects in the game, such as creature eggs and pickupable resources.
- **Debug Tools**: Additional tools for testing and debugging, useful for mod developers or advanced players.

## Project Structure

- **Collectors**: Contains scripts for tracking various collectible items in the game.
  - `CreatureEggCollector.cs`: Tracks the locations of creature eggs in the game.
  - `DebugCollector.cs`: Contains debugging tools for collecting data.
  - `ResourceTrackerCollector.cs`: Tracks resources such as minerals and other pickupable items.
- **ESP**: Includes the logic for rendering item locations visually in the game.
  - `RenderEngine.cs`: Manages the GUI rendering of item markers and overlays on the screen.
- **Properties**: Project metadata and settings.
  - `AssemblyInfo.cs`: Contains general project information, such as version and copyright details.
- **Main Classes**: Core scripts for the mod's functionality.
  - `GameHelper.cs`, `Loader.cs`, `Logger.cs`, `MenuEngine.cs`, `Render.cs`: Core components for handling game-related utilities, logging, and menu interactions.

## Getting Started

### Prerequisites

- **Unity Modding Framework**: This mod relies on a compatible modding framework for *Subnautica*. Make sure you have BepInEx or a similar framework installed.
- **Visual Studio**: This project uses C#. You'll need an IDE like Visual Studio or JetBrains Rider to build and modify the project.

### Installation

1. **Clone or Download**: Clone this repository or download it as a ZIP.
2. **Extract Files**: Extract the project files to your preferred workspace.
3. **Compile the Solution**: Open the `.sln` file in Visual Studio and compile the project.
4. **Deploy**: Copy the compiled DLLs into your *Subnautica* mod folder (usually located in the BepInEx plugins directory).

### Usage

Once installed, the mod will run when you launch *Subnautica*. Use the following features to enhance your gameplay:

- **Enable/Disable ESP**: Use the in-game menu to enable or disable the ESP features for specific resources like creature eggs and minerals.
- **Adjust Update Frequency**: You can adjust the frequency of updates for the resource trackers in the settings to optimize game performance.

## Development Notes

- **Performance Considerations**: Avoid setting very short update intervals for tracking to prevent performance issues.
- **Event-driven Updates**: Consider contributing optimizations such as using event-driven systems instead of polling to reduce overhead.

## Contributing

We welcome contributions to improve the mod! Feel free to fork the project and submit pull requests.

### Issues

If you encounter any bugs or have suggestions for new features, please open an issue in this repository.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Acknowledgements

- **Subnautica Developers**: For creating an incredible game that inspires mods like this.
- **Modding Community**: For support, ideas, and resources that made this project possible.

