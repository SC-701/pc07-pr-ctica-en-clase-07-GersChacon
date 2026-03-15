using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ISubCategoriaFlujo
    {
        Task<IEnumerable<SubCategoriaResponse>> Obtener();
        Task<SubCategoriaResponse> Obtener(Guid Id);
        Task<IEnumerable<SubCategoriaResponse>> ObtenerPorCategoria(Guid IdCategoria);
    }
}
