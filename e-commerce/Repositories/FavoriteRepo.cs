using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Interfaces;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{
    public class FavoriteRepo:IFavorite
    {
        private readonly StoreContext db;

        public FavoriteRepo(StoreContext _db)
        {
            db = _db;
        }
        public void Additem(FavItem item)
        {
            db.FavItems.Add(item);
            db.SaveChanges();
        }
        public Favorite GetUserFavorite(string id)
        {
            return db.Favorites.Include(f => f.FavItems).FirstOrDefault(f => f.UserId == id);
        }
        public FavItem IFExist(Favorite _userfav, int _productid)
        {

            var item = _userfav.FavItems.FirstOrDefault(f => f.ProductId == _productid);
            if (item != null)
            {
                return item;
            }
            return null;
        }


        public void DeleteItem(FavItem item)
        {
            db.FavItems.Remove(item);
            db.SaveChanges();
        }

    }
}
