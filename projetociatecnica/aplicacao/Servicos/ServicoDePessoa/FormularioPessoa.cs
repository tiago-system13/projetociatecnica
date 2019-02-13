using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Servicos.ServicoDePessoa
{
   public class FormularioPessoa
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="do Documento")]
        public string NumeroDocumento { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]        
        [Display(Name = "Sobre Nome")]
        public string SobreNome { get; set; }                        
        public DateTime? DataNascimento { get; set; }

        [Required]
        [Display(Name = "Tipo de Pessoa")]
        public string TipoPessoa { get; set; }
        [Required]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }
        [Required]
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        [Required]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Required]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [Required]
        [Display(Name = "UF")]
        public string Uf { get; set; }
    }
}
