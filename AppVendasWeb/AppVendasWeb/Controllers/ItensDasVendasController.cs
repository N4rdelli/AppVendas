﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppVendasWeb.Data;
using AppVendasWeb.Models;

namespace AppVendasWeb.Controllers
{
    public class ItensDasVendasController : Controller
    {
        private readonly AppVendasContext _context;

        public ItensDasVendasController(AppVendasContext context)
        {
            _context = context;
        }

        // GET: ItensDasVendas
        public async Task<IActionResult> Index(Guid ? id)
        {
            var appVendasContext = _context.ItensDaVenda.Include(i => i.Produto).Include(i => i.Venda).Where(i => i.Venda.VendaId == id);
            ViewData["VendaId"] = id;
            return View(await appVendasContext.ToListAsync());
        }

        // GET: ItensDasVendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDaVenda = await _context.ItensDaVenda
                .Include(i => i.Produto)
                .Include(i => i.Venda)
                .FirstOrDefaultAsync(m => m.ItemDaVendaId == id);
            if (itemDaVenda == null)
            {
                return NotFound();
            }
            return View(itemDaVenda);
        }

        // GET: ItensDasVendas/Create
        public IActionResult Create(Guid ? id)
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao");
            ViewData["VendaId"] = new SelectList(_context.Vendas.Where(i => i.VendaId == id), "VendaId", "NotaFiscal");
            return View();
        }

        // POST: ItensDasVendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemDaVendaId,ProdutoId,VendaId,Quantidade,Valor")] ItemDaVenda itemDaVenda)
        {
            if (ModelState.IsValid)
            {
                itemDaVenda.ItemDaVendaId = Guid.NewGuid();
                _context.Add(itemDaVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", itemDaVenda.ProdutoId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "NotaFiscal", itemDaVenda.VendaId);
            
            return View(itemDaVenda);
        }

        // GET: ItensDasVendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDaVenda = await _context.ItensDaVenda.FindAsync(id);
            if (itemDaVenda == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", itemDaVenda.ProdutoId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "NotaFiscal", itemDaVenda.VendaId);
            return View(itemDaVenda);
        }

        // POST: ItensDasVendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemDaVendaId,ProdutoId,VendaId,Quantidade,Valor")] ItemDaVenda itemDaVenda)
        {
            if (id != itemDaVenda.ItemDaVendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemDaVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemDaVendaExists(itemDaVenda.ItemDaVendaId))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", itemDaVenda.ProdutoId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "NotaFiscal", itemDaVenda.VendaId);
            return View(itemDaVenda);
        }

        // GET: ItensDasVendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDaVenda = await _context.ItensDaVenda
                .Include(i => i.Produto)
                .Include(i => i.Venda)
                .FirstOrDefaultAsync(m => m.ItemDaVendaId == id);
            if (itemDaVenda == null)
            {
                return NotFound();
            }

            return View(itemDaVenda);
        }

        // POST: ItensDasVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemDaVenda = await _context.ItensDaVenda.FindAsync(id);
            if (itemDaVenda != null)
            {
                _context.ItensDaVenda.Remove(itemDaVenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemDaVendaExists(Guid id)
        {
            return _context.ItensDaVenda.Any(e => e.ItemDaVendaId == id);
        }
    }
}
