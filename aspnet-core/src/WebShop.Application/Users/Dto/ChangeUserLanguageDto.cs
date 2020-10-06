using System.ComponentModel.DataAnnotations;

namespace WebShop.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}