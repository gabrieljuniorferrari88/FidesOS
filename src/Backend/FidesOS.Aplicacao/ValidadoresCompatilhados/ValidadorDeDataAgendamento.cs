using FidesOS.Excecao;
using FluentValidation;
using FluentValidation.Validators;

namespace FidesOS.Aplicacao.ValidadoresCompatilhados;

public class ValidadorDeDataAgendamento<T> : PropertyValidator<T, DateTime>
{

  public override bool IsValid(ValidationContext<T> context, DateTime data)
  {
    if (data == default)
    {
      context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMensagensExcecao.DATA_INVALIDA);

      return false;
    }

    if (data < DateTime.UtcNow)
    {
      context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMensagensExcecao.DATA_NO_PASSADO);

      return false;
    }

    if (data < DateTime.UtcNow && data <= DateTime.UtcNow.AddHours(3))
    {
      context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMensagensExcecao.DATA_INFERIOR_3_HORAS);

      return false;
    }

    return true;
  }
  public override string Name => "DataAgendamentoValidator";

  protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";

}
