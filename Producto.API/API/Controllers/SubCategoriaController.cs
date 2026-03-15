using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase, ISubCategoriaController
    {
        private ISubCategoriaFlujo _subCategoriaFlujo;
        private ILogger<SubCategoriaController> _logger;

        #region Operaciones
        public SubCategoriaController(ISubCategoriaFlujo subCategoriaFlujo, ILogger<SubCategoriaController> logger)
        {
            _subCategoriaFlujo = subCategoriaFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var result = await _subCategoriaFlujo.Obtener();
            if (!result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("porSubCategoria/{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var result = await _subCategoriaFlujo.Obtener(Id);
            return Ok(result);
        }

        [HttpGet("{IdCategoria}")]
        public async Task<IActionResult> ObtenerPorCategoria([FromRoute] Guid IdCategoria)
        {
            var result = await _subCategoriaFlujo.ObtenerPorCategoria(IdCategoria);
            if (!result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }
        #endregion Operaciones

        #region Helpers
        private async Task<bool> VerificarSubCategoriaExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoSubCategoriaExiste = await _subCategoriaFlujo.Obtener(Id);
            if (resultadoSubCategoriaExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion Helpers
    }
}
