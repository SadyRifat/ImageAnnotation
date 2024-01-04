using System.ComponentModel.DataAnnotations;

namespace ImageAnnotation.Models
{
    public class AssetInfo
    {
        [Key]
        public string Id { get; set; }
        public string ImgSrc { get; set; }
        public string? MarkState { get; set; }
    }
}
