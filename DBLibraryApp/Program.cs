using DBLibraryApp.BussinessLayer;
using DBLibraryApp.DAL;
using DBLibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibraryApp
{
    class Program
    {
        
        public static DBLibraryEntities db = new DBLibraryEntities();
        public static KitapIslemleri _ki = new KitapIslemleri();
        public static YazarIslemleri _yi = new YazarIslemleri();
        public static RafIslemleri _ri = new RafIslemleri();
        public static DolapIslemleri _di=new DolapIslemleri();
        public static KatIslemleri _kati = new KatIslemleri();
        public static OdaIslemleri _oi = new OdaIslemleri();

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Kat işlemleri için 1 'e basınız.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Oda işlemleri için 2 'e basınız.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Dolap işlemleri için 3 'e basınız.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Raf işlemleri için 4 'e basınız.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Kitap işlemleri için 5'e basınız.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Yazar işlemleri için 6 'e basınız.");
                Console.WriteLine("--------------------------------");
                string _tus = Console.ReadLine();

                if (_tus == "1" || _tus == "2" || _tus == "3" || _tus == "4" || _tus == "5" || _tus == "6")
                {

                    int tus = Convert.ToInt16(_tus);

                    if (tus == 1)
                    {
                        FloorOperations.FloorOperationList();
                    }
                    else if (tus == 2)
                    {
                        RoomOperations.RoomList();
                    }
                    else if (tus == 3)
                    {
                        CabinetOperations.CabinetList();
                    }
                    else if (tus == 4)
                    {
                        SelvingOperations.SelvingList();
                    }
                    else if (tus == 5)
                    {
                        BookOperations.BookList();
                    }
                    else if (tus == 6)
                    {
                        AuthorOperations.AuthorList();
                    }
                    else if (tus != 1 || tus != 2 || tus != 3 || tus != 4 || tus != 5 || tus != 6)
                    {
                        Console.WriteLine("Geçerli bir işlem seçmediniz...");
                    }
                }
            }
        }
        
    }
}
