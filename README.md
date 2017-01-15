# DiscordAnnouncer
Application for easy maintaining Announcements posts to Discord

## Dependencies
1. .Net 4.6.1
2. 7-Zip.CommandLine version 9.20.0
3. JetBrains.Annotations version 10.2.1
4. Newtonsoft.Json version 9.0.1"

## How To Setup

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

### Channnel Announcement Info
1. **Who** - Will be replaced with {WHO} when formating message, if exist in *Prefix*
2. **Type** - Will be replaced with {TYPE} when formating message, if exist in *Prefix*
3. **ProductName** - Will be replaced with {PRODUCT_NAME} when formating message, if exist in *Prefix*
4. **ChannelId** - To which channel message be posted
5. **Prefix**- line formating to be used to make prefix of message
6. **Name** - use for display in Posting tab, not editable

### Prefix Formating
1. **{WHO}** will be replaced with **Who** Property for Channel Item
2. **{TYPE}** will be replaced with **Type** Property for Channel Item
3. **{PRODUCT_NAME}** will be replaced with **ProductName** Property for Channel Item
4. **{VERSION}** will be repleaced with version number in Posting tab

