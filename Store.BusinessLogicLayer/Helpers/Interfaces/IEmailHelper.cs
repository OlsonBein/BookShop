using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        Task<bool> SendMessageAsync(string to, string body, string subject);
    }
}
