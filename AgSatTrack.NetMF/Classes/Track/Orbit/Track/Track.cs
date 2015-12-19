using System;
using System.Collections;
using System.Text;

using Zeptomoby.OrbitTools;

namespace Zeptomoby.OrbitTools
{
    public class Track
    {

        public Track()
        {
        }

        public void GetPasses(Site observer, Orbit orbit, DateTime start) {
            int passesRequired = 2;
            int totalPassesFound = 0;
            DateTime calcTime = start;
            Hashtable passes = new Hashtable();
            EciTime eci;
            Topo topoLook;
            Pass pass;

            while (totalPassesFound < passesRequired)
            {
                eci = orbit.GetPosition(calcTime);
                CoordGeo a = eci.ToGeo();
                topoLook = observer.GetLookAngle(eci);
                if (topoLook.ElevationDeg > 0)
                {
                    pass = new Pass();
                    pass.AOS = calcTime;
                    pass.AOSAz = topoLook.AzimuthDeg;
                    while (topoLook.ElevationDeg > 0)
                    {
                        eci = orbit.GetPosition(calcTime);
                        topoLook = observer.GetLookAngle(eci);
                        if (topoLook.ElevationDeg > pass.MaxEl)
                        {
                            pass.MaxEl = topoLook.ElevationDeg;
                        }
                        calcTime = calcTime.AddSeconds(1);
                        pass.LOS = calcTime;
                    }
                    pass.LOSAz = topoLook.AzimuthDeg;
                    //passes.Add(pass);
                    totalPassesFound++;
                }

                calcTime = calcTime.AddSeconds(1);
            }
            //return passes;
        }

    }
}
