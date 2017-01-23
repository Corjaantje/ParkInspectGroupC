using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using LocalDatabase.Domain;
using ParkInspectGroupC.DOMAIN;
using Account = LocalDatabase.Domain.Account;
using Assignment = LocalDatabase.Domain.Assignment;
using Availability = LocalDatabase.Domain.Availability;
using Coordinate = LocalDatabase.Domain.Coordinate;
using Customer = LocalDatabase.Domain.Customer;
using Employee = LocalDatabase.Domain.Employee;
using EmployeeStatus = LocalDatabase.Domain.EmployeeStatus;
using Inspection = LocalDatabase.Domain.Inspection;
using InspectionImage = LocalDatabase.Domain.InspectionImage;
using InspectionStatus = LocalDatabase.Domain.InspectionStatus;
using Keyword = LocalDatabase.Domain.Keyword;
using KeywordCategory = LocalDatabase.Domain.KeywordCategory;
using Module = LocalDatabase.Domain.Module;
using Question = LocalDatabase.Domain.Question;
using QuestionAnswer = LocalDatabase.Domain.QuestionAnswer;
using QuestionKeyword = LocalDatabase.Domain.QuestionKeyword;
using QuestionSort = LocalDatabase.Domain.QuestionSort;
using Region = LocalDatabase.Domain.Region;
using WorkingHours = LocalDatabase.Domain.WorkingHours;

namespace LocalDatabase.Local
{
    public class SaveToCentral
    {
        private readonly DatabaseActions _sqliteActions;
        private readonly SQLiteConnection _sqliteConnection;

