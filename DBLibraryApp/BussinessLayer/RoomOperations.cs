using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.BussinessLayer
{
    public static class RoomOperations
    {
        public static DBLibraryEntities db = new DBLibraryEntities();
        public static OdaIslemleri _oi = new OdaIslemleri();
        public static Odalar _oda;
        public static int? _tmpOda;

        public static void RoomList()
        {
            Console.WriteLine("Oda eklemek için 1' e");
            Console.WriteLine("---------------------");
            Console.WriteLine("Oda silmek için 2' ye");
            Console.WriteLine("---------------------");
            Console.WriteLine("Odada ki dolapları listelemek  için 3'e");
            Console.WriteLine("---------------------");
            string _tus = Console.ReadLine();
            if (_tus == "1" || _tus == "2" || _tus == "3")
            {
                int tus = Convert.ToInt16(_tus);
                if (tus == 1)
                {
                    RoomAdd();
                }
                else if (tus == 2)
                {
                    RoomRemove();
                }
                else if (tus == 3)
                {
                    //listeleme
                    if (_oi.Listele().Count == 0)
                    {
                        Console.WriteLine("Sistemde oda tanımlı değil");
                    }
                    else
                    {
                        var liste = _oi.Listele();
                        foreach (var item in liste)
                        {
                            Console.WriteLine("Oda Numarası: " + item.OdaAdi);
                            if (item.Dolaplars.Count != 0)
                            {
                                var dolaplar = (from odadolaplar in db.Dolaplars where odadolaplar.OdaId == item.Id select odadolaplar).ToList();
                                foreach (var _dolap in dolaplar)
                                {
                                    Console.WriteLine(item.OdaAdi + " NUMARALI ODAYA AIT  " + _dolap.DolapNo);
                                }
                                if (item.Dolaplars.Count == 0)
                                {
                                    Console.WriteLine(item.OdaAdi + " ODASINA EKLENMİŞ BİR DOLAP BULUNMAMAKTADIR");
                                }
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

        public static void RoomAdd()
        {
            Console.WriteLine("Odanın ismini giriniz");
            string _odaismi = Console.ReadLine();
            if (CompareRoom(_odaismi) == false)
            {
                _oda = new Odalar();
                _oda.OdaAdi = _odaismi;
                int kat = KatBul(_odaismi);
                _oda.KatId = kat;
                foreach (var item in db.Katlars.ToList())
                {
                    if (item.KatNo == kat)
                    {
                        db.Odalars.Add(_oda);
                        db.SaveChanges();
                        Console.WriteLine("Oda ekleme işlemi başarılı");
                    }
                }
            }
            else if (CompareRoom(_odaismi) == true)
            {
                Console.WriteLine("Bu oda ismi zaten var");
            }

        }

        public static void RoomRemove()
        {
            Odalar _silinecek;
            foreach (var listele in db.Odalars.ToList())
            {
                Console.WriteLine("Oda ID si --> " + listele.Id);

            }

            Console.WriteLine("Silmek istediğiniz Odanın ID sini giriniz");
            var tus = Console.ReadLine();
            if (isNumeric(tus))
            {

                int _tus = Convert.ToInt16(tus);
                _silinecek = db.Odalars.Find(_tus);
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

        private static bool CompareRoom(string _roomName)
        {

            var _allRooms = (from x in db.Odalars select x.OdaAdi).ToList();
            foreach (var item in _allRooms)
            {

                if (item == _roomName)
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

        public static string isEmpty(int? odaId)
        {
            if (odaId != null)
            {
                var _silinecek = db.Odalars.Find(odaId);
                if (_silinecek != null && _silinecek.Dolaplars.Count == 0)
                {
                    db.Odalars.Remove(_silinecek);
                    db.SaveChanges();
                    return "Silme işlemi başarılı !!";
                }
                else if (_silinecek == null)
                {
                    return "Böyle bir oda sistemde bulunamadı";
                }
                else if (_silinecek.Dolaplars.Count != 0)
                {

                    foreach (var _odanindolabi in _silinecek.Dolaplars.ToList())
                    {
                        Console.WriteLine(_silinecek.OdaAdi + " odanın dolabı -> " + _odanindolabi.DolapNo);
                    }
                    return "Bu Odaya atanmış dolaplar bulunmaktadır, bu yüzden silemezsiniz !!!";

                }

            }
            return "Geçerli bir tuş basınız";
        }

        public static int KatBul(string odaismi)
        {
            string _gecicioda = odaismi.Substring(1);
            string kat = "";
            int silinecekindex = 999;
            for (int i = 0; i < _gecicioda.Length; i++)
            {
                if (isNumeric(_gecicioda[i].ToString()))
                {
                    kat = kat + _gecicioda[i].ToString();
                }
                else if (isNumeric(_gecicioda[i].ToString()) != true)
                {
                    silinecekindex = i;
                }
            }

            string katdeger = kat.Remove(silinecekindex);
            return Convert.ToInt16(katdeger);
        }
    }
}
