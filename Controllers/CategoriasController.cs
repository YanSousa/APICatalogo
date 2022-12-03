using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação"); //mensagem não é obg
            }
           
            //
           
            //return _context.Categorias.AsNoTracking().Include(p => p.Produtos).Where(c => c.CategoriaId <= 5 ).ToList(); // retorna abaixo de 5
        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().ToList();
                if (categoria is null)
                {
                    return NotFound();
                }
                return Ok(categoria);
                //ou return _context.Categorias.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }          
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound();
                }
                //return categoria;  //ou assim

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }
            
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges(); // salva na tabela

            return new CreatedAtRouteResult("ObterProduto",
                new { id = categoria.CategoriaId }, categoria); // o Created vai da o 201 sucesso
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria) //Esse precisa de informar todos os dados para atualizar.
        {
            if (id != categoria.CategoriaId)
            {
                BadRequest(); //400 ERRO
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) //ou public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            //var produto = _context.Produtos.Find(id); ele procura na memoria primeiro
            if (categoria is null)
            {
                return NotFound("Produto não localizado..."); // pode ser sem mensagem, vai da apenas o erro 400
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria); // pode ser apenas o Ok, com o produto ele retorna o 200 mais o que foi deletado

        }
    }
}
