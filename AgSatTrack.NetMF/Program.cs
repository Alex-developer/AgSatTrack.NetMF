using System;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

using GHI.Glide;
using GHI.Glide.Geom;
using GHI.Glide.UI;

using GHI.Processor;
using Gadgeteer.Modules.GHIElectronics;

using Gadgeteer;

using Zeptomoby.OrbitTools;

namespace AgSatTrack.NetMF
{
    public partial class Program
    {
        static GHI.Glide.Display.Window mainWindow;
        private Point last;
        private bool touched;

        void ProgramStarted()
        {

            this.last = new Point(0, 0);
            this.touched = false;

            this.displayCP7.ScreenPressed += this.OnScreenPressed;
            this.displayCP7.ScreenReleased += this.OnScreenReleased;
            this.displayCP7.GestureDetected += this.OnGesureDetected;

            Glide.FitToScreen = true;

            Debug.Print("Program Started");

            mainWindow = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.mainWindow));
            Image image = (Image)mainWindow.GetChildByName("earthImage");

            image.Bitmap = new Bitmap(Resources.GetBytes(Resources.BinaryResources.main),Bitmap.BitmapImageType.Jpeg);
            mainWindow.BackColor = Microsoft.SPOT.Presentation.Media.Color.White;
            Glide.MainWindow = mainWindow;

            DateTime dateTime = new DateTime(2015, 12, 18, 20, 53, 0);
            Utility.SetLocalTime(dateTime);
            RealTimeClock.SetDateTime(dateTime);

            GHI.Glide.UI.Button myText = (GHI.Glide.UI.Button)mainWindow.GetChildByName("keyboard");
            myText.PressEvent += new OnPress(Glide.OpenKeyboard);

            Gadgeteer.Timer timer = new Gadgeteer.Timer(500);

            timer.Tick += TimerTick;
            timer.Start();
        }

        void TimerTick(Timer timer)
        {
            string str1 = "ISS (ZARYA)             ";
            string str2 = "1 25544U 98067A   15352.54319571  .00014358  00000-0  21741-3 0  9990";
            string str3 = "2 25544  51.6437 244.0274 0008333 295.0996 127.4199 15.54892795976727";
            Tle tle1 = new Tle(str1, str2, str3);
            Site siteEquator = new Site(52.454935, 0.201279, 0);
            Orbit orbit = new Orbit(tle1);
            DateTime now = DateTime.Now;
            EciTime eciSDP4 = orbit.GetPosition(now);
            Topo topoLook = siteEquator.GetLookAngle(eciSDP4);

            CoordGeo coords = eciSDP4.ToGeo();

            UpdateTextBlock(RealTimeClock.GetDateTime().ToString(@"dd\/MM\/yyyy HH:mm"), "date");

            UpdateTextBlock(coords.Altitude.ToString("F2"), "satAlt");
            UpdateTextBlock(coords.Latitude.ToString("F2"), "satLat");
            UpdateTextBlock((360 - coords.Longitude).ToString("F2"), "satLon");
            UpdateTextBlock(topoLook.AzimuthDeg.ToString("F2"), "satAz");
            UpdateTextBlock(topoLook.ElevationDeg.ToString("F2"), "satEl");
        }


        private void UpdateTextBlock(string text, string textBlock1)
        {
            TextBlock textBlock = (TextBlock)mainWindow.GetChildByName(textBlock1);
            textBlock.Text = text;
            mainWindow.FillRect(textBlock.Rect);
            textBlock.Invalidate();
        }

        private void OnScreenPressed(DisplayCP7 sender, DisplayCP7.TouchEventArgs e)
        {
            if (e.TouchCount <= 0 || GlideTouch.IgnoreAllEvents)
                return;

            Point touch = new Point(e.TouchPoints[0].X, e.TouchPoints[0].Y);

            if (this.touched)
            {
                this.last.X = touch.X;
                this.last.Y = touch.Y;

                if (this.last.X != touch.X || this.last.Y != touch.Y)
                    GlideTouch.RaiseTouchMoveEvent(null, new TouchEventArgs(touch));
            }
            else
            {
                this.last.X = touch.X;
                this.last.Y = touch.Y;
                this.touched = true;

                GlideTouch.RaiseTouchDownEvent(null, new TouchEventArgs(touch));
            }
        }

        private void OnScreenReleased(DisplayCP7 sender, EventArgs e)
        {
            this.touched = false;

            GlideTouch.RaiseTouchUpEvent(null, new TouchEventArgs(this.last));
        }

        private void OnGesureDetected(DisplayCP7 sender, DisplayCP7.GestureDetectedEventArgs e)
        {
            switch (e.Gesture)
            {
                case DisplayCP7.GestureType.MoveUp: GlideTouch.RaiseTouchGestureEvent(null, new TouchGestureEventArgs(TouchGesture.Up, this.last.X, this.last.Y, 0, DateTime.Now)); break;
                case DisplayCP7.GestureType.MoveLeft: GlideTouch.RaiseTouchGestureEvent(null, new TouchGestureEventArgs(TouchGesture.Left, this.last.X, this.last.Y, 0, DateTime.Now)); break;
                case DisplayCP7.GestureType.MoveDown: GlideTouch.RaiseTouchGestureEvent(null, new TouchGestureEventArgs(TouchGesture.Down, this.last.X, this.last.Y, 0, DateTime.Now)); break;
                case DisplayCP7.GestureType.MoveRight: GlideTouch.RaiseTouchGestureEvent(null, new TouchGestureEventArgs(TouchGesture.Right, this.last.X, this.last.Y, 0, DateTime.Now)); break;
                case DisplayCP7.GestureType.None: GlideTouch.RaiseTouchGestureEvent(null, new TouchGestureEventArgs(TouchGesture.NoGesture, this.last.X, this.last.Y, 0, DateTime.Now)); break;
                case DisplayCP7.GestureType.ZoomIn:
                case DisplayCP7.GestureType.ZoomOut:
                    GlideTouch.RaiseTouchGestureEvent(null, new TouchGestureEventArgs(TouchGesture.Zoom, this.last.X, this.last.Y, 0, DateTime.Now));

                    break;
            }
        }

    }
}
