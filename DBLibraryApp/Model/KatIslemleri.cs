using DBLibraryApp.DAL;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Model
{
    public class KatIslemleri : BaseClass, IRepository<Katlar>
    {
        public Katlar Bul(Katlar nesne)
        {
            if (nesne != null)
            {
                try
                {
                    _tmpKat = new Katlar();                  
                    _tmpKat = db.Katlars.Find(nesne.Id);

                    return _tmpKat;
                }
                catch
                {

                    return null;
                }
            }
            return null;
        }

        public Katlar Ekle(Katlar nesne)
        {
            if (nesne !=null)
            {
                try
                {
                    _tmpKat = new Katlar();
                    _tmpKat.KatNo = nesne.KatNo;
                    db.Katlars.Add(_tmpKat);
                    db.SaveChanges();
                    return _tmpKat;
                }
                catch 
                {

                    return null;
                }
            }
            return null;
        }

        public  IList<Katlar>Listele()
        {
            return db.Katlars.ToList();
        }

        public bool Sil(Katlar nesne)
        {
            if (nesne!=null)
            {
                try
                {
                    var _silinecekkat = db.Katlars.Find(nesne.Id);
                    if (_silinecekkat.Odalars.Count==0)
                    {
                        db.Katlars.Remove(_silinecekkat);
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
