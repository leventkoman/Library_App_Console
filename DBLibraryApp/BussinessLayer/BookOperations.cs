using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.BussinessLayer
{
    public static class BookOperations
    {
        private static DBLibraryEntities db = new DBLibraryEntities();
        private static KitapIslemleri _kitapislemi = new KitapIslemleri();
        private static Kitaplar _kitap;
        public static void BookList()
        {
            Console.WriteLine("Kitap eklemek için 1'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Kitap silmek için 2'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Kitapları ve Kitapların yazarlarını listelemek  için 3'e");
            Console.WriteLine("--------------------");
            string _tus = Console.ReadLine();
            if (_tus == "1" || _tus == "2" || _tus == "3")
            {
                int tus = Convert.ToInt16(_tus);
                if (tus == 1)
                {
                    BookAdd();
                }
                else if (tus == 2)
                {
                    BookRemove();
                }
                else if (tus == 3)
                {
                    if (_kitapislemi.Listele().Count == 0)
                    {
                        Console.WriteLine("Sistemde kitap bulunmamaktadır.");
                    }
                    else
                    {
                        var liste = _kitapislemi.Listele();
                        foreach (var item in liste)
                        {
                            Console.WriteLine("Kitabın İsmi : " + item.KitapAdi);
                            var yazarlar = (from kitapyazar in db.Yazarlars where kitapyazar.Id == item.Id select kitapyazar).ToList();
                            foreach (var _yazar in yazarlar)
                            {
                                Console.WriteLine(item.KitapAdi + " KİTABINA AİT YAZARIN ADI :  " + _yazar.Yazaradi + " SOYADI : " + _yazar.Yazarsoyadi);
                            }
                        }

                    }
                }
            }
        }
        public static void BookAdd()
        {
            //Console.WriteLine("Kitabın ismini giriniz");
            //string _kitapismi = Console.ReadLine();
            //if (CompareBook(_kitapismi) == false)
            //{
            //    _kitap = new Kitaplar();
            //    _kitap.KitapAdi = _kitapismi;
            //    _kitapislemi.Ekle(_kitap);
            //    Console.WriteLine("Kitap ekleme işlemi başarılı.");


            //}
            //else if (CompareBook(_kitapismi))
            //{
            //    Console.WriteLine("Bu kitap ismi zaten var");
            //}


             _kitap = new Kitaplar();
            Console.WriteLine("Kitabın adını giriniz.");
            _kitap.KitapAdi = Console.ReadLine();
            //Console.WriteLine("Yazarının ID sini aşağıdan seçerek giriniz.");
            //YazarIslemleri _yi = new YazarIslemleri();
            //var yazarlar = _yi.Listele();
            //foreach (var item in yazarlar)
            //{
            //    Console.WriteLine("ID : " + item.Id.ToString() + " Yazar adı : " + item.Yazaradi + " " + item.Yazarsoyadi);
            //    Console.WriteLine("*******************************");
            //}
            //var YazarId = Console.ReadLine();
            //int _yazarId = Convert.ToInt16(YazarId);
            //_kitap.YazarId = _yazarId;
            //_kitap.inStock = true;
            Console.WriteLine("ISBN Girin");
            _kitap.ISBN = Console.ReadLine();
           


            Console.WriteLine(_kitapislemi.Ekle(_kitap));

        }
        public static void BookRemove()
        {
            Kitaplar _silinecek;
            foreach (var listele in db.Kitaplars.ToList())
            {
                Console.WriteLine("Kitap ID si --> " + listele.Id);

            }

            Console.WriteLine("Silmek istediğiniz Kitabın ID sini giriniz");
            var tus = Console.ReadLine();
            if (isNumeric(tus))
            {

                int _tus = Convert.ToInt16(tus);
                _silinecek = db.Kitaplars.Find(_tus);
                if (_silinecek != null)
                {
                    Console.WriteLine(isEmpty(_silinecek.Id));
                }
                else
                {
                    Console.WriteLine("Bu ID yok");
                }

            }
        }
        public static bool isNumeric(string sayi)
        {
            int output;
            return int.TryParse(sayi, out output);
        }
        public static string isEmpty(int? bookId)
        {
            //var _silinecek = db.Kitaplars.Find(bookId);
            if (bookId != null)
            {
                var _silinecek = db.Kitaplars.Find(bookId);


                db.Kitaplars.Remove(_silinecek);
                db.SaveChanges();
                return "Silme işlemi başarılı !!";
            }



            return "Geçerli bir tuş basınız";
        }
        public static bool CompareBook(string _bookName)
        {
            var _allbooks = (from x in db.Kitaplars select x.KitapAdi).ToList();
            foreach (var item in _allbooks)
            {

                if (item == _bookName)
                {
                    return true;
                }

            }

            return false;
        }

    }

}

