using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;

namespace Store.DataAccessLayer.Models.Response
{
    public class AuthorInPrintingEditionsResponseModel
    {
        public Author Author { get; set; }

        public PrintingEdition PrintingEdition { get; set; }

        public List<string> PrintingEditionTitles { get; set; }

        public List<string> AuthorNames { get; set; }
    }
}
