﻿namespace BookStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepo;
    private readonly ILogger<BookController> _logger;
    private readonly IMapper _mapper;

    public BookController(IBookRepository bookRepo, ILogger<BookController> logger, IMapper mapper)
    {
        _bookRepo = bookRepo;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet()]
    [Route("/api/BooksWithPg")]
    public async Task<ActionResult<VirtualizeResponse<BookReadDto>>> GetBooksWithPg([FromQuery] QueryParameters queryParams)
    {
        try
        {
            return await _bookRepo.GetAllWithPg<BookReadDto>(queryParams);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetBooksWithPg)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    [HttpGet()]
    [Route("/api/Books")]
    public async Task<ActionResult<List<BookReadDto>>> GetBooks()
    {
        try
        {
            var books = await _bookRepo.GetAllAsync<BookReadDto>();
            if (books == null)
                return NotFound();

            return books;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetBooks)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookReadDto>> GetAuthor(int id)
    {
        try
        {
            if (id == null)
                return BadRequest();

            var book = await _bookRepo.GetBookAsync(id);
            if (book == null)
                return NotFound();

            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetAuthor)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateBook([FromBody] BookCreateDto bookCreateDto)
    {
        try
        {
            if (bookCreateDto == null || ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            var isCreated = await _bookRepo.CreateAsync(bookCreateDto);
            if (isCreated == false)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(CreateBook)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateBook([FromBody] BookUpdateDto bookUpdateDto)
    {
        try
        {
            if (bookUpdateDto == null || ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            var isUpdated = await _bookRepo.UpdateAsync(bookUpdateDto);
            if (isUpdated == false)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(UpdateBook)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteBook(int id)
    {
        try
        {
            if (id == null)
                return BadRequest();

            var idDeleted = await _bookRepo.DeleteAsync(id);
            if (idDeleted == false)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(DeleteBook)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }
}
