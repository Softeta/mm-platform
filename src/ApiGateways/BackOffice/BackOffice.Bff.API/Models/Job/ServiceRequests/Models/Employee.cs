using BackOffice.Bff.API.Models.Users;
using Common = Contracts.Job;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests.Models
{
    public class Employee : Common.Employee
    {
        public static Common.Employee ToServiceRequest(BackOfficeUser employee)
        {
            return new Common.Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PictureUri = employee.PictureUri
            };
        }
    }
}
