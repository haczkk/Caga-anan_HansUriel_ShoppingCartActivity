using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public Product(int id, string name, double price, int remainingStock)
    {
        Id = id;
        Name = name;
        Price = price;
        RemainingStock = remainingStock;
    }

    public void DisplayProduct()
    {
        string stockLabel = RemainingStock == 0 ? "[OUT OF STOCK]" : $"Stock: {RemainingStock}";
        Console.WriteLine($"  [{Id}] {Name,-22} PHP {Price,8:F2}   {stockLabel}");
    }

    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return quantity <= Stock;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}
