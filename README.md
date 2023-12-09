Following Tim Corey's video.

https://www.youtube.com/watch?v=R8Blt5c-Vi4

## Overivew (console)
> Program.cs
```C#
        static ShoppingCartModel cart = new ShoppingCartModel();

        static void Main(string[] args)
        {
            PopulateCartWithDemoData();

            //Console.WriteLine($"The total for the cart is {cart.GenerateTotal():C2}");
            //Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert):C2}");
            //Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount):C2}");
            Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount, AlertUser):C2}");
            Console.WriteLine();

            // using lambda, alternativni nacin pisanja BEZ koristenja one tri metode SubTotalAlert, CalculateLeveledDiscount i AlertUser
            decimal total = cart.GenerateTotal(
                (subTotal) => Console.WriteLine($"The subtotal for cart 2 is {subTotal:C2}"),
                (products, subTotal) =>
                {
                    if (subTotal > 100)
                    {
                        return subTotal * 0.80M;
                    }
                    else if (subTotal > 50)
                    {
                        return subTotal * 0.85M;
                    }
                    else if (subTotal > 10)
                    {
                        return subTotal * 0.90M;
                    }
                    else
                    {
                        return subTotal;
                    }
                },
                (message) => Console.WriteLine($"Cart 2 Alert: { message}")
                );
            Console.WriteLine($"The total for cart 2 is {total:C2}");
            //

            Console.WriteLine();
            Console.Write("Please press any key to exit the application...");
            Console.ReadKey();
        }

        //novo
        private static void SubTotalAlert(decimal subTotal) // ovo je onaj MentionDiscount mentionDiscount
                                                            // i kad je pozvan mentionDiscount, zna da treba callat ovu metodu
                                                            // jer cart.GenerateTotal(SubTotalAlert)
        {
            Console.WriteLine($"The subtotal is {subTotal:C2}");
        }

        // nakon dodavanja Func parametra calculateDiscountedTotal

        private static decimal CalculateLeveledDiscount(List<ProductModel> items, decimal subTotal)
        {
            if (subTotal > 100)
            {
                return subTotal * 0.80M;
            }
            else if (subTotal > 50)
            {
                return subTotal * 0.85M;
            }
            else if (subTotal > 10)
            {
                return subTotal * 0.90M;
            }
            else
            {
                return subTotal;
            }
        }

        // nakon dodavanja Action delegate parametra

        private static void AlertUser(string message)
        {
            Console.WriteLine(message);
        }

        // ----------------

        private static void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }
```

> ShoppingCartModel.cs
```C#
        // declaring the delegate
        public delegate void MentionDiscount(decimal subTotal); // DELEGATE ga oboja zeleno, ali je metoda

        // Program.cs ima onu SubTotalAlert(decimal subTotal) koju zelimo passat 
        // kao parametar za GenerateTotal metodu, i za nju pravimo ovdje delegata

        //------------

        public List<ProductModel> Items { get; set; } = new List<ProductModel>();

        //public decimal GenerateTotal()
        //public decimal GenerateTotal(MentionDiscount mentionDiscount)
        public decimal GenerateTotal(
            MentionDiscount mentionSubtotal,
            Func<List<ProductModel>, decimal, decimal> calculateDiscountedTotal, // func<in,in,out> naziv
            Action<string> tellUserWeAreDiscounting // passamo string a ne returna nista, void
            )
        {
            decimal subTotal = Items.Sum(x => x.Price);

            //
            mentionSubtotal(subTotal); // poziva metodu
            //
            tellUserWeAreDiscounting("We are applying your discount.");
            //

            //if (subTotal > 100)
            //{
            //    return subTotal * 0.80M;
            //}
            //else if (subTotal > 50)
            //{
            //    return subTotal * 0.85M;
            //}
            //else if (subTotal > 10)
            //{
            //    return subTotal * 0.90M;
            //}
            //else
            //{
            //    return subTotal;
            //}

            return calculateDiscountedTotal(Items, subTotal);
        }
```
