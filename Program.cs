namespace JobTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobManager jobManager = new JobManager();
            bool exit = false;

            while (!exit) {
                Console.Clear();
                Console.WriteLine("=== Job Tracker Menu ===");
                Console.WriteLine("1. Add a job application");
                Console.WriteLine("2. Update application status");
                Console.WriteLine("3. Show all applications");
                Console.WriteLine("4. Show applications by status");
                Console.WriteLine("5. Show statistics");
                Console.WriteLine("6. Exit");
                Console.Write("\nSelect an option (1-6): ");

                string input = Console.ReadLine();
                Console.Clear();

                switch (input) {
                    case "1":
                        jobManager.AddJob();
                        break;
                    case "2":
                        jobManager.UpdateStatus();
                        break;
                    case "3":
                        jobManager.ShowAll();
                        break;
                    case "4":
                        jobManager.ShowByStatus();
                        break;
                    case "5":
                        jobManager.ShowStatistics();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 6.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
