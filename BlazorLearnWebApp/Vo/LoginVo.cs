using System.ComponentModel.DataAnnotations;

namespace BlazorLearnWebApp.Vo;

public class LoginVo
{
    [Required(ErrorMessage = "用户名不能为空")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    public string? Password { get; set; }

    public bool IsKeep { get; set; }
}