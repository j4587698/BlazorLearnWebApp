using System.ComponentModel.DataAnnotations;

namespace BlazorLearnWebApp.Vo;

public class ChangePasswordVo
{
    [Required(ErrorMessage = "旧密码不能为空")]
    public string? OldPassword { get; set; }

    [Required(ErrorMessage = "新密码不能为空")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "新密码确认不能为空")]
    public string? RePassword { get; set; }
}