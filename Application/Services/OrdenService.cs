using Application.DataAccess;

using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _repository;
        private readonly ICarritoService _carritoService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;


        public OrdenService(IOrdenRepository repository, ICarritoService carritoService, IClienteRepository clienteRepository, IMapper mapper)
        {
            _repository = repository;
            _carritoService = carritoService;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public Orden CreateOrden(Orden order)
        {
            return _repository.CreateOrden(order);
        }

        public async Task<OrdenResponse> GetOrder(int limit, int page, DateTime? from = null, DateTime? to = null)
        {
            var allOrders = _repository.GetAllOrders();

            var orderCount = allOrders.Count();
            int totalPages = (int)Math.Ceiling((double)orderCount / limit);

            var OrderPage = _repository.GetOrderByPage(limit, page, from, to);

            List<OrdenDto> ordersDto = new List<OrdenDto>();

            foreach (var item in OrderPage)
            {

                List<ProductoDto> productos = new List<ProductoDto>();
                var carrito = _carritoService.GetRawCarritoById(item.CarritoId);

                foreach (var product in carrito.CarritoProductos)
                {
                    productos.Add(_mapper.Map<ProductoDto>(product.Producto));
                }
                var ordenMapeada = _mapper.Map<OrdenDto>(item);
                var cliente = _clienteRepository.GetClienteById(carrito.ClienteId);
                ordenMapeada.Productos = productos;
                ordenMapeada.ClienteNombre = cliente.Result.Nombre;
                ordenMapeada.ClienteId = cliente.Result.ClienteId;
                ordenMapeada.ClienteDni = cliente.Result.Dni;
                ordersDto.Add(ordenMapeada);
            }

            return new OrdenResponse(page, limit, orderCount, totalPages, ordersDto);

        }
    }
}
