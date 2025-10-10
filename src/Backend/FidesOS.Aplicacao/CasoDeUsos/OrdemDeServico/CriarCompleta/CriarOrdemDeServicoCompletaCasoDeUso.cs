using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.DetalhesProducao;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.CriarCompleta;

public class CriarOrdemDeServicoCompletaCasoDeUso : ICriarOrdemDeServicoCompletaCasoDeUso
{
  private readonly IRepositorioEscritaOrdemDeServico _repositorioEscritaOrdemDeServico;
  private readonly IRepositorioEscritaAlocacao _repositorioEscritaAlocacao;
  private readonly IRepositorioEscritaDetalheProducao _repositorioEscritaDetalheProducao;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioLeituraUsuario _repositorioLeituraUsuario;

  public CriarOrdemDeServicoCompletaCasoDeUso(
    IRepositorioEscritaOrdemDeServico repositorioEscritaOrdemDeServico,
    IRepositorioEscritaAlocacao repositorioEscritaAlocacao,
    IRepositorioEscritaDetalheProducao repositorioEscritaDetalheProducao,
    IUnitOfWork unitOfWork, IUsuarioLogado usuarioLogado,
    IRepositorioLeituraUsuario repositorioLeituraUsuario
    )
  {
    _repositorioEscritaOrdemDeServico = repositorioEscritaOrdemDeServico;
    _repositorioEscritaAlocacao = repositorioEscritaAlocacao;
    _repositorioEscritaDetalheProducao = repositorioEscritaDetalheProducao;
    _unitOfWork = unitOfWork;
    _usuarioLogado = usuarioLogado;
    _repositorioLeituraUsuario = repositorioLeituraUsuario;
  }

  public async Task<RespostaOrdemDeServicoDetalhadaJson> Execute(RequisicaoOrdemDeServicoCompletaJson request)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    Validate(request, usuarioLogado);

    var novaOS = new Dominio.Entidades.OrdemDeServico(request.EmpresaClienteId, request.Descricao, request.DataAgendamento);

    novaOS.AdicionarOGestorId(usuarioLogado.UserIdentificacao);

    foreach (var alocacaoRequest in request.Alocacoes)
    {
      var novaAlocacao = new AlocacaoTrabalhador(
          novaOS.OsIdentificacao,
          alocacaoRequest.TrabalhadorIdentificacao,
          alocacaoRequest.ValorCombinado
      );
      foreach (var detalheRequest in alocacaoRequest.Detalhes)
      {
        var novoDetalhe = new Dominio.Entidades.DetalheProducao(
            novaAlocacao.AlocacaoIdentificacao,
            detalheRequest.Descricao,
            detalheRequest.Valor
        );
        novaAlocacao.Detalhes.Add(novoDetalhe);
        novaAlocacao.AdicionarCusto(novoDetalhe.Valor);
      }
      novaOS.Alocacoes.Add(novaAlocacao);
    }

    await _repositorioEscritaOrdemDeServico.AddAsync(novaOS);

    await _unitOfWork.Commit();

    return novaOS.Adapt<RespostaOrdemDeServicoDetalhadaJson>();
  }

  private async Task Validate(RequisicaoOrdemDeServicoCompletaJson request, Dominio.Entidades.Usuario usuarioLogado)
  {
    var resultadoDto = new CriarOrdemDeServicoCompletaValidacao().Validate(request);

    var empresaCliente = await _repositorioLeituraUsuario.BuscarPorUserIdentificacao(request.EmpresaClienteId);
    if (empresaCliente is null || empresaCliente.Perfil != PerfilUsuario.Empresa)
      resultadoDto.Errors.Add(new("EmpresaClienteId", ResourceMensagensExcecao.EMPRESA_NAO_ENCONTRADO));

    // Validação em Massa dos Trabalhadores
    if (request.Alocacoes.Any())
    {
      var idsTrabalhadoresRequest = request.Alocacoes.Select(a => a.TrabalhadorIdentificacao).Distinct().ToList();
      var trabalhadoresDoBanco = await _repositorioLeituraUsuario.BuscarVariosPorIdentificacao(idsTrabalhadoresRequest);
      if (trabalhadoresDoBanco.Count != idsTrabalhadoresRequest.Count)
        resultadoDto.Errors.Add(new("", ResourceMensagensExcecao.NAO_ENCONTRADO_TRABALHADOR));
      else if (!trabalhadoresDoBanco.All(t => t.GestorIdentificacao == usuarioLogado.UserIdentificacao
                                        && t.Perfil == PerfilUsuario.Trabalhador && t.Status == StatusUsuario.Ativo))
        resultadoDto.Errors.Add(new("", ResourceMensagensExcecao.TRABALHADOR_NAO_PERTENCE_AO_GESTOR));
    }

    // 4. Lança todos os erros de uma vez, se houver algum
    if (resultadoDto.IsValid.IsFalse())
      throw new ErrorOnValidationException(resultadoDto.Errors.Select(e => e.ErrorMessage).ToList());
  }
}