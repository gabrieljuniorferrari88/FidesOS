using System.Diagnostics.CodeAnalysis;
namespace FidesOS.Dominio.Extencoes;
public static class StringExtensao
{
    public static bool NotEmpty([NotNullWhen(true)] this string? value)
        => string.IsNullOrWhiteSpace(value).IsFalse();
}