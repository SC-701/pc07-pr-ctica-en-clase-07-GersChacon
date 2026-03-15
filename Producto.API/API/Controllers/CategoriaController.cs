using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase, ICategoriaController
    {
        private ICategoriaFlujo _categoriaFlujo;
        private ILogger<CategoriaController> _logger;

        #region Operaciones
        public CategoriaController(ICategoriaFlujo categoriaFlujo, ILogger<CategoriaController> logger)
        {
            _categoriaFlujo = categoriaFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var result = await _categoriaFlujo.Obtener();
            if (!result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var result = await _categoriaFlujo.Obtener(Id);
            return Ok(result);
        }
        #endregion Operaciones

        #region Helpers
        private async Task<bool> VerificarCategoriaExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoCategoriaExiste = await _categoriaFlujo.Obtener(Id);
            if (resultadoCategoriaExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion Helpers
    }
}
