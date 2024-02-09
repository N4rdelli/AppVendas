using System.ComponentModel.DataAnnotations;

namespace AppVendasWeb.Models
{
    public class Venda
    {
        public Guid VendaId { get; set; }

        [Display(Name = "Data da Venda")]
        [Required(ErrorMessage = "É necessário registrar a Data da Venda.")]
        public DateTime DataVenda {  get; set; }

        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "O campo Valor Total é obrigatório.")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Número da Venda")]
        [Required(ErrorMessage = "Informe o número da venda.")]
        public int NotaFiscal { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O campo Cliente é obrigatório.")]
        public Guid ClienteId { get; set; }
        public Cliente ? Cliente { get; set; }
    }
}
