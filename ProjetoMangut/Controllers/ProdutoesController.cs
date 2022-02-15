#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMangut.Data;
using ProjetoMangut.Models;

namespace ProjetoMangut.Controllers
{
    [Authorize]
    public class ProdutoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProdutoesController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
            
        }

        // GET: Produtoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        // GET: Produtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeProduto,Preco,Imagem")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();


                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();

                    if (produto.Imagem != null)
                    {

                        try
                        {
                            var webRoot = _environment.WebRootPath;
                            var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                            var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/imgProdutos"));
                            permissionSet.AddPermission(writePermission);

                            var Extension = System.IO.Path.GetExtension(produto.Imagem.FileName);

                            var NomeArquivo = string.Concat(produto.Id.ToString(), Extension);

                            var diretorioArquivoSalvar = string.Concat(webRoot, "\\imgProdutos\\", NomeArquivo);

                            produto.Imagem.CopyTo(new FileStream(diretorioArquivoSalvar, FileMode.Create));

                            produto.url = string.Concat("https://localhost:7296", "/imgProdutos/", NomeArquivo);

                            _context.Update(produto);

                            _context.SaveChanges();
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeProduto,Preco,url")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        [HttpGet("api/ListarProdutos")]
        public async Task<JsonResult> ListarProdutos()
        {
            return Json(await _context.Produto.ToListAsync());
        }



        [AllowAnonymous]
        [HttpPost("api/AdicionarProd")]
        public async void AdicionarProd(string id, string nome, string qtd)
        {

            _context.Compras.Add(new Compras
            {
                Idproduto = Convert.ToInt32(id),
                NomeProduto = nome,
                Quantidade = Convert.ToInt32(qtd)
            });


            _context.SaveChanges();

        }
    }
}
