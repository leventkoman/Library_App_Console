using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibraryApp.Repository;

namespace DBLibraryApp.BussinessLayer
{
   public static class AuthorOperations
    {
        private static DBLibraryEntities db = new DBLibraryEntities();
        private static YazarIslemleri _yi = new YazarIslemleri();
        private static Yazarlar _tmpYazar;

        public static void AuthorList()
        {           
            Console.WriteLine("Yazar bilgilerini listelemek için 1 e basınız.");
            Console.WriteLine("--------------------");
            string _tus = Console.ReadLine();
            int tus = Convert.ToInt16(_tus); 

            if (_yi.Listele().Count == 0)
            {
                Console.WriteLine("Sisteme kayıtlı yazar bulunamadı.");
            }

            else
            {
                var liste = _yi.Listele();
                foreach (var item in liste)
                {
                    Console.WriteLine("--> : " + item.Yazaradi +" " + item.Yazarsoyadi+" yazarına ait kitalar");
                    var yazarlar = (from _authors in db.Kitaplars where _authors.YazarId == item.Id select _authors).ToList();
                    foreach (var _yazar in yazarlar)
                    {
                        Console.WriteLine(_yazar.KitapAdi+" --> kitabı "+_yazar.Raflar.Dolaplar.DolapNo+"--> numaralı dolabın "+_yazar.Raflar.RafNo+" sunda bulunmaktadır.");
                    }
                }

            }
        }

    }
}
