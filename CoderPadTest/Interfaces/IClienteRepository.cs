using CoderPadTest.DTOs;

namespace CoderPadTest.Interfaces
{
    public interface IClienteRepository
    {
        void insertCliente(ClienteDTO clienteDTO);
        void updateCliente(ClienteDTO clienteDTO);
        void deleteCliente(int clienteId);
        IList<ClienteDTO> getClientes();
        ClienteDTO getClienteById(int clienteId);
    }
}
