using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobAssignedEmployeesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobAssignedEmployees_ShouldNotChange_WhenRequestedEmployeeIdsSameAsCurrent(
            Guid jobId,
            Employee employee1,
            Employee employee2,
            Employee employee3)
        {
            // Arrange
            var expected = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee1.Id, employee1.FirstName, employee1.LastName, employee1.PictureUri),
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri)
            };

            var expectedAssignedEmployeesIds = expected.Select(x => x.Employee.Id).ToHashSet();

            var current = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee1.Id, employee1.FirstName, employee1.LastName, employee1.PictureUri),
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri)
            };

            var request = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee1.Id, employee1.FirstName, employee1.LastName, employee1.PictureUri),
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedAssignedEmployeesIds, current.Select(x => x.Employee.Id));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobAssignedEmployees_ShouldChange_WhenPartOfJobAssignedEmployeeIdsDifferent(
            Guid jobId,
            Employee employee1,
            Employee employee2,
            Employee employee3,
            Employee employee4)
        {
            // Arrange
            var expected = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri),
                new JobAssignedEmployee(jobId, employee4.Id, employee4.FirstName, employee4.LastName, employee4.PictureUri)
            };

            var expectedAssignedEmployeesIds = expected.Select(x => x.Employee.Id).ToHashSet();

            var current = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee1.Id, employee1.FirstName, employee1.LastName, employee1.PictureUri),
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri)
            };

            var request = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee4.Id, employee4.FirstName, employee4.LastName, employee4.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedAssignedEmployeesIds, current.Select(x => x.Employee.Id));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobAssignedEmployees_ShouldAdd_WhenCurrentEmployeeIdsEmpty(
            Guid jobId,
            Employee employee1,
            Employee employee2,
            Employee employee3,
            Employee employee4)
        {
            // Arrange
            var expected = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee4.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee4.FirstName, employee4.LastName, employee4.PictureUri)
            };

            var expectedAssignedEmployeesIds = expected.Select(x => x.Employee.Id).ToHashSet();

            var current = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee1.Id, employee1.FirstName, employee1.LastName, employee1.PictureUri),
            };

            var request = new List<JobAssignedEmployee>() {
                new JobAssignedEmployee(jobId, employee2.Id, employee2.FirstName, employee2.LastName, employee2.PictureUri),
                new JobAssignedEmployee(jobId, employee4.Id, employee4.FirstName, employee4.LastName, employee4.PictureUri),
                new JobAssignedEmployee(jobId, employee3.Id, employee3.FirstName, employee3.LastName, employee3.PictureUri)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedAssignedEmployeesIds, current.Select(x => x.Employee.Id));
        }
    }
}
