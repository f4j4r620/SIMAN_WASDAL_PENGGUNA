using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace AppPengguna.AST.TRK
{
    public partial class UcGridMap : UserControl
    {
        public UcGridMap(double Lat,double longt)
        {
            InitializeComponent();
            gmapMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmapMap.Position = new GMap.NET.PointLatLng(Lat, longt);


            GMapOverlay markersOverlay = new GMapOverlay("marker");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(Lat, longt),
              GMarkerGoogleType.arrow);
            marker.ToolTip = new GMapToolTip(marker);
        }
    }
}
