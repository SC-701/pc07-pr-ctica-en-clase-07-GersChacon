namespace Abstracciones.Modelos
{
    public class CategoriaBase
    {
        public string Nombre { get; set; }
    }

    public class CategoriaRequest : CategoriaBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class CategoriaResponse : CategoriaBase
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
}
