using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatLibrary.Models;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatroomsController : ControllerBase
    {
        private readonly ChatApidbContext _context;

        public ChatroomsController(ChatApidbContext context)
        {
            _context = context;
        }

        // GET: api/Chatrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chatroom>>> GetChatrooms()
        {
          if (_context.Chatrooms == null)
          {
              return NotFound();
          }
            return await _context.Chatrooms.ToListAsync();
        }

        // GET: api/Chatrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chatroom>> GetChatroom(int id)
        {
          if (_context.Chatrooms == null)
          {
              return NotFound();
          }
            var chatroom = await _context.Chatrooms.FindAsync(id);

            if (chatroom == null)
            {
                return NotFound();
            }

            return chatroom;
        }

        // PUT: api/Chatrooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatroom(int id, Chatroom chatroom)
        {
            if (id != chatroom.ChatroomId)
            {
                return BadRequest();
            }

            _context.Entry(chatroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatroomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chatrooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chatroom>> PostChatroom(Chatroom chatroom)
        {
          if (_context.Chatrooms == null)
          {
              return Problem("Entity set 'ChatApidbContext.Chatrooms'  is null.");
          }
            _context.Chatrooms.Add(chatroom);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChatroomExists(chatroom.ChatroomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChatroom", new { id = chatroom.ChatroomId }, chatroom);
        }

        // DELETE: api/Chatrooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatroom(int id)
        {
            if (_context.Chatrooms == null)
            {
                return NotFound();
            }
            var chatroom = await _context.Chatrooms.FindAsync(id);
            if (chatroom == null)
            {
                return NotFound();
            }

            _context.Chatrooms.Remove(chatroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatroomExists(int id)
        {
            return (_context.Chatrooms?.Any(e => e.ChatroomId == id)).GetValueOrDefault();
        }
    }
}
