using BookManagement.Application.Commands.CreateBook;
using BookManagement.Infrastructure.Persistence;
using FluentValidation;

namespace BookManagement.Application.Validators;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    private readonly BooksManagementDbContext _dbContext;
    public CreateBookCommandValidator(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(book => book.YearOfPublication)
                        .Must(ValidatePublicationYearNotGreaterThanCurrentYear)
                        .WithMessage("O ano de publicação não pode ser superior ao ano atual!");

        RuleFor(book => book.YearOfPublication)
            .Must(ValidatePublicationYearNotLessThanZero)
            .WithMessage("O ano de publicação não pode ser menor ou igual a 0!");

        RuleFor(book => book.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("O titulo do livro não pode ser um campo vazio!");

        RuleFor(book => book.Title)
            .Length(5, 200)
            .WithMessage("O titúlo precisa ter de 5 a 200 caracteres!");

        RuleFor(book => book.Isbn)
            .NotEmpty()
            .Length(13)
            .WithMessage("O ISBN tem que ter exatamente 13 dígitos, verifique e preencha novamente!");

        RuleFor(book => book.Isbn)
            .NotNull()
            .Must(ValidateIsbnDoesNotExist)
            .WithMessage("Este ISBN já está cadastrado, não é possivel cadastrar novamente!");

        RuleFor(book => book.Author)
            .NotEmpty()
            .WithMessage("O autor do livro não pode ser um campo em branco!");

        RuleFor(book => book.Author)
            .NotEqual(book => book.Title)
            .WithMessage("O autor não pode ser igual ao titúlo do livro");   
    }
    private bool ValidatePublicationYearNotGreaterThanCurrentYear(int publicationYear)
    {
        if (publicationYear > DateTime.Now.Year)
        {
            return false;
        }
        return true;
    }

    private bool ValidatePublicationYearNotLessThanZero(int publicationYear)
    {
        if (publicationYear <= 0)
        {
            return false;
        }
        return true;
    }

    private bool ValidateIsbnDoesNotExist(string isbn)
    {
        var isbnDb = _dbContext.Books.Any(b => b.Isbn == isbn);

        if (isbnDb)
        {
            return true;
        }
        return false;
    }
}