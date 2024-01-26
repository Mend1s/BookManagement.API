using BookManagement.Application.Commands.RenewalLoan;
using FluentValidation;

namespace BookManagement.Application.Validators;

public class RenewalLoanCommandValidator : AbstractValidator<RenewalLoanCommand>
{
	public RenewalLoanCommandValidator()
	{
        RuleFor(loan => loan.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("O Id do empréstimo não pode ser vazio!");
    }
}
