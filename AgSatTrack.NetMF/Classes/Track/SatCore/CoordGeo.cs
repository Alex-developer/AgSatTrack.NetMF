using System;

namespace Zeptomoby.OrbitTools
{
    public class CoordGeo
    {
        private double m_Latitude;

        private double m_Longitude;

        private double m_Altitude;

        public double Altitude
        {
            get
            {
                double mAltitude = this.m_Altitude;
                return mAltitude;
            }
            set
            {
                this.m_Altitude = value;
            }
        }

        public double Latitude
        {
            get
            {
                double mLatitude = this.m_Latitude;
                return mLatitude;
            }
            set
            {
                this.m_Latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                double mLongitude = this.m_Longitude;
                return mLongitude;
            }
            set
            {
                this.m_Longitude = value;
            }
        }

        public CoordGeo()
        {
            this.m_Latitude = 0;
            this.m_Longitude = 0;
            this.m_Altitude = 0;
        }

        public CoordGeo(double lat, double lon, double alt)
        {
            this.m_Latitude = Globals.ToDegrees(lat);
            this.m_Longitude = Globals.ToDegrees(lon);
            this.m_Altitude = alt;
        }
    }
}