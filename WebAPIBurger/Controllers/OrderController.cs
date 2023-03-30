using Microsoft.AspNetCore.Mvc;
using WebAPIBurger.Models;

namespace WebAPIBurger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> orders;

        public OrderController()
        {
            // Inicializar lista de pedidos
            orders = new List<Order>();
        }

        // Endpoint para enviar una orden
        [HttpPost]
        public ActionResult<decimal> PlaceOrder(Order order)
        {
            // Validar que la orden sea válida
            if (!IsValidOrder(order, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            // Calcular el precio total de la orden
            decimal totalPrice = CalculateOrderTotalPrice(order);

            // Agregar la orden a la lista de pedidos
            orders.Add(order);

            // Devolver el precio total de la orden al cliente
            return totalPrice;
        }

        // Método para validar si una orden es válida
        private bool IsValidOrder(Order order, out string errorMessage)
        {
            // Validar que la orden tenga un sándwich y como máximo un extra
            if (order.Sandwich == null || order.Sandwich.Extras.Count > 1)
            {
                errorMessage = "La orden debe tener un sándwich y como máximo un extra.";
                return false;
            }

            // Validar que no se repitan los ítems
            if (HasDuplicateItems(order))
            {
                errorMessage = "La orden no puede tener dos ítems iguales.";
                return false;
            }

            // La orden es válida
            errorMessage = null;
            return true;
        }

        // Método para calcular el precio total de una orden
        private decimal CalculateOrderTotalPrice(Order order)
        {
            decimal sandwichPrice = order.Sandwich.Price;
            decimal extrasPrice = order.Sandwich.Extras.FirstOrDefault()?.Price ?? 0;
            decimal friesPrice = order.Fries != null ? order.Fries.Price : 0;
            decimal softDrinkPrice = order.SoftDrink != null ? order.SoftDrink.Price : 0;

            decimal totalPrice = sandwichPrice + extrasPrice + friesPrice + softDrinkPrice;

            // Aplicar descuentos si corresponde
            if (order.Sandwich != null && friesPrice > 0 && softDrinkPrice > 0)
            {
                totalPrice *= 0.8m; // 20% de descuento
            }
            else if (order.Sandwich != null && softDrinkPrice > 0)
            {
                totalPrice *= 0.85m; // 15% de descuento
            }
            else if (order.Sandwich != null && friesPrice > 0)
            {
                totalPrice *= 0.9m; // 10% de descuento
            }

            return totalPrice;
        }

        // Método para verificar si una orden tiene ítems duplicados
        private bool HasDuplicateItems(Order order)
        {
            if (order.Sandwich != null)
            {
                if (order.Sandwich.Extras.Contains(order.Fries) ||
                    order.Sandwich.Extras.Contains(order.SoftDrink))
                {
                    return true;
                }
            }

            if (order.Fries != null && order.Fries.Equals(order.SoftDrink))
            {
                return true;
            }

            return false;
        }
    }
}
