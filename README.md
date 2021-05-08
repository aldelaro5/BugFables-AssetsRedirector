## BugFables-Speedrun-Practice
A BepInEx plugin assisting speedrunners of the game Bug Fables. This plugin is a port of a modified version of the game's Assembly-CSharp.dll which was previously distributed for this purpose. The port offers the following advantages:

1. It no longer requires the distribution of the game's Assembly-CSharp.dll which could lead to copyright issues
2. Since the patches are done at runtime instead of being a static patch, it allows more liberty in configuration in conjunction with BepInEx's standardized configuration scheme
3. Due to #1, the code can be distributed publically under a Git repository which allows better tracking of changes through time

> _This branch is only compatible with version 1.1 of the game. Please use the 1.0.5 branch if you must use this plugin with previous versions of the game._

## Installation instructions
> _If you already have a 1.x version of the old practice dll (one that came with an Assembly-CSharp.dll), you must uninstall it first._

If this is not done already, you first need to install the BepInEx loader into the game. To do so, [download the latest release](https://github.com/BepInEx/BepInEx/releases) by getting the zip file marked "x64". Then, simply unzip it into the game's directory such as the `BepInEx` folder as well as the 3 provided files appears from the game's directory. If you are using Steam, this directory is by default located at `C:\Program Files (x86)\Steam\steamapps\common\Bug Fables` on Windows and at `~/.steam/steam/steamapps/common/Bug Fables` on Linux. Once the files are placed, launch the game once for the installation to complete.

Once this is done, [download the latest version of the plugin](https://github.com/aldelaro5/BugFables-Speedrun-Practice/releases) and unzip it into `BepInEx/plugins` from the game's directory. You should unzip it so the folder `Speedrun-Practice` with all the files from the zip appears ***directly*** under the `plugins` folder.

To verify the installation was performed correctly, launch the game and verify that the lower left corner of the title screen. It should display information about this plugin such as the version and the authors.

## Uninstallation instruction
To uninstall the plugin, simply delete the `BepInEx/plugins/Speedrun-Practice` folder from the game's directory. 

If you want to entirely remove BepInEx and all of its plugins, delete the following under the game's directory:

* The BepInEx folder
* The file changelog.txt
* The file doorstop_config.ini
* The file winhttp.dll

## Usage instructions
This plugin functions by adding features when pressing the F keys on the keyboard during gameplay.

In the overworld:

* F1 - Show Player & Map Info
* F2 - Input Display (Keyboard Only atm)
* F3 - Heal Party
* F4 - Toggle Infinite Jump
* F5 - Toggle Speed (2x)
* F6 - Save Game
* F7 - Reload Save
* F8 - Go-to Main Menu
* F9 - Select previous position slot
* F10 - Select next position slot
* F11 - Save position in room
* F12 - Load position in room
* DEL - Toggle NoClip+Infinite Jump 

In battle:

* F3 - Instant Enemy Kill
* F4 - Instant Flee (w/o losing berries)
* F5 - Instant Retry during battle

Note: If you close or re-enter the main menu, you will lose your saved positions.

## Building and debugging instructions
This section is intended ***only for developers***. You do not need to do this if you only want to use the plugin. Refer to the ***Installation instructions*** section for this purpose.

### Building
> _NOTE: For technical reasons, this project is configured to be built using .NET Framework 3.5. Please do not change the target Framework and make sure your environement supports this version of the .NET Framework._

This project is configured for Visual Studio 2019 (previous versions may work, but are untested). To build the project, you first need to place the required dlls into the `Libs` directory present on this repository. Refer to `Libs/README.txt` for more information on which dlls to place.

Once this is done, the project should build successfully. To improve convenience, you may want to set the output path to `Bug Fables\BepInEx\plugins\Speedrun-Practice` (where `Bug Fables` is the game's directory) in the project's configuration for ease of testing.

### Debugging
To debug the plugin, you will need the [dnSpy](https://github.com/0xd4d/dnSpy/releases) program (download the file ` dnSpy-net472.zip`). Once it's installed, you will need to [download this modified version of mono.dll](https://drive.google.com/open?id=1u_xyatcUWKceWajzNImkvKQuNxKgArHi) and place it at `Mono/EmbedRuntime` from the game's directory. You may want to backup or rename the original one that comes with the game in case you want to revert it.

With this done, you will now be able to debug the plugin with dnSpy. Open the Speedrun-Practice.dll file with dnSpy, click `Start` at the top, select `Unity` as the Debug Engine and select the game's executable in the Executable field (the file `Bug Fables.exe`) and finally, click `OK`. You may now place breakpoints, use watches in the `Watch` window and see all the output produced by the plugin and Unity in the `Output` window.

## Contributions, issue reports and feature requests
All contributions via pull requests are welcome as well as issue reports on this issue tracker. You may also request features with this issue tracker.

If you are planning to submit a pull request, do not share any substantial amount of code from the game as it can lead to copyright issues and thanks to Harmony + BepInEx, it can be avoided in almost all cases. Any pull requests that contains substantial amount of code from the game will be immediately denied if I judge it can be done without sharing the code.

## Credits
This plugin was done by Benjee, wataniyob, Wintar, Lyght and I with the help of ArchaicEx.

## License
This plugin is licensed under the MIT license which grants you the permission to freely use, modify and distribute this plugin as long as the original license and its copyright notice is still present. Refer to [the MIT license](https://github.com/aldelaro5/BugFables-Speedrun-Practice/blob/master/LICENSE) for more information.

## Special Thanks
I would like to thank everyone from Moonsprout Games for making this amazing game as it brought inspiration to me and to everyone in the community it sparked.
