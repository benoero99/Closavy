using Closavy.Server.Dtos.Character;
using FluentValidation;

namespace Closavy.Server.Validations.Character;

public class CharacterCreateDtoValidator : AbstractValidator<CharacterCreateDto>
{
    public CharacterCreateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .Length(3, 20)
            .Matches(@"^\p{L}+(?: \p{L}+)*$");
    }
}
