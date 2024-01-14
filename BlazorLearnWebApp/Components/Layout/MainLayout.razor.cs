using System.Security.Claims;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorLearnWebApp.Components.Layout;

public partial class MainLayout
{
    private bool IsOpen { get; set; }

    private string? Theme { get; set; }

    private string? LayoutClassString => CssBuilder.Default()
        .AddClass(Theme)
        .AddClass("is-fixed-tab", IsFixedTab)
        .Build();

    private IEnumerable<MenuItem>? Menus { get; set; }

    /// <summary>
    /// 获得/设置 是否固定 TabHeader
    /// </summary>
    [Parameter]
    public bool IsFixedTab { get; set; }

    /// <summary>
    /// 获得/设置 是否固定页头
    /// </summary>
    [Parameter]
    public bool IsFixedHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否固定页脚
    /// </summary>
    [Parameter]
    public bool IsFixedFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 侧边栏是否外置
    /// </summary>
    [Parameter]
    public bool IsFullSide { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示页脚
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否开启多标签模式
    /// </summary>
    [Parameter]
    public bool UseTabSet { get; set; } = true;

    private ClaimsPrincipal? _user;

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        // 模拟异步获取菜单数据
        await Task.Delay(10);

        Menus = new List<MenuItem>
        {
            new() { Text = "首页", Icon = "fa-fw fa-solid fa-house", Url = "/" },
            new() { Text = "用户管理", Icon = "fa-fw fa-solid fa-desktop", Url = "user" },
            new() { Text = "角色管理", Icon = "fa-fw fa-solid fa-desktop", Url = "role" },
            new() { Text = "菜单管理", Icon = "fa-fw fa-solid fa-laptop", Url = "menu" }
        };
    }

    /// <summary>
    /// 更新组件方法
    /// </summary>
    public void Update() => StateHasChanged();

    private void ToggleDrawer()
    {
        IsOpen = !IsOpen;
    }
}