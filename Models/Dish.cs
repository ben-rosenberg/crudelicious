using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    [MaxLength(45, ErrorMessage = "Dish name must be less than 45 characters")]
    [Display(Name = "Dish Name")]
    public string Name { get; set; }

    [Required]
    [MaxLength(45, ErrorMessage = "Chef's name must be less than 45 characters")]
    [Display(Name = "Chef's Name")]
    public string Chef { get; set; }

    [Required]
    [Range(1, 5)]
    [Display(Name = "Rating")]
    public int Tastiness { get; set; }

    [Required]
    [MinValue(1)]
    public int Calories { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class MinValueAttribute : ValidationAttribute
{
    public MinValueAttribute() : base() { _MinValue = 0; }
    public MinValueAttribute(int minValue) : base() { _MinValue = minValue; }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if ((int)value < _MinValue)
        {
            return new ValidationResult($"Value must be at least {_MinValue}");
        }

        return ValidationResult.Success;
    }

    private int _MinValue;
}

}