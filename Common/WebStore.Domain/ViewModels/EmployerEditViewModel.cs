using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels;

public class EmployerEditViewModel
{
    [HiddenInput(DisplayValue = false)]
    public int ID { get; set; }
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(30, ErrorMessage = "Слишком длинная фамилия")]
    [RegularExpression(@"[А-ЯЁ][а-яё]+", ErrorMessage = "Ошибка формата")]
    public string LastName { get; set; }
    [Display(Name = "Имя")]
    [StringLength(30, ErrorMessage = "Слишком длинное имя")]
    [RegularExpression(@"[А-ЯЁ][а-яё]+", ErrorMessage = "Ошибка формата")]
    public string FirstName { get; set; }
    [Display(Name = "Отчество")]
    [StringLength(50, ErrorMessage = "Слишком длинное отчество")]
    [RegularExpression(@"[А-ЯЁ][а-яё]+", ErrorMessage = "Ошибка формата")]
    public string MiddleName { get; set; }
    [Display(Name = "Возраст")]
    [Range(18, 65, ErrorMessage = "Нетрудоспособный возраст")]
    [RegularExpression(@"[А-ЯЁ][а-яё]+", ErrorMessage = "Ошибка формата")]
    public int Age { get; set; }
    [Display(Name = "Номер телефона")]
    public int TelephoneNumber { get; set; }
    [Display(Name = "Город")]
    public string City { get; set; }
}