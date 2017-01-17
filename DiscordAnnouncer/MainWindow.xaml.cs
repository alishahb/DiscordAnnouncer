using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Alisha.DiscordAnnouncer.Base;
using Alisha.DiscordAnnouncer.Settings;
using Discord.Legacy;
using Application = System.Windows.Application;

namespace Alisha.DiscordAnnouncer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiscordAnnouncer Core { get; set; }
        public CommonSettings Settings => Core.Settings;
        public NotifyIcon NotifyIcon { get; }
        public MainWindow()
        {
            Core = new DiscordAnnouncer();
            Core.OnLogMessageRecieved += OnLogMessageRecieved;
            Core.OnServerDataRecieved += OnServerDataRecieved;
            Core.OnReadySend += OnReadySend;
            InitializeComponent();

            Title = $"Alisha's {DiscordAnnouncer.NAME} -- {Core.Version}";
            Loaded += OnLoaded;
            Application.Current.Exit += OnExit;

            NotifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = Properties.Resources.icon1,
                Visible = true,
            };
            NotifyIcon.DoubleClick += delegate
            {
                Show();
                WindowState = WindowState.Normal;
            };
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            NotifyIcon.Dispose();
        }


        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }


        private void OnReadySend(object sender, EventArgs e)
        {
            SelectedChannelChanged(null, null);
        }

        private void OnLogMessageRecieved(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ConsoleHandler.AppendText(message);
                ConsoleHandler.AppendText("\r\n");
                ConsoleHandler.ScrollToEnd();
            });
        }
        private void OnServerDataRecieved(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ServerDataHandler.AppendText(message);
                ServerDataHandler.AppendText("\r\n");
                ServerDataHandler.ScrollToEnd();
            });
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Core.Start();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            var item = Settings.SelectedChannelItem;

            var version = txtVersion.Text.Trim();

            var who = item.Who;
            var prefix = Core.Settings.SelectedChannelItem.Prefix
                .Replace("{VERSION}", version)
                .Replace("{TYPE}", item.Type)
                .Replace("{PRODUCT_NAME}", item.ProductName)
                .Replace("{WHO}", who);

            var message = prefix +
                         $"```DIFF" +
                         "\r\n" +
                         txtMessage.Text +
                         $"```";

            Core.SendMessage(message);

            Save();

        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        protected void Save()
        {
            SettingsManager<CommonSettings>.Save();
            Core.Log("Settings saved");
            Core.Start();
        }

        private void SelectedChannelChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtInfo == null) return;

            txtInfo.Text = string.Empty;
            SendButton.IsEnabled = false;
            Settings.SelectedChannelItem = null;

            if (ChannelsCombobox.SelectedItem is ChannelItem)
            {
                var channelItem = (ChannelItem)ChannelsCombobox.SelectedItem;
                var channelData = Core.GetChannelInfo(channelItem);

                SendButton.IsEnabled = channelItem != null && channelItem.ChannelId > 0 && channelData != null;
                Settings.SelectedChannelItem = channelItem;
                if (channelData == null) return;

                txtInfo.Text = $"[{channelData.Id}] #{channelData.Name} ({channelItem.ToolTip})";
            }

            Save();


        }
    }
}

