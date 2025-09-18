using FidesOS.Comunicacao.Requisicoes.Token;
using FidesOS.Comunicacao.Respostas.Token;

namespace FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;
public interface IUseRefreshTokenUseCase
{
    Task<RespostaTokensJson> Execute(RequisicaoNovoTokenJson request);
}
