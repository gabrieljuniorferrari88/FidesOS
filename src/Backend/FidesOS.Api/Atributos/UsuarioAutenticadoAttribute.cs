using Microsoft.AspNetCore.Mvc;
using FidesOS.Api.Filtros;

namespace FidesOS.Api.Atributos;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class UsuarioAutenticadoAttribute : TypeFilterAttribute<FiltroUsuarioAutenticado>
{
}
