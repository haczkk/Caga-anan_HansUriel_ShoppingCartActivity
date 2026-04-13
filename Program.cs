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
        if (RemainingStock == 0)
        {
            stockInfo = "[OUT OF STOCK]";
        }
        else
        {
            stockInfo = "Stock: " + RemainingStock;
        }

        Console.WriteLine("[" + Id + "] " + Name + "\t PHP" + Price.ToString("F2") + "\t " + stockInfo);
    }

    public double GetItemTotal(int quantity)
    {
        double total = Price * quantity;
        return total;
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
