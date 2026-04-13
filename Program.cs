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
        string stockInfo;
        
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
        if (RemainingStock >= quantity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeductStock(int quantity)
    {
        RemainingStock = RemainingStock - quantity;
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}

class Program
{
    static void Main()
    {
        Product[] menu = new Product[7];
        menu[0] = new Product(1, "Chair", 1_500.00, 5);
        menu[1] = new Product(2, "Table", 3_000.00, 3);
        menu[2] = new Product(3, "Sofa", 10_000.00, 0);
        menu[3] = new Product(4, "Bed", 15_000.00, 1);
        menu[4] = new Product(5, "Bookshelf", 5_000.00, 4);
        menu[5] = new Product(6, "Dresser", 7_500.00, 2);
        menu[6] = new Product(7, "Lamp", 2_000.00, 6);

        int maxCart = 5;
        CartItem[] cart =  new CartItem[maxCart];
        int cartCount = 0;

        string continueShopping = "Y";

        while (continueShopping == "Y")
        {
            Console.Clear();
            Console.WriteLine("======== Shopping Cart System ======");

            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].DisplayProduct();
            }

            Console.WriteLine("====================================");

            if (cartCount == maxCart)
            {
                Console.WriteLine("Cart is full. Proceeding to checkout...");
                break;
            }

            Console.Write("\nEnter product number (or 0 to checkout): ");
            string productInput = Console.ReadLine();

            int productId;
            bool productIsValid = int.TryParse(productInput, out productId);

            if (productIsValid == false)
            {
                Console.WriteLine("Invalid Input. Please enter a number.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            if (productId == 0)
            {
                break;
            }

            if (productId < 1 || productId > menu.Length)
            {
                Console.WriteLine("Invalid Product Number. Please choose from 1 to " + menu.Length + ".");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            Product selectedProduct = menu[productId - 1];

            if (selectedProduct.RemainingStock == 0)
            {
                Console.WriteLine("Sorry, '" + selectedProduct.Name + "' is out of stock.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            Console.Write("Enter quantity for " + selectedProduct.Name + ": ");
            string quantityInput = Console.ReadLine();

            int  quantity;
            bool quantityIsValid = int.TryParse(quantityInput, out quantity);

            if (quantityIsValid == false)
            {	
                Console.WriteLine("Invalid input. Please enter a whole number.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be at least 1.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }
        }
    }
}
