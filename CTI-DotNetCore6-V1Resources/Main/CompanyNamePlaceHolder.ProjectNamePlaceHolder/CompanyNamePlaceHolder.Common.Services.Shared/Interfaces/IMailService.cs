using CompanyNamePlaceHolder.Common.Services.Shared.Models.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.Common.Services.Shared.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request, CancellationToken cancellationToken = default);
    }
}