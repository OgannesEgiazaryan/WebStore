using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Entities
{
    public class ShippingDetails2
    {

        [Required(ErrorMessage = "Вставьте адрес доставки")]
        [Display(Name = "Адрес эл. почты")]
        public string Line1 { get; set; }

    }
}