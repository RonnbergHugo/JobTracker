namespace JobTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobManager jobManager = new JobManager();
            jobManager.AddJob();
            jobManager.AddJob();
            jobManager.AddJob();
            jobManager.ShowStatistics();
            jobManager.UpdateStatus();
            jobManager.ShowStatistics();
        }
    }
}
