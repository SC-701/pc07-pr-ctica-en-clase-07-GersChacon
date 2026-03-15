using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class SubCategoriaDA : ISubCategoriaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor
        public SubCategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
        public async Task<IEnumerable<SubCategoriaResponse>> Obtener()
        {
            string query = @"ObtenerSubCategorias";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoriaResponse>(query);
            return resultadoConsulta;
        }

        public async Task<SubCategoriaResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerSubCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoriaResponse>(query,
                new { Id = Id });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<IEnumerable<SubCategoriaResponse>> ObtenerPorCategoria(Guid IdCategoria)
        {
            string query = @"ObtenerSubCategoriasPorCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoriaResponse>(query,
                new { IdCategoria = IdCategoria });
            return resultadoConsulta;
        }
        #endregion

        #region Helpers
        private async Task VerificarSubCategoriaExiste(Guid Id)
        {
            SubCategoriaResponse? resultadoConsultaSubCategoria = await Obtener(Id);
            if (resultadoConsultaSubCategoria == null)
                throw new Exception("No se encontró la subcategoría");
        }
        #endregion
    }
}
