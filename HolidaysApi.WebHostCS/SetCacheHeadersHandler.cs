using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HolidaysApi.WebHostCS
{
    using System.Net.Http.Headers;

    public class SetCacheHeadersHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                task =>
                    {
                        var response = task.Result;

                        response.Headers.CacheControl = new CacheControlHeaderValue
                                                            {
                                                                Public = true,
                                                                MaxAge = TimeSpan.FromDays(30)
                                                            };

                        return response;
                    }, cancellationToken);
        }
    }
}