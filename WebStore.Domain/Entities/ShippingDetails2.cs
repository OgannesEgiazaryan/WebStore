using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Entities
{
    public class ShippingDetails2
    {

        [Required(ErrorMessage = "Вставьте адрес доставки")]
        [Display(Name = "Адрес эл. почты")]
        public string Line1 { get; set; }

        //[Display(Name = "Второй адрес")]
        //public string Line2 { get; set; }

        //[Display(Name = "Третий адрес")]
        //public string Line3 { get; set; }

        //[Required(ErrorMessage = "Укажите город")]
        //[Display(Name = "Город")]
        //public string City { get; set; }

        //[Required(ErrorMessage = "Укажите страну")]
        //[Display(Name = "Страна")]
        //public string Country { get; set; }

        //public bool GiftWrap { get; set; }
    }
}