using FidesOS.Comunicacao.Requisicoes;
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
  }
}
