namespace WebAPIBurger.Models
{
    public class Order
    {
        public int Id { get; set; }
        public MenuItem Sandwich { get; set; }
        public MenuItem Fries { get; set; }
        public MenuItem SoftDrink { get; set; }
        public decimal TotalPrice { get; set; }
        public bool HasDiscount { get; set; }
    }
}
