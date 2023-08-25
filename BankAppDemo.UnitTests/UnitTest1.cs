using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using BankApp.Data;
using System.Linq;
namespace BankApp.UnitTests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            //Arrange
            int id = 7;

            //Act
            AuthRepo repo = new AuthRepo();
            var student = repo.GetStudent(id);

            //Assert
            Assert.Null(student);
        }
    }
}