namespace ZooPlanet.Services.Admin
{
    using System.Threading.Tasks;
    using ZooPlanet.Data.Models;

    public interface IUserAdminService
    {
        Task<User> GetUserByNameAsync(string userName);
    }
}