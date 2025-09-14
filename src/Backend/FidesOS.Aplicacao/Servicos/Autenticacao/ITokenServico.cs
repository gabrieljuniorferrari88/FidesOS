using FidesOS.Dominio.Dtos;
using FidesOS.Dominio.Entidades;

namespace FidesOS.Aplicacao.Servicos.Autenticacao;

public interface ITokenServico
{
  TokensDto GenerateTokens(Usuario usuario);
}
