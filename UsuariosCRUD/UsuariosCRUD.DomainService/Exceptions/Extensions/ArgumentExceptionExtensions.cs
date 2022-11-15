namespace UsuariosCRUD.DomainService.Exceptions.Extensions;

internal static class ArgumentExceptionExtensions
{
    public static void ThrowIf(Func<bool> expression, string message, string? parameterName, Exception? innerException)
    {
        ArgumentNullException.ThrowIfNull(expression);
        if (expression())
            throw new ArgumentException(message, parameterName, innerException);
    }

    public static void ThrowIf(Func<bool> expression, string message, string? parameterName) =>
        ThrowIf(expression, message, parameterName, null);

    public static void ThrowIfCodigoUsuarioInvalido(long codigo, string? parameterName) =>
        ThrowIf(() => codigo == 0, "Código de usuário é inválido", parameterName);

}
