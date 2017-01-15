
[![Build status](https://ci.appveyor.com/api/projects/status/pcmd43hcum0dhr53?svg=true)](https://ci.appveyor.com/project/alishahb/discordannouncer)

![ICON](https://raw.githubusercontent.com/alishahb/DiscordAnnouncer/master/DiscordAnnouncer/Properties/Resources/icon-150.png)
# DiscordAnnouncer ლ(ಠ益ಠლ) .
Application for easy maintaining Announcements posts to Discord.

It allows to create as many channels setups as you want for new beta \ release versions of my products. 

But you can use it for other purpose as well :)


![GUI Screenshot - Posting](https://raw.githubusercontent.com/alishahb/DiscordAnnouncer/master/PICS/DiscordAnnouncer-Posting.png)
##Dependencies
* .Net 4.6.1 (https://www.microsoft.com/en-us/download/details.aspx?id=49981)
* 7-Zip.CommandLine version 9.20.0 (https://www.nuget.org/packages/7-Zip.CommandLine/)
* JetBrains.Annotations version 10.2.1 (https://www.nuget.org/packages/JetBrains.Annotations/)
* Newtonsoft.Json version 9.0.1 (https://www.nuget.org/packages/Newtonsoft.Json/)
* Discord.Net" version 0.9.6 (https://www.nuget.org/packages/Discord.Net)

## Download
Release build: https://ci.appveyor.com/project/alishahb/discordannouncer/build/artifacts

##How To Setup

### Preparing 
1. Create Bot in Discord (https://discordapp.com/developers/applications/me)
2. Copy Bot ClientId and Token
3. Add Bot to your Server: https://discordapp.com/oauth2/authorize?client_id=XXX=bot&permissions=0 (replace XXX with your Bot ClientId)
4. Download last release of DiscordAnnouncer or compile source
5. Launch App

### Filling Data
1. Open Settings tab and fill Server ID (you can find it in Discord -> Server Settings -> Widget
2. Connect once, it will fetch all channels data for you
3. Checkout Server Data tab and fill channels ID to the Settings Tab for Channels you want to use for Announcement.
![GUI Screenshot - Settings](https://raw.githubusercontent.com/alishahb/DiscordAnnouncer/master/PICS/DiscordAnnouncer-Settings.png)

### Channnel Announcement Info
1. **Who** - Will be replaced with {WHO} when formating message, if exist in *Prefix*
2. **Type** - Will be replaced with {TYPE} when formating message, if exist in *Prefix*
3. **ProductName** - Will be replaced with {PRODUCT_NAME} when formating message, if exist in *Prefix*
4. **ChannelId** - To which channel message be posted
5. **Prefix**- line formating to be used to make prefix of message
6. **Name** - use for display in Posting tab, not editable

![GUI Screenshot - ServerData](https://raw.githubusercontent.com/alishahb/DiscordAnnouncer/master/PICS/DiscordAnnouncer-ServerData.png)

### Prefix Formating
1. **{WHO}** will be replaced with **Who** Property for Channel Item
2. **{TYPE}** will be replaced with **Type** Property for Channel Item
3. **{PRODUCT_NAME}** will be replaced with **ProductName** Property for Channel Item
4. **{VERSION}** will be repleaced with version number in Posting tab


## Legal
1. Icon used in Application "Dragon free icon" by Roundicons  (http://www.flaticon.com/free-icon/dragon_189021), Licence: http://file000.flaticon.com/downloads/license/license.pdf
