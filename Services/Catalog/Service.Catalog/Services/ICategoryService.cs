using Course.Shared.DTOs;
using Service.Catalog.DTOs;
using Service.Catalog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Services
{
    interface ICategoryService 
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(Category category);
        Task<Response<CategoryDto>> GetByIdAsyns(string id);
    }
}
