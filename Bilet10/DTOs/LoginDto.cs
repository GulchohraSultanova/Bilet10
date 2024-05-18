using System.ComponentModel.DataAnnotations;

namespace Bilet10.DTOs
{
    public class LoginDto
    {
        [Required]
        public string EmailOrUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool  IsRemember {  get; set; }
    }
}
