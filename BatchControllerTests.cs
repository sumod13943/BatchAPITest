using BatchAPI.BatchData;
using BatchAPI.Controllers;
using BatchAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BatchAPITest
{
    [TestFixture]
    public class BatchControllerTests
    {
        private BatchController _batchController;
        private IBatchData _service;
        private ILogger<BatchController> _logger;

        [SetUp]
        public void Setup()
        {
            _logger = Mock.Of<ILogger<BatchController>>();
            _service = new BatchDataMock();
            _batchController = new BatchController(_service, _logger);
        }

        [TearDown]
        public void CleanUp()
        {
            _batchController.Dispose(); // assumes Disposable is implemented
            _batchController = null;
            //put other cleanup code here
        }
        //[Test]
        //public void Get_WhenCalled_ReturnsOkResult()
        //{
        //    var okResult = _batchController.Batch();

        //    Assert.IsInstanceOf<OkObjectResult>(okResult);
        //}

        [Test]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = _batchController.Batch(Guid.NewGuid());
            Assert.IsInstanceOf<NotFoundObjectResult>(notFoundResult);
        }

        [Test]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            var testGuid = new Guid("9E13D973-8511-45DA-8217-95C961CC1DFB");
            var okResult = _batchController.Batch(testGuid);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            Attributes attribute = new Attributes();

            Batch batch = new Batch()
            {
                BatchId = new Guid("6B13F05B-89B8-4E4A-B20E-1ED96FAA095B"),
                BusinessUnit = "BU2",
                ExpiryDate = DateTime.Now.AddDays(1),
                ACL = new ACL()
                {
                    Id = 1,
                    ReadGroups = new List<string>() { "ReadGroups1", "ReadGroups2" },
                    ReadUsers = new List<string>() { "ReadUsers1", "ReadUsers2" }
                },
                Attributes = new List<Attributes>()
                    {
                        new Attributes()
                        {
                            Id = 1,
                            Key = "Key1",
                            Value = "Value1"
                        },
                    }

            };
            var createdResponse = _batchController.Batch(batch);

            Assert.IsInstanceOf<CreatedAtActionResult>(createdResponse);
        }
    }
}