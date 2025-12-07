<div align="center">
  <img width="50" height="50" alt="cssharp" src="https://github.com/user-attachments/assets/3393573f-29be-46e1-bc30-fafaec573456" />
	<h3><strong>Trails</strong></h3>
	<h4>a plugin that allows players to use custom trails</h4>
	<h2>
		<img src="https://img.shields.io/github/downloads/exkludera-cssharp/trails/total" alt="Downloads">
		<img src="https://img.shields.io/github/stars/exkludera-cssharp/trails?style=flat&logo=github" alt="Stars">
		<img src="https://img.shields.io/github/forks/exkludera-cssharp/trails?style=flat&logo=github" alt="Forks">
		<img src="https://img.shields.io/github/license/exkludera-cssharp/trails" alt="License">
	</h2>
	<!--<a href="https://discord.gg" target="_blank"><img src="https://img.shields.io/badge/Discord%20Server-7289da?style=for-the-badge&logo=discord&logoColor=white" /></a> <br>-->
	<a href="https://ko-fi.com/exkludera" target="_blank"><img src="https://img.shields.io/badge/KoFi-af00bf?style=for-the-badge&logo=kofi&logoColor=white" alt="Buy Me a Coffee at ko-fi.com" /></a>
	<a href="https://paypal.com/donate/?hosted_button_id=6AWPNVF5TLUC8" target="_blank"><img src="https://img.shields.io/badge/PayPal-0095ff?style=for-the-badge&logo=paypal&logoColor=white" alt="PayPal"  /></a>
	<a href="https://github.com/sponsors/exkludera" target="_blank"><img src="https://img.shields.io/badge/Sponsor-696969?style=for-the-badge&logo=github&logoColor=white" alt="GitHub Sponsor" /></a>
</div>

> [!NOTE]
> inspired by [Nickelony/Trails-Chroma](https://github.com/Nickelony/Trails-Chroma)

<img src="https://github.com/user-attachments/assets/53e486cc-8da4-45ab-bc6e-eb38145aba36" height="200px"> <br>

### Requirements
- [MetaMod](https://github.com/alliedmodders/metamod-source)
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)
- [Clientprefs](https://github.com/Cruze03/Clientprefs)
- [CS2MenuManager](https://github.com/schwarper/CS2MenuManager)

## Showcase
<details>
	<summary>content</summary>
	<img src="https://github.com/user-attachments/assets/6d31beec-b2ca-47bc-8f55-fd1eda29faa2" width="125"> <br>
	<img src="https://github.com/user-attachments/assets/3bd99e1b-ffb7-4254-9e7c-9470703e6891" width="175"> <br>
	<img src="https://github.com/user-attachments/assets/1135a673-e19f-4a00-9edc-f4bfc760c45f" width="250"> <br>
	<img src="https://github.com/user-attachments/assets/af7406b0-3911-489c-91e1-3dde79002790" width="300"> <br>
	<img src="https://github.com/user-attachments/assets/7dddc6cc-a0aa-4946-9c49-c5bf6b48ceb1" width="200"> <br>
</details>

## Config

<details>
<summary>Trails</summary>
	
```json
{
  "Prefix": "{red}[{orange}T{yellow}r{green}a{lightblue}i{darkblue}l{purple}s{red}]",
  "Permission": ["@css/reservation", "#css/vip"],
  "MenuCommands": ["trails", "trail"],
  "HideTrailsCommands": ["hidetrails", "hidetrail"],
  "MenuType": "CenterHtmlMenu",
  "ChatMessages": true,
  "TicksForUpdate": 1,
  "Trails": {
    "1": {
      "Name": "Rainbow Trail",
      "Color": "rainbow"
    },
    "2": {
      "Name": "Particle Trail",
      "File": "particles/ambient_fx/ambient_sparks_glow.vpcf"
    },
    "3": {
      "Name": "Red Trail",
      "Color": "255 0 0",
      "Width": 3,
      "Lifetime": 2
    },
    "4": {
      "Name": "Example Settings",
      "File": "materials/sprites/laserbeam.vtex",
      "Color": "255 255 255",
      "Width": 1,
      "Lifetime": 1
    }
  }
}
```
</details>
