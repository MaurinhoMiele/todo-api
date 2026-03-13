using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> Get()
        {
            var tarefas = await _context.Tarefas.AsNoTracking().ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> Get(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if(tarefa is null)
            {
                return NotFound("Id não encontrado");
            }

            return Ok(tarefa);
        }

        [HttpPost]
        public ActionResult Post(Tarefa tarefas)
        {
            if(tarefas is null)
            {
                return BadRequest();
            }

            _context.Tarefas.Add(tarefas);
            _context.SaveChanges();
            return Ok(tarefas);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Tarefa tarefa)
        {
            if(id != tarefa.Id)
            {
                return BadRequest($"Id {id} não existe");
            }

            _context.Entry(tarefa).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(tarefa);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(p => p.Id == id);

            if(tarefa == null)
            {
                return BadRequest($"Id {id} não existe");
            }

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }
    }
}
