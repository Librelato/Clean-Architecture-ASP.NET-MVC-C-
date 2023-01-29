using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirme Password")]
        [Compare("Password", ErrorMessage ="Password don´t match")]
        public string ConfirmPassword { get; set;}
    }
}
