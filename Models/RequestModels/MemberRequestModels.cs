using System.ComponentModel.DataAnnotations;

namespace webstore.Models.RequestModels
{
    public class AlertProfileRequestModel
    {
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string account { get; set; }

        [StringLength(20)]
        public string username { get; set; }

        [StringLength(40)]
        public string address { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [EmailAddress]
        public string email { get; set; }
    }

    public class ChangePassword
    {
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string currentPassword { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string newPassword { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,30}$")]
        public string verify { get; set; }
    }
}