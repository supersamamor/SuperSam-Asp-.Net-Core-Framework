using ProjectNamePlaceHolder.Application.Requests.Mail;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}