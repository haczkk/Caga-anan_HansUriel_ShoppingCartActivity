namespace ShoppingCartSystem
{
    public class Receipt
    {
        public string ReceiptNumber { get; private set; }
        public string DateAndTime { get; private set; }
        public CartItem[] Items { get; private set; }
        public int ItemCount { get; private set; }
        public double GrandTotal { get; private set; }
        public double DiscountAmount { get; private set; }
        public double FinalTotal { get; private set; }
        public double Payment { get; private set; }
        public double Change { get; private set; }

        public Receipt(
            string receiptNumber,
            string dateAndTime,
            CartItem[] items,
            int itemCount,
            double grandTotal,
            double discountAmount,
            double finalTotal,
            double payment,
            double change)
        {
            ReceiptNumber = receiptNumber;
            DateAndTime = dateAndTime;
            Items = items;
            ItemCount = itemCount;
            GrandTotal = grandTotal;
            DiscountAmount = discountAmount;
            FinalTotal = finalTotal;
            Payment = payment;
            Change = change;
        }

        public void DisplayReceipt()
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("              SHOPPING CART SYSTEM v2.0                ");
            Console.WriteLine("========================================================");
            Console.WriteLine("  Receipt No  : #" + ReceiptNumber);
            Console.WriteLine("  Date / Time : " + DateAndTime);
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("  ITEMS PURCHASED:");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("  " + "Product".PadRight(15) + "Price".PadRight(14) + "Qty".PadRight(6) + "Subtotal");
            Console.WriteLine("  " + new string('-', 50));

            for (int i = 0; i < ItemCount; i++)
            {
                CartItem item = Items[i];
                double subtotal = item.Product.Price * item.Quantity;

                Console.WriteLine("  " + item.Product.Name.PadRight(15) + ("PHP " + item.Product.Price.ToString("F2")).PadRight(14) + ("x" + item.Quantity).PadRight(6) + "PHP " + subtotal.ToString("F2"));
            }

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("  Grand Total      :  PHP " + GrandTotal.ToString("F2"));

            if (DiscountAmount > 0)
            {
                Console.WriteLine("  Discount (10%)   :  PHP " + DiscountAmount.ToString("F2"));
            }

            Console.WriteLine("  Final Total      :  PHP " + FinalTotal.ToString("F2"));
            Console.WriteLine("  Payment          :  PHP " + Payment.ToString("F2"));
            Console.WriteLine("  Change           :  PHP " + Change.ToString("F2"));
            Console.WriteLine("========================================================");
            Console.WriteLine("           Thank you for shopping with us!              ");
            Console.WriteLine("========================================================");
        }

        public void DisplaySummary()
        {
            Console.WriteLine(
                "  Receipt #" + ReceiptNumber +
                "  |  " + DateAndTime +
                "  |  Final Total: PHP " + FinalTotal.ToString("F2")
            );
        }
    }
}
