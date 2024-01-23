using BookManagement.API.Models;
using BookManagement.Application.InputModels;
using BookManagement.Application.ViewModels;
using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksManagementDbContext _dbContext;
    public BooksController(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookViewModel>>> GetAll()
    {
        var books = await _dbContext.Books.ToListAsync();

        if (books is null) return BadRequest();

        var bookViewModel = books.Select(book => new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            YearOfPublication = book.YearOfPublication
        }).ToList();

        return bookViewModel;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookViewModel>> GetById(int id)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);

        if (book is null) return NotFound();

        var bookViewModel = new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            YearOfPublication = book.YearOfPublication
        };
        
        return bookViewModel;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookInputModel createBookInputModel)
    {
        var book = new Book(createBookInputModel.Title, createBookInputModel.Author, createBookInputModel.ISBN, createBookInputModel.YearOfPublication);

        await _dbContext.Books.AddAsync(book);

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, createBookInputModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBookModel updateBookModel)
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == id);

        if (book is null) return BadRequest();

        book.UpdateBook(updateBookModel.Title, updateBookModel.Author, updateBookModel.ISBN, updateBookModel.YearOfPublication);

        await _dbContext.SaveChangesAsync();

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == id);

        if (book is null) return NotFound();

        _dbContext.Books.Remove(book);

        await _dbContext.SaveChangesAsync();

        return Ok(book);
    }
}
