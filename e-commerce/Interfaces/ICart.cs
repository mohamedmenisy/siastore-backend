using e_commerce.DTO;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Interfaces
{
    public interface ICart
    {

        public void SetItems(CartItems cartitem);
        public UserCart GetUserCart(string userid);
        public CartItems IFExist(UserCart _usercart, int productid);
        public void UpdateCartItem(CartItems cartitem);
        public void UpdateUserCart(UserCart _usercart);


        public void DeleteFromCart(CartItems item);

        public void UpdateStock(Product _product);
         public void UpdateQuantity(CartItems item);

        public void DeleteCartItems(int cartid);

    }
}
