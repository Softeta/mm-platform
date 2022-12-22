using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class JobAssignedEmployeeTests
    {
        [Theory, AutoMoqData]
        public void Update_ShouldUpdate(
            Guid jobId,
            Guid employeeId,
            string firstName,
            string lastName,
            string pictureUri,
            string newFirstName,
            string newLastName,
            string newPictureUri)
        {
            var assignedEmployee = new JobAssignedEmployee(jobId, employeeId, firstName, lastName, pictureUri);

            // Act
            assignedEmployee.Update(newFirstName, newLastName, newPictureUri);

            // Assert
            Assert.Equal(newFirstName, assignedEmployee.Employee.FirstName);
            Assert.Equal(newLastName, assignedEmployee.Employee.LastName);
            Assert.Equal(newPictureUri, assignedEmployee.Employee.PictureUri);
        }
    }
}
