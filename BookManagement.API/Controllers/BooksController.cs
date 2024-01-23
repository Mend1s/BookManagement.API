using BookManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateBookModel createBookModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(GetById), new { id = createBookModel.Id }, createBookModel);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateBookModel updateBookModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
}
