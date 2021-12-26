using System.ComponentModel.DataAnnotations;

namespace LocalFTPUploadProject.Models
{
    public class UploadFileViewModelcs
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
        public string? FileName { get; set; }
    }
}
