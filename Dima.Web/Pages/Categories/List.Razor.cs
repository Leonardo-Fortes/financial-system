using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Web.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

    public partial class ListCategoriesPage : ComponentBase
    {
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService DialogService { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
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
                
            }
        }
        catch(Exception ex)  
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

    public async void OnDeleteButtonClikedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox("ATENÇÃO",$"AO PROSSEGUIR a categoria {title} será excluida. Está é uma ação irreversivel, deseja continuar?", yesText:"EXCLUIR",cancelText:"CANCELAR");
        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            await Handler.DeleteAsync(new DeleteCategoryRequest { Id = id });
            Categories.RemoveAll(c => c.Id == id);
            Snackbar.Add($"Categoria {title} excluida", Severity.Success);
        }
        catch(Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    public Func<Category, bool> Filter => category =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
        // Convertendo o id para string e comparando com o SeatchTerm, e usando o StringComparison.OrdinalIgnoreCase para ignorar espaçamentos
        if (category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
        || category.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (category.Description is not null && category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}

