using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastPoll.Controllers;
using FastPoll.Models;
using FastPoll.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FastPollTests
{
    [TestClass]
    public class PollsControllerTests
    {
        private ApplicationDbContext _context;
        private PollsController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Create an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new PollsController(_context);
        }

        // Test for GET: Polls/Create (checks if the Create page is shown)
        [TestMethod]
        public void Create_Get_ReturnsView()
        {
            // Act
            var result = _controller.Create() as ViewResult;
            // Assert
            Assert.IsNotNull(result);  // Check if a view is returned
        }

        // Test for POST: Polls/Create with valid data (checks if it redirects to Index)
        [TestMethod]
        public void Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var newPoll = new Poll
            {
                Question = "What is your favorite color?",
                CreatedAt = System.DateTime.Now,
                CreatedBy = "Admin"
            };
            // Act
            var result = _controller.Create(newPoll).Result as RedirectToActionResult;
            // Assert
            Assert.AreEqual("Index", result?.ActionName);  // Check if it redirects to Index
        }

        // Test for POST: Polls/Create with an empty question (checks if it stays on the Create page with an error)
        [TestMethod]
        public void Create_EmptyQuestion_ReturnsView()
        {
            // Arrange
            var newPoll = new Poll
            {
                Question = "",  // Invalid because the question is empty
                CreatedAt = System.DateTime.Now,
                CreatedBy = "Admin"
            };
            _controller.ModelState.AddModelError("Question", "Question is required.");  // Simulate validation error
            // Act
            var result = _controller.Create(newPoll).Result as ViewResult;
            // Assert
            Assert.IsNotNull(result);  // Check if it returns the same page with the error
        }

        // Test for POST: Polls/Create with missing "CreatedBy" field (checks if it stays on the Create page with an error)
        [TestMethod]
        public void Create_MissingCreatedBy_ReturnsView()
        {
            // Arrange
            var newPoll = new Poll
            {
                Question = "What is your favorite color?",
                CreatedAt = System.DateTime.Now,
                CreatedBy = ""  // Invalid because "CreatedBy" is empty
            };
            _controller.ModelState.AddModelError("CreatedBy", "CreatedBy is required.");  // Simulate validation error
            // Act
            var result = _controller.Create(newPoll).Result as ViewResult;
            // Assert
            Assert.IsNotNull(result);  // Check if it returns the same page with the error
        }

        // Test for POST: Polls/Create with a null poll object (checks if it returns a bad request)
        [TestMethod]
        public void Create_NullPoll_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Create(null).Result as BadRequestResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));  // Check if it returns BadRequest
        }

        // Test for POST: Polls/Create with a future "CreatedAt" date (checks if it stays on the Create page with an error)
        [TestMethod]
        public void Create_FutureCreatedAt_ReturnsView()
        {
            // Arrange
            var newPoll = new Poll
            {
                Question = "What is your favorite color?",
                CreatedAt = System.DateTime.Now.AddDays(1),  // Invalid because the date is in the future
                CreatedBy = "Admin"
            };
            _controller.ModelState.AddModelError("CreatedAt", "CreatedAt cannot be in the future.");  // Simulate validation error
            // Act
            var result = _controller.Create(newPoll).Result as ViewResult;
            // Assert
            Assert.IsNotNull(result);  // Check if it returns the same page with the error
        }

        // Test for POST: Polls/Create with a duplicate question (checks if it returns an error)
        [TestMethod]
        public void Create_DuplicateQuestion_ReturnsViewWithError()
        {
            // Arrange
            var existingPoll = new Poll
            {
                Question = "What is your favorite color?",
                CreatedAt = System.DateTime.Now,
                CreatedBy = "Admin"
            };
            
            // Add an existing poll to simulate the duplicate question scenario
            _context.Polls.Add(existingPoll);
            _context.SaveChanges();

            var newPoll = new Poll
            {
                Question = "What is your favorite color?",  // Same question as the existing one
                CreatedAt = System.DateTime.Now,
                CreatedBy = "Admin"
            };

            _controller.ModelState.AddModelError("Question", "This question already exists.");  // Simulate validation error
            // Act
            var result = _controller.Create(newPoll).Result as ViewResult;
            // Assert
            Assert.IsNotNull(result);  // Check if it returns the same page with the error
        }
    }
}
