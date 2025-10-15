using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker {
    public enum ApplicationStatus {
        Applied,
        Interview,
        Offer,
        Rejected
    }
    internal class JobApplication {
        public string CompanyName;
        public string PositionTitle;
        public DateTime ApplicationDate;
        public ApplicationStatus Status;
        public DateTime? ResponseDate;
        public int SalaryExpectation;

        public int GetDaysSinceApplied() {
            return (DateTime.Now - ApplicationDate).Days;
        }
        public string GetSummary() {
            return "You applied to " + CompanyName + " on " + ApplicationDate + " for the position " + PositionTitle + " with the expected salary of " + SalaryExpectation + " and the current satus is " + Status;
        }

    }
}
