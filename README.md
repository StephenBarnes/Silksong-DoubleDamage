 Plugin for Silksong to multiply damage inflicted by the player. The damage multiplier is 2 by default (double damage) but it can be configured. It should apply to all damage inflicted by the player (by nail, spells, etc.) but not to damage inflicted by enemies. Multiplier is applied after all other modifiers.

To install:
1. Install BepInEx 5. (Tested with 5.4.23.3; other versions may also work.)
2. Put DoubleDamage.dll in BepInEx/plugins folder.
3. Start the game. Player's damage should be doubled.
4. After running the game once, it will create a config file at BepInEx/config/stebars.silksong.double-damage.cfg. You can edit this to change the damage multiplier. After changing the config, restart the game to apply the change.

Source code: https://github.com/StephenBarnes/Silksong-DoubleDamage 

To build from source, on Linux: Set up NET SDK 8.0 and run `dotnet restore && dotnet build -c Release`. The DLL will be at bin/Release/net48/DoubleDamage.dll.
