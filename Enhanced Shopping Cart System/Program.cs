using ShoppingCartSystem;

namespace ShoppingCartSystem
{
    class Program
    {
        const int MAX_CART          = 10;
        const int MAX_ORDERS        = 50;
        const int LOW_STOCK_LEVEL   = 5;
        const double DISCOUNT_MIN   = 5000;
        const double DISCOUNT_RATE  = 0.10;

        static Product[] menu        = new Product[10];
        static CartItem[] cart       = new CartItem[MAX_CART];
        static int cartCount         = 0;

        static Receipt[] orderHistory = new Receipt[MAX_ORDERS];
        static int orderCount         = 0;
        
        static void Main()
        {
            menu[0] = new Product(1,  "Chair",       1_500.00, 10, "Furniture");
            menu[1] = new Product(2,  "Table",       3_000.00, 10, "Furniture");
            menu[2] = new Product(3,  "Sofa",       10_000.00, 10, "Furniture");
            menu[3] = new Product(4,  "Bed",        15_000.00, 10, "Furniture");
            menu[4] = new Product(5,  "Bookshelf",   5_000.00, 10, "Furniture");
            menu[5] = new Product(6,  "Dresser",     7_500.00, 10, "Appliances");
            menu[6] = new Product(7,  "Ref",        25_000.00, 10, "Appliances");
            menu[7] = new Product(8,  "Microwave",   8_000.00, 10, "Appliances");
            menu[8] = new Product(9,  "Aircon",     35_000.00, 10, "Appliances");
            menu[9] = new Product(10, "Wall Clock",    800.00, 10, "Home Decor");
    
            bool running = true;
    
            while (running)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();
    
                if (choice == "1")
                {
                    ShoppingLoop();
                }
                else if (choice == "2")
                {
                    ShowOrderHistory();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\nGoodbye! Thank you for using our system.");
                    running = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid choice. Please enter 1, 2, or 3.");
                    PressAnyKey();
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║     SHOPPING CART SYSTEM  v2.0       ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║   1. Start Shopping                  ║");
            Console.WriteLine("║   2. View Order History              ║");
            Console.WriteLine("║   3. Exit                            ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("\nEnter your choice: ");
        }

        static void ShoppingLoop()
        {
            bool shopping = true;

            while (shopping)
            {
                Console.Clear();
                PrintDivider('=', 62);
                Console.WriteLine(CenterText("PRODUCT CATALOG", 62));
                PrintDivider('=', 62);

                Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
                PrintDivider('-', 62);

                for (int i = 0; i < menu.Length; i++)
                {
                    menu[i].DisplayProduct();
                }

                PrintDivider('=', 62);
                Console.WriteLine(" Cart: " + cartCount + " / " + MAX_CART + " items");
                PrintDivider('-', 62);
                Console.WriteLine("  1. Add Item to Cart");
                Console.WriteLine("  2. Search Product by Name");
                Console.WriteLine("  3. Filter by Category");
                Console.WriteLine("  4. Manage Cart (View / Update / Remove / Checkout)");
                Console.WriteLine("  5. Back to Main Menu");
                PrintDivider('-', 62);
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AddItemToCart();
                }
                else if (choice == "2")
                {
                    SearchProduct();
                }
                else if (choice == "3")
                {
                    FilterByCategory();
                }
                else if (choice == "4")
                {
                    bool checkoutDone = CartMenu();
                    if (checkoutDone)
                        shopping = false;
                }
                else if (choice == "5")
                {
                    shopping = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid choice. Please enter 1 to 5.");
                    PressAnyKey();
                }
            }
        }

        static void AddItemToCart()
        {
            Console.Clear();
            PrintDivider('=', 62);
            Console.WriteLine(CenterText("ADD ITEM TO CART", 62));
            PrintDivider('=', 62);

            Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
            PrintDivider('-', 62);
            for (int i = 0; i < menu.Length; i++)
                menu[i].DisplayProduct();
            PrintDivider('=', 62);

            if (cartCount == MAX_CART)
            {
                Console.Clear();
                Console.WriteLine("\nYour cart is full (" + MAX_CART + " items max).");
                Console.WriteLine("Please remove an item before adding more.");
                PressAnyKey();
                return;
            }

            Console.Write("\nEnter product number to add (or 0 to cancel): ");
            string productInput = Console.ReadLine();

            int productId;
            if (!int.TryParse(productInput, out productId))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a number.");
                PressAnyKey();
                return;
            }

            if (productId == 0) return;

            if (productId < 1 || productId > menu.Length)
            {
                Console.Clear();
                Console.WriteLine("Invalid product number. Choose from 1 to " + menu.Length + ".");
                PressAnyKey();
                return;
            }

            Product selected = menu[productId - 1];

            if (selected.RemainingStock == 0)
            {
                Console.Clear();
                Console.WriteLine("Sorry, '" + selected.Name + "' is out of stock.");
                PressAnyKey();
                return;
            }

            Console.Write("Enter quantity for " + selected.Name + " (available: " + selected.RemainingStock + "): ");
            string quantityInput = Console.ReadLine();

            int quantity;
            if (!int.TryParse(quantityInput, out quantity))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a whole number.");
                PressAnyKey();
                return;
            }

