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
            jobManager.ShowByStatus();
            jobManager.UpdateStatus();
            jobManager.ShowByStatus();
        }
    }
}
