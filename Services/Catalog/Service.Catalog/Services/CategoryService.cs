using AutoMapper;
using Course.Shared.DTOs;
using MongoDB.Driver;
using Service.Catalog.Configuration;
using Service.Catalog.DTOs;
using Service.Catalog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        const int SUCCESS_CODE = 200;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString); //clienti tanımladım cliente bağlandım
            var database = client.GetDatabase(databaseSettings.DatabaseName); //veritabanı aldım

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(c => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), SUCCESS_CODE);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category) //const correctness parameter must be const if does not change 
        {
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200); // 200--> magic number

        }
        public async Task<Response<CategoryDto>> GetByIdAsyns(string id)//const correctness parameter must be const if does not change 
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404); //404 --> magic number
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200); //200 --> magic number
        }


    }
}
