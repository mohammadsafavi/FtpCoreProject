using System.ComponentModel.DataAnnotations;

namespace LocalFTPUploadProject.Models
{
    public class FileListDirectoryViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Type { get; set; }
        public decimal? Size { get; set; }
        public string? Url { get; set; }
        public FileListDirectoryViewModel()
        {
            
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Url = "http://www.mohammadsafavi.com/" + this.Name;
        }
    }
}
