using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Data;
using Ticketing.Domain.Models;

namespace Ticketing.Domain.Services
{
    public class UserService
    {
        private readonly MySqlUserDao _repo;

        public UserService(MySqlUserDao repo)
        {
            _repo = repo;
        }

        // Mantengo tus parámetros PBKDF2 (EXCELENTE)
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;

        private static string HashPassword(string password)
        {  //PBKDF2 para hash de contraseñas
            // Generar una sal aleatoria
            //es lo que hace que no se vea el texto de la contraseña 
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

            if (!int.TryParse(parts[0], out var iterations))
                return false;

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
                return false;
            }
        }

        public async Task RegisterAsync(User u, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es obligatoria.");

            // Validar email duplicado
            var existing = await _repo.GetAsync(x => x.Email == u.Email);
            if (existing != null)
                throw new InvalidOperationException("Ya existe un usuario con ese correo.");

            u.PasswordHash = HashPassword(password);
            await _repo.AddAsync(u);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _repo.GetAsync(u => u.Email == email);
            if (user == null) return null;

            if (VerifyPassword(user.PasswordHash, password))
                return user;

            return null;
        }

        public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();
    }
}
