using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

public static class NameServices
{
    public static string CharacterRegularty(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        name = name.Replace("ç", "c")
                   .Replace("Ç", "C")
                   .Replace("ğ", "g")
                   .Replace("Ğ", "G")
                   .Replace("ı", "i")
                   .Replace("İ", "I")
                   .Replace("ö", "o")
                   .Replace("Ö", "O")
                   .Replace("ş", "s")
                   .Replace("Ş", "S")
                   .Replace("ü", "u")
                   .Replace("Ü", "U");

        string invalidChars = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
        foreach (char c in invalidChars)
        {
            name = name.Replace(c.ToString(), string.Empty);
        }

        // @, !, ? gibi işaretleri de temizle
        name = Regex.Replace(name, @"[^a-zA-Z0-9_.-]", "");

        return name;
    }
}
