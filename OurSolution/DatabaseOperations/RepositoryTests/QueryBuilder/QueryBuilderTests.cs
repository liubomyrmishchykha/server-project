using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Repository.QueryBuilder;

namespace RepositoryTests.QueryBuilder
{
    [TestClass]
    public class QueryBuilderTests
    {
        private Query _query = new Mock<Query>().Object;
        private QueryBuilder<User> _queryBuilder = new Mock<QueryBuilder<User>>().Object;

        [TestMethod]
        public void GetAll_Users_Success()
        {
            //Arrange
            _query.BuildStoredProcedureName<User>("GetAll");
            //Act 
            var result = _queryBuilder.GetAll<User>();
            //Assert
            Assert.IsTrue(result.GetProcedureName().Equals(_query.GetProcedureName()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectById_IdLessThenZero_ArgumentException()
        {
            //Arrange
            int id = -1;
            _query.BuildStoredProcedureName<User>("SelectById");
            //Act
            _queryBuilder.SelectById<User>(id);
            //Assert 
            Assert.Fail("Not throwed ArgumentException when id is less then zero");
        }
    }
}
