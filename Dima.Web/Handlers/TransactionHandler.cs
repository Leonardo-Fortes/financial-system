using Dima.Core.Common.Extencions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace Dima.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient _cliente = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await _cliente.PostAsJsonAsync("v1/transactions", request);
            return  await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ??
                new Response<Transaction?>(null, 400, "Não foi possivel criar sua transação");
                
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await _cliente.DeleteAsync($"v1/transactions/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ??
                new Response<Transaction?>(null, 400, "não foi possivel excluir a transação");
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        => await _cliente.GetFromJsonAsync<Response<Transaction?>>($"v1/transactions/{request.Id}") ??
            new Response<Transaction?>(null, 400, "Não foi possivel buscar a transação");
        

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";
            var startDate = request.StartDate is not null ? request.StartDate.Value.ToString(format) : DateTime.Now.GetFirstDay().ToString(format);
            var endDate = request.EndDate is not null ? request.EndDate.Value.ToString(format) : DateTime.Now.GetLastDay().ToString(format);

            var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";
           return await _cliente.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url) ??
                new PagedResponse<List<Transaction>?>(null, 400, "Não foi possivel obter todas transações");
        }
    

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await _cliente.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() ??
                new Response<Transaction?>(null, 400, "Falha ao atualizar");
        }
    }
}
