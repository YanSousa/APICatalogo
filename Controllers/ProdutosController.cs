using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.AsNoTracking().ToList(); //Nunca retornar todos os produtos colocar um .Take(10) os 10 primeiros...
            if (produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            if(produto is null)
            {
                return NotFound();
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {

            if (produto is null)
            {
                return BadRequest();
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges(); // salva na tabela
 
            return new CreatedAtRouteResult("ObterProduto", 
                new { id = produto.ProdutoId }, produto); // o Created vai da o 201 sucesso
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) //Esse precisa de informar todos os dados para atualizar.
        {
            if (id != produto.ProdutoId)
            {
                BadRequest(); //400 ERRO
            }
              _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            //var produto = _context.Produtos.Find(id); ele procura na memoria primeiro
            if (produto is null)
            {
                return NotFound("Produto não localizado..."); // pode ser sem mensagem, vai da apenas o erro 400
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto); // pode ser apenas o Ok, com o produto ele retorna o 200 mais o que foi deletado

        }

    }
}
