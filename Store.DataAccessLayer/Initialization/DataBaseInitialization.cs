using Microsoft.AspNetCore.Identity;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Entities;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using static Store.DataAccessLayer.Common.Constants.Constants;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Initialization
{
    public class DataBaseInitialization
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _dbContext;
        public DataBaseInitialization(RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager, ApplicationContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        private void InitializeRole()
        {
            var roles = new List<Role>()
            { 
                new Role{ Name = RoleConstants.Admin.ToString()},
                new Role{ Name = RoleConstants.User.ToString()}
            };
            if (_roleManager.RoleExistsAsync(RoleConstants.Admin.ToString()).GetAwaiter().GetResult() &&
                _roleManager.RoleExistsAsync(RoleConstants.User.ToString()).GetAwaiter().GetResult()) 
            {
                return;
            }
            foreach (var role in roles)
            {
                _roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }
        }

        private void InitializeAdmin()
        {
            var user = new ApplicationUser
            {
                FirstName = BaseInitConstants.AdminFirstName,
                LastName = BaseInitConstants.AdminLastName,
                Email = BaseInitConstants.AdminEmail
            };
            user.UserName = user.Email;
            user.EmailConfirmed = true;

            if (_userManager.FindByNameAsync(BaseInitConstants.AdminFirstName).GetAwaiter().GetResult() == null)
            {
                _userManager.CreateAsync(user, BaseInitConstants.AdminPassword).GetAwaiter().GetResult();
                var appUser = _userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(appUser, UserRole.Admin.ToString()).GetAwaiter().GetResult();
            }
        }

        private void InitializePrintingEdition()
        {
            var printingEdition = new PrintingEdition()
            { 
                CreationDate = DateTime.Now,
                Title = BaseInitConstants.PrintingEditionTitle,
                Type = ProductType.Book,
                Price = BaseInitConstants.PrintingEditionPrice, 
                Currency = Currency.USD,
                Status = StatusType.Unpaid
            };
            var author = new Author() 
            {
                Name = BaseInitConstants.AuthorName,
                CreationDate = DateTime.Now
            };
            var printingEditionResult = _dbContext.PrintingEditions
                .Where(x => x.Title == BaseInitConstants.PrintingEditionTitle).FirstOrDefault();
            if (printingEditionResult == null)
            {
                _dbContext.PrintingEditions.Add(printingEdition);
            }
            var authorResult = _dbContext.Authors
                .Where(x => x.Name == BaseInitConstants.AuthorName).FirstOrDefault();
            if (authorResult == null)
            {
                _dbContext.Authors.Add(author);
            }
            _dbContext.SaveChanges();
        }

        public void InitializeAuthorInPrintingEdition()
        {           
            var author = _dbContext.Authors.Where(x => x.Name == BaseInitConstants.AuthorName).FirstOrDefault();
            var printingEdition = _dbContext.PrintingEditions.Where(x => x.Title == BaseInitConstants.PrintingEditionTitle).FirstOrDefault();
            var authorInBooks = new AuthorInPrintingEdition()
            {
                Author = author,
                PrintingEdition = printingEdition,
                CreationDate = DateTime.Now
            };
            var result = _dbContext.AuthorInPrintingEditions.Where(x => x.Author.Name == BaseInitConstants.AuthorName).FirstOrDefault();
            if (result == null)
            {
                _dbContext.AuthorInPrintingEditions.Add(authorInBooks);
            }
            _dbContext.SaveChanges();
        }
        public void InitializeAll()
        {
            InitializeRole();
            InitializeAdmin();
            InitializePrintingEdition();
            InitializeAuthorInPrintingEdition();
        }
    }   
}
