using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.BussinessLayer
{
    public static class SelvingOperations
    {
        private static DBLibraryEntities db = new DBLibraryEntities();
        private static RafIslemleri _ri = new RafIslemleri();
        private static Raflar _raf;
        private static int? _tmpraf;
        public static void SelvingList()
        {
            Console.WriteLine("Raf eklemek için 1'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Raf silmek için 2'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Rafta ki odaları listelemek  için 3'e");
            Console.WriteLine("--------------------");

            string _tus = Console.ReadLine();
            if (_tus == "1" || _tus == "2" || _tus == "3")
            {
                int tus = Convert.ToInt16(_tus);
                if (tus==1)
                {
                    SelvingAdd();
                }
                else if (tus==2)
                {
                    SelvingRemove();
                }
                else if (tus==3)
                {
                    if (_ri.Listele().Count==0)
                    {
                        Console.WriteLine("Sisteme kayıtlı raf bulunamadı.");
                    }
                    else
                    {
                        var liste = _ri.Listele();
                        foreach (var item in liste)
                        {
                            Console.WriteLine("Raf numarası : "+ item.RafNo);
                            if (item.Kitaplars.Count!=0)
                            {
                                var raflar = (from rafkitap in db.Kitaplars where rafkitap.RafId == item.Id select rafkitap).ToList();
                                foreach (var _raf in raflar)
                                {
                                    Console.WriteLine(item.RafNo + " NUMARALI RAFA AIT  " + _raf.KitapAdi);
                                }
                            }
                            else if (item.Kitaplars.Count == 0)
                            {
                                Console.WriteLine(item.RafNo + " RAFINA EKLENMİŞ BİR KİTAP BULUNMAMAKTADIR");
                            }
                        }
                    }
                }
            }
        }
        public static void SelvingAdd()
        {

            Console.WriteLine("Rafın ismini giriniz");
            string _rafNumara = Console.ReadLine();
            if (isNumeric(_rafNumara))
            {


                int? _rafNO = Convert.ToInt16(_rafNumara);


                if (CompareSelving(_rafNO) == false)
                {
                    _raf = new Raflar();
                    _raf.RafNo = _rafNO;
                    _ri.Ekle(_raf);
                }
                else
                {
                    Console.WriteLine("Bu raf daha önceden sisteme eklendi : " + CompareSelving(_rafNO) + " numaralı raf zaten var");
                }


            }
        }
        public static void SelvingRemove()
        {
            Raflar _silinecek;
            foreach (var listele in db.Raflars.ToList())
            {
                Console.WriteLine("Raf ID si --> " + listele.Id);

            }

            Console.WriteLine("Silmek istediğiniz Rafın ID sini giriniz");
            var tus = Console.ReadLine();
            if (isNumeric(tus))
            {

                int _tus = Convert.ToInt16(tus);
                _silinecek = db.Raflars.Find(_tus);
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
        public static bool CompareSelving(int? _selvingNo)
        {
            var _allSelvings = (from x in db.Raflars select x.RafNo).ToList();
            foreach (var item in _allSelvings)
            {

                if (item == _selvingNo)
                {
                    return true;
                }

            }

            return false;
        }
        public static bool isNumeric(string sayi)
        {
            int output;
            return int.TryParse(sayi, out output);
        }
        public static string isEmpty(int? SelvingId)
        {
            if (SelvingId != null)
            {
                var _silinecek = db.Raflars.Find(SelvingId);
                if (_silinecek != null && _silinecek.Kitaplars.Count == 0)
                {
                    db.Raflars.Remove(_silinecek);
                    db.SaveChanges();
                    return "Silme işlemi başarılı !!";
                }
                else if (_silinecek == null)
                {
                    return "Böyle bir raf sistemde bulunamadı";
                }
                else if (_silinecek.Kitaplars.Count != 0)
                {

                    foreach (var _raftakikitap in _silinecek.Kitaplars.ToList())
                    {
                        Console.WriteLine(_silinecek.RafNo + " raftaki kitap -> " + _raftakikitap.KitapAdi);
                    }
                    return "Bu rafta kitap bulunamamatadır, bu yüzden silemezsiniz !!!";

                }

            }
            return "Geçerli bir tuş basınız";
        }
    }
}
