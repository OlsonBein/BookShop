using Dapper.Contrib.Extensions;
using Store.DataAccessLayer.Repositories.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class AuthorInPrintingEdition : BaseEntity
    {

        public long AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [Write(false)]
        public Author Author { get; set; }

        
        public long PrintingEditionId { get; set; }

        [ForeignKey("PrintingEditionId")]
        [Write(false)]
        public PrintingEdition PrintingEdition { get; set; }
    }
}
