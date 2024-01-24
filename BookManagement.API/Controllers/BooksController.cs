using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.DeleteBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Queries.GetAllBooks;
using BookManagement.Application.Queries.GetBookById;
using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksManagementDbContext _dbContext;
    private readonly IMediator _mediator;
    public BooksController(BooksManagementDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookViewModel>>> GetAll()
    {
        var query = new GetAllBookQuery();

        var books = await _mediator.Send(query);

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookViewModel>> GetById(int id)
    {
        var bookId = new GetBookByIdQuery(id);

        var bookResult = await _mediator.Send(bookId);

        if (bookResult is null) return NotFound();

        return Ok(bookResult);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
    {
        if (command is null) return BadRequest();

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = id }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBookCommand updateBookCommand)
    {
        var command = new UpdateBookCommand
            (id,
            updateBookCommand.Title,
            updateBookCommand.Author,
            updateBookCommand.Isbn,
            updateBookCommand.YearOfPublication);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteBookCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}
