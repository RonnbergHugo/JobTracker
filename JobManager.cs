using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace JobTracker {
    internal class JobManager {
        List<JobApplication> jobApplications = new List<JobApplication>();
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
            
            jobApplications.Add(new JobApplication() {
                CompanyName = company,
                PositionTitle = position,
                SalaryExpectation = salary
            });

            if (companies.ContainsKey(company)) {
                companies[company].Add(jobApplications.Last());
            }
            else {
                companies.Add(company, new List<JobApplication> { jobApplications.Last() });
            }
            Console.WriteLine("Your application has been added.");
            Console.ReadKey();
        }

        public void UpdateStatus() {
            MultiSelectionPrompt<JobApplication> prompt;
            List<JobApplication> selectedPositions;
            do {
                Console.Clear();

                prompt = new MultiSelectionPrompt<JobApplication>()
                    .Title("Which job application status would you like to update?")
                    .PageSize(jobApplications.Count)
                    .InstructionsText("[grey](Press [blue]<space>[/] to toggle and [green]<enter>[/] to accept)[/]")
                    .UseConverter(item => string.IsNullOrEmpty(item.PositionTitle) ? $"[bold yellow]{item.CompanyName}[/]" : item.PositionTitle);

                foreach (KeyValuePair<string, List<JobApplication>> c in companies) {
                    JobApplication company = new JobApplication();
                    company.CompanyName = c.Key;
                    List<JobApplication> positions = jobApplications.Where(j => j.CompanyName == c.Key).Select(j => new JobApplication() { CompanyName = j.CompanyName, PositionTitle = j.PositionTitle }).ToList();
                    prompt.AddChoiceGroup(company, positions);
                }

                selectedPositions = AnsiConsole.Prompt(prompt);
                Console.WriteLine("You have selected:");
                foreach (JobApplication j in selectedPositions) {
                    Console.WriteLine(j.PositionTitle + " at " + j.CompanyName);
                }
                Console.WriteLine("Continue? y/n");
            }
            while (string.Equals("n", Console.ReadLine(), StringComparison.OrdinalIgnoreCase));

            Console.Clear();

            jobApplications.ForEach(j => selectedPositions.ForEach(s => j.Status = j.PositionTitle == s.PositionTitle && j.CompanyName == s.CompanyName ? AnsiConsole.Prompt(
                    new SelectionPrompt<ApplicationStatus>()
                        .Title("To what would you like to update the status of " + j.PositionTitle + " at " + j.CompanyName + "? The current status is " + j.Status)
                        .PageSize(4)
                        .AddChoices(Enum.GetValues<ApplicationStatus>())) : j.Status));

            jobApplications.ForEach(j => selectedPositions.ForEach(s => Console.Write(j.PositionTitle == s.PositionTitle && j.CompanyName == s.CompanyName ? "You changed the status of " + j.PositionTitle + " at " + j.CompanyName + " to " + j.Status + "\n" : "")));
            Console.ReadKey();
        }

        public void ShowAll() {
            Console.Clear();

            foreach (KeyValuePair<string, List<JobApplication>> c in companies) {
                Console.WriteLine(c.Key);
                c.Value.ForEach(c => Console.WriteLine(c.ResponseDate == null ? "    " + c.GetSummary() + "." : "    " + c.GetSummary() + " and they responded on " + c.ResponseDate + "."));
            }
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
                foreach (JobApplication p in c.Value.Where(j => selection.Contains(j.Status))) {
                    Console.WriteLine(p.ResponseDate == null ? "    " + p.GetSummary() + "." : "    " + p.GetSummary() + " and they responded on " + p.ResponseDate + ".");
                }
            }
            Console.ReadKey();
        }

        public void ShowStatistics() {

        }
    }
}