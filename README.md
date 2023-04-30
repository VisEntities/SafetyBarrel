# Your invisibility cloak in Rust
Tired of constantly running and hiding from danger? Say goodbye to those days with SafetyBarrel. Now, you can explore the wilderness with confidence, knowing that danger can't touch you. Don't wait any longer! Barrel your way out of trouble and face your enemies head-on!

![](https://i.imgur.com/SHNWh5s.png)

------

## Adding costumes
Simply find the desired attire shortname on sites like [CorrosionHour](https://www.corrosionhour.com/rust-item-list/) and add it to the clothing list in the configuration below.
```json
  "Clothing": [
    "barrelcostume",
    "cratecostume",
    "ghostsheet",
    "scarecrow.suit"
  ],
```

-------
## The science behind SafetyBarrel
Once a player equips one of the costumes specified in the configuration, they become invisible to Patrol Helicopters, Bradley APCs, Scientists, and Traps.

Additionally, players can also gain immunity to damage while wearing these costumes.

--------
## Permissions

- `safetybarrel.use` - Grants players the ability to become invisible and safe from harm.

-------
## Configuration
```json
{
  "Version": "1.0.0",
  "Clothing": [
    "barrelcostume",
    "cratecostume",
    "ghostsheet",
    "scarecrow.suit"
  ],
  "Enable Damage Immunity": true,
  "Can Be Seen By NPC": false,
  "Can Be Seen By Helicopter": false,
  "Can Be Seen By Bradley": false,
  "Can Be Seen By Trap": false
}
```

------
## Keep the mod alive
Creating plugins is my passion, and I love nothing more than exploring new ideas and bringing them to the community. But it takes hours of work every day to maintain and improve these plugins that you have come to love and rely on.

With your support on [Patreon](https://www.patreon.com/VisEntities), you're  giving me the freedom to devote more time and energy into what I love, which in turn allows me to continue providing new and exciting updates to the community.

![](https://i.imgur.com/XnVYNcw.png)

A portion of the contributions will also be donated to the uMod team as a token of appreciation for their dedication to coding quality, inspirational ideas, and time spent for the community.

------
## Credits
* Originally created by **Panduck**, up to version 0.0.9
* Completely rewritten from scratch and maintained to present by **Dana**.