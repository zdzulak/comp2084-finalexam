using F2019Movies.Controllers;
using F2019Movies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F2019MoviesTest
{
    [TestClass]
    public class MoviesControllerTest
    {
        private f19Context db;
        MoviesController moviesController;
        List<Movie> movies = new List<Movie>();

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<f19Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            db = new f19Context(options);

            var studio = new Studio
            {
                StudioId = 502,
                Name = "Some Studio"
            };

            movies.Add(new Movie
            {
                MovieId = 11,
                Title = "Movie One",
                Year = 2017,
                Revenue = 100000,
                Studio = studio
            });

            movies.Add(new Movie
            {
                MovieId = 22,
                Title = "Movie Two",
                Year = 2018,
                Revenue = 200000,
                Studio = studio
            });

            movies.Add(new Movie
            {
                MovieId = 33,
                Title = "Movie Three",
                Year = 2016,
                Revenue = 300000,
                Studio = studio
            });

            movies.Add(new Movie
            {
                MovieId = 44,
                Title = "Movie Four",
                Year = 2019,
                Revenue = 400000,
                Studio = studio
            });

            foreach (var p in movies)
            {
                db.Add(p);
            }

            db.SaveChanges();
            moviesController = new MoviesController(db);
        }

        [TestMethod]
        public void IndexValidLoadsMovies()
        {
            var result = moviesController.Index();

            var viewResult = (ViewResult)result.Result;
            List<Movie> model = (List<Movie>)viewResult.Model;

            CollectionAssert.AreEqual(movies.OrderByDescending(m => m.Revenue).ToList(), model);
        }

        [TestMethod]
        public void DetailsValidId()
        {
            var result = (ViewResult)moviesController.Details(10).Result;

            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {

        }

        [TestMethod]
        public void DetailsNoId()
        {

        }

    }
}
