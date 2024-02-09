using System.ComponentModel.DataAnnotations;

namespace AppVendasWeb.Models
{
    public class ItemDaVenda
    {
        public Guid ItemDaVendaId { get; set; }

        [Display(Name = "Produto Vendido")]
        [Required(ErrorMessage = "É necessário informar o Produto.")]
        public Guid ProdutoId { get; set; }
        public Produto ? Produto { get; set; }

        [Display(Name = "Venda")]
        [Required(ErrorMessage = "É necessário registrar a Data da Venda.")]
        public Guid VendaId { get; set; }
        public Venda ? Venda { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }


    }
}
