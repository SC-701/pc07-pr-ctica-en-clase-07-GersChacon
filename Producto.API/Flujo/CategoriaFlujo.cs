using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class CategoriaFlujo : ICategoriaFlujo
    {
        private readonly ICategoriaDA _categoriaDA;

        public CategoriaFlujo(ICategoriaDA categoriaDA)
        {
            _categoriaDA = categoriaDA;
        }

        public async Task<IEnumerable<CategoriaResponse>> Obtener()
        {
            return await _categoriaDA.Obtener();
        }

        public Task<CategoriaResponse> Obtener(Guid Id)
        {
            return _categoriaDA.Obtener(Id);
        }
    }
}