        public SaveToCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public bool Save()
        {
            //Start saving to central database, table for table
            var action = false;

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

            action = Coordinate();
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
                var list = new List<Region>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Region.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Region();
                            _new.Region1 = r.Region1;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Region.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<EmployeeStatus>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.EmployeeStatus.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.EmployeeStatus();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.EmployeeStatus.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Employee>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    var sqlget = "SELECT * FROM Employee WHERE ExistsInCentral = 0";
                    var reader = _sqliteActions.Get(_sqliteConnection, sqlget);
                    var result = reader.Select("");


                    foreach (var item in result)
                    {
                        var temp = new Employee();
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
                        temp.IsInspecter = Convert.ToInt32(item[12]) == 1 ? true : false;
                        temp.IsManager = Convert.ToInt32(item[13]) == 1 ? true : false;
                        temp.ManagerId = Convert.ToInt64(item[14]);
                        temp.DateCreated = Convert.ToDateTime(item[15].ToString());
                        temp.DateUpdated = Convert.ToDateTime(item[16].ToString());

                        list.Add(temp);
                    }
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            //Get id's from central db
                            var regionId = GetRegionId(Convert.ToInt32(r.RegionId));
                            var employeeStatusId = GetEmployeeStatusId(Convert.ToInt32(r.EmployeeStatusId));
                            var managerId = 0;
                            if ((r.ManagerId != 0) && (r.ManagerId != null))
                                managerId = GetEmployeeId(Convert.ToInt32(r.ManagerId));

                            //Create new Employee
                            var _new = new ParkInspectGroupC.DOMAIN.Employee();
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
                                _new.RegionId = regionId;

                            if (managerId != 0)
                                _new.ManagerId = managerId;

                            context.Employee.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Account>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Account.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Account();
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
                var list = new List<Availability>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Availability.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Availability();
                            _new.EmployeeId = GetEmployeeId(Convert.ToInt32(r.EmployeeId));
                            _new.Date = r.Date;
                            _new.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                            _new.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Availability.Add(_new);
                        }
                        context.SaveChanges();
                    }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } //TODO check this function, starttime and endtime are being saved ass 'null' in central

        private bool WorkingHours()
        {
            try
            {
                var list = new List<WorkingHours>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.WorkingHours.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.WorkingHours();
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
                var list = new List<Customer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Customer.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Customer();
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
                var list = new List<Assignment>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Assignment.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Assignment();
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
                var list = new List<InspectionStatus>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionStatus.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.InspectionStatus();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.InspectionStatus.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Inspection>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Inspection.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Inspection();
                            _new.AssignmentId = GetAssignmentId(Convert.ToInt32(r.AssignmentId));
                            _new.RegionId = GetRegionId(Convert.ToInt32(r.RegionId));
                            _new.Location = r.Location;
                            _new.StartDate = r.StartDate;
                            _new.EndDate = r.EndDate;
                            _new.StatusId = GetInspectionStatusId(Convert.ToInt32(r.StatusId));
                            _new.InspectorId = GetEmployeeId(Convert.ToInt32(r.InspectorId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Inspection.Add(_new);
                        }
                        context.SaveChanges();
                    }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Coordinate()
        {
            try
            {
                var list = new List<Coordinate>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Coordinate.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Coordinate();
                            _new.Longitude = r.Longitude;
                            _new.Latitude = r.Latitude;
                            _new.Note = r.Note;
                            _new.InspectionId = GetInspectionId(Convert.ToInt32(r.InspectionId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Coordinate.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<InspectionImage>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionImage.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.InspectionImage();
                            _new.File = r.File;
                            _new.InspectionId = GetInspectionId(Convert.ToInt32(r.InspectionId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.InspectionImage.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<KeywordCategory>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.KeywordCategory.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.KeywordCategory();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.KeywordCategory.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Keyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Keyword.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Keyword();
                            _new.CategoryId = GetKeywordCatId(Convert.ToInt32(r.CategoryId));
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Keyword.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Module>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Module.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Module();
                            _new.Name = r.Name;
                            _new.Description = r.Description;
                            _new.Note = r.Note;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Module.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<QuestionSort>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionSort.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.QuestionSort();
                            _new.Description = r.Description;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionSort.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Question>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Question.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Question();
                            _new.SortId = GetQuestionSortId(Convert.ToInt32(r.SortId));
                            _new.Description = r.Description;
                            _new.ModuleId = GetModuleId(Convert.ToInt32(r.ModuleId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Question.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Questionaire>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questionaire.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new Questionnaire();
                            _new.InspectionId = GetInspectionId(Convert.ToInt32(r.InspectionId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.Questionnaire.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<QuestionAnswer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionAnswer.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.QuestionAnswer();
                            _new.QuestionnaireId = GetQuestionnaireId(Convert.ToInt32(r.QuestionnaireId));
                            _new.QuestionId = GetQuestionId(Convert.ToInt32(r.QuestionId));
                            _new.Result = r.Result;
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionAnswer.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<QuestionaireModule>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionaireModule.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new QuestionnaireModule();
                            _new.ModuleId = GetModuleId(Convert.ToInt32(r.ModuleId));
                            _new.QuestionnaireId = GetQuestionnaireId(Convert.ToInt32(r.QuestionaireId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionnaireModule.Add(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<QuestionKeyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionKeyword.Where(r => r.ExistsInCentral == 0).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.QuestionKeyword();
                            _new.QuestionId = GetQuestionId(Convert.ToInt32(r.QuestionId));
                            _new.KeywordId = GetKeywordId(Convert.ToInt32(r.KeywordId));
                            _new.DateCreated = r.DateCreated;
                            _new.DateUpdated = r.DateUpdated;

                            context.QuestionKeyword.Add(_new);
                        }
                        context.SaveChanges();
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
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT Region, DateCreated FROM Region WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oReg = get.Rows[0][0];
                var regionName = oReg.ToString();

                var r = context.Region.Where(x => x.Region1 == regionName).First();
                id = r.Id;
            }
            return id;
        }

        private int GetEmployeeStatusId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT Description, DateCreated FROM EmployeeStatus WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oEm = get.Rows[0][0];
                var text = oEm.ToString();

                var r = context.EmployeeStatus.Where(x => x.Description == text).First();
                id = r.Id;
            }
            return id;
        }

        private int GetEmployeeId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Employee WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                ParkInspectGroupC.DOMAIN.Employee r = null;
                try
                {
                    r = context.Employee.Where(x => x.DateCreated == lastDateAdded).First();
                    id = r.Id;
                }
                catch (Exception)
                {
                    id = i;
                }
                
            }
            return id;
        }

        private int GetCustomerId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Customer WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Customer.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetAssignmentId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Assignment WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Assignment.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetInspectionStatusId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM InspectionStatus WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.InspectionStatus.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetInspectionId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Inspection WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Inspection.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetKeywordCatId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM KeywordCategory WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.KeywordCategory.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetKeywordId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Keyword WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Keyword.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetQuestionSortId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM QuestionSort WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.QuestionSort.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetQuestionnaireId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Questionaire WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Questionnaire.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetQuestionId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Question WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Question.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        private int GetModuleId(int i)
        {
            var id = 0;
            using (var context = new ParkInspectEntities())
            {
                var getId = "SELECT DateCreated FROM Module WHERE Id = '" + i + "'";
                var get = _sqliteActions.Get(_sqliteConnection, getId);
                var oDate = get.Rows[0][0];
                var lastDateAdded = (DateTime) oDate;

                var r = context.Module.Where(x => x.DateCreated == lastDateAdded).First();
                id = r.Id;
            }
            return id;
        }

        #endregion
    }
}