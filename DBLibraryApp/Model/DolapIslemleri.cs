using DBLibraryApp.DAL;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Model
{
    public class DolapIslemleri : BaseClass, IRepository<Dolaplar>
    {
        public Dolaplar Bul(Dolaplar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    
                    _tmpDolap = new Dolaplar();
                    _tmpDolap = db.Dolaplars.Find(nesne.Id);
                    

                    return _tmpDolap;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public Dolaplar Ekle(Dolaplar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    _tmpDolap = new Dolaplar();
                    _tmpDolap.DolapNo = nesne.DolapNo;
                    db.Dolaplars.Add(_tmpDolap);
                    db.SaveChanges();
                    return _tmpDolap;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public IList<Dolaplar> Listele()
        {
            return db.Dolaplars.ToList();
        }

        public bool Sil(Dolaplar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    var _silinecekdolap = db.Dolaplars.Find(nesne.Id);
                    if (_silinecekdolap.Raflars.Count == 0)
                    {
                        db.Dolaplars.Remove(_silinecekdolap);
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
