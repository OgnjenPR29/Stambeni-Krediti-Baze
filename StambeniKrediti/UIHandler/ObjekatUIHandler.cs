using StambeniKrediti.DAO;
using StambeniKrediti.DAO.Impl;
using StambeniKrediti.Model;
using StambeniKrediti.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.UIHandler
{
    public class ObjekatUIHandler
    {
        public static readonly ObjekatService objekatService = new ObjekatService();
        public void HandleObjekti()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju za rad nad objektima:");
                Console.WriteLine("1 - Prikaz svih objekata");
                Console.WriteLine("2 - Prikaz objekta po identifikatoru");
                Console.WriteLine("3 - Unos jednog objekta");
                Console.WriteLine("4 - Unos vise objekata");
                Console.WriteLine("5 - Izmena po identifikatoru");
                Console.WriteLine("6 - Brisanje po identifikatoru");
                Console.WriteLine("7 - Provera postojanja objekta");
                Console.WriteLine("X - Izlazak iz rukovanja objektima");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        ShowById();
                        break;
                    case "3":
                        HandleSingleInsert();
                        break;
                    case "4":
                        HandleMultipleInserts();
                        break;
                    case "5":
                        HandleUpdate();
                        break;
                    case "6":
                        HandleDelete();
                        break;
                    case "7":
                        HandleExist();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }
        private void ShowAll()
        {
            Console.WriteLine(Objekat.GetFormattedHeader());

            try
            {
                foreach (Objekat objekat in objekatService.FindAll())
                {
                    Console.WriteLine(objekat);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void HandleExist()
        {
            Console.WriteLine("Unesite id objekta : ");
            int id = int.Parse(Console.ReadLine());
            if (objekatService.ExistsById(id)) {
                Console.WriteLine("Postoji objekat za zadatim id-em");
            }
            else
            {
                Console.WriteLine("Ne postoji objekat za zadatim id-em");

            }
        }
        private void ShowById()
        {
            Console.WriteLine("IDO: ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                Objekat Objekat = objekatService.FindById(id);

                Console.WriteLine(Objekat.GetFormattedHeader());
                Console.WriteLine(Objekat);
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void HandleSingleInsert()
        {

            Console.WriteLine("IDO: ");
            int ido = int.Parse(Console.ReadLine());

            Console.WriteLine("IDLica: ");
            string idL = Console.ReadLine();

            Console.WriteLine("IDVrsteObjekta: ");
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

        }

        private void HandleUpdate()
        {
            Console.WriteLine("IDO: ");
            int ido = int.Parse(Console.ReadLine());

            try
            {
                if (!objekatService.ExistsById(ido))
                {
                    Console.WriteLine("Uneta vrednost ne postoji!");
                    return;
                }

                Console.WriteLine("IDLica: ");
                string idL = Console.ReadLine();

                Console.WriteLine("IDVrsteObjekta: ");
                int idVO = int.Parse(Console.ReadLine());

                Console.WriteLine("Povrsina: ");
                double povrsina = Double.Parse(Console.ReadLine());

                Console.WriteLine("Adresa: ");
                string adresa = Console.ReadLine();

                Console.WriteLine("Vrednost: ");
                double vrednost = Double.Parse(Console.ReadLine());

                int updated = objekatService.Save(new Objekat(ido, idL, idVO, povrsina, adresa, vrednost));
                if (updated != 0)
                {
                    Console.WriteLine("Objekat na adresi \"{0}\" uspešno unet.", adresa);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void HandleDelete()
        {
            Console.WriteLine("IDObjekta: ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                int deleted = objekatService.DeleteById(id);
                if (deleted != 0)
                {
                    Console.WriteLine("Objekat sa id-em \"{0}\" uspešno obrisan.", id);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void HandleMultipleInserts()
        {
            List<Objekat> listaObjekata = new List<Objekat>();
            String answer;
            do
            {
                Console.WriteLine("IDO: ");
                int ido = int.Parse(Console.ReadLine());

                Console.WriteLine("IDLica: ");
                string idL = Console.ReadLine();

                Console.WriteLine("IDVrsteObjekta: ");
                int idVO = int.Parse(Console.ReadLine());

                Console.WriteLine("Povrsina: ");
                double povrsina = Double.Parse(Console.ReadLine());

                Console.WriteLine("Adresa: ");
                string adresa = Console.ReadLine();

                Console.WriteLine("Vrednost: ");
                double vrednost = Double.Parse(Console.ReadLine());

                listaObjekata.Add(new Objekat(ido, idL, idVO, povrsina, adresa, vrednost));

                Console.WriteLine("Unesi još jedan objekat? (ENTER za potvrdu, X za odustanak)");
                answer = Console.ReadLine();
            } while (!answer.ToUpper().Equals("X"));

            try
            {
                int numInserted = objekatService.SaveAll(listaObjekata);
                Console.WriteLine("Uspešno uneto {0} objekata.", numInserted);
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
