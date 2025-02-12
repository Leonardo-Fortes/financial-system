using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace Dima.Web.Pages.Transactions
{
    public partial class CreateTransactionsPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public CreateTransactionRequest InputModel { get; set; } = new();
        public List<Category> Categories { get; set; } = [];
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ITransactionHandler TransactionHandler { get; set; } = null!;
        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCategoryRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Categories = result.Data ?? [];
                    //Para o ID não iniciar como 0 
                    InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
                }
            }
            catch (Exception ex) 
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }

        }
        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await TransactionHandler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message ?? "", Severity.Success);
                    NavigationManager.NavigateTo("/lancamentos/historico");
                }
                else
                    Snackbar.Add(result.Message ?? "", Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
