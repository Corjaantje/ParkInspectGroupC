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
using ParkInspectGroupC.Properties;
using GalaSoft.MvvmLight;
using System;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectionViewModel : ViewModelBase
    {
        public InspectionViewModel()
        {
            _searchCriteria = "";
            fillInspections();

            StartSearch = new RelayCommand(refillObservableCollection);

            BladerCommand = new RelayCommand(getFileFromDirectory, canGetFile);
            SubmitCommand = new RelayCommand(submitImage);
            AddInspection = new RelayCommand(addInspection);
			ShowQuestionnaire = new RelayCommand(showQuestionnaire);
            DeleteInspection = new RelayCommand(deleteInspection);
            assemblyFile = Directory.GetCurrentDirectory();
            var source = assemblyFile + "\\..\\..\\Image\\Testfotos\\01.jpg";
            SourceName = source;

        }

		private void showQuestionnaire()
		{
            bool questionnaireAlreadyFilled = false;
            if (SelectedInspection != null)
            {
                // check if this inspection already has a filled questionnaire, show those results instead of edit screen
                using (var context = new LocalParkInspectEntities())
                {
                    if ((from ques in context.Questionaire where ques.InspectionId == SelectedInspection.Id select ques).Count() > 0)
                    {
                        questionnaireAlreadyFilled = true;
                    }
                }

                if (questionnaireAlreadyFilled)
                {
                    RaisePropertyChanged("SelectedInspection");
                    Settings.Default.SingleInspectionResultsId = SelectedInspection.Id;
                    Navigator.SetNewView(new AssignmentResultView());
                }
                else
                {
                    RaisePropertyChanged("SelectedInspection");
                    Settings.Default.QuestionnaireSelectedInspectionId = SelectedInspection.Id;
                    Navigator.SetNewView(new QuestionnaireView());
                }
            }
		}

		private void fillInspections()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var result = context.Inspection.ToList();

                    Inspections = new ObservableCollection<Inspection>();
					
                    foreach(var inspection in result)
                    {
                        if(inspection.AssignmentId == Settings.Default.AssignmentId)
                        {
                            Inspections.Add(inspection);
                        }
					}
					allInspections = new ObservableCollection<Inspection>(Inspections);


				}

            }
            catch(Exception e)
            {
				Debug.Write(e.StackTrace);
                allInspections = new ObservableCollection<Inspection>();
				Inspections = new ObservableCollection<Inspection>();
                Inspections.Add(new Inspection {Id = 100, Location = "Something went wrong"});
            }

            refillObservableCollection();
        }

        private void refillObservableCollection()
        {
            var result = from Inspection in allInspections
                orderby Inspection.Id ascending
                where Inspection.Location.ToLower().Contains(SearchCriteria)
                   || Inspection.Id.ToString().Contains(SearchCriteria)
                select Inspection;

            Inspections = new ObservableCollection<Inspection>(result);

            RaisePropertyChanged("Inspections");
        }

		public void filterInspections(int AssignmentId)
		{

			var result = from Inspection in allInspections
						 orderby Inspection.Id ascending
						 where Inspection.AssignmentId == AssignmentId
						 select Inspection;

			Inspections = new ObservableCollection<Inspection>(result);
			
			RaisePropertyChanged("Inspections");
			Debug.WriteLine("Hello World");
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
                Settings.Default.InspectionId = SelectedInspection.Id;
                Settings.Default.InspectionAssignmentId = SelectedInspection.AssignmentId;
                Settings.Default.InspectionRegionId = SelectedInspection.RegionId;
                Settings.Default.InspectionStatusId = SelectedInspection.StatusId;
                Settings.Default.InspectionLocation = SelectedInspection.Location;
                Settings.Default.InspectionStartDatum = SelectedInspection.StartDate;
                Settings.Default.InspectionEindDatum = SelectedInspection.EndDate;
                Navigator.SetNewView(new InspectionEditView());
        }

        public void hideAddInspection()
        {
            Navigator.Back();
            RaisePropertyChanged("Inspections");
        }


        public void deleteInspection()
        {
            using (var context = new LocalParkInspectEntities())
            {
				var inspection = context.Inspection.Single(i => i.Id == SelectedInspection.Id);

				inspection.ExistsInCentral = 3;

                context.SaveChanges();
            }

            _inspections.Remove(SelectedInspection);
            allInspections.Remove(SelectedInspection);
            RaisePropertyChanged("Inspections");
        }

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
                refillObservableCollection();
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
		public ICommand ShowQuestionnaire { get; set; }

        #endregion
    }
}