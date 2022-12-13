using System.Text.Json.Serialization;

namespace ImageSearchServer.MVVM.Model
{

    public class ImageModel : ObservableObject
    {

        private string _id = string.Empty;
        private string _owner = string.Empty;
        private string _secret = string.Empty;
        private string _server = string.Empty;
        private string _title = string.Empty;
        private string _url = string.Empty;
        private int _farm;
        private int _isPublic;
        private int _isFriend;
        private int _isFamily;


        [JsonPropertyName("id")]
        public string Id
        {
            get { return _id; }
            set { 
                _id = value; 
                OnPropertyChanged(); 
            }
        }


        [JsonPropertyName("owner")]
        public string Owner
        {
            get { return _owner; }
            set { _owner = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("secret")]
        public string Secret
        {
            get { return _secret; }
            set { _secret = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("server")]
        public string Server
        {
            get { return _server; }
            set { _server = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("farm")]
        public int Farm
        {
            get { return _farm; }
            set { _farm = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("ispublic")]
        public int IsPublic 
        {
            get { return _isPublic; }
            set { _isPublic = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("isfriend")]
        public int IsFriend
        {
            get { return _isFriend; }
            set { _isFriend = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("isfamily")]
        public int IsFamily
        {
            get { return _isFamily; }
            set { _isFamily = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("url")]
        public string URL
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(); }
        }
    }
}
