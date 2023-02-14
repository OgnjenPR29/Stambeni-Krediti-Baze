using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.Model
{
    public class BilansStanja
    {
        public int IdBS { get; set; }
        public string IdL { get; set; }
        public double Saldo { get; set; }
        public double Dug { get; set; }
        public double Kamata { get; set; }

        public BilansStanja(int idBS, string idL, double saldo, double dug, double kamata)
        {
            this.IdBS = idBS;
            this.IdL = idL;
            this.Saldo = saldo;
            this.Dug = dug;
            this.Kamata = kamata;
        }

        public override bool Equals(object obj)
        {
            var stanja = obj as BilansStanja;
            return stanja != null &&
                   IdBS == stanja.IdBS &&
                   IdL == stanja.IdL &&
                   Saldo == stanja.Saldo &&
                   Dug == stanja.Dug &&
                   Kamata == stanja.Kamata;
        }

        public override int GetHashCode()
        {
            int hashCode = -318663404;
            hashCode = hashCode * -1521134295 + IdBS.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdL);
            hashCode = hashCode * -1521134295 + Saldo.GetHashCode();
            hashCode = hashCode * -1521134295 + Dug.GetHashCode();
            hashCode = hashCode * -1521134295 + Kamata.GetHashCode();
            return hashCode;
        }
    }
}
