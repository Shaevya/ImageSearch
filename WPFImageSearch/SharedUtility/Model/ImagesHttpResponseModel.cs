using System.Text.Json.Serialization;

namespace ImageSearchServer.MVVM.Model
{
    public class ImagesHttpResponseModel {

        [JsonPropertyName("photos")]
        public ImageInfoModel ImagesInfo { get; set; }
    }
}
