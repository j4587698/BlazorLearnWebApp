using System.ComponentModel.DataAnnotations;
using BlazorLearnWebApp.Entity;

namespace BlazorLearnWebApp.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public class UserAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string str)
        {
            return false;
        }

        var user = UserEntity.Where(x => x.UserName == str).First();
        return user == null;
    }
}