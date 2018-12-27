using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Model;
using WebApi.Repo;
using Xunit;

namespace WebApi.Tests
{
    public class WebApiTests
   {
       private readonly TestServer _server;
       private readonly Guid _exceptionGeneratingGuid = Guid.NewGuid();
       private Game _mockExercise;

        public WebApiTests()
       {
           _mockExercise = Game.CreateGame(6, 6, Constants.InitialTimeFrameSeconds);

           var mockRepo = new Mock<IGameRepo>();
           mockRepo.Setup(setup => setup.Get(It.IsAny<Guid>())).Returns<Guid>(id =>
           {
               if(id.Equals(_exceptionGeneratingGuid)) throw new Exception("Test");

               return _mockExercise;
           });
           mockRepo.Setup(setup => setup.Update(It.IsAny<Game>())).Callback<Game>(ex => _mockExercise = ex);

           _server = new TestServer(Program.CreateWebHostBuilder(null)
               .UseStartup<Startup>()
               .ConfigureTestServices(services =>
               {
                   // Not necessary for this exercise as our Repo is talking to an in-memory DB only
                   // its more from the point that if this was a real app with DB then,
                   // repo would need to be mocked
                   services.AddSingleton(mockRepo.Object);
               }));

       }

       [Fact]
       public async Task Get_StartsGame_AtBeginnerLevel()
       {
           var client = _server.CreateClient();
           var response = await client.GetAsync("/api/game");

           response.EnsureSuccessStatusCode();

           var exercise = await response.Content.ReadAsAsync<Game>();
           Assert.True(exercise.Level == Level.Beginner);
       }

       [Fact]
       public async Task Post_ThreeCorrectAnswers_IncreasesGameLevel()
       {
           var client = _server.CreateClient();

           async Task<GameResult> PostAnswer()
           {
               var answer = _mockExercise.A + _mockExercise.B;
               var response = await client.PostAsync($"/api/game/{Guid.NewGuid()}/{answer}", null);
               return await response.Content.ReadAsAsync<GameResult>();
           }

           await PostAnswer();
           await PostAnswer();
           var result = await PostAnswer();

           Assert.True(result.Game.Level == Level.Talented);

       }

       [Fact]
       public async Task Post_WithErrorGeneratingId_Returns500()
       {
           var client = _server.CreateClient();
           var response = await client.PostAsync($"/api/game/{this._exceptionGeneratingGuid}/1", null);
           var apiError = await response.Content.ReadAsAsync<ApiError>();

           Assert.True(response.StatusCode == HttpStatusCode.InternalServerError);
           Assert.NotNull(apiError);

       }

       
    }
}
