using Spectre.Console;

namespace JobTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobManager jobManager = new JobManager();
            bool exit = false;

            jobManager.JobApplications.Add(new JobApplication
            {
                CompanyName = "Volvo Cars",
                PositionTitle = "Systemutvecklare",
                Status = ApplicationStatus.Applied,
                ApplicationDate = DateTime.Now.AddDays(-15),
                SalaryExpectation = 45000,
                ResponseDate = null
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Spotify",
                PositionTitle = "Backend Developer",
                Status = ApplicationStatus.Interview,
                ApplicationDate = DateTime.Now.AddDays(-10),
                SalaryExpectation = 52000,
                ResponseDate = DateTime.Now.AddDays(-3)
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Ericsson",
                PositionTitle = "Testare",
                Status = ApplicationStatus.Rejected,
                ApplicationDate = DateTime.Now.AddDays(-25),
                SalaryExpectation = 40000,
                ResponseDate = DateTime.Now.AddDays(-5)
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "IKEA IT",
                PositionTitle = "Frontend Developer",
                Status = ApplicationStatus.Offer,
                ApplicationDate = DateTime.Now.AddDays(-7),
                SalaryExpectation = 48000,
                ResponseDate = DateTime.Now.AddDays(-1)
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Saab AB",
                PositionTitle = "C# Utvecklare",
                Status = ApplicationStatus.Applied,
                ApplicationDate = DateTime.Now.AddDays(-30),
                SalaryExpectation = 46000,
                ResponseDate = null
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Klarna",
                PositionTitle = "Fullstack Developer",
                Status = ApplicationStatus.Interview,
                ApplicationDate = DateTime.Now.AddDays(-12),
                SalaryExpectation = 55000,
                ResponseDate = null
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Tietoevry",
                PositionTitle = "Systemanalytiker",
                Status = ApplicationStatus.Applied,
                ApplicationDate = DateTime.Now.AddDays(-20),
                SalaryExpectation = 47000,
                ResponseDate = null
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Northvolt",
                PositionTitle = "Automation Engineer",
                Status = ApplicationStatus.Rejected,
                ApplicationDate = DateTime.Now.AddDays(-18),
                SalaryExpectation = 49000,
                ResponseDate = DateTime.Now.AddDays(-2)
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Microsoft Sverige",
                PositionTitle = "Cloud Architect",
                Status = ApplicationStatus.Offer,
                ApplicationDate = DateTime.Now.AddDays(-9),
                SalaryExpectation = 68000,
                ResponseDate = DateTime.Now.AddDays(-1)
            });

            jobManager.JobApplications.Add(new JobApplication {
                CompanyName = "Göteborgs Stad IT",
                PositionTitle = "IT-tekniker",
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
                    "1. Add a job application",
                    "2. Update application status",
                    "3. Show all applications",
                    "4. Show applications by status",
                    "5. Show statistics",
                    "6. Exit"
                        }));

                switch (choice) {
                    case "1. Add a job application":
                        jobManager.AddJob();
                        break;
                    case "2. Update application status":
                        jobManager.UpdateStatus();
                        break;
                    case "3. Show all applications":
                        jobManager.ShowAll();
                        break;
                    case "4. Show applications by status":
                        jobManager.ShowByStatus();
                        break;
                    case "5. Show statistics":
                        jobManager.ShowStatistics();
                        break;
                    case "6. Exit":
                        exit = true;
                        AnsiConsole.MarkupLine("[green]Goodbye![/]");
                        break;
                }
            }
        }
    }
}
