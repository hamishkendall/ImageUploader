using System.ComponentModel.DataAnnotations;

namespace PhotoUploader.Models
{
    public class FileUploadModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select atleast 1 photo/video.")]
        public List<IFormFile> Files { get; set; }
    }
}
