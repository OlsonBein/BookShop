using Store.BusinessLogicLayer.Models.Account;
using Store.BusinessLogicLayer.Models.Users;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;

namespace Store.BusinessLogicLayer.Mappers
{
    public class UserMapper
    {
        public static ApplicationUser MapModelToEntity(UserModelItem model)
        {
            var applicationUser = new ApplicationUser()
            {
                Avatar = model.Avatar,
                Email = model.Email,
                Id = model.Id,
                EmailConfirmed = model.EmailConfirmed,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = $"{model.FirstName} {model.LastName}",
                LockoutEnabled = true
            };
            return applicationUser;
        }

        public static UserModelItem MapEntityToModel(ApplicationUser applicationUser)
        {
            var model = new UserModelItem()
            {
                Avatar = applicationUser.Avatar,
                Email = applicationUser.Email,
                Id = applicationUser.Id,
                EmailConfirmed = applicationUser.EmailConfirmed,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName
            };
            return model;
        }

        public static ApplicationUser MapRegistrationModelToEntity(RegistrationModel model)
        {
            var applicationUser = new ApplicationUser()
            {               
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            return applicationUser;
        }

        public static ApplicationUser EditEntityByAdminRole(ApplicationUser applicationUser, UserModelItem model)
        {
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            return applicationUser;
        }

        public static ApplicationUser EditEntityByUserRole(ApplicationUser applicationUser, UserModelItem model)
        {
            applicationUser.Email = model.Email;
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            return applicationUser;
        }

        public static UserModelItem MapResponseModelToModelItem(UserResponseModel responseModel)
        {
            var modelItem = new UserModelItem()
            { 
                Email = responseModel.User.Email,
                FirstName = responseModel.User.FirstName,
                LastName = responseModel.User.LastName,
                Id = responseModel.User.Id ,
                EmailConfirmed = responseModel.User.EmailConfirmed
            };
            return modelItem;
        }


    }
}
