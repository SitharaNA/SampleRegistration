using System.ComponentModel.DataAnnotations;

namespace Contract
{
    public class UserName
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
