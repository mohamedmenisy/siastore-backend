using e_commerce.DTO;
using e_commerce.Models;

namespace e_commerce.Interfaces
{
    public interface IFavorite
    {
        public void Additem(FavItem item);
        public Favorite GetUserFavorite(string userid);
        public FavItem IFExist(Favorite _userfavcart, int productid);
        public void DeleteItem(FavItem item);



    }
}
