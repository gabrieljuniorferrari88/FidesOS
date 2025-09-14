using FidesOS.Dominio.Dtos;
using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Seguranca.Tokens;

namespace FidesOS.Aplicacao.Servicos.Autenticacao;

public class TokenServico : ITokenServico
{
  private readonly ITokenGeradorAcesso _accessTokenGenerator;
  private readonly IRefreshTokenGenerator _refreshTokenGenerator;
  public TokenServico(
      ITokenGeradorAcesso accessTokenGenerator,
      IRefreshTokenGenerator refreshTokenGenerator)
  {
    _accessTokenGenerator = accessTokenGenerator;
    _refreshTokenGenerator = refreshTokenGenerator;
  }

  public TokensDto GenerateTokens(Usuario usuario)
  {
    (var accessToken, var accessTokenIdentifier) = _accessTokenGenerator.Generate(usuario);
    var refreshToken = _refreshTokenGenerator.Generate();

    return new TokensDto
    {
      Access = accessToken,
      Refresh = refreshToken,
      AccessTokenId = accessTokenIdentifier
    };
  }
}
