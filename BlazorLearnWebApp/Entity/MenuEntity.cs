using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FreeSql;
using FreeSql.DataAnnotations;

namespace BlazorLearnWebApp.Entity;

[Description("菜单表")]
public class MenuEntity : BaseEntity<MenuEntity, int>
{
    [Description("菜单名")]
    [Required(ErrorMessage = "菜单名不能为空")]
    public string? MenuName { get; set; }

    [Description("Url")]
    [Required(ErrorMessage = "Url不能为空")]
    public string? Url { get; set; }

    [Description("图标")]
    [Required(ErrorMessage = "图标不能为空")]
    public string? Icon { get; set; }

    public int ParentId { get; set; }

    [Navigate(nameof(ParentId))]
    public MenuEntity? Parent { get; set; }

    [Navigate(nameof(ParentId))]
    public List<MenuEntity>? Children { get; set; }

    [Navigate(ManyToMany = typeof(RoleMenuEntity))]
    public List<RoleEntity>? Roles { get; set; }
}