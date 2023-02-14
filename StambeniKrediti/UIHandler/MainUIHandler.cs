using StambeniKrediti.DAO;
using StambeniKrediti.DAO.Impl;
using StambeniKrediti.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.UIHandler
{
    public class MainUIHandler
    {
        private readonly ObjekatUIHandler objekatUIHandler = new ObjekatUIHandler();
        private readonly ComplexQueryUIHandler complexQueryUIHandler = new ComplexQueryUIHandler();
        public void HandleMainMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("1 - Rukovanje Objektima");
                Console.WriteLine("2 - Kompleksni upiti");

                Console.WriteLine("X - Izlazak iz programa");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        objekatUIHandler.HandleObjekti();
                        break;
                    case "2":
                        complexQueryUIHandler.HandleComplexQueryMenu();
                        break;
                        
                }

            } while (!answer.ToUpper().Equals("X"));

        }

       

    }
}
