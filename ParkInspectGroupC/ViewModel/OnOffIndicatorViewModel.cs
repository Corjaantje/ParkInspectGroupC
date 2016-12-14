using GalaSoft.MvvmLight;
using LocalDatabase;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ParkInspectGroupC.Miscellaneous;

namespace ParkInspectGroupC.ViewModel
{
    public class OnOffIndicatorViewModel : ViewModelBase
    {
        LocalDatabaseMain ldb = new LocalDatabaseMain("ParkInspect");
        private string _OnOffIndicator;
        public string OnOffIndicator
        {
            get { return _OnOffIndicator; }
            set { _OnOffIndicator = value; RaisePropertyChanged("OnOffIndicator"); }
        }
        private Brush _IndicatorColor;
        public Brush IndicatorColor
        {
            get { return _IndicatorColor; }
            set { _IndicatorColor = value; RaisePropertyChanged("IndicatorColor"); }
        }
        public OnOffIndicatorViewModel()
        {
            CheckConnection();
        }

        private async void CheckConnection()
        {
            Task task = Task.Run(() =>
            {
                OnOffIndicator = "Checking...";
                BrushConverter bc = new BrushConverter();
                IndicatorColor = (Brush)bc.ConvertFrom("Orange");
                //Check to see if there is a connection with the central db (Azure db)
                using (var db = new ParkInspectEntities())
                {
                    DbConnection conn = db.Database.Connection;
                    try
                    {
                        conn.Open();
                        OnOffIndicator = "Online";
                        IndicatorColor = (Brush)bc.ConvertFrom("LightGreen");
                    }
                    catch
                    {
                        OnOffIndicator = "Offline";
                        IndicatorColor = (Brush)bc.ConvertFrom("Red");
                    }
                }
            });

            await task;
        }
    }
}
