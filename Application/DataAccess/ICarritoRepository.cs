using Domain.Entities;

namespace Application.DataAccess
{
    /// <summary>
    /// Interface for CarritoRepository.
    /// </summary>
    public interface ICarritoRepository
    {
        /// <summary>
        /// Method to obtain a carrito given a valid id. If no carrito with a given id is found, the method may return null.
        /// </summary>
        /// <param name="carritoId">The id of the carrito.</param>
        /// <returns>An instance of carrito with the provided id.</returns>
        public Task<Carrito?> GetCarritoById(Guid carritoId);

        /// <summary>
        /// Method to obtaing RawCarritoById.
        /// </summary>
        /// <param name="carritoId">The guid of the carrito.</param>
        /// <returns>An instance of carrito with the provided id.</returns>
        public Task<Carrito?> GetRawCarritoById(Guid carritoId);

        /// <summary>
        /// Method to obtain a carrito given a valid client Id.
        /// </summary>
        /// <param name="clientId">The clientId</param>
        /// <returns>An instance of carrito of an specific client.</returns>
        public Task<Carrito?> GetCarritoByClientId(int clientId);

        /// <summary>
        /// Method to update an existing carrito.
        /// </summary>
        /// <param name="Carrito">The instace of carrito to be updated.</param>
        /// <returns>The updated carrito.</returns>
        public Task<Carrito?> UpdateCarrito(Carrito Carrito);

        /// <summary>
        /// Deletes an existing instance of carrito.
        /// </summary>
        /// <param name="CarritoId">The id of the carrito to be deleted.</param>
        /// <returns>The deleted carrito.</returns>
        public Task<Carrito?> DeleteCarritoById(Guid CarritoId);

        /// <summary>
        /// Updates a CarritoProducto.
        /// </summary>
        /// <param name="carrito">An instance of CarritoProducto.</param>
        /// <returns>The updated instance of CarritoProducto.</returns>
        public Task<CarritoProducto?> UpdateCarritoProducto(CarritoProducto carrito);

        /// <summary>
        /// Creates a new entry of a carrity in the database.
        /// </summary>
        /// <param name="carrito">The carrito to be created.</param>
        /// <returns>An instance with the data of the carrito created.</returns>
        public Task<Carrito> CreateCarrito(Carrito carrito);

        /// <summary>
        /// Deletes an existing CarritoProducto.
        /// </summary>
        /// <param name="carritoProducto">The CarritoProducto to be deleted.</param>
        /// <returns>The deleted CarritoProducto</returns>
        public Task<CarritoProducto?> DeleteCarritoProducto(CarritoProducto carritoProducto);

        /// <summary>
        /// An instance of CarritoProducto given a valid carritoId and a producotId.
        /// </summary>
        /// <param name="carritoId">The id of the carrito.</param>
        /// <param name="productoId">The id of the producto.</param>
        /// <returns>The corresponding instance of the carrito.</returns>
        public Task<CarritoProducto?> GetCarritoProductoById(Guid carritoId, int productoId);




    }
}
