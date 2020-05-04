using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;

namespace Store.DataAccessLayer.Models.Response
{
    public class AuthorsResponseModel
    {
        public Author Author { get; set; }

        public ICollection<string> PrintingEditionTitles { get; set; } = new List<string>();
    }
}
