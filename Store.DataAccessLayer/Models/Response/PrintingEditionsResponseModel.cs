using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;

namespace Store.DataAccessLayer.Models.Response
{
    public class PrintingEditionsResponseModel
    {
        public PrintingEdition PrintingEdition { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
