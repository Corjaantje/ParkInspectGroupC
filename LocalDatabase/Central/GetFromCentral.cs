using LocalDatabase.Domain;
using LocalDatabase.Local;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDatabase.Central
{
    public class GetFromCentral
    {
        DatabaseActions _sqliteActions;
        SQLiteConnection _sqliteConnection;
        public GetFromCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }
        public bool Sync(string dbName)
        {
            if (ClearLocal() && CreateLocal(dbName) && GetDataAndSaveToLocal())
            {
                return true;
            }

            return false;
        }
        private bool ClearLocal()
        {
            try
            {
                string sql = "DROP TABLE QuestionKeyword;" +
                                "DROP TABLE QuestionaireModule;" +
                                "DROP TABLE QuestionAnswer;" +
                                "DROP TABLE Questionaire;" +
                                "DROP TABLE Question;" +
                                "DROP TABLE QuestionSort;" +
                                "DROP TABLE Module;" +
                                "DROP TABLE Keyword;" +
                                "DROP TABLE KeywordCategory;" +
                                "DROP TABLE InspectionImage;" +
                                "DROP TABLE Coordinate;" +
                                "DROP TABLE Inspection;" +
                                "DROP TABLE InspectionStatus;" +
                                "DROP TABLE Assignment;" +
                                "DROP TABLE Customer;" +
                                "DROP TABLE WorkingHours;" +
                                "DROP TABLE Availability;" +
                                "DROP TABLE Account;" +
                                "DROP TABLE Employee;" +
                                "DROP TABLE EmployeeStatus;" +
                                "DROP TABLE Region;";

                _sqliteActions.CUD(_sqliteConnection, sql);
                return true;
            }
            catch (Exception)
            {
                Debug.WriteLine("Clear goes wrong!");
                return false;
            }
        }
        private bool CreateLocal(string dbName)
        {
            CreateSQLiteDatabase createDB = new CreateSQLiteDatabase();
            return createDB.Create(_sqliteActions, dbName, true); //When true database is created and thus exists, when false creating database failed
        }
        private bool GetDataAndSaveToLocal()
        {
            bool action = false;

            action = GetRegion();
            if (!action) return false;

            action = GetEmployeeStatus();
            if (!action) return false;

            action = GetEmployee();
            if (!action) return false;

            action = GetAccount();
            if (!action) return false;

            action = GetAvailability();
            if (!action) return false;

            action = GetWorkingHours();
            if (!action) return false;

            action = GetCustomer();
            if (!action) return false;

            action = GetAssignment();
            if (!action) return false;

            action = GetInspectionStatus();
            if (!action) return false;

            action = GetInspection();
            if (!action) return false;

            action = GetCoordinate();
            if (!action) return false;

            action = GetInspectionImage();
            if (!action) return false;

            action = GetKeywordCategory();
            if (!action) return false;

            action = GetKeyword();
            if (!action) return false;

            action = GetModule();
            if (!action) return false;

            action = GetQuestionSort();
            if (!action) return false;

            action = GetQuestion();
            if (!action) return false;

            action = GetQuestionaire();
            if (!action) return false;

            action = GetQuestionAnswer();
            if (!action) return false;

            action = GetQuestionaireModule();
            if (!action) return false;

            action = GetQuestionKeyword();

            return action;
        }

        #region Database tables
        private bool GetRegion()
        {
            List<ParkInspectGroupC.DOMAIN.Region> list = new List<ParkInspectGroupC.DOMAIN.Region>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Region r in context.Region)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Region r in list)
                    {
                        Domain.Region _new = new Domain.Region();
                        _new.Id = r.Id;
                        _new.Region1 = r.Region1;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Region.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetEmployeeStatus()
        {
            List<ParkInspectGroupC.DOMAIN.EmployeeStatus> list = new List<ParkInspectGroupC.DOMAIN.EmployeeStatus>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.EmployeeStatus r in context.EmployeeStatus)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.EmployeeStatus r in list)
                    {
                        Domain.EmployeeStatus _new = new Domain.EmployeeStatus();
                        _new.Id = r.Id;
                        _new.Description = r.Description;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.EmployeeStatus.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetEmployee()
        {
            List<ParkInspectGroupC.DOMAIN.Employee> list = new List<ParkInspectGroupC.DOMAIN.Employee>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Employee r in context.Employee)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Employee r in list)
                    {
                        Domain.Employee _new = new Domain.Employee();
                        _new.Id = r.Id;
                        _new.FirstName = r.FirstName;
                        _new.Prefix = r.Prefix;
                        _new.SurName = r.SurName;
                        _new.Gender = r.Gender;
                        _new.City = r.City;
                        _new.Address = r.Address;
                        _new.ZipCode = r.ZipCode;
                        _new.Phonenumber = r.Phonenumber;
                        _new.Email = r.Email;
                        _new.RegionId = r.RegionId;
                        _new.EmployeeStatusId = r.EmployeeStatusId;
                        _new.IsInspecter = r.IsInspecter;
                        _new.IsManager = r.IsManager;
                        _new.ManagerId = r.ManagerId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Employee.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetAccount()
        {
            List<ParkInspectGroupC.DOMAIN.Account> list = new List<ParkInspectGroupC.DOMAIN.Account>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Account r in context.Account)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Account r in list)
                    {
                        Domain.Account _new = new Domain.Account();
                        _new.Id = r.Id;
                        _new.Username = r.Username;
                        _new.Password = r.Password;
                        _new.UserGuid = r.UserGuid;
                        _new.EmployeeId = r.EmployeeId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Account.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetAvailability()
        {
            List<ParkInspectGroupC.DOMAIN.Availability> list = new List<ParkInspectGroupC.DOMAIN.Availability>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Availability r in context.Availability)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Availability r in list)
                    {
                        Domain.Availability _new = new Domain.Availability();
                        _new.EmployeeId = r.EmployeeId;
                        _new.Date = r.Date;
                        _new.StartTime = Convert.ToDateTime(r.StartTime.ToString());
                        _new.EndTime = Convert.ToDateTime(r.EndTime.ToString());
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Availability.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetWorkingHours()
        {
            List<ParkInspectGroupC.DOMAIN.WorkingHours> list = new List<ParkInspectGroupC.DOMAIN.WorkingHours>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.WorkingHours r in context.WorkingHours)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.WorkingHours r in list)
                    {
                        Domain.WorkingHours _new = new Domain.WorkingHours();
                        _new.EmployeeId = r.EmployeeId;
                        _new.Date = r.Date;
                        _new.StartTime = Convert.ToDateTime(r.StartTime.ToString());
                        _new.EndTime = Convert.ToDateTime(r.EndTime.ToString());
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.WorkingHours.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetCustomer()
        {
            List<ParkInspectGroupC.DOMAIN.Customer> list = new List<ParkInspectGroupC.DOMAIN.Customer>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Customer r in context.Customer)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Customer r in list)
                    {
                        Domain.Customer _new = new Domain.Customer();
                        _new.Id = r.Id;
                        _new.Name = r.Name;
                        _new.Address = r.Address;
                        _new.Location = r.Location;
                        _new.Phonenumber = r.Phonenumber;
                        _new.Email = r.Email;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Customer.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetAssignment()
        {
            List<ParkInspectGroupC.DOMAIN.Assignment> list = new List<ParkInspectGroupC.DOMAIN.Assignment>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Assignment r in context.Assignment)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Assignment r in list)
                    {
                        Domain.Assignment _new = new Domain.Assignment();
                        _new.Id = r.Id;
                        _new.CustomerId = r.CustomerId;
                        _new.ManagerId = r.ManagerId;
                        _new.Description = r.Description;
                        _new.StartDate = r.StartDate;
                        _new.EndDate = r.EndDate;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Assignment.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetInspectionStatus()
        {
            List<ParkInspectGroupC.DOMAIN.InspectionStatus> list = new List<ParkInspectGroupC.DOMAIN.InspectionStatus>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.InspectionStatus r in context.InspectionStatus)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.InspectionStatus r in list)
                    {
                        Domain.InspectionStatus _new = new Domain.InspectionStatus();
                        _new.Id = r.Id;
                        _new.Description = r.Description;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.InspectionStatus.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetInspection()
        {
            List<ParkInspectGroupC.DOMAIN.Inspection> list = new List<ParkInspectGroupC.DOMAIN.Inspection>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Inspection r in context.Inspection)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Inspection r in list)
                    {
                        Domain.Inspection _new = new Domain.Inspection();
                        _new.Id = r.Id;
                        _new.AssignmentId = r.AssignmentId;
                        _new.RegionId = r.RegionId;
                        _new.Location = r.Location;
                        _new.StartDate = r.StartDate;
                        _new.EndDate = r.EndDate;
                        _new.StatusId = r.StatusId;
                        _new.InspectorId = r.InspectorId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Inspection.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetInspectionImage()
        {
            List<ParkInspectGroupC.DOMAIN.InspectionImage> list = new List<ParkInspectGroupC.DOMAIN.InspectionImage>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.InspectionImage r in context.InspectionImage)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.InspectionImage r in list)
                    {
                        Domain.InspectionImage _new = new Domain.InspectionImage();
                        _new.Id = r.Id;
                        _new.File = r.File;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.InspectionImage.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetCoordinate()
        {
            List<ParkInspectGroupC.DOMAIN.Coordinate> list = new List<ParkInspectGroupC.DOMAIN.Coordinate>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Coordinate r in context.Coordinate)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Coordinate r in list)
                    {
                        Domain.Coordinate _new = new Domain.Coordinate();
                        _new.Id = r.Id;
                        _new.Longitude = r.Longitude;
                        _new.Latitude = r.Latitude;
                        _new.Note = r.Note;
                        _new.InspectionId = r.InspectionId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Coordinate.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetKeywordCategory()
        {
            List<ParkInspectGroupC.DOMAIN.KeywordCategory> list = new List<ParkInspectGroupC.DOMAIN.KeywordCategory>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.KeywordCategory r in context.KeywordCategory)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.KeywordCategory r in list)
                    {
                        Domain.KeywordCategory _new = new Domain.KeywordCategory();
                        _new.Id = r.Id;
                        _new.Description = r.Description;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.KeywordCategory.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetKeyword()
        {
            List<ParkInspectGroupC.DOMAIN.Keyword> list = new List<ParkInspectGroupC.DOMAIN.Keyword>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Keyword r in context.Keyword)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Keyword r in list)
                    {
                        Domain.Keyword _new = new Domain.Keyword();
                        _new.Id = r.Id;
                        _new.CategoryId = r.CategoryId;
                        _new.Description = r.Description;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Keyword.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetModule()
        {
            List<ParkInspectGroupC.DOMAIN.Module> list = new List<ParkInspectGroupC.DOMAIN.Module>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Module r in context.Module)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Module r in list)
                    {
                        Domain.Module _new = new Domain.Module();
                        _new.Id = r.Id;
                        _new.Name = r.Name;
                        _new.Description = r.Description;
                        _new.Note = r.Note;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Module.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetQuestionSort()
        {
            List<ParkInspectGroupC.DOMAIN.QuestionSort> list = new List<ParkInspectGroupC.DOMAIN.QuestionSort>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionSort r in context.QuestionSort)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionSort r in list)
                    {
                        Domain.QuestionSort _new = new Domain.QuestionSort();
                        _new.Id = r.Id;
                        _new.Description = r.Description;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.QuestionSort.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetQuestion()
        {
            List<ParkInspectGroupC.DOMAIN.Question> list = new List<ParkInspectGroupC.DOMAIN.Question>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Question r in context.Question)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Question r in list)
                    {
                        Domain.Question _new = new Domain.Question();
                        _new.Id = r.Id;
                        _new.SortId = r.SortId;
                        _new.Description = r.Description;
                        _new.ModuleId = r.ModuleId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Question.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetQuestionaire()
        {
            List<ParkInspectGroupC.DOMAIN.Questionnaire> list = new List<ParkInspectGroupC.DOMAIN.Questionnaire>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Questionnaire r in context.Questionnaire)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.Questionnaire r in list)
                    {
                        Domain.Questionaire _new = new Domain.Questionaire();
                        _new.Id = r.Id;
                        _new.InspectionId = r.InspectionId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.Questionaire.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetQuestionAnswer()
        {
            List<ParkInspectGroupC.DOMAIN.QuestionAnswer> list = new List<ParkInspectGroupC.DOMAIN.QuestionAnswer>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionAnswer r in context.QuestionAnswer)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionAnswer r in list)
                    {
                        Domain.QuestionAnswer _new = new Domain.QuestionAnswer();
                        _new.QuestionnaireId = r.QuestionnaireId;
                        _new.QuestionId = r.QuestionId;
                        _new.Result = r.Result;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.QuestionAnswer.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetQuestionaireModule()
        {
            List<ParkInspectGroupC.DOMAIN.QuestionnaireModule> list = new List<ParkInspectGroupC.DOMAIN.QuestionnaireModule>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionnaireModule r in context.QuestionnaireModule)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionnaireModule r in list)
                    {
                        Domain.QuestionaireModule _new = new Domain.QuestionaireModule();
                        _new.ModuleId = r.ModuleId;
                        _new.QuestionaireId = r.QuestionnaireId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.QuestionaireModule.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool GetQuestionKeyword()
        {
            List<ParkInspectGroupC.DOMAIN.QuestionKeyword> list = new List<ParkInspectGroupC.DOMAIN.QuestionKeyword>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionKeyword r in context.QuestionKeyword)
                    {
                        list.Add(r);
                    }
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (ParkInspectGroupC.DOMAIN.QuestionKeyword r in list)
                    {
                        Domain.QuestionKeyword _new = new Domain.QuestionKeyword();
                        _new.QuestionId = r.QuestionId;
                        _new.KeywordId = r.KeywordId;
                        _new.DateCreated = r.DateCreated;
                        _new.DateUpdated = r.DateUpdated;
                        _new.ExistsInCentral = 1;

                        localContext.QuestionKeyword.Add(_new);
                    }
                    localContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}