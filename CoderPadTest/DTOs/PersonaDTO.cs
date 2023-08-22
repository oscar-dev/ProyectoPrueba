namespace CoderPadTest.DTOs
{
    public class PersonaDTO
    {
        public PersonaDTO()
        {
            this.Nombre = "";
            this.Genero = "";
            this.Identificacion = "";
            this.Direccion = "";
            this.Telefono = "";
        }

        // Tabla Persona
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
