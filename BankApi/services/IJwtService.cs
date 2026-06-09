using BankApi.Models;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace BankApi.Interfaces
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}