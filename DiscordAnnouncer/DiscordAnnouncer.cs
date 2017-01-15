using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Alisha.DiscordAnnouncer.Base;
using Alisha.DiscordAnnouncer.Settings;
using Discord;
using Discord.Legacy;
using Channel = Discord.API.Client.Channel;

namespace Alisha.DiscordAnnouncer
{
    public delegate void Data(string message);
    class DiscordAnnouncer
    {
        public event Data OnLogMessageRecieved;
        public event Data OnServerDataRecieved;
        public event EventHandler OnReadySend;

        public const string NAME = "Discord Announcer";

        public DiscordClient Client { get; private set; }
        public Server Server { get; private set; }
        public Discord.Channel Channel { get; private set; }
        public CommonSettings Settings { get; private set; }
        public string LogPath { get; private set; }
        public bool IsRunning { get; protected set; }

        public Version Version => typeof(DiscordAnnouncer).Assembly.GetName().Version;
        public void SendMessage(string message)
        {
            Channel = Server.AllChannels.FirstOrDefault(c => c.Id == Settings.SelectedChannelItem?.ChannelId);

            if (Channel == null)
            {
                Log($"Can't send message to channel  -- no channel with ID {Settings.SelectedChannelItem.ChannelId} found on current server");
                return;
            }
            Log($"Sending Message to channel #{Channel.Id} <{Channel.Name}>");
            Channel.SendMessage(message);
        }

        public ConsoleManager Console { get; set; }
        public DiscordAnnouncer()
        {
            Settings = SettingsManager<CommonSettings>.Create("Settings");

            if (Settings.Channels == null)
            {
                Settings.Channels = new ObservableCollection<ChannelItem>()
                {
                    new ChannelItem() {ChannelId = 0, Type = "Beta"},
                    new ChannelItem() {ChannelId = 0, Type = "Release"}
                };
            }

            if (Settings.SelectedChannelItem == null)
                Settings.SelectedChannelItem = Settings.Channels.FirstOrDefault(c => c.ChannelId > 0);

            SettingsManager<CommonSettings>.Save();


            LogPath = Path.Combine(Environment.CurrentDirectory, "log.txt");

            File.WriteAllText(LogPath, " ");


        }

        public async void Start()
        {
            if (IsRunning) return;

            Client = new DiscordClient((x) =>
            {
                x.LogLevel = LogSeverity.Debug;
                x.LogHandler = Log;
                x.AppName = NAME;
                x.AppUrl = "https://github.com/alishahb/DiscordAnnouncer";
                x.AppVersion = Version.ToString();
            });


            if (Settings.ServerID == 0 || string.IsNullOrWhiteSpace(Settings.Token))
            {
                IsRunning = false;
                MessageBox.Show("Fill out Server ID & Token Settings Tab before proceed");
                return;

            }
            IsRunning = true;

            //

            Client.ServerAvailable += Ready;

            //Console = new ConsoleManager();

            //_client.MessageReceived += async (s, e) =>
            //{
            //    if (!e.Message.IsAuthor)
            //        await e.Channel.SendMessage(e.Message.Text);
            //};



            //Client.ExecuteAndWait(async () =>
            //{
            Log($"--- Starting Connect", LogType.Console);

            await Task.Run(() =>
           {
               Client.ExecuteAndWait(async () =>
               {
                   await Client.Connect(Settings.Token, TokenType.Bot);
               });
           });


            //});
        }



        private void Ready(object sender, ServerEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                Log($"--- Joined Server <{e.Server.Name}>", LogType.Console);

                Client.SetGame(NAME);

                Log("---- SERVER INFO", LogType.ServerData);
                Log($"[{e.Server.Id}] {e.Server.Name}", LogType.ServerData);
                Log("---- END SERVER INFO", LogType.ServerData);

                if (Server == null)
                    Server = Client.Servers.FirstOrDefault(c => c.Id == Settings.ServerID);

                Log("---- CHANNELS", LogType.ServerData);
                foreach (var channel in e.Server.AllChannels)
                {
                    Log($"[{channel.Id}] {channel.Name}", LogType.ServerData);
                }
                Log("---- END CHANNELS", LogType.ServerData);

                Log("---- ROLES", LogType.ServerData);
                foreach (var role in e.Server.Roles)
                {
                    Log($"[{role.Id}] {role.Name}", LogType.ServerData);
                }
                Log("---- END ROLES", LogType.ServerData);

                if (Settings.SelectedChannelItem == null || Settings.SelectedChannelItem.ChannelId == 0)
                {
                    Exit();
                }
                else OnReadySend?.Invoke(null, null);

                Application.Current.MainWindow.Focus();

            });
        }

        private static DateTime _delay = DateTime.MinValue;
        private static void Exit()
        {
            if (DateTime.Now - _delay < TimeSpan.FromMinutes(1)) return;

            if (MessageBox.Show("Fill out ChannelId (grab from Server Data Tab) Settings.json before proceed") == MessageBoxResult.OK)
            {
                // Environment.Exit(0);
            }
        }

        internal enum LogType
        {
            Console,
            ServerData
        }

        public void Log(string message) => Log(message, LogType.Console);
        private void Log(object sender, LogMessageEventArgs e)
        {
            Log($"[{e.Severity}][{e.Source}] {e.Message}", LogType.Console);
        }

        private void Log(string message, LogType type)
        {
            //System.Console.WriteLine(message);
            File.AppendAllText(LogPath, $"[{DateTime.Now}] {message}\r\n");

            switch (type)
            {
                case LogType.Console:
                    OnLogMessageRecieved?.Invoke(message);
                    break;
                case LogType.ServerData:
                    OnServerDataRecieved?.Invoke(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

        }


        public Discord.Channel GetChannelInfo(ChannelItem channelItem)
        {
            return Server?.AllChannels.FirstOrDefault(c => c.Id == channelItem.ChannelId);
        }
    }
}
