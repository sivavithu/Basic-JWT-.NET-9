using JWT_Auth.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWT_Auth.Service
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(Guid id);
        Task<BookDto> CreateAsync(BookDto dto);
        Task<BookDto?> UpdateAsync(Guid id, BookDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}