using LudoGameApi.Controllers;
using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LudoGameApi_Test
{
    public class UnitTest1
    {
        /// <summary>
        ///  Testing SessionNameController
        /// </summary> 


        /* Testing SessionNameController.CreateSession(string name); */
        [Fact]
        public async Task When_Creating_NonExisting_GameSession_Expect_Ok()
        {
            //Arrange
            DbContextOptions<LudoGameContext> dummyOptions = new DbContextOptionsBuilder<LudoGameContext>().Options;
            var myContextMoq = new Mock<LudoGameContext>(dummyOptions);

            List<GameSession> session = new List<GameSession>(){
                new GameSession(){ Name = "BlackMamba"}
            };

            myContextMoq.Setup(x => x.SessionName).ReturnsDbSet(session);

            var testingSession = new SessionNamesController(myContextMoq.Object);

            //Act
            var result = await testingSession.CreateSession("LudoGänget");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Creating_Existing_GameSession_Expect_BadRequest()
        {
            //Arrange
            DbContextOptions<LudoGameContext> dummyOptions = new DbContextOptionsBuilder<LudoGameContext>().Options;
            var myContextMoq = new Mock<LudoGameContext>(dummyOptions);

            List<GameSession> session = new List<GameSession>(){
                new GameSession(){ Name = "BlackMamba"}
            };

            myContextMoq.Setup(x => x.SessionName).ReturnsDbSet(session);

            var testingSession = new SessionNamesController(myContextMoq.Object);

            //Act
            var result = await testingSession.CreateSession("BlackMamba");

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        /* Testing SessionNameController.DeleteSession(string name); */
        [Fact]
        public async Task When_Deleting_Existing_GameSession_Expect_Ok()
        {
            //Arrange
            DbContextOptions<LudoGameContext> dummyOptions = new DbContextOptionsBuilder<LudoGameContext>().Options;
            var myContextMoq = new Mock<LudoGameContext>(dummyOptions);

            List<GameSession> session = new List<GameSession>(){
                new GameSession(){ Name = "BlackMamba"}
            };

            myContextMoq.Setup(x => x.SessionName).ReturnsDbSet(session);

            var testingSession = new SessionNamesController(myContextMoq.Object);

            //Act
            var result = await testingSession.DeleteSession("BlackMamba");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Deleting_NonExisting_GameSession_Expect_BadRequest()
        {
            //Arrange
            DbContextOptions<LudoGameContext> dummyOptions = new DbContextOptionsBuilder<LudoGameContext>().Options;
            var myContextMoq = new Mock<LudoGameContext>(dummyOptions);

            List<GameSession> session = new List<GameSession>(){
                new GameSession(){ Name = "BlackMamba"}
            };

            myContextMoq.Setup(x => x.SessionName).ReturnsDbSet(session);

            var testingSession = new SessionNamesController(myContextMoq.Object);

            //Act
            var result = await testingSession.DeleteSession("LudoGänget");

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
