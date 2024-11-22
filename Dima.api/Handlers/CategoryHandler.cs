using Dima.api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
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
                return new Response<Category?>(category, 201, "Categoria criada com sucesso");
            }

            catch 
            {
               return new Response<Category?>(null, 500, "Não foi possivel criar a categoria");
            }


        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {

            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserID == request.UserId);
                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, message: "categoria removida com sucesso");
            }
            catch
            {
               return new Response<Category?>(null, 500, "Não foi possivel deletar a categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
        {
            try
            {
                var query = context.Categories.AsNoTracking().Where(x => x.UserID == request.UserId).OrderBy(x => x.Title);
                var category = await query.Skip((request.PageNumber -1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(category, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>>(null, 500, "Não foi possivel consultar as categorias");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var caregory = await context.Categories.
                    AsNoTracking().
                    FirstOrDefaultAsync(x => x.Id == request.Id && x.UserID == request.UserId);

                return caregory is null
                    ? new Response<Category?>(null, 404, "Categoria não encontrada")
                    : new Response<Category?>(caregory);
            }
            catch
            {
                return new Response<Category?>(null,500, "Não foi possivel recuperar a categoria");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserID == request.UserId);
                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description;
                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, message: "categoria atualizada com sucesso");
            }
            catch
            {
               return  new Response<Category?>(null, 500, "Não foi possivel alterar a categoria");
            }
            
        }
    }
}