            if (quantity <= 0)
            {
                Console.Clear();
                Console.WriteLine("Quantity must be at least 1.");
                PressAnyKey();
                return;
            }

            int existingIndex = FindInCart(selected.Id);

            int totalWanted = (existingIndex >= 0)
                ? cart[existingIndex].Quantity + quantity
                : quantity;

            if (!selected.HasEnoughStock(totalWanted))
            {
                Console.Clear();
                Console.WriteLine("Not enough stock. Only " + selected.RemainingStock + " available.");
                PressAnyKey();
                return;
            }

            if (existingIndex >= 0)
            {
                cart[existingIndex].Quantity += quantity;
                selected.DeductStock(quantity);

                Console.WriteLine("\nCart updated: " + selected.Name + " quantity is now " + cart[existingIndex].Quantity + ".");
            }
            else
            {
                cart[cartCount] = new CartItem(selected, quantity);
                cartCount++;
                selected.DeductStock(quantity);
 
                Console.WriteLine("\nAdded to cart: " + selected.Name + " x" + quantity + " = PHP " + selected.GetItemTotal(quantity).ToString("F2"));
            }

            PressAnyKey();
        }

        static void SearchProduct()
        {
            Console.Clear();
            PrintDivider('=', 62);
            Console.WriteLine(CenterText("SEARCH PRODUCT", 62));
            PrintDivider('=', 62);

            Console.Write("\nEnter product name to search: ");
            string keyword = Console.ReadLine().ToLower().Trim();

            Console.WriteLine("\n--- Search Results for \"" + keyword + "\" ---");
            Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
            PrintDivider('-', 62);

            bool found = false;

            for (int i = 0; i < menu.Length; i++)
            {
                if (menu[i].Name.ToLower().Contains(keyword))
                {
                    menu[i].DisplayProduct();
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("  No products found matching \"" + keyword + "\".");
 
            PressAnyKey();
        }

        static void FilterByCategory()
        {
            Console.Clear();
            PrintDivider('=', 46);
            Console.WriteLine(CenterText("FILTER BY CATEGORY", 46));
            PrintDivider('=', 46);
            Console.WriteLine("  1. Furniture");
            Console.WriteLine("  2. Appliances");
            Console.WriteLine("  3. Home Decor");
            Console.WriteLine("  4. Show All");
            Console.Write("\nEnter your choice: ");

            string choice = Console.ReadLine();
            string category;

            if      (choice == "1") category = "Furniture";
            else if (choice == "2") category = "Appliances";
            else if (choice == "3") category = "Home Decor";
            else if (choice == "4") category = "All";
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice.");
                PressAnyKey();
                return;
            }

            string header = (category == "All") ? "All Products" : category;
            Console.WriteLine("\n--- " + header + " ---");
            Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
            PrintDivider('-', 62);

            for (int i = 0; i < menu.Length; i++)
            {
                if (category == "All" || menu[i].Category == category)
                    menu[i].DisplayProduct();
            }

            PressAnyKey();
        }

        static bool CartMenu()
        {
            bool inMenu       = true;
            bool didCheckout  = false;

            while (inMenu)
            {
                Console.Clear();
                PrintDivider('=', 46);
                Console.WriteLine(CenterText("CART MANAGEMENT", 46));
                PrintDivider('=', 46);
                Console.WriteLine("  1. View Cart");
                Console.WriteLine("  2. Update Item Quantity");
                Console.WriteLine("  3. Remove Item from Cart");
                Console.WriteLine("  4. Clear Cart");
                Console.WriteLine("  5. Proceed to Checkout");
                Console.WriteLine("  6. Back to Shopping");
                PrintDivider('-', 46);
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ViewCart();
                    PressAnyKey();
                }
                else if (choice == "2")
                {
                    UpdateCartItem();
                }
                else if (choice == "3")
                {
                    RemoveCartItem();
                }
                else if (choice == "4")
                {
                    ClearCart();
                }
                else if (choice == "5")
                {
                    if (cartCount == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\nYour cart is empty. Please add items first.");
                        PressAnyKey();
                    }
                    else
                    {
                        Checkout();
                        didCheckout = true;
                        inMenu      = false;
                    }
                }
                else if (choice == "6")
                {
                    inMenu = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid choice. Please enter 1 to 6.");
                    PressAnyKey();
                }
            }

            return didCheckout;
        }
    }
}
