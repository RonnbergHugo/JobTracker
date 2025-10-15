namespace JobTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobApplication job = new JobApplication();
            job.CompanyName = "Nvidia";
            job.Status = ApplicationStatus.Applied;
            job.SalaryExpectation = 50000;
            job.PositionTitle = "CEO";

            Console.WriteLine(job.GetSummary() + "\n\n" + job.GetDaysSinceApplied());
        }
    }
}
