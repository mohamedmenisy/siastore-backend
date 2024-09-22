using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Interfaces;
using e_commerce.Migrations;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{

    public class CartRepo : ICart
    {
        private readonly StoreContext db;

        public CartRepo(StoreContext _db)
        {
            db = _db;
        }

        public void SetItems(CartItems cartitem)
        {
            db.CartItems.Add(cartitem);
            db.SaveChanges();

        }
        public UserCart GetUserCart(string _userid)
        {
            return db.UserCarts.Include(usercart => usercart.CartItems).FirstOrDefault(cart => cart.UserId == _userid);
        }

        public CartItems IFExist(UserCart _usercart, int _productid)
        {

            var cartitem = _usercart.CartItems.FirstOrDefault(c => c.ProductId == _productid);
            if (cartitem != null)
            {
                return cartitem;
            }
            return null;
        }
        public void UpdateCartItem(CartItems cartitem)
        { 
            

            db.CartItems.Update(cartitem);
            db.SaveChanges();


        }
        public void UpdateUserCart(UserCart _userCart)

        {
            _userCart.TotalQuantity = _userCart.CartItems.Sum(ci => ci.Quantity);
            _userCart.TotalPrice = _userCart.CartItems.Sum(ci => ci.TotalPrice);
            db.UserCarts.Update(_userCart);
            db.SaveChanges();


        }
        public void DeleteFromCart(CartItems item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                UpdateCartItem(item);
                
            }
            else
            {
                db.CartItems.Remove(item);
                db.SaveChanges();
            }

        }
        public void UpdateQuantity(CartItems item)
        {

            UpdateCartItem(item);

        }


        public void UpdateStock(Product _product)
        {
            
            db.Products.Update(_product);
            db.SaveChanges();
        }
        public void DeleteCartItems(int cartid)
        {
            var cartitems = db.CartItems.Where(c => c.CartId == cartid).ToList();
            for (int i = 0; i < cartitems.Count; i++)
            {
                db.CartItems.Remove(cartitems[i]);
                db.SaveChanges();
            }
        }


    }
}
