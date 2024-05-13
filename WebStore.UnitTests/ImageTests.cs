using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using WebStore.WebUI.Controllers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Game с данными изображения
            SoftWares soft = new SoftWares
            {
                ID_SoftWare = 2,
                Name = "Игра2",
                Photo1 = new byte[] { },
                Image_Mime_Type = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares> {
                new SoftWares {ID_SoftWare = 1, Name = "Игра1"},
                soft,
                new SoftWares {ID_SoftWare = 3, Name = "Игра3"}
            }.AsQueryable());

            // Организация - создание контроллера
            WebController controller = new WebController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(soft.Image_Mime_Type, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<IWebRepository> mock = new Mock<IWebRepository>();
            mock.Setup(m => m.App).Returns(new List<SoftWares> {
                new SoftWares {ID_SoftWare = 1, Name = "Игра1"},
                new SoftWares {ID_SoftWare = 2, Name = "Игра2"}
            }.AsQueryable());

            // Организация - создание контроллера
            WebController controller = new WebController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}