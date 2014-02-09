using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HolidaysApi.WebHostCS
{
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

                        response.Headers.CacheControl.Public = true;
                        response.Headers.CacheControl.MaxAge = TimeSpan.FromDays(30);

                        return response;
                    }, cancellationToken);
        }
    }
}