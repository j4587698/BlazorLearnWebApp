﻿using System.Security.Claims;
using BlazorLearnWebApp.Components.Components;
using BlazorLearnWebApp.Entity;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Console = System.Console;

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

    private UserEntity? _userEntity;

    private List<string> _authUrl = new List<string>();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DispatchService.Subscribe(Notify);
    }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

        if (_user == null)
        {
            return;
        }

        var userName = _user.FindFirst(ClaimTypes.Name)?.Value;
        _userEntity = await UserEntity.Where(x => x.UserName == userName).FirstAsync();
        RefreshMenu();
    }

    private void RefreshMenu()
    {
        var roleId = _user!.FindFirst(ClaimTypes.Role)?.Value;
        if (roleId == null)
        {
            return;
        }

        var role = RoleEntity.Where(x => x.Id == int.Parse(roleId)).IncludeMany(x => x.Menus).First();

        if (role == null || role.Menus == null)
        {
            return;
        }

        _authUrl = role.Menus.Select(x => x.Url!).ToList();
        Menus = CascadingMenu(role.Menus, 0);
    }
    
    private List<MenuItem> CascadingMenu(List<MenuEntity> menuEntities, int parentId) => menuEntities
        .Where(x => x.ParentId == parentId)
        .Select(x => new MenuItem { Text = x.MenuName, Items = CascadingMenu(menuEntities, x.Id), Icon = x.Icon, Url = x.Url})
        .ToList();

    /// <summary>
    /// 更新组件方法
    /// </summary>
    public void Update() => StateHasChanged();

    private void ToggleDrawer()
    {
        IsOpen = !IsOpen;
    }
    
    private Task<bool> OnAuthorizing(string url)
    {
        var localPath = new Uri(url).LocalPath;
        Console.WriteLine(localPath);
        if (_authUrl.Any(x => x == localPath))
        {
            return Task.FromResult(true);
        }
        return Task.FromResult(true);
    }
    
    private void ChangePassword()
    {
        DialogService.Show(new DialogOption()
        {
            ShowFooter = false,
            Title = "修改密码",
            Size = Size.Medium,
            BodyTemplate = BootstrapDynamicComponent.CreateComponent<ChangePassword>(new Dictionary<string, object?>()
            {
                [nameof(BlazorLearnWebApp.Components.Components.ChangePassword.UserEntity)] = _userEntity
            }).Render()
        });
    }

    private async Task Notify(DispatchEntry<int> payload)
    {
        if (payload.Name == Constant.RefreshMenuKey && _userEntity?.RoleId == payload.Entry)
        {
            RefreshMenu();
            await InvokeAsync(StateHasChanged);
            await ToastService.Show(new ToastOption()
            {
                Title = "test",
                Content = "Refresh"
            });
        }
    }

    public void Dispose()
    {
        DispatchService.UnSubscribe(Notify);
    }

    private async Task UploadAvatar()
    {
        var result = await DialogService.ShowModal<UploadAvatar>(new ResultDialogOption()
        {
            ShowFooter = true,
            Title = "更新头像",
            Size = Size.Medium,
            ComponentParameters = new Dictionary<string, object>()
            {
                [nameof(BlazorLearnWebApp.Components.Components.UploadAvatar.UserEntity)] = _userEntity
            }
        });
        if (result == DialogResult.Yes)
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}