using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosCRUD.DomainService.Exceptions;

public sealed partial class DomainServiceException : Exception
{
    public DomainServiceException(string? mensagem) : base(mensagem) { }

    public DomainServiceException(string? mensagem, Exception? innerException) : base(mensagem, innerException) { }

    public static void ThrowIf(Func<bool> expression, string? mensagem, Exception? innerException)
    {
        ArgumentNullException.ThrowIfNull(expression);
        if (expression())
            throw new DomainServiceException(mensagem, innerException);
    }

    public static void ThrowIf(Func<bool> expression, string? mensagem) =>
        ThrowIf(expression, mensagem, null);

    public static void ThrowIfNull<TModel>(TModel modelo, string? mensagem) =>
        ThrowIf(() => modelo is null, mensagem);

    public static void ThrowIfNull<TModel>(TModel modelo, string? mensagem, Exception? innerException) =>
        ThrowIf(() => modelo is null, mensagem, innerException);
}
