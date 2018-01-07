﻿using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.Models;
using Example.Modularity.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.Modularity.Tests.Controllers.Movies
{
    public class WhenUpdatesArrived : ScenarioTestingBase
    {
        private const string Cinema1 = "Cinema1";
        private const string Cinema2 = "Cinema2";
        
        private CinemaUpdate[] _cinemaUpdates;
        private HttpClient _client;

        public override Task Given()
        {
            var context = new MoviesContextFactory()
                .AddEntities(Data.Movie())
                .Create();

            var server = new ServerFactory()
                .AddMock(context)
                .AddMock<IQueueClient>()
                .Create();

            _client = server.CreateClient();
            return Task.CompletedTask;
        }


        public override async Task When()
        {
            var result = await _client.GetAsync("api/movieUpdates");

            var content = await result.Content.ReadAsStringAsync();

            _cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(content);
        }

        [Test]
        public void ThenCinema1IsReturned()
        {
            Assert.That(_cinemaUpdates[0].Name, Is.EqualTo(Cinema1));
        }

        [Test]
        public void ThenCinema1MoviesCollectionIsNotEmpty()
        {
            Assert.IsNotEmpty(_cinemaUpdates[0].AddedMovies);
        }

        [Test]
        public void ThenCinema2IsReturned()
        {
            Assert.That(_cinemaUpdates[1].Name, Is.EqualTo(Cinema2));
        }

        [Test]
        public void ThenCinema2MoviesCollectionIsNotEmpty()
        {
            Assert.IsNotEmpty(_cinemaUpdates[1].AddedMovies);
        }
    }
}
