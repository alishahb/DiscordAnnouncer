using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Alisha.DiscordAnnouncer.Base;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Alisha.DiscordAnnouncer.Settings
{
    [DataContract]
    public class CommonSettings
    {

        #region Channels
        protected ObservableCollection<ChannelItem> _channels;
        [DataMember]
        public ObservableCollection<ChannelItem> Channels
        {
            get { return _channels; }
            set { _channels = value; OnPropertyChanged(); }
        }
        #endregion EndOf-Channels


        #region Token
        protected string _token;
        [DataMember]
        public string Token
        {
            get { return _token; }
            set { _token = value; OnPropertyChanged(); }
        }
        #endregion EndOf-Token



        #region ServerID
        protected ulong _serverID;
        [DataMember]
        public ulong ServerID
        {
            get { return _serverID; }
            set { _serverID = value; OnPropertyChanged(); }
        }
        #endregion EndOf-ServerID



        #region SelectedChannelItem
        protected ChannelItem _selectedChannelItem;
        [DataMember]
        public ChannelItem SelectedChannelItem
        {
            get { return _selectedChannelItem; }
            set { _selectedChannelItem = value; OnPropertyChanged(); }
        }
        #endregion EndOf-SelectedChannelItem



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion EndOf-INotifyPropertyChanged


    }
}
