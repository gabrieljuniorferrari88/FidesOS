using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Comunicacao.Requisicoes.Usuario;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;
using Mapster;

namespace FidesOS.Aplicacao.Servicos.Mapeamentos;

public static class ConfiguracaoDeMapeamentos
{
  public static void Configure()
  {
    TypeAdapterConfig<RequisicaoRegistrarUsuarioJson, Dominio.Entidades.Usuario>
        .NewConfig()
        .Ignore(dest => dest.Senha);

    TypeAdapterConfig<RequisicaoOrdemDeServicoJson, Dominio.Entidades.OrdemDeServico>
        .NewConfig()
        .Ignore(dest => dest.GestorIdentificacao)
        .Ignore(dest => dest.Status);

    TypeAdapterConfig<Dominio.Entidades.OrdemDeServico, RespostaOrdemDeServicoResumidaJson>
            .NewConfig()
            .Map(dest => dest.Id, src => src.OsIdentificacao);

    TypeAdapterConfig<Dominio.Entidades.OrdemDeServico, RespostaOrdemDeServicoJson>
            .NewConfig()
            .Map(dest => dest.Id, src => src.OsIdentificacao);

    TypeAdapterConfig<Dominio.Entidades.AlocacaoTrabalhador, RespostaAlocacaoJson>
            .NewConfig()
            .Map(dest => dest.Id, src => src.AlocacaoIdentificacao);

    // Mapeia a entidade DetalheProducao para a sua resposta JSON
    TypeAdapterConfig<Dominio.Entidades.DetalheProducao, RespostaDetalheProducaoJson>
        .NewConfig()
        .Map(dest => dest.Id, src => src.DetalheIdentificacao);

    // Mapeia a entidade AlocacaoTrabalhador para a sua resposta JSON
    TypeAdapterConfig<Dominio.Entidades.AlocacaoTrabalhador, RespostaAlocacaoTrabalhadorJson>
        .NewConfig()
        .Map(dest => dest.Id, src => src.AlocacaoIdentificacao);

    // Mapeia a entidade OrdemDeServico para a sua resposta JSON detalhada
    TypeAdapterConfig<Dominio.Entidades.OrdemDeServico, RespostaOrdemDeServicoDetalhadaJson>
        .NewConfig()
        .Map(dest => dest.Id, src => src.OsIdentificacao);

    TypeAdapterConfig<Dominio.Entidades.DetalheProducao, RespostaDetalheProducaoJson>
        .NewConfig()
        .Map(dest => dest.Id, src => src.DetalheIdentificacao);
  }
}