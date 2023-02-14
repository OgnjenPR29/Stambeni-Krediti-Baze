using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.Model
{
    public class Lice
    {
        public string IdL { get; set; }
        public string ImeL { get; set; }
        public string PrzL { get; set; }
        public string VrstaL { get; set; }
        public double Mes_PrihodiL { get; set; }

        public Lice() { }
        public Lice(string idL, string imeL, string przL, string vrstaL, double mes_PrihodiL)
        {
            this.IdL = idL;
            this.ImeL = imeL;
            this.PrzL = przL;
            this.VrstaL = vrstaL;
            this.Mes_PrihodiL = mes_PrihodiL;
        }

        public override bool Equals(object obj)
        {
            var lice = obj as Lice;
            return lice != null &&
                   IdL == lice.IdL &&
                   ImeL == lice.ImeL &&
                   PrzL == lice.PrzL &&
                   VrstaL == lice.VrstaL &&
                   Mes_PrihodiL == lice.Mes_PrihodiL;
        }

        public override int GetHashCode()
        {
            int hashCode = 384345334;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdL);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ImeL);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PrzL);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(VrstaL);
            hashCode = hashCode * -1521134295 + Mes_PrihodiL.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("{0,-4} {1,-15} {2,-15} {3,-10} {4,-20} ",
                this.IdL, this.ImeL, this.PrzL, this.VrstaL, this.Mes_PrihodiL);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-4} {1,-15} {2,-15} {3,-10} {4,-20} ",
                "IDL", "IMEL", "PRZL", "VRSTAL", "MES_PRIHODIL");
        }
    }
}
