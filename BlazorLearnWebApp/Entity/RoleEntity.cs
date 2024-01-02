using System.ComponentModel;
using FreeSql;
using FreeSql.DataAnnotations;

namespace BlazorLearnWebApp.Entity;

[Description("角色表")]
public class RoleEntity : BaseEntity<RoleEntity, int>
{
    [Description("角色名")]
    public string? RoleName { get; set; }

    [Navigate(nameof(UserEntity.RoleId))]
    public List<UserEntity>? Users { get; set; }

    [Navigate(ManyToMany = typeof(RoleMenuEntity))]
    public List<MenuEntity>? Menus { get; set; }
}