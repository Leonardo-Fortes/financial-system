using Dima.api.Data;
using Dima.Core.Configurations;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace Dima.api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Type = request.Type,
                    PaidOrReceivedAt = DateTime.UtcNow,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId
                };
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
            }

            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel criar a transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                

                if(transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, message: "Transação deletada");

            }

            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel deletar a transação");
            }
        }

        public async Task <PagedResponse<List<Transaction?>>> GetAllAsync(GetAllTransactionsRequest request)
        {
            try
            {
                var query = context.Transactions.AsNoTracking().Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);
                var result = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                var count = await query.CountAsync();

                
                return new PagedResponse<List<Transaction?>>(result, count, request.PageNumber, request.PageSize);
            }


            catch
            {
                return new PagedResponse<List<Transaction?>>(null, 500, "Não foi possivel consultar as transações");
            }


        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var result = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (result is null)
                    return new Response<Transaction?>(null, 404, "transação não encontrada");
                return new Response<Transaction?>(result, message: "Transação concluida");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel consultar a transação");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var result = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (result is null) return new Response<Transaction?>(null, 404, "transação não encontrada");

                var update = new Transaction
                {

                    Title = request.Title,
                    Type = request.Type,
                    PaidOrReceivedAt = DateTime.UtcNow,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId
                };

                context.Transactions.Update(update);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(update, message: "Transação atualizada com sucesso");

            }

            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel atualizar a transação");
            }
        }
    }
}
