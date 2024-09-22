namespace e_commerce.Services
{
    public interface IMailingService
    {
        Task SendEmailAsync(string mailto,string subject, List<string> body,IList<IFormFile> attachments = null);
    }
}
