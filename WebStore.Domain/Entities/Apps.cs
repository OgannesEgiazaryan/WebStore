using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebStore.Domain.Entities
{
    public class SoftWares
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID_SoftWare { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название программного обеспечения")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для программного обеспечения")]
        public string Description { get; set; }


        [HiddenInput(DisplayValue = false)]
        public int ID_Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ID_Seller { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ID_Event { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required(ErrorMessage = "Пожалуйста, введите цену программного обеспечения")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public int Price { get; set; }

        [Display(Name = "Количество продаж")]
        [Required(ErrorMessage = "Пожалуйста, введите количество продаж")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для количества продаж")]
        public int Count_Sale { get; set; }


        //Разобаться и исправить(КОСТЫЛЬ МОЖНО РОСТО INT)
        [Display(Name = "Вес(Гб)")]
        [Required(ErrorMessage = "Пожалуйста, введите вес программного обеспечения")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для веса")]
        public int Weight_GB { get; set; }

        [ForeignKey("ID_Category")]
        public virtual Categorys Category { get; set; }

        [ForeignKey("ID_Seller")]
        public virtual Sellers Seller { get; set; }

        [ForeignKey("ID_Event")]
        public virtual Events Event { get; set; }

        

        public string Image_Mime_Type { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание(длинное)")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для программного обеспечения")]
        public string LongDescription { get; set; }

        [Display(Name = "Операционные системы")]
        [Required(ErrorMessage = "Пожалуйста, введите поддерживаемые ОС")]
        public string OS { get; set; }

        [Display(Name = "ОЗУ(Гб)")]
        [Required(ErrorMessage = "Пожалуйста, введите минимально требуемое количество ОЗУ")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для веса")]
        public int RAM { get; set; }

        [Display(Name = "Количество копий")]
        [Required(ErrorMessage = "Пожалуйста, введите количество копий")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для количества копий")]
        public int Copy_Count { get; set; }

        public byte[] Photo1 { get; set; }

        public byte[] Image1 { get; set; }

        public byte[] Image2 { get; set; }

        public byte[] Image3 { get; set; }


        public virtual ICollection<Orders> Order { get; set; }
        public virtual ICollection<Reviews> Review { get; set; }
    }


    public class Categorys
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID_Category { get; set; }

        [Display(Name = "Название категории")]
        [Required(ErrorMessage = "Пожалуйста, введите название категории программного обеспечения")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для категории программного обеспечения")]
        public string Description { get; set; }

        public byte[] Photo { get; set; }

        public string ImageMimeType { get; set; }

        public virtual ICollection<SoftWares> SoftWares { get; set; }
    }

    public class Sellers
    {
        [Key]
        public int ID_Seller{ get; set; }

        [Display(Name = "Издатель")]
        [Required(ErrorMessage = "Пожалуйста, введите название издателя программного обеспечения")]
        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Photo { get; set; }

        public string ImageMimeType { get; set; }

        public virtual ICollection<SoftWares> SoftWares { get; set; }
    }

    public class Orders
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Order_ID { get; set; }

        [Display(Name = "Имя клиента")]
        public string Order_Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Электронная почта")]
        public string Order_Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Order_Soft_ID { get; set; }
        
        public int Order_Quantity { get; set; }

        [ForeignKey("Order_Soft_ID")]
        public virtual SoftWares Software { get; set; }
    }

    public class Reviews
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID_Review { get; set; }

        [Display(Name = "Дата отзыва")]
        [Required(ErrorMessage = "Пожалуйста, выберите дату отзыва")]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст отзыва")]
        [Required(ErrorMessage = "Пожалуйста, введите текст отзыва")]
        public string Text { get; set; }

        [Display(Name = "Рейтинг")]
        [Required(ErrorMessage = "Пожалуйста, выберите рейтинг программного обеспечения")]
        //[Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для веса")]        
        public int Rating { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ID_Software { get; set; }

        [Display(Name = "Пользователь")]
        [Required(ErrorMessage = "Пожалуйста, введите ваше имя")]
        public string User { get; set; }

        [ForeignKey("ID_Software")]
        public virtual SoftWares Software { get; set; }
    }

    public class Events
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Event_ID { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название события")]
        public string Event_Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание события")]
        public string Event_Description { get; set; }

        public virtual ICollection<SoftWares> SoftWares { get; set; }

    }

    public class BestOfferViewModel
    {
        public SoftWares Software { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }

    public class ProductDetailsViewModel
    {
        public SoftWares Product { get; set; }
        public IEnumerable<BestOfferViewModel> BestOffers { get; set; }
        public IEnumerable<BestOfferViewModel> RelatedProducts { get; set; }
        public int ReviewCount { get; set; }
        public double AverageRating { get; set; }
        public Dictionary<int, int> RatingCounts { get; set; }
    }


    public class RelatedProductViewModel
    {
        public SoftWares Product { get; set; }
        public double AverageRating { get; set; }
    }
}
