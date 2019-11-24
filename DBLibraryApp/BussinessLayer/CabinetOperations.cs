using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp.BussinessLayer
{
    public static class CabinetOperations
    {
        private static DBLibraryEntities db = new DBLibraryEntities();
        private static DolapIslemleri _di = new DolapIslemleri();
        private static Dolaplar _dolap;
        private static int? _tmpdolap;


        public static void CabinetList()
        {
            Console.WriteLine("Dolap eklemek için 1'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Dolap silmek için 2'e");
            Console.WriteLine("--------------------");
            Console.WriteLine("Dolaptaki ki Rafları listelemek  için 3'e");
            Console.WriteLine("--------------------");
            string _tus = Console.ReadLine();

            if (_tus == "1" || _tus == "2" || _tus == "3")
            {
                int tus = Convert.ToInt16(_tus);

                if (tus == 1)
                {

                    CabinetAdd();

                }
                else if (tus == 2)
                {
                    CabinetRemove();
                }
                else if (tus == 3)
                {

                    if (_di.Listele().Count == 0)
                    {
                        Console.WriteLine("Sistemde kat tanımlı değil");
                    }
                    else
                    {
                        var liste = _di.Listele();
                        foreach (var item in liste)
                        {
                            Console.WriteLine("Dolap numarası " + item.DolapNo);
                            if (item.Raflars.Count != 0)
                            {
                                var raflar = (from dolapraf in db.Raflars where dolapraf.DolapId == item.Id select dolapraf).ToList();
                                foreach (var _raf in raflar)
                                {
                                    Console.WriteLine(item.DolapNo + " NUMARALI DOLABA AIT  " + _raf.RafNo);
                                }
                            }
                            else if (item.Raflars.Count == 0)
                            {
                                Console.WriteLine(item.DolapNo + " DOLABINA EKLENMİŞ BİR RAF BULUNMAMAKTADIR");
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
        public static void CabinetAdd()
        {
            //int _tmpCabinetID;
            //Console.WriteLine("Dolabın numarasını giriniz");
            //string _dolapno = Console.ReadLine();
            //if (CompareCabinet(_dolapno) == false)
            //{
            //    _dolap = new Dolaplar();
            //    _dolap.DolapNo = _dolapno;
            //    Console.WriteLine("Dolabı eklemek istediğiniz odanın ID sini aşağıdan seçerek giriniz");
            //    foreach (var item in db.Odalars.ToList())
            //    {
            //        Console.WriteLine("ID : " + item.Id + " Oda adı -> " + item.OdaAdi);
            //    }
            //    var _dolapID = Console.ReadLine();
            //    if (isNumeric(_dolapID))
            //    {
            //        _tmpCabinetID = Convert.ToInt16(_dolapID);
            //        _dolap.OdaId = _tmpCabinetID;
            //        _di.Ekle(_dolap);
            //        db.SaveChanges();
            //        Console.WriteLine("Dolap ekleme işlemi başarılı");
            //    }
            //    else if (isNumeric(_dolapID) != true)
            //    {
            //        Console.WriteLine("Lütfen numerik değerler giriniz.");
            //    }


            //}
            //else if (CompareCabinet(_dolapno))
            //{
            //    Console.WriteLine("Bu dolap numarası zaten var");
            //}
            try
            {
                Console.WriteLine("Dolabın ismini giriniz");
                string _dolapismi = Console.ReadLine();
                if (CompareCabinet(_dolapismi) == false)
                {
                    _dolap = new Dolaplar();
                    _dolap.DolapNo = _dolapismi;
                    string oda = OdaBul(_dolapismi);
                    _dolap.OdaId = Convert.ToInt16(_dolapismi);
                    foreach (var item in db.Odalars.ToList())
                    {
                        if (item.OdaAdi == oda)
                        {
                            db.Dolaplars.Add(_dolap);
                            db.SaveChanges();
                            Console.WriteLine("Dolap ekleme işlemi başarılı");
                        }
                    }
                }
                else if (CompareCabinet(_dolapismi) == true)
                {
                    Console.WriteLine("Bu Dolap ismi zaten var");
                }
            }
            catch 
            {

                throw;
            }

        }
        public static void CabinetRemove()
        {
            Dolaplar _silinecek;
            foreach (var listele in db.Raflars.ToList())
            {
                Console.WriteLine("Dolap ID si --> " + listele.Id);

            }

            Console.WriteLine("Silmek istediğiniz Dolabın ID sini giriniz");
            var tus = Console.ReadLine();
            if (isNumeric(tus))
            {

                int _tus = Convert.ToInt16(tus);
                _silinecek = db.Dolaplars.Find(_tus);
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
        public static bool CompareCabinet(string DolapName)
        {
            var _dolapnumarasi = (from x in db.Dolaplars select x.DolapNo).ToList();
            foreach (var item in _dolapnumarasi)
            {
                if (item == DolapName.ToString())
                {

                    if (item == DolapName.ToString())
                    {
                        return true;
                    }

                }

            }

            return false;
        }
        public static string isEmpty(int? dolapId)
        {
            if (dolapId != null)
            {
                var _silinecek = db.Dolaplars.Find(dolapId);
                if (_silinecek != null && _silinecek.Raflars.Count == 0)
                {
                    db.Dolaplars.Remove(_silinecek);
                    db.SaveChanges();
                    return "Silme işlemi başarılı !!";
                }
                else if (_silinecek == null)
                {
                    return "Böyle bir dolap sistemde bulunamadı";
                }
                else if (_silinecek.Raflars.Count != 0)
                {

                    foreach (var _dolaprafi in _silinecek.Raflars.ToList())
                    {
                        Console.WriteLine(_silinecek.DolapNo + " dolabın rafı -> " + _dolaprafi.RafNo);
                    }
                    return "Bu dolaba atanmış raflar bulunmaktadır, bu yüzden silemezsiniz !!!";

                }

            }
            return "Geçerli bir tuş basınız";
        }
        public static string OdaBul(string dolapismi)
        {
            string _gecicidolap = dolapismi.Substring(1);
            string oda = "";
            int silinecekindex = 999;
            for (int i = 0; i < _gecicidolap.Length; i++)
            {
                if (isNumeric(_gecicidolap[i].ToString()))
                {
                    oda = oda + _gecicidolap[i].ToString();
                }
                else if (isNumeric(_gecicidolap[i].ToString()) != true)
                {
                    silinecekindex = i;
                }
            }

            string odadeger = oda.Remove(silinecekindex);
            return Convert.ToString(odadeger);
        }
    }

}
