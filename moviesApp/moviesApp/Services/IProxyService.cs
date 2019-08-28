using System.Net.Http;
using System.Threading.Tasks;

namespace moviesApp.Services
{
    public interface IProxyService
    {
        Task<HttpResponseMessage> GetFromUrl(string url);
    }
}
