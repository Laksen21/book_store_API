using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using book_store_API.Models;
using book_store_API.Data;

namespace book_store_API.Controllers
{
    [Route("api/book-store/[action]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly ApiContext _context;
        public BookStoreController(ApiContext context) {  _context = context; }

        //add / edit
        [HttpPost]
        public JsonResult addEdit(Book book) 
        {
            if(book.Id == 0)
            {
                _context.Books.Add(book);
            } else
            {
                var inDb = _context.Books.Find(book.Id);

                if(inDb == null)
                {
                    return new JsonResult(NotFound());
                }

                inDb.Title = book.Title;
                inDb.Author = book.Author;
                inDb.Isbn = book.Isbn;
                inDb.PublicationDate = book.PublicationDate;
            }

            _context.SaveChanges();
            return new JsonResult(Ok(book));
        }

        //getById
        [HttpGet]
        public JsonResult getById(int id)
        {
            var result = _context.Books.Find(id);

            if(result == null) 
            { 
                return new JsonResult(NotFound()); 
            }

            return new JsonResult(result);
        }

        //getAll
        [HttpGet]
        public JsonResult getAll() 
        {
            var result = _context.Books.ToList();

            return new JsonResult(result);
        }

        //delete
        [HttpDelete]
        public JsonResult delete(int id)
        {
            var result = _context.Books.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Books.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
