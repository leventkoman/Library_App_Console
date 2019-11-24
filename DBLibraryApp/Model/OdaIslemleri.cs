using DBLibraryApp.DAL;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Model
{
    public  class OdaIslemleri : BaseClass, IRepository<Odalar>
    {
        public Odalar Bul(Odalar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    _tmpOda = new Odalar();
                    _tmpOda = db.Odalars.Find(nesne.Id);
                    return _tmpOda;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public Odalar Ekle(Odalar nesne)
        {
            if (nesne!=null)
            {
                try
                {
                    _tmpOda = new Odalar();
                    _tmpOda.KatId = nesne.KatId;
                    _tmpOda.OdaAdi = nesne.OdaAdi;
                    db.Odalars.Add(_tmpOda);
                    db.SaveChanges();
                    return _tmpOda;
                }
                catch
                {

                    return null;
                }
            }

            return null;
        }

        public IList<Odalar> Listele()
        {
            return db.Odalars.ToList();
        }

        public bool Sil(Odalar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    var _silinecekoda = db.Odalars.Find(nesne.Id);
                    if (_silinecekoda.Dolaplars.Count == 0)
                    {
                        db.Odalars.Remove(_silinecekoda);
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

        public static void deneme()
        {
            string a = "K1O1";
            string[] test = a.Split(' ');
        }
    }
}
