using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.PrintingEdition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task <PrintingEditionsModel> GetAllAsync(PrintingEditionsFilterModel model);
        Task<BaseModel> CreateAsync(PrintingEditionsModelItem model);
        Task<BaseModel> UpdateAsync(PrintingEditionsModelItem model);
        Task<BaseModel> DeleteAsync(long id);
        Task<PrintingEditionsModelItem> GetById(long id);
    }
}
