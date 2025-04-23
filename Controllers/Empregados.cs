using EmpregadoApi.Context;
using EmpregadoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EmpregadoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Empregados : ControllerBase
    {
        private readonly AppDbContext _context;
        public Empregados(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cadastro>> Get()
        {
            var empregado = _context.cadastroempregado.ToList();
            return Ok(empregado);
        }

        [HttpGet("{id:int}", Name = "ObterEmpregado")]
        public ActionResult<Cadastro> Get(int id)
        {
            var empregado = _context.cadastroempregado.FirstOrDefault(e => e.EmpregadoId == id);
            if (empregado == null)
            {
                return NotFound("Empregado não encontrado!");
            }
            return empregado;
        }

        [HttpPost]
        public ActionResult Post(Cadastro cadastro)
        {
            if (cadastro is null)
            {
                return BadRequest();
            }
            _context.cadastroempregado.Add(cadastro);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterEmpregado", new { id = cadastro.EmpregadoId }, cadastro);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cadastro cadastro)
        {
            if (id != cadastro.EmpregadoId)
            {
                return BadRequest();
            }
            _context.Entry(cadastro).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(cadastro);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var empregado = _context.cadastroempregado.FirstOrDefault(e => e.EmpregadoId == id);
            if (empregado == null)
            {
                return NotFound("Empregado não encontrado!");
            }
            _context.cadastroempregado.Remove(empregado);
            _context.SaveChanges();
            return Ok("Empregado removido com sucesso!");
        }
    }
};
