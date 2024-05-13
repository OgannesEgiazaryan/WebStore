using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebStore.Domain.Concrete;
using WebStore.WebUI.HtmlHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Controllers;
using WebStore.WebUI.Models;
using GameStore.Domain.Concrete;

namespace WebStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
    {
        new SoftWares { ID_SoftWare = 1, Name = "Игра1"},
        new SoftWares { ID_SoftWare = 2, Name = "Игра2"},
        new SoftWares { ID_SoftWare = 3, Name = "Игра3"},
        new SoftWares { ID_SoftWare = 4, Name = "Игра4"},
        new SoftWares { ID_SoftWare = 5, Name = "Игра5"}
    });
            WebController controller = new WebController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            SoftsListViewModel result = (SoftsListViewModel)controller.List(null, 2).Model;
            // Утверждение
            List<SoftWares> games = result.App.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Игра4");
            Assert.AreEqual(games[1].Name, "Игра5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
            {
                new SoftWares { ID_SoftWare = 1, Name = "Игра1"},
                new SoftWares { ID_SoftWare = 2, Name = "Игра2"},
                new SoftWares { ID_SoftWare = 3, Name = "Игра3"},
                new SoftWares { ID_SoftWare = 4, Name = "Игра4"},
                new SoftWares { ID_SoftWare = 5, Name = "Игра5"}
            });
            WebController controller = new WebController(mock.Object);
            controller.pageSize = 3;

            // Act
            SoftsListViewModel result = (SoftsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Games()
        {
            // Организация (arrange)
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
    {
        new SoftWares { ID_SoftWare = 1, Name = "Игра1", Category = new Categorys { ID_Category = 1 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 2, Name = "Игра2", Category = new Categorys { ID_Category = 2 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 3, Name = "Игра3", Category = new Categorys { ID_Category = 3 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 4, Name = "Игра4", Category = new Categorys { ID_Category = 4 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 5, Name = "Игра5", Category = new Categorys { ID_Category = 5 /* другие свойства */ }}
    });
            WebController controller = new WebController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<SoftWares> result = ((SoftsListViewModel)controller.List("Cat2", 1).Model)
                .Games.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Игра2" && result[0].Category.Name == "Графические редакторы");
            Assert.IsTrue(result[1].Name == "Игра4" && result[1].Category.Name == "Аудиоредакторы");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares> {
        new SoftWares { ID_SoftWare = 1, Name = "Игра1", Category = new Categorys { ID_Category = 1 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 2, Name = "Игра2", Category = new Categorys { ID_Category = 2 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 3, Name = "Игра3", Category = new Categorys { ID_Category = 3 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 4, Name = "Игра4", Category = new Categorys { ID_Category = 4 /* другие свойства */ }},

    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "Аудиоредакторы");
            Assert.AreEqual(results[1], "Графические редакторы");
            Assert.AreEqual(results[2], "Игры");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new SoftWares[] {
        new SoftWares { ID_SoftWare = 1, Name = "Игра1", Category = new Categorys { ID_Category = 1 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 2, Name = "Игра2", Category = new Categorys { ID_Category = 2 /* другие свойства */ }},

    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Шутер";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            /// Организация (arrange)
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares>
    {
       new SoftWares { ID_SoftWare = 1, Name = "Игра1", Category = new Categorys { ID_Category = 1 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 2, Name = "Игра2", Category = new Categorys { ID_Category = 2 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 3, Name = "Игра3", Category = new Categorys { ID_Category = 3 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 4, Name = "Игра4", Category = new Categorys { ID_Category = 4 /* другие свойства */ }},
        new SoftWares { ID_SoftWare = 5, Name = "Игра5", Category = new Categorys { ID_Category = 5 /* другие свойства */ }}

    });
            WebController controller = new WebController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((SoftsListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((SoftsListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((SoftsListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((SoftsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }

}
