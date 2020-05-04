using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemoved { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Avatar { get; set; }
    }
}
