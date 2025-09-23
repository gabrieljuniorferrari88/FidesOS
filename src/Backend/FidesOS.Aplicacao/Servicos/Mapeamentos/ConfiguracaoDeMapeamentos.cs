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
  }
}