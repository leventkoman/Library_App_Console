

using DBLibraryApp.DAL;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Model
{
    public class RafIslemleri : BaseClass, IRepository<Raflar>
    {
        public Raflar Bul(Raflar nesne)
        {
            if (nesne != null)
            {
                try
                {
                   
                    _tmpRaf = new Raflar();
                    _tmpRaf = db.Raflars.Find(nesne.Id);
                   

                    return _tmpRaf;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public Raflar Ekle(Raflar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    _tmpRaf = new Raflar();
                    _tmpRaf.RafNo = nesne.RafNo;
                    db.Raflars.Add(_tmpRaf);
                    db.SaveChanges();
                    return _tmpRaf;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public IList<Raflar> Listele()
        {
            return db.Raflars.ToList();
        }

        public bool Sil(Raflar nesne)
        {

            if (nesne != null)
            {
                try
                {
                    var _silinecekkat = db.Raflars.Find(nesne.Id);
                    if (_silinecekkat.Kitaplars.Count == 0)
                    {
                        db.Raflars.Remove(_silinecekkat);
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

