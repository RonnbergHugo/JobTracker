using Spectre.Console;

namespace JobTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobManager jobManager = new JobManager();
            bool exit = false;

            jobManager.AddJob(new JobApplication {
                CompanyName = "Volvo Cars",
                PositionTitle = "System Developer",
                Status = ApplicationStatus.Applied,
                ApplicationDate = DateTime.Now.AddDays(-15),
                SalaryExpectation = 45000,
                ResponseDate = null
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Spotify",
                PositionTitle = "Backend Developer",
                Status = ApplicationStatus.Interview,
                ApplicationDate = DateTime.Now.AddDays(-10),
                SalaryExpectation = 52000,
                ResponseDate = DateTime.Now.AddDays(-3)
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Ericsson",
                PositionTitle = "Tester",
                Status = ApplicationStatus.Rejected,
                ApplicationDate = DateTime.Now.AddDays(-25),
                SalaryExpectation = 40000,
                ResponseDate = DateTime.Now.AddDays(-5)
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "IKEA IT",
                PositionTitle = "Frontend Developer",
                Status = ApplicationStatus.Offer,
                ApplicationDate = DateTime.Now.AddDays(-7),
                SalaryExpectation = 48000,
                ResponseDate = DateTime.Now.AddDays(-1)
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Volvo Cars",
                PositionTitle = "Backend Developer",
                Status = ApplicationStatus.Interview,
                ApplicationDate = DateTime.Now.AddDays(-6),
                SalaryExpectation = 53000,
                ResponseDate = null
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Klarna",
                PositionTitle = "Fullstack Developer",
                Status = ApplicationStatus.Interview,
                ApplicationDate = DateTime.Now.AddDays(-12),
                SalaryExpectation = 55000,
                ResponseDate = null
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Spotify",
                PositionTitle = "Frontend Developer",
                Status = ApplicationStatus.Applied,
                ApplicationDate = DateTime.Now.AddDays(-4),
                SalaryExpectation = 50000,
                ResponseDate = null
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Northvolt",
                PositionTitle = "Automation Engineer",
                Status = ApplicationStatus.Rejected,
                ApplicationDate = DateTime.Now.AddDays(-18),
                SalaryExpectation = 49000,
                ResponseDate = DateTime.Now.AddDays(-2)
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Microsoft Sweden",
                PositionTitle = "Cloud Architect",
                Status = ApplicationStatus.Offer,
                ApplicationDate = DateTime.Now.AddDays(-9),
                SalaryExpectation = 68000,
                ResponseDate = DateTime.Now.AddDays(-1)
            });

            jobManager.AddJob(new JobApplication {
                CompanyName = "Göteborg IT Services",
                PositionTitle = "IT Technician",
                Status = ApplicationStatus.Applied,
                ApplicationDate = DateTime.Now.AddDays(-5),
                SalaryExpectation = 39000,
                ResponseDate = null
            });

            while (!exit) {
                Console.Clear();

                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold green]Job Tracker Menu[/]")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                    "Add a job application",
                    "Update application status",
                    "Show all applications",
                    "Show applications by status",
                    "Show statistics",
                    "Remove application",
                    "Exit"
                        }));

                switch (choice) {
                    case "Add a job application":
                        jobManager.AddJob();
                        break;
                    case "Update application status":
                        jobManager.UpdateStatus();
                        break;
                    case "Show all applications":
                        jobManager.ShowAll();
                        break;
                    case "Show applications by status":
                        jobManager.ShowByStatus();
                        break;
                    case "Show statistics":
                        jobManager.ShowStatistics();
                        break;
                    case "Remove application":
                        jobManager.RemoveJobApplication();
                        break;
                    case "Exit":
                        exit = true;
                        AnsiConsole.MarkupLine("[green]Goodbye![/]");
                        break;
                }
            }
        }
    }
}
