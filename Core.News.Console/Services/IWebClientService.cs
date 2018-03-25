using System.Threading;
using System.Threading.Tasks;

namespace Core.News
{
    public interface IWebClientService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}