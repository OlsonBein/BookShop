using Microsoft.AspNetCore.Http;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Users;
using System;
using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {     
        Task<UserModelItem> GetByIdAsync(long id);
        Task<UserModel> GetAllAsync(UsersFilterModel model, long adminId);
        Task<BaseModel> EditAsync(UserModelItem model, long id);
        Task<BaseModel> DeleteAsync(long id);
        Task<BaseModel> BlockAsync(long id, bool block);
        Task<BaseModel> SetPhotoAsync(UserModelItem model);
    }
}
