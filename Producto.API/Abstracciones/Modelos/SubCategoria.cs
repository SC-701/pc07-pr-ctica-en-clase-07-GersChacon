namespace Abstracciones.Modelos
{
    public class SubCategoriaBase
    {
        public string Nombre { get; set; }
    }

    public class SubCategoriaRequest : SubCategoriaBase
    {
        public Guid IdCategoria { get; set; }
    }

    public class SubCategoriaResponse : SubCategoriaBase
    {
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }
}
