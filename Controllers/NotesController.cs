using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAppAPI___ASP.DataAccess;
using NotesAppAPI___ASP.Models.Domain;
using NotesAppAPI___ASP.Models.DTO;

namespace NotesAppAPI___ASP.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext connection;

        public NotesController(NotesDbContext connection)
        {
            this.connection = connection;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            return Ok(await connection.Notes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute]int id)
        {
            Note note = await connection.Notes.FindAsync(id);
            
            if(note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NoteCreationDTO noteCreationDTO)
        {
            Note note = new Note
            {
                Title = noteCreationDTO.Title,
                Description = noteCreationDTO.Description,
                ColorHex = noteCreationDTO.ColorHex,
                DateCreated = DateTime.Now,
            };

            await connection.Notes.AddAsync(note);
            await connection.SaveChangesAsync();
            return Ok(note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]NoteCreationDTO noteCreationDTO)
        {
            Note note = await connection.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            note.Title = noteCreationDTO.Title;
            note.Description = noteCreationDTO.Description;
            note.ColorHex = noteCreationDTO.ColorHex;

            connection.Notes.Update(note);
            await connection.SaveChangesAsync();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            Note note = await connection.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            connection.Notes.Remove(note);
            return Ok(await connection.SaveChangesAsync());
        }
    }
}
