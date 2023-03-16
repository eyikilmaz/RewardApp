using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RewardApp.Api.WebApi.Controllers;

namespace RewardApp.Test
{
    public class UserControllerUnitTest
    {

        // UnitOfWork_Condition_ExpectedResult
        // UnitOfWork_ExpectedResult_Condition


        [Test]
        public void Test1()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            UserController userController = new UserController(mediator.Object);

            //Action
            Guid gid = Guid.NewGuid();
            var result = userController.Get(gid);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}