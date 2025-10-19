using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace JobTracker {
    internal class JobManager {
        public List<JobApplication> JobApplications = new List<JobApplication>();
        Dictionary<string, List<JobApplication>> companies = new Dictionary<string, List<JobApplication>>();

        public void AddJob() {
            string company;
            string position;
            int salary;
            Console.Clear();

            do {
                Console.Write("Where did you apply? ");
                company = Console.ReadLine();
                Console.WriteLine("To which position did you apply? ");
                Console.Write("What salary do you expect? ");
                Console.Clear();
            }
            while (company == null || company == "");

            do {
                Console.WriteLine("Where did you apply? " + company);
                Console.Write("To which position did you apply? ");
                position = Console.ReadLine();
                Console.Write("What salary do you expect? ");
                Console.Clear();
            }
            while (position == null || position == "");

            do {
                Console.WriteLine("Where did you apply? " + company);
                Console.WriteLine("To which position did you apply? " + position);
                Console.Write("What salary do you expect? ");
                int.TryParse(Console.ReadLine(), out salary);
                Console.Clear();
            }
            while (salary <= 0);
            
            JobApplications.Add(new JobApplication() {
                CompanyName = company,
                PositionTitle = position,
                SalaryExpectation = salary
            });

            if (companies.ContainsKey(company)) {
                companies[company].Add(JobApplications.Last());
            }
            else {
                companies.Add(company, new List<JobApplication> { JobApplications.Last() });
            }
            Console.WriteLine("Your application has been added.\nPress any key to continue...");
            Console.ReadKey();
        }

        public void UpdateStatus() {
            List<JobApplication> selectedJobApplications;
            do {
                Console.Clear();

                MultiSelectionPrompt<JobApplication> prompt = new MultiSelectionPrompt<JobApplication>()
                    .Title("Which job application status would you like to update?")
                    .PageSize(JobApplications.Count > 2 ? JobApplications.Count : 3)
                    .InstructionsText("[grey](Press [blue]<space>[/] to toggle and [green]<enter>[/] to accept)[/]")
                    .UseConverter(item => string.IsNullOrEmpty(item.PositionTitle) ? $"[bold yellow]{item.CompanyName}[/]" : item.PositionTitle);

                foreach (KeyValuePair<string, List<JobApplication>> c in companies) {
                    JobApplication company = new JobApplication();
                    company.CompanyName = c.Key;
                    prompt.AddChoiceGroup(company, JobApplications.Where(j => j.CompanyName == c.Key).ToList());
                }

                selectedJobApplications = AnsiConsole.Prompt(prompt);
                Console.WriteLine("You have selected:");
                selectedJobApplications.ForEach(j => Console.WriteLine(j.PositionTitle + " at " + j.CompanyName));
                Console.WriteLine("Continue? y/n");
            }
            while (string.Equals("n", Console.ReadLine(), StringComparison.OrdinalIgnoreCase));

            Console.Clear();

            JobApplications.ForEach(j => j.Status = selectedJobApplications.Contains(j) ? AnsiConsole.Prompt(
                    new SelectionPrompt<ApplicationStatus>()
                        .Title("To what would you like to update the status of " + j.PositionTitle + " at " + j.CompanyName + "? The current status is " + j.Status)
                        .PageSize(4)
                        .AddChoices(Enum.GetValues<ApplicationStatus>())) : j.Status);

            JobApplications.ForEach(j => Console.Write(selectedJobApplications.Contains(j) ? "You changed the status of " + j.PositionTitle + " at " + j.CompanyName + " to " + j.Status + "\n" : ""));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void ShowAll() {
            Console.Clear();

            foreach (KeyValuePair<string, List<JobApplication>> c in companies) {
                Console.WriteLine(c.Key);
                c.Value.ForEach(j => Console.WriteLine(j.ResponseDate == null ? "    " + j.GetSummary() + "." : "    " + j.GetSummary() + " and they responded on " + j.ResponseDate + "."));
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void ShowByStatus() {
            Console.Clear();

            List<ApplicationStatus> selection = AnsiConsole.Prompt(
                new MultiSelectionPrompt<ApplicationStatus>()
                    .Title("Which status would you like to show?")
                    .PageSize(4)
                    .InstructionsText("[grey](Press [blue]<space>[/] to toggle and [green]<enter>[/] to accept)[/]")
                    .AddChoices(Enum.GetValues<ApplicationStatus>()));

            foreach (KeyValuePair<string, List<JobApplication>> c in companies.Where(p => p.Value.Any(j => selection.Contains(j.Status)))) {
                Console.WriteLine(c.Key);
                foreach (JobApplication j in c.Value.Where(j => selection.Contains(j.Status))) {
                    Console.WriteLine(j.ResponseDate == null ? "    " + j.GetSummary() + "." : "    " + j.GetSummary() + " and they responded on " + j.ResponseDate + ".");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void ShowStatistics() {
            Console.Clear();

            (string, string) choice = AnsiConsole.Prompt(
            new SelectionPrompt<(string, string)>()
                .Title("Which statistic would you like to show?")
                .PageSize(7)
                .UseConverter(item => item.Item1)
                .AddChoices(new List<(string, string)>() { ("Amount by status", ""), ("Average response time", ""), ("No response in: [italic]Days[/]", "") })
                .AddChoiceGroup(("Order by application date", ""), new List<(string, string)>() { ("Ascending", "0"), ("Descending", "0") })
                .AddChoiceGroup(("Order by response date", ""), new List<(string, string)>() { ("Ascending", "1"), ("Descending", "1") }));

            switch ((choice.Item1, choice.Item2)) {
                case ("Amount by status", _):
                    Console.WriteLine(JobApplications.Any() ? "Applied: " + JobApplications.Where(j => j.Status == Enum.GetValues<ApplicationStatus>()[0]).Count() : "There are no job applications.");
                    Console.WriteLine(JobApplications.Any() ? "Interview: " + JobApplications.Where(j => j.Status == Enum.GetValues<ApplicationStatus>()[1]).Count() : "There are no job applications.");
                    Console.WriteLine(JobApplications.Any() ? "Offer: " + JobApplications.Where(j => j.Status == Enum.GetValues<ApplicationStatus>()[2]).Count() : "There are no job applications.");
                    Console.WriteLine(JobApplications.Any() ? "Rejected: " + JobApplications.Where(j => j.Status == Enum.GetValues<ApplicationStatus>()[3]).Count() : "There are no job applications.");
                    break;
                case ("Average response time", _):
                    Console.WriteLine(JobApplications.Any() ? JobApplications.Where(j => j.ResponseDate != null).Any() ? JobApplications.Where(j => j.ResponseDate != null).Average(j => (j.ResponseDate.Value - j.ApplicationDate).Days) : "There are no applications with responses." : "There are no job applications.");
                    break;
                case ("Ascending", "0"):
                    Console.WriteLine(JobApplications.Any() ? string.Join("\n", JobApplications.OrderBy(j => j.ApplicationDate).Select(j => j.GetSummary())) : "There are no job applications.");
                    break;
                case ("Descending", "0"):
                    Console.WriteLine(JobApplications.Any() ? string.Join("\n", JobApplications.OrderByDescending(j => j.ApplicationDate).Select(j => j.GetSummary())) : "There are no job applications.");
                    break;
                case ("Ascending", "1"):
                    Console.WriteLine(JobApplications.Any() ? JobApplications.Any(j => j.ResponseDate != null) ? string.Join("\n", JobApplications.OrderBy(j => j.ResponseDate).Select(j => j.GetSummary())) : "There are no applications with responses." : "There are no job applications.");
                    break;
                case ("Descending", "1"):
                    Console.WriteLine(JobApplications.Any() ? JobApplications.Any(j => j.ResponseDate != null) ? string.Join("\n", JobApplications.OrderByDescending(j => j.ResponseDate).Select(j => j.GetSummary())) : "There are no applications with responses." : "There are no job applications.");
                    break;
                default:
                    int days = AnsiConsole.Prompt(
                        new TextPrompt<int>("No response in: ")
                            .DefaultValue(14));
                    Console.WriteLine(JobApplications.Any() ? JobApplications.Any(j => j.ResponseDate == null) ? JobApplications.Any(j => j.GetDaysSinceApplied() > days) ? string.Join("\n", JobApplications.Where(j => j.GetDaysSinceApplied() > days && j.ResponseDate == null).Select(j => j.GetSummary())) : "There are no applications without responses older than " + days + " days." : "There are only applications with responses.\n": "There are no job applications.");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}