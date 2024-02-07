using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlazorLearnWebApp.Attributes;
using FreeSql;
using FreeSql.DataAnnotations;
using MiniExcelLibs.Attributes;

namespace BlazorLearnWebApp.Entity;

[Description("用户信息表")]
public class UserEntity : BaseEntity<UserEntity, int>
{
    [ExcelColumn(Name = "用户名")]
    [Description("用户名")]
    [Required(ErrorMessage = "用户名不能为空")]
    [User(ErrorMessage = "用户名不能重复")]
    public string? UserName { get; set; }

    [DisplayName("密码")]
    public string? Password { get; set; }

    [DisplayName("显示名")]
    public string? NickName { get; set; }

    [DisplayName("头像地址")]
    public string? AvatarPath { get; set; }

    [Description("角色Id")]
    public int RoleId { get; set; }

    [Navigate(nameof(RoleId))]
    public RoleEntity? Role { get; set; }
    
}