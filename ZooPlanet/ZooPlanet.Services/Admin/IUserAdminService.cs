namespace ZooPlanet.Services.Admin
{
    using ZooPlanet.Data.Models;

    using System.Threading.Tasks;

    public interface IUserAdminService
    {
        Task<User> GetUserByNameAsync(string userName);
    }
}