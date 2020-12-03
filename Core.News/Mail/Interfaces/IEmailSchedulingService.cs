namespace Core.News.Services
{
    public interface IEmailSchedulingService
    {
        void CreateJobs();
        void Shutdown();
    }
}