using GloboTicket.TicketManagement.Application.Models.Mail;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Contracts.Infrastructure.Mail
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}