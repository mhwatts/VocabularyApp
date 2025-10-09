using System.Security.Cryptography;
using System.Text;

namespace VocabularyApp.WebApi.Helpers;

public static class PasswordHelper
{
    /// <summary>
    /// Hashes a password using SHA256 with a salt
    /// </summary>
    public static string HashPassword(string password)
    {
        // Generate a random salt
        var saltBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        
        var salt = Convert.ToBase64String(saltBytes);
        var hash = HashPasswordWithSalt(password, salt);
        
        // Return salt:hash format
        return $"{salt}:{hash}";
    }
    
    /// <summary>
    /// Verifies a password against a stored hash
    /// </summary>
    public static bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;
                
            var salt = parts[0];
            var hash = parts[1];
            
            var computedHash = HashPasswordWithSalt(password, salt);
            return hash == computedHash;
        }
        catch
        {
            return false;
        }
    }
    
    private static string HashPasswordWithSalt(string password, string salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password + salt);
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }
}