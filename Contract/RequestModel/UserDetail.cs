using System.ComponentModel.DataAnnotations;

namespace Contract
{
    public class UserDetail
    { 
        [Required]
        public UserName Name { get; set; }

        [Required]
        [RegularExpression("[^ @]*@[^ @]*")]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        public string Password { get; set; }
    }
}
