using DBLibraryApp.DAL;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Model
{
    public class KitapIslemleri : BaseClass, IRepository<Kitaplar>
    {
        public Kitaplar Bul(Kitaplar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    
                    _tmpKitap = new Kitaplar();
                    _tmpKitap = db.Kitaplars.Find(nesne);
                    db.Kitaplars.Find(_tmpKitap.Id);
                    db.SaveChanges();

                    return _tmpKitap;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public Kitaplar Ekle(Kitaplar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    _tmpKitap = new Kitaplar();
                    _tmpKitap.KitapAdi = nesne.KitapAdi;
                    db.Kitaplars.Add(_tmpKitap);
                    db.SaveChanges();
                    return _tmpKitap;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public IList<Kitaplar> Listele()
        {
            return db.Kitaplars.ToList();
        }

        public bool Sil(Kitaplar nesne)
        {

            if (nesne != null)
            {
                try
                {
                    var _silinecekkitap = db.Kitaplars.Find(nesne.Id);
                    
                  
                        db.Kitaplars.Remove(_silinecekkitap);
                        db.SaveChanges();
                        return true;
                  
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
