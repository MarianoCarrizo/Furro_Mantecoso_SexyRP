using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class VentaDtoForDisplay
    {
        public ClienteDto Cliente { get; set; } = null!;
        public List<ProductoDto> Productos { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
