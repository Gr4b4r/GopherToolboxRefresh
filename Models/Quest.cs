using System;
using System.ComponentModel.DataAnnotations;

namespace GopherToolboxRefresh.Models
{
    public class Quest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime QuestDateStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [EndDateAfterStartDate("QuestDateStart")]
        public DateTime QuestDateEnd { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }  // Nullable
    }

    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public EndDateAfterStartDateAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime)value;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            var startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate < startDate)
            {
                return new ValidationResult("The end date must be later than the start date.");
            }

            return ValidationResult.Success;
        }
    }
}