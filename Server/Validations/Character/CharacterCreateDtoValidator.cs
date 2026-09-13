using Closavy.Server.Dtos.Character;
using Closavy.Server.Models.Character.Enums;
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
        RuleFor(c => c.Class)
            .NotEmpty()
            .IsInEnum();
        RuleFor(c => c.Deity)
            .NotEmpty()
            .IsInEnum();
        RuleFor(c => c.Race)
            .NotEmpty()
            .IsInEnum();
    }
}
