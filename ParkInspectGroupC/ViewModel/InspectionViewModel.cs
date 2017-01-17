using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectionViewModel
    {
        public InspectionViewModel()
        {
            _searchCriteria = "";
            fillInspections();

            StartSearch = new RelayCommand(refillObservableCollection);

            BladerCommand = new RelayCommand(getFileFromDirectory, canGetFile);
            SubmitCommand = new RelayCommand(submitImage);
            AddInspection = new RelayCommand(addInspection);
            DeleteInspection = new RelayCommand(deleteInspection);
            assemblyFile = Directory.GetCurrentDirectory();
            var source = assemblyFile + "\\..\\..\\Image\\silvio.jpeg";
            SourceName = source;

            //BitmapImage i = new BitmapImage(uri);
            //SourceName = i;
        }

        private void fillInspections()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var result = context.Inspection.ToList();

                    allInspections = new ObservableCollection<Inspection>(result);

                    Inspections = new ObservableCollection<Inspection>(result);
                }
            }
            catch
            {
                allInspections = new ObservableCollection<Inspection>();
                Inspections.Add(new Inspection {Id = 100, Location = "Something went wrong"});
            }

            refillObservableCollection();
        }

        private void refillObservableCollection()
        {
            var result = from Inspection in Inspections
                orderby Inspection.Id ascending
                where Inspection.Location.Contains(SearchCriteria)
                select Inspection;
            Inspections = new ObservableCollection<Inspection>(allInspections);

            RaisePropertyChanged("Inspections");
        }

        public void submitImage()
        {
            if (SelectedInspection != null)
            {
                var stream = new MemoryStream();
                var bitmap = new Bitmap(SourceName);
                bitmap.Save(stream, ImageFormat.Bmp);

                var byteArray = stream.GetBuffer();
                var image = new InspectionImage();
                var imageFile = "";
                //##Uitgecomenteerd door gebrek aan uiteindelijke implementatie
                //int counter = 0;

                //foreach (byte byte1 in byteArray)
                //{
                //    //imageFile += byte1;

                //    //if (counter >= 16)
                //    //{
                //    //    break;
                //    //}
                //    //counter++;
                //}
                //Debug.WriteLine(byteArray.Length);

                using (var context = new LocalParkInspectEntities())
                {
                    var newImage = new InspectionImage
                    {
                        File = imageFile,
                        InspectionId = SelectedInspection.Id
                    };

                    context.InspectionImage.Add(newImage);
                    context.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Selecteer aub een inspectie");
            }
        }

        public bool canGetFile()
        {
            return true;
        }

        public void getFileFromDirectory()
        {
            //Debug.WriteLine(SourceName);
            //Debug.WriteLine(SelectedInspection.Id);
            var dialog = new OpenFileDialog();
            dialog.Filter =
                "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            using (var folderDialog = dialog)
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //string directoryPath = Path.GetDirectoryName(filePath);

                    //string targetPath = assemblyFile + "\\..\\..\\Image\\" + folderDialog.FileName;
                    //string destFile = System.IO.Path.Combine(targetPath);
                    ////File.Create(assemblyFile + "\\..\\..\\Image\\silvio.jpeg");
                    //System.IO.File.Copy(folderDialog.fil, destFile, true);
                    //ImageString = assemblyFile + "\\..\\..\\Image\\silvio.jpeg";
                    //SourceName = folderDialog.FileName;
                    SourceName = folderDialog.FileName;
                    //BitmapImage i = new BitmapImage(uri);


                    //SourceName = i;
                    RaisePropertyChanged("SourceName");
                }
            }
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        public void imageToByteArray(string path)
        {
            var imgdata = File.ReadAllBytes(path);
            for (var i = 0; i < imgdata.Count(); i++)
            {
                int byte1 = imgdata[i];
            }
        }

        public void addInspection()
        {
            Navigator.SetNewView(new InspectionCreationView());
        }

        public void editInspection()
        {
            if (SelectedInspection == null)
                MessageBox.Show("Selecteer aub een inspectie");

            else
                Navigator.SetNewView(new InspectionEditView());
        }

        public void hideAddInspection()
        {
            Navigator.SetNewView(new InspectionView());
            RaisePropertyChanged("Inspections");
        }


        public void deleteInspection()
        {
            using (var context = new LocalParkInspectEntities())
            {
                var inspections = context.Inspection.ToList();

                foreach (var inspection in inspections)
                    if (inspection.Id == SelectedInspection.Id)
                        context.Inspection.Remove(inspection);
                context.SaveChanges();
            }

            _inspections.Remove(SelectedInspection);
            allInspections.Remove(SelectedInspection);
            RaisePropertyChanged("Inspections");
        }


        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region properties

        private ObservableCollection<Inspection> _inspections;

        public ObservableCollection<Inspection> Inspections
        {
            get { return _inspections; }
            set
            {
                _inspections = value;
                RaisePropertyChanged("Inspections");
            }
        }

        // maybe create fancier location search criteria
        private string _searchCriteria;

        public string SearchCriteria
        {
            get { return _searchCriteria; }

            set
            {
                _searchCriteria = value;
                Debug.WriteLine(_searchCriteria);
                var searchedInspections = new ObservableCollection<Inspection>();

                foreach (var inspection in allInspections)
                    if ((SearchCriteria != null) && inspection.Location.ToLower().Contains(SearchCriteria.ToLower()))
                        searchedInspections.Add(inspection);


                Inspections = searchedInspections;

                RaisePropertyChanged("SearchCriteria");
                RaisePropertyChanged("Inspections");
            }
        }


        public ICommand StartSearch { get; set; }

        private ObservableCollection<Inspection> allInspections;

        public ICommand BladerCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand AddInspection { get; set; }

        public ICommand EditInspection { get; set; }
        public ICommand DeleteInspection { get; set; }
        private string _imageSource;
        private readonly string assemblyFile;

        public string SourceName
        {
            get { return _imageSource; }

            set
            {
                _imageSource = value;
                RaisePropertyChanged("SourceName");
            }
        }


        public Inspection SelectedInspection { get; set; }

        #endregion
    }
}