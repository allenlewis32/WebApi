using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IConfiguration _configuration;
        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get()
        {
            List<BookModel> books = new();
            SqlConnection conn = new(_configuration.GetConnectionString("db"));
            try
            {
                conn.Open();
                SqlCommand cmd = new("select * from books", conn);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read())
                    {
                        BookModel book = new();
                        book.Id = reader.GetInt32(0);
                        book.BookCode = reader.GetString(1);
                        book.BookTitle = reader.GetString(2);
                        book.Author = reader.GetString(3);
                        book.Price = reader.GetInt32(4);
                        book.SupplierId = reader.GetString(5);
                        books.Add(book);
                    }
                }
            }
            catch
            {
                return NotFound();
            }
            finally
            {
                conn.Close();
            }
            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
