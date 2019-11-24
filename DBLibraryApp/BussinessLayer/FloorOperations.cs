using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using DBLibraryApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.BussinessLayer
{
    public static class FloorOperations
    {
        private static DBLibraryEntities db = new DBLibraryEntities();
        private static KatIslemleri _ki = new KatIslemleri();
        private static Katlar _kat;
        private static int? _tmpKat;

        public static void FloorOperationList()
        {
            Console.WriteLine("Kat eklemek için 1'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Kat silmek için 2'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Katta ki odaları listelemek  için 3'e");
            Console.WriteLine("--------------------");
            string _tus = Console.ReadLine();

            if (_tus == "1" || _tus == "2" || _tus == "3")
            {
                int tus = Convert.ToInt16(_tus);

                if (tus == 1)
                {

                    FloorAdd();

                }
                else if (tus == 2)
                {
                    FloorRemove();
                }
                else if (tus == 3)
                {

                    if (_ki.Listele().Count == 0)
                    {
                        Console.WriteLine("Sistemde kat tanımlı değil");
                    }
                    else
                    {
                        var liste = _ki.Listele();
                        foreach (var item in liste)
                        {
                            Console.WriteLine("Kat numarası " + item.KatNo);
                            if (item.Odalars.Count != 0)
                            {
                                var odalar = (from katodalar in db.Odalars where katodalar.KatId == item.Id select katodalar).ToList();
                                foreach (var _oda in odalar)
                                {
                                    Console.WriteLine(item.KatNo + " NUMARALI KATA AIT  " + _oda.OdaAdi);
                                }
                            }
                            else if (item.Odalars.Count == 0)
                            {
                                Console.WriteLine(item.KatNo + " KATINA EKLENMİŞ BİR ODA BULUNMAMAKTADIR");
                            }
                        }
                    }
                }
                else if (tus != 1 || tus != 2 || tus != 3)
                {
                    Console.WriteLine("Geçerli bir işlem seçmediniz...");
                }
            }
        }

        public static void FloorAdd()
        {
            Console.WriteLine("Katın ismini giriniz");
            string _katismi = Console.ReadLine();
            if (IsNumeric(_katismi))
            {


                int? _katNO = Convert.ToInt16(_katismi);


                if (CompareFloor(_katNO) == 0)
                {
                    _kat = new Katlar();
                    _kat.KatNo = _katNO;
                    _ki.Ekle(_kat);
                }
                else
                {
                    Console.WriteLine("Bu kat daha önceden sisteme eklendi : " + CompareFloor(_katNO) + " numaralı kat zaten var");
                }


            }


        }

        public static void FloorRemove()
        {
            Katlar _silinecek;
            foreach (var listele in db.Odalars.ToList())
            {
                Console.WriteLine("Kat ID si --> " + listele.Id);

            }

            Console.WriteLine("Silmek istediğiniz Katın ID sini giriniz");
            var tus = Console.ReadLine();
            if (IsNumeric(tus))
            {

                int _tus = Convert.ToInt16(tus);
                _silinecek = db.Katlars.Find(_tus);
                if (_silinecek!=null)
                {
                    Console.WriteLine(isEmpty(_silinecek.Id));
                }
                else
                {
                    Console.WriteLine("Bu ID yok");   
                }

            }
        }

        public static bool IsNumeric(string sayi)
        {
            int output;
            return int.TryParse(sayi, out output);
        }

        public static int CompareFloor(int? KatNo)
        {
            var _katlarnumarasi = (from x in db.Katlars select x.KatNo).ToList();
            foreach (var item in _katlarnumarasi)
            {
                if (item == KatNo)
                {

                    int donenKat = item.Value;

                    return donenKat;

                }

            }

            return 0;
        }

        public static string isEmpty(int? katId)
        {
            if (katId!=null)
            {
                var _silinecek = db.Katlars.Find(katId);
                if (_silinecek != null && _silinecek.Odalars.Count == 0)
                {
                    db.Katlars.Remove(_silinecek);
                    db.SaveChanges();
                    return "Silme işlemi başarılı !!";
                }
                else if (_silinecek == null)
                {
                    return "Böyle bir kat sistemde bulunamadı";
                }
                else if (_silinecek.Odalars.Count != 0)
                {

                    foreach (var _katinodasi in _silinecek.Odalars.ToList())
                    {
                        Console.WriteLine(_silinecek.KatNo+" katının odası -> "+_katinodasi.OdaAdi);
                    }
                    return "Bu kata atanmış odalar bulunmaktadır, bu yüzden silemezsiniz !!!";

                }
               
            }
            return "Geçerli bir tuş basınız";
        }
    }
}