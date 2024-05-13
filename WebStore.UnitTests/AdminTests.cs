using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
            {
                new SoftWares { ID_SoftWare = 1, Name = "Игра1"},
                new SoftWares { ID_SoftWare = 2, Name = "Игра2"},
                new SoftWares { ID_SoftWare = 3, Name = "Игра3"},
                new SoftWares { ID_SoftWare = 4, Name = "Игра4"},
                new SoftWares { ID_SoftWare = 5, Name = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<SoftWares> result = ((IEnumerable<SoftWares>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Игра1", result[0].Name);
            Assert.AreEqual("Игра2", result[1].Name);
            Assert.AreEqual("Игра3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
    {
                new SoftWares { ID_SoftWare = 1, Name = "Игра1"},
                new SoftWares { ID_SoftWare = 2, Name = "Игра2"},
                new SoftWares { ID_SoftWare = 3, Name = "Игра3"},
                new SoftWares { ID_SoftWare = 4, Name = "Игра4"},
                new SoftWares { ID_SoftWare = 5, Name = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            SoftWares game1 = controller.Edit(1).ViewData.Model as SoftWares;
            SoftWares game2 = controller.Edit(2).ViewData.Model as SoftWares;
            SoftWares game3 = controller.Edit(3).ViewData.Model as SoftWares;

            // Assert
            Assert.AreEqual(1, game1.ID_SoftWare);
            Assert.AreEqual(2, game2.ID_SoftWare);
            Assert.AreEqual(3, game3.ID_SoftWare);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
    {
      new SoftWares { ID_SoftWare = 1, Name = "Игра1"},
                new SoftWares { ID_SoftWare = 2, Name = "Игра2"},
                new SoftWares { ID_SoftWare = 3, Name = "Игра3"},
                new SoftWares { ID_SoftWare = 4, Name = "Игра4"},
                new SoftWares { ID_SoftWare = 5, Name = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            SoftWares result = controller.Edit(6).ViewData.Model as SoftWares;

            // Assert
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IWebRepository> mock = new Mock<IWebRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            SoftWares soft = new SoftWares { Name = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(soft);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveSoft(soft));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IWebRepository> mock = new Mock<IWebRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            SoftWares soft = new SoftWares { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(soft);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveSoft(It.IsAny<SoftWares>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Games()
        {
            // Организация - создание объекта Game
            SoftWares soft = new SoftWares { ID_SoftWare = 2, Name = "Игра2" };

            // Организация - создание имитированного хранилища данных
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
    {
      new SoftWares { ID_SoftWare = 1, Name = "Игра1"},
                new SoftWares { ID_SoftWare = 2, Name = "Игра2"},
                new SoftWares { ID_SoftWare = 3, Name = "Игра3"},
                new SoftWares { ID_SoftWare = 4, Name = "Игра4"},
                new SoftWares { ID_SoftWare = 5, Name = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(soft.ID_SoftWare);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Game
            mock.Verify(m => m.DeleteSoft(soft.ID_SoftWare));
        }
    }
}