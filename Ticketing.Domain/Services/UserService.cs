using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Data;
using Ticketing.Domain.Models;

namespace Ticketing.Domain.Services;

public class UserService
{
    private readonly IJsonRepository<User> _repo;
    public UserService(IJsonRepository<User> repo) => _repo = repo;

    // Parámetros de PBKDF2
    private const int SaltSize = 16; // bytes
    private const int KeySize = 32;  // bytes
    private const int Iterations = 100_000;

    private static string HashPassword(string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var key = pbkdf2.GetBytes(KeySize);
        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
    }

    private static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrEmpty(hashedPassword)) return false;

        var parts = hashedPassword.Split('.');
        if (parts.Length != 3) return false;
        if (!int.TryParse(parts[0], out var iterations)) return false;

        try
        {
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = pbkdf2.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
        }
        catch
        {
            // Si el formato es inválido o base64 incorrecto
            return false;
        }
    }

    // Registra usuario: recibe el password en claro y lo hashea antes de guardar
    public async Task RegisterAsync(User u, string password)
    {
        if (u == null) throw new ArgumentNullException(nameof(u));
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Que te pasa?,  contraseña es obligatoria.", nameof(password));

        // Evitar duplicados por email
        var existing = await _repo.GetAsync(x => x.Email.Equals(u.Email, StringComparison.OrdinalIgnoreCase));
        if (existing != null)
            throw new InvalidOperationException("Los sentimos, Ya existe un usuario con ese correo.");

        u.PasswordHash = HashPassword(password);
        await _repo.AddAsync(u);
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _repo.GetAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user == null) return null;

        // Si ya está en formato hash, validamos
        if (VerifyPassword(user.PasswordHash, password))
            return user;

        // Si no coincide con hash, comprobamos si es el caso legacy (password en texto plano)
        if (user.PasswordHash == password)
        {
            // Migramos al nuevo hash en el repositorio
            var newHash = HashPassword(password);
            await _repo.UpdateAsync(x => x.Id == user.Id, x => x.PasswordHash = newHash);
            user.PasswordHash = newHash;
            return user;
        }

        return null;
    }

    public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();
}
