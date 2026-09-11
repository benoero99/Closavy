using Closavy.Server.Dtos.Account;
using FluentValidation;

namespace Closavy.Server.Validations.Character;

public class AccountCreateDtoValidator : AbstractValidator<AccountCreateDto>
{
    public AccountCreateDtoValidator()
    {
        RuleFor(c => c.DisplayName)
            .NotEmpty()
            .Length(3, 20)
            .Matches(@"^\p{L}+(?: \p{L}+)*$");
    }
}
