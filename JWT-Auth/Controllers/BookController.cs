using JWT_Auth.Models;
using JWT_Auth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JWT_Auth.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]  // Requires login (JWT)
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BookDto dto)
        {
            try
            {
                var book = await _bookService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Or log ex
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, BookDto dto)
        {
            var book = await _bookService.UpdateAsync(id, dto);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var success = await _bookService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}