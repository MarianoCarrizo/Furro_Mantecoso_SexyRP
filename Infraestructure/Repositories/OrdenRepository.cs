﻿using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistance;

namespace Infraestructure.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly AppDbContext _context;

        public OrdenRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Orden CreateOrden(Orden orden)
        {
            _context.Ordenes.Add(orden);
            _context.SaveChanges();

            return orden;
        }




        public List<Orden> GetOrderByPage(int limit, int page, DateTime? from = null, DateTime? to = null)
        {
            int skip = limit;

            if (from == null && to == null)
            {
                return _context.Ordenes
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .OrderBy(o => o.Fecha.Date)
                    .ToList();
            }
            return _context.Ordenes
                .Where(o => o.Fecha.Date >= from && o.Fecha.Date <= to)
                .Skip((page - 1) * limit)
                .Take(limit)
                .OrderBy(o => o.Fecha.Date)
                .ToList();
        }

        public List<Orden> GetAllOrders(DateTime? from = null, DateTime? to = null)
        {
            if (from == null && to == null)
            {
                return _context.Ordenes
                    .OrderBy(o => o.Fecha.Date)
                    .ToList();
            }
            return _context.Ordenes
                .Where(o => o.Fecha.Date >= from && o.Fecha.Date <= to)
                .OrderBy(o => o.Fecha.Date)
                .ToList();
        }
    }
}
