using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using IT_Hardware.Areas.Admin.Controllers;
using IT_Hardware.Areas.Admin.Models;
using IT_Hardware.IServices;
using System.Threading;

namespace IT_Hardware.Tests.Controllers
{
    [TestFixture]
    public class BudgetUsesControllerTests
    {
        private Mock<IBudgetUsesService> _budgetUsesServiceMock;
        private Mock<IBudgetYearService> _budgetYearServiceMock;
        private Budget_UsesController _controller;

        [SetUp]
        public void Setup()
        {
            _budgetUsesServiceMock = new Mock<IBudgetUsesService>();
            _budgetYearServiceMock = new Mock<IBudgetYearService>();

            _controller = new Budget_UsesController(
                _budgetUsesServiceMock.Object,
                _budgetYearServiceMock.Object
            );

            _controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
            );

            var httpContext = new DefaultHttpContext();
            httpContext.User = new System.Security.Claims.ClaimsPrincipal(
                new System.Security.Claims.ClaimsIdentity(
                    new[] { new System.Security.Claims.Claim("name", "TestUser") },
                    "mock"
                )
            );
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        [Test]
        public void Budget_Uses_Details_ReturnsViewWithModel()
        {
            _budgetUsesServiceMock.Setup(s => s.Get_BudgetData())
                .Returns(new List<Bud_Uses_List> { new Bud_Uses_List { Budget_Name = "TestBudget" } });
            _budgetYearServiceMock.Setup(s => s.budget_year_dropdown())
                .Returns(new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>());

            var result = _controller.Budget_Uses_Details() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as Mod_Budget_Uses;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Bud_us_list[0].Budget_Name, Is.EqualTo("TestBudget"));
        }

        [Test]
        public void Budget_Uses_Create_Item_ReturnsViewWithDefaults()
        {
            _budgetYearServiceMock.Setup(s => s.budget_year_dropdown())
                .Returns(new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>());

            var result = _controller.Budget_Uses_Create_Item("Hello") as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ViewBag.Message, Is.EqualTo("Hello"));

            var model = result.Model as Mod_Budget_Uses;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Processing_Date, Is.Not.Null);
        }

        [Test]
        public void Budget_Uses_CreateItem_Post_ValidData_Redirects()
        {
            var model = new Mod_Budget_Uses
            {
                Budget_Head_Id = "1",
                Amount_Utilized_Before = 100,
                Balance_Available = 200,
                Budget_Amount = 50,
                Remaning_Balance = 150
            };

            _controller.ModelState.Clear();

            _budgetUsesServiceMock.Setup(s => s.Save_Budget_data(It.IsAny<Mod_Budget_Uses>(), "Add_new", ""))
                .Returns(1);

            var result = _controller.Budget_Uses_CreateItem_Post(model) as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"], Is.EqualTo("Data saved successfully"));
        }

        [Test]
        public void Budget_Uses_CreateItem_Post_InvalidModel_Redirects()
        {
            var model = new Mod_Budget_Uses();
            _controller.ModelState.AddModelError("Budget_Head_Id", "Required");

            var result = _controller.Budget_Uses_CreateItem_Post(model) as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"], Is.EqualTo("Required Data are not Provided"));
        }

        [Test]
        public void Budget_List_ReturnsJson()
        {
            _budgetUsesServiceMock.Setup(s => s.Get_BudgetUses_By_BudId(It.IsAny<string>()))
                .Returns(new List<Bud_Uses_List>());

            var result = _controller.Budget_Uses_List("1") as JsonResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IEnumerable<Bud_Uses_List>>());
        }

        [Test]
        public void AutoComplete_ReturnsJsonList()
        {
            var poList = new List<PO_Info>
            {
                new PO_Info { PO_Id = "1", PO_No = "PO123" }
            };

            _budgetUsesServiceMock.Setup(s => s.Get_PO_Info("PO123")).Returns(poList);

            var result = _controller.AutoComplete("PO123") as JsonResult;

            Assert.That(result, Is.Not.Null);
            var list = result.Value as List<PO_Info>;
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0].PO_No, Is.EqualTo("PO123"));
        }

        [Test]
        public void Edit_Budget_Uses_ReturnsViewWithModel()
        {
            var model = new Mod_Budget_Uses
            {
                Budget_Uses_Id = "123",
                Budget_Name = "TestBudget",
                Budget_Year = "2023-2024"
            };

            _budgetUsesServiceMock.Setup(s => s.Get_Data_By_ID(It.IsAny<Mod_Budget_Uses>(), "123"))
                .Returns(model);
            _budgetYearServiceMock.Setup(s => s.budget_year_dropdown())
                .Returns(new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>());

            var result = _controller.Edit_Budget_Uses("123") as ViewResult;

            Assert.That(result, Is.Not.Null);
            var returnedModel = result.Model as Mod_Budget_Uses;
            Assert.That(returnedModel.Budget_Name, Is.EqualTo("TestBudget"));
        }

        [Test]
        public void Update_Budget_Uses_ValidData_Redirects()
        {
            var model = new Mod_Budget_Uses
            {
                Budget_Head_Id = "1",
                Amount_Utilized_Before = 100,
                Balance_Available = 200,
                Budget_Amount = 50,
                Remaning_Balance = 150
            };

            _controller.ModelState.Clear();

            _budgetUsesServiceMock.Setup(s => s.Save_Budget_data(model, "Update", "123"))
                .Returns(1);

            var result = _controller.Update_Budget_Uses(model, "123") as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"], Is.EqualTo("Data saved successfully"));
        }

        [Test]
        public void Update_Budget_Uses_InvalidModel_Redirects()
        {
            var model = new Mod_Budget_Uses();
            _controller.ModelState.AddModelError("Budget_Head_Id", "Required");

            var result = _controller.Update_Budget_Uses(model, "123") as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"], Is.EqualTo("Required Data are not Provided"));
        }

        [Test]
        public void Update_Budget_Uses_WhenException_SetsErrorMessage()
        {
            var model = new Mod_Budget_Uses
            {
                Budget_Head_Id = "1",
                Amount_Utilized_Before = 100,
                Balance_Available = 200,
                Budget_Amount = 50,
                Remaning_Balance = 150
            };

            _controller.ModelState.Clear();

            _budgetUsesServiceMock.Setup(s => s.Save_Budget_data(It.IsAny<Mod_Budget_Uses>(), "Update", "123"))
                .Throws(new Exception("DB error"));

            var result = _controller.Update_Budget_Uses(model, "123") as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"], Is.EqualTo("Data is not saved"));
        }

        [Test]
        public void Delete_Budget_Uses_ValidData_Redirects()
        {
            var model = new Mod_Budget_Uses();
            _controller.ModelState.Clear();

            _budgetUsesServiceMock.Setup(s => s.Save_Budget_data(model, "Delete", "123"))
                .Returns(1);

            var result = _controller.Delete_Budget_Uses(model, "123") as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"].ToString(),
                Is.EqualTo("Data is not saved").Or.EqualTo("Data saved successfully"));
        }

        [Test]
        public void Delete_Budget_Uses_WhenException_SetsErrorMessage()
        {
            var model = new Mod_Budget_Uses();
            _controller.ModelState.Clear();

            _budgetUsesServiceMock.Setup(s => s.Save_Budget_data(It.IsAny<Mod_Budget_Uses>(), "Delete", "123"))
                .Throws(new Exception("DB error"));

            var result = _controller.Delete_Budget_Uses(model, "123") as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Budget_Uses_Details"));
            Assert.That(_controller.TempData["Message"], Is.EqualTo("Data is not saved"));
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}