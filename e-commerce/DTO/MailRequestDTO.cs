using System.ComponentModel.DataAnnotations;

namespace e_commerce.DTO
{
    public class MailRequestDTO
    {
        [Required]
        public string ToEmail { get; set; }
        [Required]

        public string Subject { get; set; }
        [Required]

        public string Body { get; set; }
        public IList<IFormFile> Attachments  { get; set; }

    }
}
