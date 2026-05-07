namespace ShoppingCartSystem
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int RemainingStock { get; set; }
        public string Category { get; private set; }

        public Product(int id, string name, double price, int remainingStock, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = remainingStock;
            Category = category;
        }

        public void DisplayProduct()
        {
            string stockInfo;

            if (RemainingStock == 0)
                stockInfo = "[OUT OF STOCK]";
            else
                stockInfo = "Stock: " + RemainingStock;

            Console.WriteLine(("[" + Id + "]").PadRight(5) + Name.PadRight(15) + ("PHP " + Price.ToString("F2")).PadRight(16) + stockInfo.PadRight(16) + "| " + Category);
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return RemainingStock >= quantity;
        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }

        public void Restock(int quantity)
        {
            RemainingStock += quantity;
        }
    }
}
