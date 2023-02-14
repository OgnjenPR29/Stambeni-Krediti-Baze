using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.Model
{
    public class Objekat
    {
       
        public int IdO { get; set; }
        public string IdL { get; set; }
        public int IdVO { get; set; }
        public double Povrsina { get; set; }
        public string Adresa { get; set; }
        public double Vrednost { get; set; }
        public Objekat() { }

        public Objekat(int idO, string idL, int idVO, double povrsina, string adresa, double vrednost)
        {
            this.IdO = idO;
            this.IdL = idL;
            this.IdVO = idVO;
            this.Povrsina = povrsina;
            this.Adresa = adresa;
            this.Vrednost = vrednost;
        }

        public override bool Equals(object obj)
        {
            var objekat = obj as Objekat;
            return objekat != null &&
                   IdO == objekat.IdO &&
                   IdL == objekat.IdL &&
                   IdVO == objekat.IdVO &&
                   Povrsina == objekat.Povrsina &&
                   Adresa == objekat.Adresa &&
                   Vrednost == objekat.Vrednost;
        }

        public override int GetHashCode()
        {
            int hashCode = -1421312546;
            hashCode = hashCode * -1521134295 + IdO.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IdL);
            hashCode = hashCode * -1521134295 + IdVO.GetHashCode();
            hashCode = hashCode * -1521134295 + Povrsina.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Adresa);
            hashCode = hashCode * -1521134295 + Vrednost.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return string.Format("{0,-4} {1,-4} {2,-4} {3,-8} {4,-20} {5, -10}",
                this.IdO, this.IdL, this.IdVO, this.Povrsina, this.Adresa, this.Vrednost);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-4} {1,-4} {2,-4} {3,-8} {4,-20} {5, -10}",
                "IDO", "IDL", "IDVO", "POVRSINA", "ADRESA", "VREDNOST");
        }
    }
}
