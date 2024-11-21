using Dima.api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserID = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return new Response<Category>(category);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Falha ao criar categoria");
            }


        }

        public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
