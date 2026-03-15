using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {
        private readonly ISubCategoriaDA _subCategoriaDA;

        public SubCategoriaFlujo(ISubCategoriaDA subCategoriaDA)
        {
            _subCategoriaDA = subCategoriaDA;
        }

        public async Task<IEnumerable<SubCategoriaResponse>> Obtener()
        {
            return await _subCategoriaDA.Obtener();
        }

        public Task<SubCategoriaResponse> Obtener(Guid Id)
        {
            return _subCategoriaDA.Obtener(Id);
        }

        public async Task<IEnumerable<SubCategoriaResponse>> ObtenerPorCategoria(Guid IdCategoria)
        {
            return await _subCategoriaDA.ObtenerPorCategoria(IdCategoria);
        }
    }
}
