using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace moviesApp.Services
{
    public class ProxyService: IProxyService
    {
        public ProxyService()
        {
        }

        public async Task<HttpResponseMessage> GetFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                return response;
            }
        }
    }
}
