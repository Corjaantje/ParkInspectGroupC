using LocalDatabase.Domain;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDatabase.Local
{
    public class SaveToCentral
    {
        SQLiteConnection _sqliteConnection;
        DatabaseActions _sqliteActions;
        public SaveToCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public bool Save()
        {
            //Start saving to central database, table for table
            bool action = false;

            action = Region();
            if (!action) return false;

            action = EmployeeStatus();
            if (!action) return false;

            action = Employee();
            if (!action) return false;

            action = Account();
            if (!action) return false;

            action = Availability();
            if (!action) return false;

            action = WorkingHours();
            if (!action) return false;

            action = Customer();
            if (!action) return false;

            action = Assignment();
            if (!action) return false;

            action = InspectionStatus();
            if (!action) return false;

            action = Inspection();
            if (!action) return false;

            action = InspectionImage();
            if (!action) return false;

            action = KeywordCategory();
            if (!action) return false;

            action = Keyword();
            if (!action) return false;

            action = Module();
            if (!action) return false;

            action = QuestionSort();
            if (!action) return false;

            action = Question();
            if (!action) return false;

            action = Questionaire();
            if (!action) return false;

            action = QuestionAnswer();
            if (!action) return false;

            action = QuestionaireModule();
            if (!action) return false;

            action = QuestionKeyword();

            return action;
        }

        #region Database tables
        private bool Region()
        {
            try
            {
                List<Domain.Region> list = new List<Domain.Region>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Regions.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Region r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Region _new = new ParkInspectGroupC.DOMAIN.Region();
                            _new.Region1 = r.Region1;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Region.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool EmployeeStatus()
        {
            try
            {
                List<Domain.EmployeeStatu> list = new List<Domain.EmployeeStatu>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.EmployeeStatus.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.EmployeeStatu r in list)
                        {
                            ParkInspectGroupC.DOMAIN.EmployeeStatus _new = new ParkInspectGroupC.DOMAIN.EmployeeStatus();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.EmployeeStatus.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Employee()
        {
            try
            {
                List<Domain.Employee> list = new List<Domain.Employee>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    string sqlget = "SELECT * FROM Employee WHERE ExistsInCentral = 0";
                    DataTable reader = _sqliteActions.Get(_sqliteConnection, sqlget);
                    DataRow[] result = reader.Select("");


                    foreach (var item in result)
                    {
                        Domain.Employee temp = new Domain.Employee();
                        temp.FirstName = item[1].ToString();
                        temp.Prefix = item[2].ToString();
                        temp.SurName = item[3].ToString();
                        temp.Gender = item[4].ToString();
                        temp.City = item[5].ToString();
                        temp.Address = item[6].ToString();
                        temp.ZipCode = item[7].ToString();
                        temp.Phonenumber = item[8].ToString();
                        temp.Email = item[9].ToString();
                        temp.RegionId = Convert.ToInt64(item[10]);
                        temp.EmployeeStatusId = Convert.ToInt64(item[11]);
                        temp.IsInspecter = (Convert.ToInt32(item[12]) == 1) ? true : false;
                        temp.IsManager = (Convert.ToInt32(item[13]) == 1) ? true : false;
                        temp.ManagerId = Convert.ToInt64(item[14]);
                        temp.DateCreated = Convert.ToDateTime(item[15].ToString());
                        temp.DateUpdated = Convert.ToDateTime(item[16].ToString());

                        list.Add(temp);
                    }
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Employee r in list)
                        {
                            //Get id's from central db
                            int regionId = GetRegionId(Convert.ToInt32(r.RegionId));
                            int employeeStatusId = GetEmployeeStatusId(Convert.ToInt32(r.EmployeeStatusId));
                            int managerId = 0;
                            if (r.ManagerId != 0 && r.ManagerId != null)
                            {
                                managerId = GetEmployeeId(Convert.ToInt32(r.ManagerId));
                            }

                            //Create new Employee
                            ParkInspectGroupC.DOMAIN.Employee _new = new ParkInspectGroupC.DOMAIN.Employee();
                            _new.FirstName = r.FirstName;
                            _new.Prefix = r.Prefix;
                            _new.SurName = r.SurName;
                            _new.Gender = r.Gender;
                            _new.City = r.City;
                            _new.Address = r.Address;
                            _new.ZipCode = r.ZipCode;
                            _new.Phonenumber = r.Phonenumber;
                            _new.Email = r.Email;
                            _new.EmployeeStatusId = employeeStatusId;
                            _new.IsInspecter = r.IsInspecter;
                            _new.IsManager = r.IsManager;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            if (regionId != 0)
                            {
                                _new.RegionId = regionId;
                            }

                            if (managerId != 0)
                            {
                                _new.ManagerId = managerId;
                            }

                            context.Employee.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Account()
        {
            try
            {
                List<Domain.Account> list = new List<Domain.Account>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Accounts.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Account r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Account _new = new ParkInspectGroupC.DOMAIN.Account();
                            _new.Username = r.Username;
                            _new.Password = r.Password;
                            _new.UserGuid = r.UserGuid;
                            _new.EmployeeId = GetEmployeeId(Convert.ToInt32(r.EmployeeId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Account.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Availability()
        {
            try
            {
                List<Domain.Availability> list = new List<Domain.Availability>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Availabilities.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Availability r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Availability _new = new ParkInspectGroupC.DOMAIN.Availability();
                            _new.EmployeeId = GetEmployeeId(Convert.ToInt32(r.EmployeeId));
                            _new.Date = r.Date;
                            _new.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                            _new.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Availabilities.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }//TODO check this function, starttime and endtime are being saved ass 'null' in central
        private bool WorkingHours()
        {
            try
            {
                List<Domain.WorkingHour> list = new List<Domain.WorkingHour>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.WorkingHours.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.WorkingHour r in list)
                        {
                            ParkInspectGroupC.DOMAIN.WorkingHour _new = new ParkInspectGroupC.DOMAIN.WorkingHour();
                            _new.EmployeeId = GetEmployeeId(Convert.ToInt32(r.EmployeeId));
                            _new.Date = r.Date;
                            _new.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                            _new.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.WorkingHours.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Customer()
        {
            try
            {
                List<Domain.Customer> list = new List<Domain.Customer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Customers.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Customer r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Customer _new = new ParkInspectGroupC.DOMAIN.Customer();
                            _new.Name = r.Name;
                            _new.Address = r.Address;
                            _new.Location = r.Location;
                            _new.Phonenumber = r.Phonenumber;
                            _new.Email = r.Email;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Customer.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Assignment()
        {
            try
            {
                List<Domain.Assignment> list = new List<Domain.Assignment>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Assignments.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Assignment r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Assignment _new = new ParkInspectGroupC.DOMAIN.Assignment();
                            _new.CustomerId = GetCustomerId(Convert.ToInt32(r.CustomerId));
                            _new.ManagerId = GetEmployeeId(Convert.ToInt32(r.ManagerId));
                            _new.Description = r.Description;
                            _new.StartDate = r.StartDate;
                            _new.EndDate = r.EndDate;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Assignment.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool InspectionStatus()
        {
            try
            {
                List<Domain.InspectionStatu> list = new List<Domain.InspectionStatu>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionStatus.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.InspectionStatu r in list)
                        {
                            ParkInspectGroupC.DOMAIN.InspectionStatus _new = new ParkInspectGroupC.DOMAIN.InspectionStatus();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.InspectionStatus.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Inspection()
        {
            try
            {
                List<Domain.Inspection> list = new List<Domain.Inspection>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Inspections.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Inspection r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Inspection _new = new ParkInspectGroupC.DOMAIN.Inspection();
                            _new.AssignmentId = GetAssignmentId(Convert.ToInt32(r.AssignmentId));
                            _new.RegionId = GetRegionId(Convert.ToInt32(r.RegionId));
                            _new.Location = r.Location;
                            _new.StartDate = r.StartDate;
                            _new.EndDate = r.EndDate;
                            _new.StatusId = GetInspectionStatusId(Convert.ToInt32(r.StatusId));
                            _new.InspectorId = GetEmployeeId(Convert.ToInt32(r.InspectorId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Inspections.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool InspectionImage()
        {
            try
            {
                List<Domain.InspectionImage> list = new List<Domain.InspectionImage>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionImages.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.InspectionImage r in list)
                        {
                            ParkInspectGroupC.DOMAIN.InspectionImage _new = new ParkInspectGroupC.DOMAIN.InspectionImage();
                            _new.File = r.File;
                            _new.InspectionId = GetInspectionId(Convert.ToInt32(r.InspectionId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.InspectionImage.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool KeywordCategory()
        {
            try
            {
                List<Domain.KeywordCategory> list = new List<Domain.KeywordCategory>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.KeywordCategories.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.KeywordCategory r in list)
                        {
                            ParkInspectGroupC.DOMAIN.KeywordCategory _new = new ParkInspectGroupC.DOMAIN.KeywordCategory();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.KeywordCategory.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Keyword()
        {
            try
            {
                List<Domain.Keyword> list = new List<Domain.Keyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Keywords.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Keyword r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Keyword _new = new ParkInspectGroupC.DOMAIN.Keyword();
                            _new.CategoryId = GetKeywordCatId(Convert.ToInt32(r.CategoryId));
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Keyword.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Module()
        {
            try
            {
                List<Domain.Module> list = new List<Domain.Module>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Modules.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Module r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Module _new = new ParkInspectGroupC.DOMAIN.Module();
                            _new.Name = r.Name;
                            _new.Description = r.Description;
                            _new.Note = r.Note;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Modules.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool QuestionSort()
        {
            try
            {
                List<Domain.QuestionSort> list = new List<Domain.QuestionSort>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionSorts.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionSort r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionSort _new = new ParkInspectGroupC.DOMAIN.QuestionSort();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionSorts.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Question()
        {
            try
            {
                List<Domain.Question> list = new List<Domain.Question>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questions.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Question r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Question _new = new ParkInspectGroupC.DOMAIN.Question();
                            _new.SortId = GetQuestionSortId(Convert.ToInt32(r.SortId));
                            _new.Description = r.Description;
                            _new.ModuleId = GetModuleId(Convert.ToInt32(r.ModuleId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Questions.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Questionaire()
        {
            try
            {
                List<Domain.Questionaire> list = new List<Domain.Questionaire>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questionaires.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Questionaire r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Questionnaire _new = new ParkInspectGroupC.DOMAIN.Questionnaire();
                            _new.InspectionId = GetInspectionId(Convert.ToInt32(r.InspectionId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Questionnaires.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool QuestionAnswer()
        {
            try
            {
                List<Domain.QuestionAnswer> list = new List<Domain.QuestionAnswer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionAnswers.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionAnswer r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionAnswer _new = new ParkInspectGroupC.DOMAIN.QuestionAnswer();
                            _new.QuestionnaireId = GetQuestionnaireId(Convert.ToInt32(r.QuestionnaireId));
                            _new.QuestionId = GetQuestionId(Convert.ToInt32(r.QuestionId));
                            _new.Result = r.Result;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionAnswer.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool QuestionaireModule()
        {
            try
            {
                List<Domain.QuestionaireModule> list = new List<Domain.QuestionaireModule>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionaireModules.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionaireModule r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionnaireModule _new = new ParkInspectGroupC.DOMAIN.QuestionnaireModule();
                            _new.ModuleId = GetModuleId(Convert.ToInt32(r.ModuleId));
                            _new.QuestionnaireId = GetQuestionnaireId(Convert.ToInt32(r.QuestionaireId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionnaireModule.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool QuestionKeyword()
        {
            try
            {
                List<Domain.QuestionKeyword> list = new List<Domain.QuestionKeyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionKeywords.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionKeyword r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionKeyword _new = new ParkInspectGroupC.DOMAIN.QuestionKeyword();
                            _new.QuestionId = GetQuestionId(Convert.ToInt32(r.QuestionId));
                            _new.KeywordId = GetKeywordId(Convert.ToInt32(r.KeywordId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionKeyword.Add(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Get Info From Central Database
        private int GetRegionId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT Region, DateCreated FROM Region WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oReg = get.Rows[0][0];
                string regionName = oReg.ToString();

                ParkInspectGroupC.DOMAIN.Region r = context.Region.Where(x => x.Region1 == regionName).First();
                id = r.Id;
            }
            return id;
        }
        private int GetEmployeeStatusId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT Description, DateCreated FROM EmployeeStatus WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oEm = get.Rows[0][0];
                string text = oEm.ToString();

                ParkInspectGroupC.DOMAIN.EmployeeStatus r = context.EmployeeStatus.Where(x => x.Description == text).First();
                id = r.Id;
            }
            return id;
        }
        private int GetEmployeeId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Employee WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Employee r = context.Employee.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetCustomerId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Customer WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Customer r = context.Customer.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetAssignmentId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Assignment WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Assignment r = context.Assignment.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetInspectionStatusId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM InspectionStatus WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.InspectionStatus r = context.InspectionStatus.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetInspectionId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Inspection WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Inspection r = context.Inspections.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetKeywordCatId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM KeywordCategory WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.KeywordCategory r = context.KeywordCategory.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetKeywordId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Keyword WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Keyword r = context.Keyword.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetQuestionSortId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM QuestionSort WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.QuestionSort r = context.QuestionSorts.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetQuestionnaireId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Questionaire WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Questionnaire r = context.Questionnaires.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetQuestionId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Question WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Question r = context.Questions.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        private int GetModuleId(int i)
        {
            int id = 0;
            using (var context = new ParkInspectEntities())
            {
                string getId = "SELECT DateCreated FROM Module WHERE Id = '" + i + "'";
                DataTable get = _sqliteActions.Get(_sqliteConnection, getId);
                object oDate = get.Rows[0][0];
                DateTime lastDateAdded = (DateTime)oDate;

                ParkInspectGroupC.DOMAIN.Module r = context.Modules.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }
        #endregion
    }
}