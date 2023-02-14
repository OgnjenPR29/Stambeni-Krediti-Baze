using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.Model
{
    public class VrstaObjekta
    {
        public int IdVO { get; set; }
        public string NazivVO { get; set; }

       
        public VrstaObjekta(int idVO, string nazivVO)
        {
            this.IdVO = idVO;
            this.NazivVO = nazivVO;
        }


        public override bool Equals(object obj)
        {
            var objekta = obj as VrstaObjekta; 
            return objekta != null &&
                   IdVO == objekta.IdVO &&
                   NazivVO == objekta.NazivVO;
        }
        public override int GetHashCode()
        {
            int hashCode = -913852795;
            hashCode = hashCode * -1521134295 + IdVO.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NazivVO);
            return hashCode;
        }
    }
}
