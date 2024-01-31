using System.ComponentModel.DataAnnotations;
using BlazorLearnWebApp.Vo;
using BootstrapBlazor.Components;

namespace BlazorLearnWebApp;

public class RePasswordValidator : IValidator
{
    public void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (context.ObjectInstance is ChangePasswordVo changePasswordVo)
        {

            switch (context.MemberName)
            {
                case nameof(changePasswordVo.RePassword):
                    if (changePasswordVo.RePassword != changePasswordVo.NewPassword)
                    {
                        results.Add(new ValidationResult("两次输入密码不一致", new[] { context.MemberName }));
                    }
                    break;
                case nameof(ChangePasswordVo.NewPassword):
                    if (changePasswordVo.NewPassword?.Length < 6)
                    {
                        results.Add(new ValidationResult("密码最少为6位", new []{context.MemberName}));
                    }
                    break;
            }
            
        }
    }
}