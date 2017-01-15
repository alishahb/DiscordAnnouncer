using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Alisha.DiscordAnnouncer.Base
{
    public class ChannelItem : INotifyPropertyChanged, IEquatable<ChannelItem>
    {
        public ChannelItem()
        {
            Prefix = ":golf: {WHO} **{PRODUCT_NAME}** new {TYPE} pushed @{VERSION},  restart HB required :)";
            Who = "@everyone";

        }

        #region Who
        protected string _who;
        [DataMember]
        public string Who
        {
            get { return _who; }
            set { _who = value; OnPropertyChanged(); }
        }
        #endregion EndOf-Who


        #region Type
        protected string _type;
        [DataMember]
        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        #endregion EndOf-Type


        #region ProductName
        protected string _productName;
        [DataMember]
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(); }
        }
        #endregion EndOf-ProductName


        #region Prefix
        protected string _prefix;
        [DataMember]
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; OnPropertyChanged(); }
        }
        #endregion EndOf-Prefix


        #region ChannelId
        protected ulong _channelId;
        [DataMember]
        public ulong ChannelId
        {
            get { return _channelId; }
            set { _channelId = value; OnPropertyChanged(); }
        }
        #endregion EndOf-ChannelId

        public string Name => $"{ProductName} [{Type}]";

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(ChannelItem other) => other?.ChannelId == ChannelId && Name == other?.Name;
        public override bool Equals(object obj) => (obj is ChannelItem) && Equals((ChannelItem)obj);
        public override int GetHashCode() => (int)ChannelId;

        #endregion EndOf-INotifyPropertyChanged


    }
}
