using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Data;







namespace Ticketing.Domain.Services;

public class UserService
{
    private readonly IJsonRepository<User> _repo;
    public UserService(IJsonRepository<User> repo) => _repo = repo;

    public Task RegisterAsync(User u) => _repo.AddAsync(u);

    public async Task<User?> LoginAsync(string email, string password)
    {
        // Sencillo: compara texto (en producción usar hash)
        return await _repo.GetAsync(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            u.PasswordHash == password);
    }

    public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();
}
