using FidesOS.Excecao;
using FluentValidation;
using FluentValidation.Validators;

namespace FidesOS.Aplicacao.ValidadoresCompatilhados;

public class ValidadorDeSenha<T> : PropertyValidator<T, string>
{
  public override bool IsValid(ValidationContext<T> context, string password)
  {
    if (string.IsNullOrEmpty(password))
    {
      context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMensagensExcecao.SENHA_EM_BRANCO);

      return false;
    }

    if (password.Length < 6)
    {
      context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMensagensExcecao.INVALIDO_SENHA);

      return false;
    }

    return true;
  }

  public override string Name => "PasswordValidator";

  protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
