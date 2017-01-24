using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using LocalDatabase.Domain;
using LocalDatabase.Local;
using ParkInspectGroupC.DOMAIN;
using Account = ParkInspectGroupC.DOMAIN.Account;
using Assignment = ParkInspectGroupC.DOMAIN.Assignment;
using Availability = ParkInspectGroupC.DOMAIN.Availability;
using Coordinate = ParkInspectGroupC.DOMAIN.Coordinate;
using Customer = ParkInspectGroupC.DOMAIN.Customer;
using Employee = ParkInspectGroupC.DOMAIN.Employee;
using EmployeeStatus = ParkInspectGroupC.DOMAIN.EmployeeStatus;
using Inspection = ParkInspectGroupC.DOMAIN.Inspection;
using InspectionImage = ParkInspectGroupC.DOMAIN.InspectionImage;
using InspectionStatus = ParkInspectGroupC.DOMAIN.InspectionStatus;
using Keyword = ParkInspectGroupC.DOMAIN.Keyword;
using KeywordCategory = ParkInspectGroupC.DOMAIN.KeywordCategory;
using Module = ParkInspectGroupC.DOMAIN.Module;
using Question = ParkInspectGroupC.DOMAIN.Question;
using QuestionAnswer = ParkInspectGroupC.DOMAIN.QuestionAnswer;
using QuestionKeyword = ParkInspectGroupC.DOMAIN.QuestionKeyword;
using QuestionSort = ParkInspectGroupC.DOMAIN.QuestionSort;
using Region = ParkInspectGroupC.DOMAIN.Region;
using WorkingHours = ParkInspectGroupC.DOMAIN.WorkingHours;

namespace LocalDatabase.Central
{
    public class GetFromCentral
    {
        private readonly DatabaseActions _sqliteActions;
        private readonly SQLiteConnection _sqliteConnection;

        public GetFromCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public bool Sync(string dbName)
        {
            if (ClearLocal() && CreateLocal(dbName) && GetDataAndSaveToLocal())
                return true;

            return false;
        }

        private bool ClearLocal()
        {
            try
            {
                var sql = "DROP TABLE QuestionKeyword;" +
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
            var createDB = new CreateSQLiteDatabase();
            return createDB.Create(_sqliteActions, dbName, true);
                //When true database is created and thus exists, when false creating database failed
        }

        private bool GetDataAndSaveToLocal()
        {
            var action = false;

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
            var list = new List<Region>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Region)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Region();
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
            var list = new List<EmployeeStatus>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.EmployeeStatus)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.EmployeeStatus();
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
            var list = new List<Employee>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Employee)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Employee();
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
            var list = new List<Account>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Account)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Account();
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
            var list = new List<Availability>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Availability)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Availability();
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
            var list = new List<WorkingHours>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.WorkingHours)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.WorkingHours();
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
            var list = new List<Customer>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Customer)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Customer();
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
            var list = new List<Assignment>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Assignment)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Assignment();
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
            var list = new List<InspectionStatus>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.InspectionStatus)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.InspectionStatus();
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
            var list = new List<Inspection>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Inspection)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Inspection();
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
            var list = new List<InspectionImage>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.InspectionImage)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.InspectionImage();
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
            var list = new List<Coordinate>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Coordinate)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Coordinate();
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
            var list = new List<KeywordCategory>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.KeywordCategory)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.KeywordCategory();
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
            var list = new List<Keyword>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Keyword)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Keyword();
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
            var list = new List<Module>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Module)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Module();
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
            var list = new List<QuestionSort>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.QuestionSort)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.QuestionSort();
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
            var list = new List<Question>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Question)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.Question();
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
            var list = new List<Questionnaire>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.Questionnaire)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Questionaire();
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
            var list = new List<QuestionAnswer>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.QuestionAnswer)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.QuestionAnswer();
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
            var list = new List<QuestionnaireModule>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.QuestionnaireModule)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new QuestionaireModule();
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
            var list = new List<QuestionKeyword>();

            try
            {
                using (var context = new ParkInspectEntities())
                {
                    foreach (var r in context.QuestionKeyword)
                        list.Add(r);
                }

                using (var localContext = new LocalParkInspectEntities())
                {
                    foreach (var r in list)
                    {
                        var _new = new Domain.QuestionKeyword();
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