using System;
using System.Collections;
using Microsoft.SPOT;

namespace AgSatTrack.Classes.TLEClasses
{
    public class TLEGroup
    {

        #region Private Stuff
        private ArrayList _tles;
        private string _name;
        #endregion

        #region Constructor
        public TLEGroup(string name)
        {
            _tles = new ArrayList();
            Name = name;
        }
        #endregion

        #region Getters and Setters
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ArrayList TLEData
        {
            get { return _tles; }
        }
        #endregion

        #region Methods
        public void Add(string line1, string line2, string line3)
        {
            TLEData tleData = new TLEData(line1, line2, line3);
            _tles.Add(tleData);
        }
        #endregion

    }
}
