using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<ProductoResponse> productos { get; set; } = new List<ProductoResponse>();
        public string? ErrorApi { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProductos");
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

                var respuesta = await cliente.SendAsync(solicitud);

                if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    productos = new List<ProductoResponse>();
                    return;
                }

                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones)
                            ?? new List<ProductoResponse>();
            }
            catch (HttpRequestException ex) when (ex.InnerException is System.Net.Sockets.SocketException)
            {
                ErrorApi = "No fue posible conectarse a la API. Verifique que el servidor esté en ejecución.";
            }
            catch (HttpRequestException ex)
            {
                ErrorApi = $"Error al comunicarse con la API: {ex.Message}";
            }
            catch (Exception ex)
            {
                ErrorApi = $"Error inesperado: {ex.Message}";
            }
        }
    }
}
