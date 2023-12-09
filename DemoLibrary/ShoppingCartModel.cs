using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class ShoppingCartModel
    {
        // novo

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
    }
}
