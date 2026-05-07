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
                PrintDivider('=', 64);
                Console.WriteLine(CenterText("PRODUCT CATALOG", 64));
                PrintDivider('=', 64);

                Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
                PrintDivider('-', 64);

                for (int i = 0; i < menu.Length; i++)
                {
                    menu[i].DisplayProduct();
                }

                PrintDivider('=', 64);
                Console.WriteLine(" Cart: " + cartCount + " / " + MAX_CART + " items");
                PrintDivider('-', 64);
                Console.WriteLine("  1. Add Item to Cart");
                Console.WriteLine("  2. Search Product by Name");
                Console.WriteLine("  3. Filter by Category");
                Console.WriteLine("  4. Manage Cart (View / Update / Remove / Checkout)");
                Console.WriteLine("  5. Back to Main Menu");
                PrintDivider('-', 64);
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
            PrintDivider('=', 64);
            Console.WriteLine(CenterText("ADD ITEM TO CART", 64));
            PrintDivider('=', 64);

            Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
            PrintDivider('-', 64);
            for (int i = 0; i < menu.Length; i++)
                menu[i].DisplayProduct();
            PrintDivider('=', 64);

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
            PrintDivider('=', 64);
            Console.WriteLine(CenterText("SEARCH PRODUCT", 64));
            PrintDivider('=', 64);

            Console.Write("\nEnter product name to search: ");
            string keyword = Console.ReadLine().ToLower().Trim();

            Console.WriteLine("\n--- Search Results for \"" + keyword + "\" ---");
            Console.WriteLine(" " + "ID".PadRight(5) + "Name".PadRight(15) + "Price".PadRight(16) + "Stock".PadRight(16) + "Category");
            PrintDivider('-', 64);

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
            PrintDivider('-', 64);

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

        static void ViewCart()
        {
            Console.Clear();
            PrintDivider('=', 62);
            Console.WriteLine(CenterText("YOUR CART", 62));
            PrintDivider('=', 62);

            if (cartCount == 0)
            {
                Console.WriteLine("  Your cart is empty.");
                PrintDivider('=', 62);
                return;
            }

            Console.WriteLine("  " + "#".PadRight(4) + "Product".PadRight(15) + "Price".PadRight(14) + "Qty".PadRight(6) + "Subtotal");
            PrintDivider('-', 62);

            double runningTotal = 0;

            for (int i = 0; i < cartCount; i++)
            {
                CartItem item     = cart[i];
                double   subtotal = item.Product.Price * item.Quantity;
                runningTotal     += subtotal;

                Console.WriteLine("  " + (i + 1 + ".").PadRight(4) + item.Product.Name.PadRight(15) + ("PHP " + item.Product.Price.ToString("F2")).PadRight(14) + ("x" + item.Quantity).PadRight(6) + "PHP " + subtotal.ToString("F2"));
            }

            PrintDivider('-', 62);

            double possibleDiscount = (runningTotal >= DISCOUNT_MIN)
                ? runningTotal * DISCOUNT_RATE
                : 0;

            Console.WriteLine("  Cart Total   : PHP " + runningTotal.ToString("F2") + "  (" + cartCount + " item(s))");

            if (possibleDiscount > 0)
            {
                Console.WriteLine("  Discount 10% : PHP " + possibleDiscount.ToString("F2") + "  (applies at checkout)");
                Console.WriteLine("  Est. Final   : PHP " + (runningTotal - possibleDiscount).ToString("F2"));
            }
 
            PrintDivider('=', 62);
        }

        static void UpdateCartItem()
        {
            Console.Clear();
            ViewCart();

            if (cartCount == 0)
            {
                PressAnyKey();
                return;
            }

            Console.Write("Enter cart item number to update (or 0 to cancel): ");
            string input = Console.ReadLine();

            int itemNum;
            if (!int.TryParse(input, out itemNum))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a number.");
                PressAnyKey();
                return;
            }

            if (itemNum == 0) return;

            if (itemNum < 1 || itemNum > cartCount)
            {
                Console.Clear();
                Console.WriteLine("Invalid cart number. Choose from 1 to " + cartCount + ".");
                PressAnyKey();
                return;
            }

            CartItem selected = cart[itemNum - 1];

            Console.WriteLine("\nProduct      : " + selected.Product.Name);
            Console.WriteLine("Current Qty  : " + selected.Quantity);
            Console.WriteLine("Extra Stock  : " + selected.Product.RemainingStock + " more available");
            Console.Write("\nEnter the NEW total quantity (or 0 to cancel): ");

            string qInput = Console.ReadLine();
            int newQty;

            if (!int.TryParse(qInput, out newQty))
            {
                Console.Clear();
                Console.WriteLine("Invalid input.");
                PressAnyKey();
                return;
            }

            if (newQty == 0) return;

            if (newQty < 1)
            {
                Console.Clear();
                Console.WriteLine("Quantity must be at least 1. Use 'Remove' to delete an item.");
                PressAnyKey();
                return;
            }

            int difference = newQty - selected.Quantity;

            if (difference > 0)
            {
                if (!selected.Product.HasEnoughStock(difference))
                {
                    Console.Clear();
                    Console.WriteLine("Not enough stock. Only " +
                                      selected.Product.RemainingStock + " extra available.");
                    PressAnyKey();
                    return;
                }
                selected.Product.DeductStock(difference);
            }
            else if (difference < 0)
            {
                selected.Product.Restock(-difference);
            }

            selected.Quantity = newQty;
 
            Console.WriteLine("\n" + selected.Product.Name + " quantity updated to " + newQty + ".");
            PressAnyKey();
        }

        static void RemoveCartItem()
        {
            Console.Clear();
            ViewCart();

            if (cartCount == 0)
            {
                PressAnyKey();
                return;
            }

            Console.Write("Enter cart item number to remove (or 0 to cancel): ");
            string input = Console.ReadLine();

            int itemNum;
            if (!int.TryParse(input, out itemNum))
            {
                Console.Clear();
                Console.WriteLine("Invalid input.");
                PressAnyKey();
                return;
            }

            if (itemNum == 0) return;

            if (itemNum < 1 || itemNum > cartCount)
            {
                Console.Clear();
                Console.WriteLine("Invalid cart number. Choose from 1 to " + cartCount + ".");
                PressAnyKey();
                return;
            }

            CartItem toRemove = cart[itemNum - 1];
            string   name     = toRemove.Product.Name;

            toRemove.Product.Restock(toRemove.Quantity);

            for (int i = itemNum - 1; i < cartCount - 1; i++)
            {
                cart[i] = cart[i + 1];
            }
            cart[cartCount - 1] = null;
            cartCount--;

            Console.WriteLine("\n'" + name + "' has been removed from your cart.");
            PressAnyKey();
        }

        static void ClearCart()
        {
            Console.Clear();
            PrintDivider('=', 60);
            Console.WriteLine(CenterText("CLEAR CART", 60));
            PrintDivider('=', 60);

            if (cartCount == 0)
            {
                Console.WriteLine("\nYour cart is already empty.");
                PressAnyKey();
                return;
            }

            string confirm = AskYesNo("\nAre you sure you want to clear your entire cart?");

            if (confirm == "Y")
            {
                for (int i = 0; i < cartCount; i++)
                {
                    cart[i].Product.Restock(cart[i].Quantity);
                    cart[i] = null;
                }
                cartCount = 0;
                Console.WriteLine("Cart cleared successfully.");
            }
            else
            {
                Console.WriteLine("Clear cart cancelled.");
            }
 
            PressAnyKey();
        }

        static void Checkout()
        {
            Console.Clear();
            PrintDivider('=', 62);
            Console.WriteLine(CenterText("CHECKOUT SUMMARY", 62));
            PrintDivider('=', 62);

            ViewCart();

            double grandTotal = 0;

            for (int i = 0; i < cartCount; i++)
                grandTotal += cart[i].Product.Price * cart[i].Quantity;

            double discountAmount = 0;
            double finalTotal     = grandTotal;

            if (grandTotal >= DISCOUNT_MIN)
            {
                discountAmount = grandTotal * DISCOUNT_RATE;
                finalTotal     = grandTotal - discountAmount;
 
                Console.WriteLine("  ** 10% discount applied! You save PHP " + discountAmount.ToString("F2") + " **");
            }

            Console.WriteLine("\n  Final Total  : PHP " + finalTotal.ToString("F2"));
            PrintDivider('-', 62);

            double payment = 0;

            while (true)
            {
                Console.Write("  Enter payment amount  : PHP ");
                string payInput = Console.ReadLine().Trim();

                if (!double.TryParse(payInput, out payment))
                {
                    Console.Clear();
                    Console.WriteLine("  Invalid input. Please enter a valid numeric amount.");
                    Console.WriteLine("  Final Total  : PHP " + finalTotal.ToString("F2"));
                    PrintDivider('-', 62);
                    continue;
                }

                if (payment < finalTotal)
                {
                    Console.Clear();
                    Console.WriteLine("  Insufficient payment. Final total is PHP " + finalTotal.ToString("F2") + ". Please enter more.");
                    Console.WriteLine("  Final Total  : PHP " + finalTotal.ToString("F2"));
                    PrintDivider('-', 62);
                    continue;
                }

                break;
            }

            double change = payment - finalTotal;

            Console.WriteLine("  Change       : PHP " + change.ToString("F2"));

            orderCount++;
            string receiptNumber = orderCount.ToString("D4"); // e.g. "0001"
            string dateAndTime   = DateTime.Now.ToString("MMMM dd, yyyy  h:mm tt");

            CartItem[] snapshot = new CartItem[cartCount];
            for (int i = 0; i < cartCount; i++)
                snapshot[i] = cart[i];

            Receipt receipt = new Receipt(
                receiptNumber,
                dateAndTime,
                snapshot,
                cartCount,
                grandTotal,
                discountAmount,
                finalTotal,
                payment,
                change
            );

            orderHistory[orderCount - 1] = receipt;

            for (int i = 0; i < cartCount; i++)
                cart[i] = null;
            cartCount = 0;

            Console.Clear();
            receipt.DisplayReceipt();

            ShowLowStockAlert();

            Console.WriteLine();
            Console.WriteLine("  Press any key to return to the Main Menu...");
            Console.ReadKey();
        }

        static void ShowLowStockAlert()
        {
            bool anyLow = false;

            for (int i = 0; i < menu.Length; i++)
            {
                if (menu[i].RemainingStock > 0 && menu[i].RemainingStock <= LOW_STOCK_LEVEL)
                {
                    if (!anyLow)
                    {
                        Console.WriteLine();
                        Console.WriteLine("  !! LOW STOCK ALERT !!");
                        PrintDivider('-', 46);
                        anyLow = true;
                    }

                    Console.WriteLine("  * " + menu[i].Name + " has only " + menu[i].RemainingStock + " stock(s) left!");
                }
            }

            if (anyLow)
                PrintDivider('-', 46);
        }

        static void ShowOrderHistory()
        {
            Console.Clear();
            PrintDivider('=', 78);
            Console.WriteLine(CenterText("ORDER HISTORY", 78));
            PrintDivider('=', 78);

            if (orderCount == 0)
            {
                Console.WriteLine("  No orders have been placed yet.");
                PrintDivider('=', 78);
                PressAnyKey();
                return;
            }

            for (int i = 0; i < orderCount; i++)
            {
                Console.Write("  " + (i + 1) + ". ");
                orderHistory[i].DisplaySummary();
            }

            PrintDivider('-', 78);
            Console.Write("\nEnter order number to view full receipt (or 0 to go back): ");
            string input = Console.ReadLine();

            int choice;
            if (int.TryParse(input, out choice) && choice >= 1 && choice <= orderCount)
            {
                Console.Clear();
                orderHistory[choice - 1].DisplayReceipt();
            }

            PressAnyKey();
        }

        static int FindInCart(int productId)
        {
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == productId)
                    return i;
            }
            return -1;
        }

        static string AskYesNo(string question)
        {
            while (true)
            {
                Console.Write(question + " (Y/N): ");
                string answer = Console.ReadLine().ToUpper().Trim();
 
                if (answer == "Y" || answer == "N")
                    return answer;
 
                Console.Clear();
                Console.WriteLine("  Invalid input. Please enter Y or N only.");
            }
        }

        static void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void PrintDivider(char ch, int length)
        {
            Console.WriteLine(new string(ch, length));
        }

        static string CenterText(string text, int width)
        {
            int padding = (width - text.Length) / 2;
            return new string(' ', padding) + text;
        }
    }
}
