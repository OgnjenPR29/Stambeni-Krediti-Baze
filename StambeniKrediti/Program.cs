﻿using StambeniKrediti.UIHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti
{
    class Program
    {
        private static readonly MainUIHandler mainUIHandler = new MainUIHandler();

        static void Main(string[] args)
        {
            mainUIHandler.HandleMainMenu();
        }
    }
}
