using PublicLibrary.Data.Models;

namespace PublicLibrary.IRepositories
{
    public interface IJwtRepository
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
