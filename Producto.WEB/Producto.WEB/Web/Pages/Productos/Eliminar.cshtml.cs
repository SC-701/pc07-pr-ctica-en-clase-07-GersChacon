using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public ProductoResponse? producto { get; set; }
        public string? ErrorApi { get; set; }

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
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

            return Page();
        }

        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarProducto");
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();
                return RedirectToPage("./Index");
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

            return Page();
        }
    }
}
