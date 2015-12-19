using System;
using Microsoft.SPOT;

namespace AgSatTrack.Classes.TLEClasses
{
    public class TLEData
    {

        #region Private Stuff
        private string _line1;
        private string _line2;
        private string _line3;
        #endregion

        #region Constructor
        public TLEData(string line1, string line2, string line3)
        {
            Line1 = line1;
            Line2 = line2;
            Line3 = line3;
        }
        #endregion

        #region Getters and Setters
        public string Line1
        {
            get { return _line1; }
            set { _line1 = value; }
        }
        public string Line2
        {
            get { return _line2; }
            set { _line2 = value; }
        }
        public string Line3
        {
            get { return _line3; }
            set { _line3 = value; }
        }
        #endregion
    }
}
