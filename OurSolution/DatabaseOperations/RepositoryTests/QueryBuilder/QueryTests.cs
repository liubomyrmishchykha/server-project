using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Repository.Interfaces;
using Repository.QueryBuilder;
//using static System.String;

namespace RepositoryTests.QueryBuilder
{
    [TestClass]
    public class QueryTests
    {
        Query _query = new Mock<Query>().Object;

        [TestMethod]
        public void BuildStoredProcedureName_TypeUserOperationGetAll_uspGetAllUsers()
        {
            //Arrange
            string operation = "GetAll";
            string expectedResult = "uspGetAllUsers";
            //Act 
            _query.BuildStoredProcedureName<User>(operation);
            //Assert
            Assert.IsTrue(expectedResult.Equals(_query.GetProcedureName()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildStoredProcedureName_EmptyString_ArgumentException()
        {
            //Arrange
            string emptyString = string.Empty;
            //Act
            _query.BuildStoredProcedureName<User>(emptyString);
            //Assert 
            Assert.Fail("Not throwed ArgumentException when input string was empty");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildStoredProcedureName_NullString_ArgumentException()
        {
            //Arrange
            string nullString = null;
            //Act
            _query.BuildStoredProcedureName<User>(nullString);
            //Assert
            Assert.Fail("Not throwed ArgumentException when input string was empty");
        }
    }
}
