using AutoMapper;
using Course.Shared.DTOs;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Service.Catalog.Configuration;
using Service.Catalog.DTOs;
using Service.Catalog.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Services
{
    public class CoursesService : ICoursesService
    {
       
        private readonly IMongoCollection<Courses> _courseCollection;

        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CoursesService(IMapper mapper, IDatabaseSettings databaseSettings) 
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName); 

            _courseCollection = database.GetCollection<Courses>(databaseSettings.CourseCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courseGet = await _courseCollection.Find(course => true).ToListAsync();
       
            if (courseGet.Any())
            {
                foreach (var item in courseGet)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courseGet = new List<Courses>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courseGet), 200); // 200 magic number --> sabit sayı const değişken tanımlanabilir
        }
        public async Task<Response<CourseDto>> GetAllAsync(string id) //unneccesary copy assignmet?
        {
            var course = await _courseCollection.Find<Courses>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404); //404 --> magic number
            }
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);// 200 --> magic number
        }
    }
}
 