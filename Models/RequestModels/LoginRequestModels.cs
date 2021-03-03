using System.ComponentModel.DataAnnotations;

namespace webstore.Models.RequestModels
{
    public class LoginRequestModel
    {
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string account { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string password { get; set; }
    }

    public class SignUpRequestModel
    {
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string account { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string password { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string passwordConfirm { get; set; }
    }
}