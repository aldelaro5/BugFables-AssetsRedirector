## BugFables-AssetsRedirector
A BepInEx plugin redirecting the assets of the game Bug Fables. This plugin allows modders to easilly customise the sounds, music, sprites and text data of the game by simply dropping the edited assets into a folder. This not only makes modding more accessible, but it also improves testability as it is not necessary to pack the assets into the data.unity3d file. The assets are dynamically replaced when the game needs them if they are present

## Installation instructions
If this is not done already, you first need to install the BepInEx loader into the game. To do so, [download the latest release](https://github.com/BepInEx/BepInEx/releases) by getting the zip file marked "x64". Then, simply unzip it into the game's directory such as the `BepInEx` folder as well as the 3 provided files appears from the game's directory. If you are using Steam, this directory is by default located at `C:\Program Files (x86)\Steam\steamapps\common\Bug Fables` on Windows and at `~/.steam/steam/steamapps/common/Bug Fables` on Linux. Once the files are placed, launch the game once for the installation to complete.

Once this is done, [download the latest version of the plugin](https://github.com/aldelaro5/BugFables-AssetsRedirector/releases) and unzip it into `BepInEx/plugins` from the game's directory. You should unzip it so the folder `AssetsRedirector` with all the files from the zip appears ***directly*** under the `plugins` folder.

## Uninstallation instruction
To uninstall the plugin, simply delete the `BepInEx/plugins/AssetsRedirector` folder from the game's directory. 

If you want to entirely remove BepInEx and all of its plugins, delete the following under the game's directory:

* The BepInEx folder
* The file changelog.txt
* The file doorstop_config.ini
* The file winhttp.dll

## Usage instructions
This plugin comes with an empty directory structure that matches the assets structure of the game. To override an asset, you simply have to put the edited file at the same place than the location of the asset in the game. Therefore, the first step is to get an extraction of the assets structure to get the original assets and to know where to place them.

This can be done using [uTinyRipper](https://sourceforge.net/projects/utinyripper/files/) which is a simple executable that allows to extract all the assets of the game.

### Extracting the assets with uTinyRipper
To use it, you first need to locate your game's directory. This can be done on the steam version by performing the following steps:

- Right click on the game in your library and select "Properties"
- Select "LOCAL FILES" on the left
- Click "Browse"
- Your file explorer should open at the game's directory

If you are using the itch.io version or the GOG version, you will need to locate where the game has been installed.

Once done, start uTinyRipper and simply Drag and drop the folder ending with `_Data`. 

![Screenshot](https://raw.githubusercontent.com/aldelaro5/BugFables-AssetsRedirector/master/Docs/uTinyRipper.gif)

The program will load the structure. Once done, click the "Export" button that appears and choose the location where you want the assets to be extracted.

> NOTE: it is HIGHLY recommended to extract to a new folder and do not extract them at the same folder as the plugin (you will copy the files later).

This process can take up to 15 minutes. Once done, you should see a `data` folder appearing at the location you chose.

### Explanation of the directory structure
To proceed with copying an asset, you will need to check the `data/Assets/Ressources` folder from where you have performed the extraction. This folder is the root folder of ALL assets that can be redirected using this plugin. Specifically, the `audio`, `data` and `sprites` folder are supported by this plugin. If you check the plugin's directory, you will see these 3 corresponding folders. You will also notice that the folders INSIDE of them also corresponds to what you got from uTinyRipper. 

> NOTE: The capitalisation of the names of the folder may differ, but NEVER RENAME THE ONES PROVIDED BY THE PLUGIN! They are correct in the plugin's folder, but uTinyRipper doesn't name them with the correct capitalisation after the extraction.

### Overriding assets
From there, to override an asset, perform the following:

- Take an asset for example, Vi's sprites are located at `sprites/entities/bee0.png`
- Copy the file to the corresponding folder in the plugin's directory (in this example, it would be at `Sprites/Entities/bee0.png`)
- Perform any edits on the asset you would like; you can do anything as long as the file is named, located and in the same format than the original
- Start up the game and observe the asset has been overriden by the one you provided

Since this plugin override the assets dynamically, it is even possible to edit the file while the game is opened and see the changes immediately when the asset is being reloaded by the game.

### Notes on distribution
To distribute your mod, simply zip the whole directory and tell your users to unzip it in the plugins folder.
> NOTE: DO NOT DISTRIBUTE THE ORIGINAL ASSETS WITH YOUR MOD! Doing so is technically a coyright infrigement since you are distributing content which you are not the author. ONLY distribute files you have modified yourself as you own the modification, but not the original content. This plugin is not responsible for issues which you may run into if you do not respect the copyright of Moonsprout Games. Please, respect the ethics and the copyrights of the original author.

### Notes about supported assets
Here are the list of supported assets and the method of redirecting them as well as their supported format:
- Non entities sprites (.png only, XUnity.ResourceRedirector)
- Entities sprites (.png only, Harmony patch, the entities gets their sprites overriden after their first Update and the list of overriden entities gets cleaned up every 5 minutes to remove destroyed ones)
- `Misc/main1.png` and `Misc/main2.png` (.png only set the texture of the material using XUnity.ResourceRedirector)
- Sound effects (.wav only, XUnity.ResourceRedirector)
- Music (.wav only, Harmony patch, this patches the `PlayMusic` method of the game)
- TextAssset in the data folder (either no extension or .txt or .bytes, XUnity.ResourceRedirector)

## Building and debugging instructions
This section is intended ***only for developers***. You do not need to do this if you only want to use the plugin. Refer to the ***Installation instructions*** section for this purpose.

### Building
> _NOTE: For technical reasons, this project is configured to be built using .NET Framework 3.5. Please do not change the target Framework and make sure your environement supports this version of the .NET Framework._

This project is configured for Visual Studio 2019 (previous versions may work, but are untested). To build the project, you first need to place the required dlls into the `Libs` directory present on this repository. Refer to `Libs/README.txt` for more information on which dlls to place.

Once this is done, the project should build successfully. To improve convenience, you may want to set the output path to `Bug Fables\BepInEx\plugins\AssetsRedirector` (where `Bug Fables` is the game's directory) in the project's configuration for ease of testing.

### Debugging
To debug the plugin, you will need the [dnSpy](https://github.com/0xd4d/dnSpy/releases) program (download the file ` dnSpy-net472.zip`). Once it's installed, you will need to [download this modified version of mono.dll](https://drive.google.com/open?id=1u_xyatcUWKceWajzNImkvKQuNxKgArHi) and place it at `Mono/EmbedRuntime` from the game's directory. You may want to backup or rename the original one that comes with the game in case you want to revert it.

With this done, you will now be able to debug the plugin with dnSpy. Open the Speedrun-Practice.dll file with dnSpy, click `Start` at the top, select `Unity` as the Debug Engine and select the game's executable in the Executable field (the file `Bug Fables.exe`) and finally, click `OK`. You may now place breakpoints, use watches in the `Watch` window and see all the output produced by the plugin and Unity in the `Output` window.

## Contributions, issue reports and feature requests
All contributions via pull requests are welcome as well as issue reports on this issue tracker. You may also request features with this issue tracker.

If you are planning to submit a pull request, do not share any substantial amount of code from the game as it can lead to copyright issues and thanks to Harmony + BepInEx, it can be avoided in almost all cases. Any pull requests that contains substantial amount of code from the game will be immediately denied if I judge it can be done without sharing the code.

## License
This plugin is licensed under the MIT license which grants you the permission to freely use, modify and distribute this plugin as long as the original license and its copyright notice is still present. Refer to [the MIT license](https://github.com/aldelaro5/BugFables-Speedrun-Practice/blob/master/LICENSE) for more information.

## Special Thanks
I would like to thank everyone from Moonsprout Games for making this amazing game as it brought inspiration to me and to everyone in the community it sparked.
