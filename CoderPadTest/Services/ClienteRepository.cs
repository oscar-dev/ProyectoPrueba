using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Services
{
    public class ClienteRepository
        : IClienteRepository, IDisposable
    {
        private BaseContext _baseContext;
        private bool disposed = false;

        public ClienteRepository(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._baseContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void insertCliente(ClienteDTO clienteDTO)
        {
            validarClienteDTO(clienteDTO);

            var persona = new Persona()
            { 
                Nombre = clienteDTO.Nombre,
                Genero = clienteDTO.Genero,
                Direccion = clienteDTO.Direccion,
                Identificacion = clienteDTO.Identificacion,
                Telefono = clienteDTO.Telefono,
                Edad = clienteDTO.Edad
            };

            this._baseContext.Personas.Add(persona);

            this._baseContext.SaveChanges();

            var cliente = new Cliente()
            {
                Contrasenia = "",
                PersonaId = persona.PersonaId,
                Estado = true
            };

            this._baseContext.Clientes.Add(cliente);

            this._baseContext.SaveChanges();
        }
        private bool validarClienteDTO(ClienteDTO clienteDTO)
        {
            if (clienteDTO.Nombre == null || clienteDTO.Nombre.Trim().Length <= 0)
            {
                throw new Exception("Nombre inválido");
            }
            if (clienteDTO.Identificacion == null || clienteDTO.Identificacion.Trim().Length <= 0)
            {
                throw new Exception("Identificación inválida");
            }

            if (clienteDTO.Direccion == null || clienteDTO.Direccion.Trim().Length <= 0)
            {
                throw new Exception("Dirección inválida");
            }

            return true;
        }
        public void updateCliente(ClienteDTO clienteDTO)
        {
            validarClienteDTO(clienteDTO);

            var cliente = this._baseContext.Clientes.Where(c => c.ClienteId == clienteDTO.ClienteId).FirstOrDefault();

            if (cliente != null)
            {
                cliente.Estado = clienteDTO.Estado;

                this._baseContext.SaveChanges();

                var persona = this._baseContext.Personas.Where(p => p.PersonaId == cliente.PersonaId).FirstOrDefault();

                if (persona != null)
                {
                    persona.Nombre = clienteDTO.Nombre;
                    persona.Identificacion = clienteDTO.Identificacion;
                    persona.Edad = clienteDTO.Edad;
                    persona.Direccion = clienteDTO.Direccion;
                    persona.Genero = clienteDTO.Genero;
                    persona.Telefono = clienteDTO.Telefono;

                    this._baseContext.SaveChanges();
                }
                else
                {
                    throw new NotFoundException("Persona no encontrada. Id: " + cliente.PersonaId );
                }
            }
            else
            {
                throw new NotFoundException("Cliente no encontrado. Id: " + clienteDTO.ClienteId);
            }
        }
        public void deleteCliente(int clienteId)
        {
            var cliente = this._baseContext.Clientes.Where( c => c.ClienteId == clienteId ).FirstOrDefault();

            if( cliente != null )
            {
                int personaId = cliente.PersonaId;

                this._baseContext.Clientes.Remove(cliente);
                this._baseContext.SaveChanges();

                var persona = this._baseContext.Personas.Where( p => p.PersonaId == personaId ).FirstOrDefault();

                if( persona != null )
                {
                    this._baseContext.Personas.Remove(persona);
                    this._baseContext.SaveChanges();
                } else
                {
                    throw new NotFoundException("Persona no encontrada. Id: " + personaId);
                }
            } else
            {
                throw new NotFoundException("Cliente no encontrado. Id: " + clienteId);
            }
        }
        public IList<ClienteDTO> getClientes()
        {
            using ( var context = this._baseContext)
            {
                var clientes = from c in context.Clientes
                               join p in context.Personas on c.PersonaId equals p.PersonaId
                               select new ClienteDTO
                               {
                                   ClienteId = c.ClienteId,
                                   Estado = c.Estado,
                                   Nombre = p.Nombre,
                                   Genero = p.Genero,
                                   Edad = p.Edad,
                                   Identificacion = p.Identificacion,
                                   Direccion = p.Direccion,
                                   Telefono = p.Telefono
                               };

                return clientes.ToList();
            }
        }
        public ClienteDTO getClienteById(int clienteId)
        {
            using (var context = this._baseContext)
            {
                var cliente = (from c in context.Clientes
                               join p in context.Personas on c.PersonaId equals p.PersonaId
                               where (c.ClienteId == clienteId)                               
                               select new ClienteDTO
                               {
                                   ClienteId = c.ClienteId,
                                   Estado = c.Estado,
                                   Nombre = p.Nombre,
                                   Genero = p.Genero,
                                   Edad = p.Edad,
                                   Identificacion = p.Identificacion,
                                   Direccion = p.Direccion,
                                   Telefono = p.Telefono
                               }).Take(1)
                               .FirstOrDefault();

                if( cliente == null )
                {
                    throw new NotFoundException("Cliente no encontrado. Id: " + clienteId);
                }
                
                return cliente;
            }
        }
    }
}
