using Dima.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dima.Core.Responses
{
    public class PagedResponse<TData> : Response<TData> // Resposta Página quando tiver muita informação
    {
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (Double)PageSize);
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int TotalCount { get; set; }

        [JsonConstructor]
        public PagedResponse(int totalCount, TData? data, int currentPage = 1, int pageSize = Configuration.DefaultPageSize) : base(data)
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public PagedResponse(TData? data, int code = Configuration.DefaultStatusCode, string message = null) : base (data, code, message) 
        {
           
        }
    }
}
