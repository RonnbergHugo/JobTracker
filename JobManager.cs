using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker {
    internal class JobManager {
        List<JobApplication> jobApplications = new List<JobApplication>();

        public void AddJob() {
            string company;
            string position;
            int salary;
            do {
                Console.Write("Where did you apply? ");
                company = Console.ReadLine();
                Console.WriteLine("To which position did you apply? ");
                Console.Write("What salary do you expect? ");
                Console.Clear();
            }
            while (company == null || company == "");

            do {
                Console.WriteLine("Where did you apply? ");
                Console.Write("To which position did you apply? ");
                position = Console.ReadLine();
                Console.Write("What salary do you expect? ");
                Console.Clear();
            }
            while (position == null || position == "");

            do {
                Console.WriteLine("Where did you apply? ");
                Console.WriteLine("To which position did you apply? ");
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
            Console.WriteLine("Your application has been added.");
        }

        public void UpdateStatus() {

        }

        public void ShowAll() {

        }

        public void ShowByStatus() {

        }

        public void ShowStatistics() {

        }
    }
}
