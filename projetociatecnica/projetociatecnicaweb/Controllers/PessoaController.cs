using aplicacao.Servicos.ServicoDePessoa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projetociatecnicaweb.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IServicoPessoa _servPessoa;

        public PessoaController(IServicoPessoa servPessoa)
        {
            _servPessoa = servPessoa;
        }        

        // GET: Pessoa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NovoCliente()
        {            
            var cliente = new FormularioPessoa();
            return View("Cadastrar", cliente);
        }

        public ActionResult IndexEditar(int id,string tipoPessoa)
        {            
            var cliente = _servPessoa.CarregarPessoa(id,tipoPessoa);
            return View("Editar", cliente);
        }

        public ActionResult IndexVisualizarFornecedor(int id, string tipoPessoa)
        {
            var cliente = _servPessoa.CarregarPessoa(id, tipoPessoa);
            return View("VisualizarCliente", cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(FormularioPessoa formularioDeClienete)
        {            
            if (ModelState.IsValid)
            {                             
                    try
                    {                                            
                        _servPessoa.CadastrarPessoa(formularioDeClienete);
                        TempData["Sucesso"] = "Cadastro de cliente realizado com sucesso!";
                        TempData["Situacao"] = "Sucesso!";
                        return RedirectToAction("Index");
                    }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeClienete);
                }
                catch (ValidationException v)
                    {
                        ModelState.AddModelError(string.Empty, v.Message);
                        return View("Cadastrar", formularioDeClienete);
                    }
                }                      

            return View("Cadastrar", formularioDeClienete);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(FormularioPessoa formularioDeCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {                  
                    _servPessoa.EditarPessoa(formularioDeCliente);
                    TempData["Sucesso"] = "Atualização do clienete realizado com sucesso!";
                    TempData["Situacao"] = "Sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ValidationException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeCliente);
                }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeCliente);
                }
            }
            return View("Editar", formularioDeCliente);
        }

        [HttpGet]
        public JsonResult ListarClientes(PesquisaPessoa dadosPesquisa)
        {
            var listaFornecedores = _servPessoa.ListarPessoas(dadosPesquisa);
            return Json(new
            {
                RegistrosPorPagina = listaFornecedores.NumeroDeRegistrosPorPagina,
                Pagina = listaFornecedores.IndiceDePagina,
                TotalDePaginas = listaFornecedores.TotalDePaginas,
                TotalDeRegistros = listaFornecedores.TotalDeRegistros,
                Lista = listaFornecedores.ToList(),
            },
              JsonRequestBehavior.AllowGet);
        }       

        public ActionResult Excluir(int id, string tipoPessoa)
        {                     
             _servPessoa.ExcluirPessoa(id, tipoPessoa);           
                TempData["Sucesso"] = "Clienete Excluido com Sucesso!";
                TempData["Situacao"] = "Sucesso!";
                return Json(new { data = true }, JsonRequestBehavior.AllowGet);            
        }
    }
}