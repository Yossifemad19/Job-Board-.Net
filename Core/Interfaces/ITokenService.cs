using Core.Entities;
using Core.Interfaces;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken<T>(T user ,string role) where T:IUser;
    }
}
