using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Teste.redeserve.Models;

namespace Teste.redeserve.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _environment;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult questao4()
        {
            var clsTeste = new ClsTeste (){ Codigo = 1000, Descricao = "Teste"};
            return Ok(clsTeste);
        } 
        
        [HttpGet]
        public async Task<IActionResult> BuscaCep(string valorCep)
        {
            try
            {
                var ws = new WSCorreios.AtendeClienteClient();
                var resultado = await ws.consultaCEPAsync(valorCep);
                return Ok(resultado);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Imagem()
        {
            try
            {
                byte[] bytes;
                string base64;
                string pastaFotos = Path.Combine(_environment.WebRootPath, "Imgs");
                string caminhoArquivo = Path.Combine(pastaFotos, "redeservice.jpg");
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Open))
                {
                    
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                        base64 = Convert.ToBase64String(bytes);
                    }
                }
                return Ok("Imagem alterada para Base 64 com sucesso " + base64);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
