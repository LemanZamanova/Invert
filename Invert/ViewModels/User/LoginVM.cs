using System.ComponentModel.DataAnnotations;

namespace Invert.ViewModels
{
    public class LoginVM
    {
        [MaxLength(256)]
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistant { get; set; }

    }
}
