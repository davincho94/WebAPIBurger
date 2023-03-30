namespace WebAPIBurger.Models
{
    public class Sandwich
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<MenuItem> Extras { get; set; }
    }
}
