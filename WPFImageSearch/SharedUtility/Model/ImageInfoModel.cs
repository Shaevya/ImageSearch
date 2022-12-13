using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ImageSearchServer.MVVM.Model
{
    public class ImageInfoModel
    {
        public ImageInfoModel()
        {
            Images = new List<ImageModel>();
        }
        [JsonPropertyName("page")]
        public int CurrentPage { get; set; }


        [JsonPropertyName("pages")]
        public int TotalPages { get; set; }


        [JsonPropertyName("perpage")]
        public int ItemsPerPage { get; set; }


        [JsonPropertyName("total")]
        public int TotalResults { get; set; }


        [JsonPropertyName("photo")]
        public List<ImageModel> Images { get; set; }
    }
}
