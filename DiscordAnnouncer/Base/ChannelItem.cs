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

        public bool Equals(ChannelItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_who, other._who) && string.Equals(_type, other._type) && string.Equals(_productName, other._productName) && string.Equals(_prefix, other._prefix) && _channelId == other._channelId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChannelItem)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _who?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (_type?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_productName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_prefix?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ _channelId.GetHashCode();
                return hashCode;
            }
        }

        #endregion EndOf-INotifyPropertyChanged


    }
}
