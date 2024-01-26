using System.Diagnostics.CodeAnalysis;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorLearnWebApp.Components.Components;

[CascadingTypeParameter(nameof(TItem))]
public partial class AdminTable<TItem> where TItem : class, new()
{

    private Table<TItem>? _table;
    
    [NotNull]
    [Parameter]
    public RenderFragment<TItem>? TableColumns { get; set; }
    
    [Parameter]
    public Func<TItem,ItemChangedType,Task<bool>>? OnSaveAsync { get; set; }
    
    [Parameter] public Func<IEnumerable<TItem>,Task<bool>>? OnDeleteAsync { get; set; }
    
    [Parameter] public bool IsTree { get; set; }
    
    [Parameter] public Func<IEnumerable<TItem>,Task<IEnumerable<TableTreeNode<TItem>>>>? TreeNodeConverter { get; set; }
    
    [Parameter] public Func<TItem,bool>? ShowExtendEditButtonCallback { get; set; }
    
    [Parameter] public Func<TItem,bool>? ShowExtendDeleteButtonCallback { get; set; }
    
    [Parameter] public RenderFragment<TItem>? BeforeRowButtonTemplate { get; set; }
    
    [Parameter] public RenderFragment? TableToolbarTemplate { get; set; }
    
    [Parameter] public RenderFragment? TableExtensionToolbarTemplate { get; set; }
    
    [Parameter] public RenderFragment<TItem>? RowButtonTemplate { get; set; }

    [Parameter] public bool IsPagination { get; set; } = true;
    
    [Parameter] public bool IsMultipleSelect { get; set; } = false;


    public async Task QueryAsync()
    {
        await _table!.QueryAsync();
    }
}