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

        [Required(ErrorMessage = "Введите номер карты")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Номер карты должен состоять из 16 цифр")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Введите срок действия карты")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Введите срок действия в формате MM/YY")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "Введите CVV")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV должен состоять из 3 цифр")]
        public string CVV { get; set; }
    }
}