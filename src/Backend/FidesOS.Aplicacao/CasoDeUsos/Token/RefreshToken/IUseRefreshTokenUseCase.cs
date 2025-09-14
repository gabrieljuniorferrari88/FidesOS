using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;
public interface IUseRefreshTokenUseCase
{
    Task<RespostaTokensJson> Execute(RequisicaoNovoTokenJson request);
}
