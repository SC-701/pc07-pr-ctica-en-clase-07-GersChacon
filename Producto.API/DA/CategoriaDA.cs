using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor
        public CategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
        public async Task<IEnumerable<CategoriaResponse>> Obtener()
        {
            string query = @"ObtenerCategorias";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CategoriaResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CategoriaResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CategoriaResponse>(query,
                new { Id = Id });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task VerificarCategoriaExiste(Guid Id)
        {
            CategoriaResponse? resultadoConsultaCategoria = await Obtener(Id);
            if (resultadoConsultaCategoria == null)
                throw new Exception("No se encontró la categoría");
        }
        #endregion
    }
}
