using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse productoResponse { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }
        [BindProperty]
        public Guid subCategoriaSeleccionada { get; set; }
        public string? ErrorApi { get; set; }

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

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    await ObtenerCategorias();
                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    productoResponse = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

                    if (productoResponse != null)
                    {
                        var categoriaItem = categorias.FirstOrDefault(c => c.Text == productoResponse.Categoria);
                        if (categoriaItem != null)
                        {
                            categoriaSeleccionada = Guid.Parse(categoriaItem.Value);
                            var listaSubCategorias = await ObtenerSubCategorias(categoriaSeleccionada);
                            subcategorias = listaSubCategorias.Select(sc => new SelectListItem
                            {
                                Value = sc.Id.ToString(),
                                Text = sc.Nombre,
                                Selected = sc.Nombre == productoResponse.SubCategoria
                            }).ToList();
                            var subCategoriaItem = subcategorias.FirstOrDefault(sc => sc.Text == productoResponse.SubCategoria);
                            if (subCategoriaItem != null)
                                subCategoriaSeleccionada = Guid.Parse(subCategoriaItem.Value);
                        }
                        else
                        {
                            subcategorias = new List<SelectListItem>();
                        }
                    }
                }
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

        public async Task<ActionResult> OnPost()
        {
            ModelState.Remove("productoResponse.SubCategoria");
            ModelState.Remove("productoResponse.Categoria");

            if (!ModelState.IsValid)
            {
                await ObtenerCategorias();
                if (categoriaSeleccionada != Guid.Empty)
                {
                    var listaSubCategorias = await ObtenerSubCategorias(categoriaSeleccionada);
                    subcategorias = listaSubCategorias.Select(sc => new SelectListItem
                    {
                        Value = sc.Id.ToString(),
                        Text = sc.Nombre,
                        Selected = sc.Id == subCategoriaSeleccionada
                    }).ToList();
                }
                else
                {
                    subcategorias = new List<SelectListItem>();
                }
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
                var cliente = new HttpClient();
                var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(
                    string.Format(endpoint, productoResponse.Id),
                    new ProductoRequest
                    {
                        Nombre = productoResponse.Nombre,
                        Descripcion = productoResponse.Descripcion,
                        Precio = productoResponse.Precio,
                        Stock = productoResponse.Stock,
                        CodigoBarras = productoResponse.CodigoBarras,
                        IdSubCategoria = subCategoriaSeleccionada
                    });
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

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
            categorias = resultadoDeserializado.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
            }).ToList();
        }

        private async Task<List<SubCategoria>> ObtenerSubCategorias(Guid categoriaId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }
            return new List<SubCategoria>();
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaId)
        {
            var subcategorias = await ObtenerSubCategorias(categoriaId);
            return new JsonResult(subcategorias);
        }
    }
}
