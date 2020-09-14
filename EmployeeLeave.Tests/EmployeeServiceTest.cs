using EmployeeLeave.Core.Services;
using EmployeeLeave.Core.Repository;
using EmployeeLeave.Core.Models;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using System;

namespace EmployeeLeave.Tests
{
    [TestFixture]
    class EmployeeServiceTest
    {
        private EmployeeService sut;
        private IDatabaseRepo repo;

        [SetUp]
        public void SetUp()
        {
            repo = A.Fake<IDatabaseRepo>();
            sut = new EmployeeService(repo);
        }

        [Test]
        public void AddEmployee_nullObjectParameter_reutrn_false()
        {
            //Arrange
            Employee emp = null;

            //Act
            var result = sut.AddEmployee(emp);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateEmployee_validation_ok()
        {
            //Arrange
            Employee emp = new Employee()
            {
                Id = 1,
                First_name = "Jane",
                Last_name = "test",
                Email = "email@em.com",
                City = "Bitola"
            };

            //Act
            var result = sut.ValidateEmployee(emp);

            //Assert
            Assert.True(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [Test]
        public void ValidateEmployee_validation_notOk()
        {
            //Arrange
            Employee emp = new Employee()
            {
                Id = 1,
                First_name = "",
                Last_name = "",
                Email = "email@em.com",
                City = "Bitola"
            };

            //Act
            var result = sut.ValidateEmployee(emp);

            //Assert
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [Test]
        public void DeleteEmployee_idNotExist_return_false()
        {
            //Arrange
            int id = 5555;

            //Act
            A.CallTo(() => repo.DeleteEmployee(A<int>.Ignored)).Returns(false);
            var result = sut.DeleteEmployee(id);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetEmployee_idNotExist_return_null()
        {
            //Arrange
            int id = 5555;

            //Act
            A.CallTo(() => repo.GetEmployeeById(A<int>.Ignored)).Returns(null);
            var result = sut.GetEmployeeById(id);

            //Assert
            Assert.IsNull(result);
        }

        
    }
}
