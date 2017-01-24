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
    public class UpdateToCentral
    {
        private DatabaseActions _sqliteActions;
        private SQLiteConnection _sqliteConnection;

        public UpdateToCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public Tuple<bool, List<UpdateMessage>> Update()
        {
            var UpdateMessages = new List<UpdateMessage>();
            Tuple<bool, List<UpdateMessage>> action;

            #region Region Table

            var region = Region();
            if (region.Item1)
                foreach (var item in region.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region EmployeeStatus Table

            action = EmployeeStatus();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Employee Table

            action = Employee();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Account Table

            action = Account();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Availability Table

            action = Availability();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region WorkingHours Table

            action = WorkingHours();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Customer Table

            action = Customer();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Assignment Table

            action = Assignment();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region InspectionStatus Table

            action = InspectionStatus();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Inspection Table

            action = Inspection();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Coordinate Table

            action = Coordinate();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region InspectionImage Table

            action = InspectionImage();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region KeywordCategory Table

            action = KeywordCategory();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Keyword Table

            action = Keyword();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Module Table

            action = Module();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region QuestionSort Table

            action = QuestionSort();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Question Table

            action = Question();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region Questionaire Table

            action = Questionaire();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region QuestionAnswer Table

            action = QuestionAnswer();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region QuestionaireModule Table

            action = QuestionaireModule();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            #region QuestionKeyword Table

            action = QuestionKeyword();
            if (action.Item1)
                foreach (var item in action.Item2)
                    UpdateMessages.Add(item);
            else
                return Tuple.Create(false, UpdateMessages);

            #endregion

            return Tuple.Create(true, UpdateMessages);
        }

        private Tuple<bool, List<UpdateMessage>> Region()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Region>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Region.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var region = (from x in context.Region where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (region.DateUpdated == r.DateUpdated)
                            {
                                region.Region1 = r.Region1;
                                region.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Region";
                                message.CentralDatabaseName = "Region";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = region.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = region.DateUpdated;
                                message.Message = "Er was een conflict bij deze region. Wat u wilt opslaan: '" +
                                                  r.Region1 + "'. Wat centraal opgeslagen staat: '" + region.Region1 +
                                                  "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> EmployeeStatus()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<EmployeeStatus>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.EmployeeStatus.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.EmployeeStatus where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "EmployeeStatus";
                                message.CentralDatabaseName = "EmployeeStatus";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze werknemer status. Wat u wilt opslaan: '" +
                                    r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Employee()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Employee>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Employee.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Employee where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.FirstName = r.FirstName;
                                central.Prefix = r.Prefix;
                                central.SurName = r.SurName;
                                central.Gender = r.Gender;
                                central.City = r.City;
                                central.Address = r.Address;
                                central.ZipCode = r.ZipCode;
                                central.Phonenumber = r.Phonenumber;
                                central.Email = r.Email;
                                central.RegionId = Convert.ToInt32(r.RegionId);
                                central.EmployeeStatusId = Convert.ToInt32(r.EmployeeStatusId);
                                central.IsInspecter = r.IsInspecter;
                                central.IsManager = r.IsManager;
                                if (r.ManagerId == null)
                                    central.ManagerId = null;
                                else
                                    central.ManagerId = Convert.ToInt32(r.ManagerId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Employee";
                                message.CentralDatabaseName = "Employee";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze werknemer. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Account()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Account>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Account.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Account where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Username = r.Username;
                                central.Password = r.Password;
                                central.UserGuid = r.UserGuid;
                                central.EmployeeId = Convert.ToInt32(r.EmployeeId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Account";
                                message.CentralDatabaseName = "Account";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij dit account. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Availability()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Availability>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Availability.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.EmployeeId);
                            var _date = r.Date;
                            var central =
                                (from x in context.Availability where x.EmployeeId == _id select x).Where(
                                    x => x.Date == _date).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.EmployeeId = Convert.ToInt32(r.EmployeeId);
                                central.Date = r.Date;
                                central.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                                central.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Availability";
                                message.CentralDatabaseName = "Availability";
                                message.LocalId = Convert.ToInt32(r.EmployeeId);
                                message.CentralId = central.EmployeeId;
                                message.LocalId_DateTime = r.Date;
                                message.CentralId_DateTime = central.Date;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze beschikbaarheid. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> WorkingHours()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<WorkingHours>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.WorkingHours.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.EmployeeId);
                            var _date = r.Date;
                            var central =
                                (from x in context.WorkingHours where x.EmployeeId == _id select x).Where(
                                    x => x.Date == _date).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.EmployeeId = Convert.ToInt32(r.EmployeeId);
                                central.Date = r.Date;
                                central.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                                central.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "WorkingHours";
                                message.CentralDatabaseName = "WorkingHours";
                                message.LocalId = Convert.ToInt32(r.EmployeeId);
                                message.CentralId = central.EmployeeId;
                                message.LocalId_DateTime = r.Date;
                                message.CentralId_DateTime = central.Date;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze gewerkte uren. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Customer()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Customer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Customer.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Customer where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Name = r.Name;
                                central.Address = r.Address;
                                central.Location = r.Location;
                                central.Phonenumber = r.Phonenumber;
                                central.Email = r.Email;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Customer";
                                message.CentralDatabaseName = "Customer";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze klant. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Assignment()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Assignment>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Assignment.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Assignment where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.CustomerId = Convert.ToInt32(r.CustomerId);
                                central.ManagerId = Convert.ToInt32(r.ManagerId);
                                central.Description = r.Description;
                                central.StartDate = r.StartDate;
                                central.EndDate = r.EndDate;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Assignment";
                                message.CentralDatabaseName = "Assignment";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze opdracht. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> InspectionStatus()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<InspectionStatus>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionStatus.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.InspectionStatus where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "InspectionStatus";
                                message.CentralDatabaseName = "InspectionStatus";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze inspectie status. Wat u wilt opslaan: '" +
                                    r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Inspection()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Inspection>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Inspection.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Inspection where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.AssignmentId = Convert.ToInt32(r.AssignmentId);
                                central.RegionId = Convert.ToInt32(r.RegionId);
                                central.Location = r.Location;
                                central.StartDate = r.StartDate;
                                central.EndDate = r.EndDate;
                                central.StatusId = Convert.ToInt32(r.StatusId);
                                if (r.InspectorId == null)
                                    central.InspectorId = null;
                                else
                                    central.InspectorId = Convert.ToInt32(r.InspectorId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Inspection";
                                message.CentralDatabaseName = "Inspection";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze inspectie. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Coordinate()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Coordinate>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Coordinate.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Coordinate where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Longitude = r.Longitude;
                                central.Latitude = r.Latitude;
                                central.Note = r.Note;
                                central.InspectionId = Convert.ToInt32(r.InspectionId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Coordinate";
                                message.CentralDatabaseName = "Coordinate";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict bij deze coordinaten. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> InspectionImage()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<InspectionImage>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionImage.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.InspectionImage where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.File = r.File;
                                if (r.InspectionId == null)
                                    central.InspectionId = null;
                                else
                                    central.InspectionId = Convert.ToInt32(r.InspectionId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "InspectionImage";
                                message.CentralDatabaseName = "InspectionImage";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze inspectie foto. Wat u wilt opslaan: '" +
                                                  r.File + "'. Wat centraal opgeslagen staat: '" + central.File + "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> KeywordCategory()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<KeywordCategory>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.KeywordCategory.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.KeywordCategory where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "KeywordCategory";
                                message.CentralDatabaseName = "KeywordCategory";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met deze keyword category. Wat u wilt opslaan: '" +
                                    r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Keyword()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Keyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Keyword.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Keyword where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.CategoryId = Convert.ToInt32(r.CategoryId);
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Keyword";
                                message.CentralDatabaseName = "Keyword";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met dit keyword. Wat u wilt opslaan: '" +
                                                  r.Description + "'. Wat centraal opgeslagen staat: '" +
                                                  central.Description + "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Module()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Module>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Module.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Module where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Name = r.Name;
                                central.Description = r.Description;
                                central.Note = r.Note;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Module";
                                message.CentralDatabaseName = "Module";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met deze module. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> QuestionSort()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<QuestionSort>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionSort.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.QuestionSort where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionSort";
                                message.CentralDatabaseName = "QuestionSort";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze vraag soort. Wat u wilt opslaan: '" +
                                                  r.Description + "'. Wat centraal opgeslagen staat: '" +
                                                  central.Description + "'.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Question()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Question>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Question.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Question where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.SortId = Convert.ToInt32(r.SortId);
                                central.Description = r.Description;
                                if (r.ModuleId == null)
                                    central.ModuleId = null;
                                else
                                    central.ModuleId = Convert.ToInt32(r.ModuleId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Question";
                                message.CentralDatabaseName = "Question";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met deze vraag. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> Questionaire()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<Questionaire>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questionaire.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.Id);
                            var central = (from x in context.Questionnaire where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.InspectionId = Convert.ToInt32(r.InspectionId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "Questionaire";
                                message.CentralDatabaseName = "Questionnaire";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met deze questionnaire. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> QuestionAnswer()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<QuestionAnswer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionAnswer.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.QuestionnaireId);
                            var _questionId = Convert.ToInt32(r.QuestionId);
                            var central =
                                (from x in context.QuestionAnswer where x.QuestionnaireId == _id select x).Where(
                                    x => x.QuestionId == _questionId).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.QuestionnaireId = Convert.ToInt32(r.QuestionnaireId);
                                central.QuestionId = Convert.ToInt32(r.QuestionId);
                                central.Result = r.Result;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionAnswer";
                                message.CentralDatabaseName = "QuestionAnswer";
                                message.LocalId = Convert.ToInt32(r.QuestionnaireId);
                                message.CentralId = central.QuestionnaireId;
                                message.LocalId_Extra = Convert.ToInt32(r.QuestionId);
                                message.CentralId_Extra = central.QuestionId;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met dit antwoord op een vraag. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> QuestionaireModule()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<QuestionaireModule>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionaireModule.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.ModuleId);
                            var _questionId = Convert.ToInt32(r.QuestionaireId);
                            var central =
                                (from x in context.QuestionnaireModule where x.QuestionnaireId == _questionId select x)
                                    .Where(x => x.ModuleId == _id).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.QuestionnaireId = Convert.ToInt32(r.QuestionaireId);
                                central.ModuleId = Convert.ToInt32(r.ModuleId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionaireModule";
                                message.CentralDatabaseName = "QuestionnaireModule";
                                message.LocalId = Convert.ToInt32(r.QuestionaireId);
                                message.CentralId = central.QuestionnaireId;
                                message.LocalId_Extra = Convert.ToInt32(r.ModuleId);
                                message.CentralId_Extra = central.ModuleId;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met deze vragen module. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }

        private Tuple<bool, List<UpdateMessage>> QuestionKeyword()
        {
            var UpdateList = new List<UpdateMessage>();
            try
            {
                var list = new List<QuestionKeyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionKeyword.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _id = Convert.ToInt32(r.QuestionId);
                            var _KeywordId = Convert.ToInt32(r.KeywordId);
                            var central =
                                (from x in context.QuestionKeyword where x.QuestionId == _id select x).Where(
                                    x => x.KeywordId == _KeywordId).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.QuestionId = Convert.ToInt32(r.QuestionId);
                                central.KeywordId = Convert.ToInt32(r.KeywordId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                var message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionKeyword";
                                message.CentralDatabaseName = "QuestionKeyword";
                                message.LocalId = Convert.ToInt32(r.QuestionId);
                                message.CentralId = central.QuestionId;
                                message.LocalId_Extra = Convert.ToInt32(r.KeywordId);
                                message.CentralId_Extra = central.KeywordId;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message =
                                    "Er was een conflict met deze vraag keyword. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
                        }
                    }
                return Tuple.Create(true, UpdateList);
            }
            catch (Exception)
            {
                return Tuple.Create(false, UpdateList);
            }
        }
    }
}