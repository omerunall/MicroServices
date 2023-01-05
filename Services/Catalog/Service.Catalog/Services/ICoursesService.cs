using Course.Shared.DTOs;
using Service.Catalog.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Services
{
    interface ICoursesService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
 
    }
}
