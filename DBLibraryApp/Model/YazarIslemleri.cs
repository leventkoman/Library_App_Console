using DBLibraryApp.DAL;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Model
{
    public class YazarIslemleri : BaseClass, IRepository<Yazarlar>
    {
        public Yazarlar Bul(Yazarlar nesne)
        {
            if (nesne!=null)
            {
                try
                {
                    _tmpYazar= new Yazarlar();
                    _tmpYazar = db.Yazarlars.Find(nesne.Id);
                    
                    return _tmpYazar;
                }
                catch 
                {

                    return null;
                }
            }
            return null;
        }

        public Yazarlar Ekle(Yazarlar nesne)
        {
            throw new NotImplementedException();
        }

        public IList<Yazarlar> Listele()
        {
            return db.Yazarlars.ToList();

        }

        public bool Sil(Yazarlar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    var _silinecekkat = db.Yazarlars.Find(nesne.Id);
                    if (_silinecekkat.Kitaplars.Count == 0)
                    {
                        db.Yazarlars.Remove(_silinecekkat);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch
                {

                    return false;
                }
            }
            return false;
        }
    }
}
