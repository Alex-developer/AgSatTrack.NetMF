using System;
using System.Collections;
using System.Text;

namespace Zeptomoby.OrbitTools
{
    public class Pass
    {
    //    private List<SSP> ssp = new List<SSP>();

        public DateTime AOS { get; set; }
        public DateTime LOS { get; set; }
        public double MaxEl { get; set; }
        public double AOSAz { get; set; }
        public double LOSAz { get; set; }

        public void AddSSP() {
        }

    }
}
