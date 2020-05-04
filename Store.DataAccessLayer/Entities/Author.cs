using Dapper.Contrib.Extensions;
using Store.DataAccessLayer.Repositories.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        [NotMapped]
        [Write(false)]
        public ICollection<string> BooksTitles { get; set; }
        public Author()
        {
            BooksTitles = new List<string>();
        }
    }
}

