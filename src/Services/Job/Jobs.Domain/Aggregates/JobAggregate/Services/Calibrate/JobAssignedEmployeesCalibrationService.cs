using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobAssignedEmployeesCalibrationService
    {
        public static void Calibrate(this List<JobAssignedEmployee> current, IEnumerable<JobAssignedEmployee> request, Guid jobId)
        {
            var requestEmployeeIds = new HashSet<Guid>(request.Select(e => e.Employee.Id));
            var currentEmployeeIds = new HashSet<Guid>(current.Select(e => e.Employee.Id));

            var equals = requestEmployeeIds.SetEquals(currentEmployeeIds);

            if (!equals)
            {
                current.RemoveAll(e => !requestEmployeeIds.Contains(e.Employee.Id));

                var assignedEmployeesToAdd = request.Where(e => !currentEmployeeIds.Contains(e.Employee.Id));

                foreach (var assignedEmployee in assignedEmployeesToAdd)
                {
                    current.Add(new JobAssignedEmployee(
                        jobId, 
                        assignedEmployee.Employee.Id,
                        assignedEmployee.Employee.FirstName,
                        assignedEmployee.Employee.LastName,
                        assignedEmployee.Employee.PictureUri)
                    );
                }
            }
        }
    }
}
