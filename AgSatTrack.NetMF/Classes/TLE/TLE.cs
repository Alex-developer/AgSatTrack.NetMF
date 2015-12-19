using System;
using System.IO;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace AgSatTrack.Classes.TLEClasses
{
    public class TLE
    {
        private ArrayList _groups;

        public TLE()
        {
            _groups = new ArrayList();
        }

        public void Add(TLEGroup group)
        {
            _groups.Add(group);
        }

    }
}
