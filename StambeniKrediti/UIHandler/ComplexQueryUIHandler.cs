using StambeniKrediti.DAO;
using StambeniKrediti.DAO.Impl;
using StambeniKrediti.Model;
using StambeniKrediti.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StambeniKrediti.UIHandler
{
    class ComplexQueryUIHandler
    {
        private static readonly ComplexService complexService = new ComplexService();
        public static readonly ObjekatService objekatService = new ObjekatService();

        public void HandleComplexQueryMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite funkcionalnost:");
                Console.WriteLine("1 - 3. stavka - unesite IDL za prikaz svih objekata koji pripadaju tom Licu : ");
                Console.WriteLine("2 - 4. stavka- lica po vrsti fizicka/pravna");
                Console.WriteLine("3 - 5. stavka kupovina novog objekta ");
                Console.WriteLine("4 - 6. unos vrste objekta i izlistavanje svih");
                Console.WriteLine("X  - Izlazak iz kompleksnih upita");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        Stavka3();
                        break;
                    case "2":
                        Stavka4();
                        break;
                    case "3":
                        Stavka5();
                        break;
                    case "4":
                        Stavka6();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void Stavka3()
        {
            Console.WriteLine("Unesite IDL:");
            string idl = Console.ReadLine();

            Console.WriteLine(Objekat.GetFormattedHeader() + "\n");
            double ukupnaVrednost = 0;
            try
            {
                foreach (Objekat objekat in complexService.objektiZadatogIDLa(idl))
                {
                    Console.WriteLine(objekat);
                    ukupnaVrednost += objekat.Vrednost;
                }

                Console.WriteLine("\n\tUkupna vrednost objekata lica " + idl +  " iznosi " + ukupnaVrednost + " evra.");
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Stavka4()
        {
            try
            {
                var flica = complexService.LicaPoVrsti()["FIZICKO"];
                List<string> liceidl = new List<string>();
                int ukupnoObj = 0;

                Console.WriteLine("OBJEKTI FIZICKIH LICA: \n");
                Console.WriteLine(string.Format("{0,-4} {1,-20} {2,-15} {3,-4} {4,-15} {5, -15}",
                "IDO", "VRSTA OBJEKTA", "VREDNOST(e)", "IDL", "IMEL", "PRZL"));

                foreach (Lice l in flica)
                {
                    foreach (Objekat objekat in complexService.objektiZadatogIDLa(l.IdL))
                    {
                        Console.WriteLine(string.Format("{0,-4} {1,-20} {2,-15} {3,-4} {4,-15} {5, -15}",
                        objekat.IdO, complexService.VrstaObjektaPoId(objekat.IdVO), objekat.Vrednost, l.IdL, l.ImeL, l.PrzL));
                        ukupnoObj++;
                    }
                    liceidl.Add(l.IdL);
                }
                Console.WriteLine("UKUPAN BROJ OBJEKATA: " + ukupnoObj +
                    "; UKUPAN DUG : " + complexService.UkupanDug(liceidl) + " dinara.");


                Console.WriteLine();
                var plica = complexService.LicaPoVrsti()["PRAVNO"];
                ukupnoObj = 0;
                liceidl = new List<string>();

                Console.WriteLine("OBJEKTI PRAVNIH LICA: \n");
                Console.WriteLine(string.Format("{0,-4} {1,-20} {2,-15} {3,-4} {4,-15} {5, -15}",
               "IDO", "VRSTA OBJEKTA", "VREDNOST(e)", "IDL", "IMEL", "PRZL"));


                foreach (Lice l in plica)
                {
                    foreach (Objekat objekat in complexService.objektiZadatogIDLa(l.IdL))
                    {
                        Console.WriteLine(string.Format("{0,-4} {1,-20} {2,-15} {3,-4} {4,-15} {5, -15}",
                        objekat.IdO, complexService.VrstaObjektaPoId(objekat.IdVO), objekat.Vrednost, l.IdL, l.ImeL, l.PrzL));
                        ukupnoObj++;
                    }
                    liceidl.Add(l.IdL);
                }
                Console.WriteLine("UKUPAN BROJ OBJEKATA: " + ukupnoObj +
                   "; UKUPAN DUG : " + complexService.UkupanDug(liceidl) + " dinara.");

            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void Stavka5()
        {
            Console.WriteLine("Unesite ID lica za kupovinu novih objekata : ");
            string idL = Console.ReadLine();
            if (!complexService.ExistsById(idL))
            {
                Console.WriteLine("Ne postoji lice sa tim id-em!");
                return ;
            }

            Console.WriteLine("Otpocinjemo kupovinu objekta...");
            Thread.Sleep(1000);
            
            Console.WriteLine("IDO: ");
            int ido = int.Parse(Console.ReadLine());

            Console.WriteLine("IDVrsteObjekta(1-5): ");
            int idVO = int.Parse(Console.ReadLine());

            Console.WriteLine("Povrsina: ");
            double povrsina = Double.Parse(Console.ReadLine());

            Console.WriteLine("Adresa: ");
            string adresa = Console.ReadLine();

            Console.WriteLine("Vrednost: ");
            double vrednost = Double.Parse(Console.ReadLine());

            try
            {
                int inserted = objekatService.Save(new Objekat(ido, idL, idVO, povrsina, adresa, vrednost));
                if (inserted != 0)
                {
                    Console.WriteLine("Objekat na adresi \"{0}\" uspešno unet.", adresa);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                BilansStanja bs = complexService.FindByIdL(idL);
                Console.WriteLine(bs.IdBS + " " + bs.IdL);

                double cenaUEvrima = vrednost * 120;

                bs.Dug += cenaUEvrima;
                bs.Kamata += (0.1 * cenaUEvrima);
                bs.Saldo -= (bs.Dug + bs.Kamata);

                complexService.SaveBilans(bs);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Stavka6()
        {
            Console.WriteLine("Unesite vrstu objekta:");
            string vrstaObjekta = Console.ReadLine();

            double ukupnaVrednost = 0;
            int ukupnoObjekata = 0;
            int idvo = 0;
            try
            {
                bool nadjen = false;
                foreach (var item in complexService.GetVrstaObjekta())
                {
                    if (item.NazivVO==vrstaObjekta)
                    {
                        nadjen = true;
                        idvo = item.IdVO;
                    }
                }
                if(nadjen == false)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ne postoji uneta vrsta objekta");
                return;
            }
            try
            {

                Console.WriteLine(Objekat.GetFormattedHeader() + "\n");
                foreach (Objekat objekat in objekatService.FindAll())
                {
                    if(objekat.IdVO == idvo)
                    {
                        Console.WriteLine(objekat);
                        ukupnaVrednost += objekat.Vrednost;
                        ukupnoObjekata++;
                    }
                   
                }

                Console.WriteLine("\n\t Prosecna vrednost objekata te vrste: " + ukupnaVrednost/ukupnoObjekata +" evra.");
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
