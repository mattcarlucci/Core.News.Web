using System.Threading;
using System.Threading.Tasks;

namespace Core.News
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}