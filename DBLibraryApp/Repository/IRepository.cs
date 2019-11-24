using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Repository
{
  public  interface IRepository<T>
    {
        IList<T> Listele();
        T Bul(T nesne);
        T Ekle(T nesne);
        bool Sil(T nesne);
    }
}
