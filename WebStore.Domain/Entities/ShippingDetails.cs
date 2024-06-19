using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вставьте адрес доставки")]
        [Display(Name = "Адрес эл. почты")]
        public string Line1 { get; set; }
    }
}