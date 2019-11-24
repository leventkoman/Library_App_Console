using DBLibraryApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.Repository
{
   public class BaseClass
    {
        public DBLibraryEntities db = new DBLibraryEntities();
        public Kitaplar _tmpKitap;
        public Yazarlar _tmpYazar;
        public Raflar _tmpRaf;
        public Dolaplar _tmpDolap;
        public Katlar _tmpKat;
        public Odalar _tmpOda;
        
    }
}
