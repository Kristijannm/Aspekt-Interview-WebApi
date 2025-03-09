namespace CompanyContacts.Shared.Helpers;

public static class HashingHelper
{
    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    public static bool VerifyPassword(string enteredPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
}