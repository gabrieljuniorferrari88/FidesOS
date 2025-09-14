using FidesOS.Aplicacao.Servicos.Autenticacao;
using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositorios.RefreshToken;
using FidesOS.Dominio.Seguranca.Tokens;
using FidesOS.Excecao.ExcecaoBase;

namespace FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;
public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly ITokenServico _tokenService;
    private readonly IRefreshTokenRepositorioEscrita _refreshTokenWriteOnlyRepository;
    private readonly IRefreshTokenRepositorioLeitura _refreshTokenReadOnlyRepository;
    private readonly ITokenValidadorAcesso _accessTokenValidator;
    private readonly IUnitOfWork _unitOfWork;

    public UseRefreshTokenUseCase(
        ITokenServico tokenService,
        IRefreshTokenRepositorioEscrita refreshTokenWriteOnlyRepository,
        IRefreshTokenRepositorioLeitura refreshTokenReadOnlyRepository,
        ITokenValidadorAcesso accessTokenValidator,
        IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _refreshTokenWriteOnlyRepository = refreshTokenWriteOnlyRepository;
        _refreshTokenReadOnlyRepository = refreshTokenReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenValidator = accessTokenValidator;
    }
    
    public async Task<RespostaTokensJson> Execute(RequisicaoNovoTokenJson request)
    {
        var refreshToken = await _refreshTokenReadOnlyRepository.Get(request.RefreshToken);
        if (refreshToken is null)
            throw new RefreshTokenNotFoundException();

        var accessTokenId = _accessTokenValidator.GetAccessTokenIdentifier(request.AccessToken);
        if(refreshToken.AccessTokenId != accessTokenId)
            throw new RefreshTokenNotFoundException();

        var expireAt = refreshToken.CriadoEm.AddDays(7);
        if(DateTime.UtcNow > expireAt)
            throw new RefreshTokenExpiredException();

        var tokens = _tokenService.GenerateTokens(refreshToken.User);

        await _refreshTokenWriteOnlyRepository.Add(new Dominio.Entidades.RefreshToken
        {
            UserId = refreshToken.UserId,
            Token = tokens.Refresh,
            AccessTokenId = tokens.AccessTokenId
        });

        await _unitOfWork.Commit();

        return new RespostaTokensJson
        {
            RefreshToken = tokens.Refresh,
            AccessToken = tokens.Access
        };
    }
}