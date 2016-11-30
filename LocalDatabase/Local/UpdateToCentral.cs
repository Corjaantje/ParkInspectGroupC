using LocalDatabase.Domain;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDatabase.Local
{
    public class UpdateToCentral
    {
        SQLiteConnection _sqliteConnection;
        DatabaseActions _sqliteActions;
        public UpdateToCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public Tuple<bool,List<UpdateMessage>> Update()
        {
            List<UpdateMessage> UpdateMessages = new List<UpdateMessage>();
            Tuple<bool, List<UpdateMessage>> action;

            #region Region Table
            Tuple<bool, List<UpdateMessage>> region = Region();
            if (region.Item1)
            {
                foreach (var item in region.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region EmployeeStatus Table
            action = EmployeeStatus();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Employee Table
            action = Employee();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Account Table
            action = Account();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Availability Table
            action = Availability();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region WorkingHours Table
            action = WorkingHours();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Customer Table
            action = Customer();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Assignment Table
            action = Assignment();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region InspectionStatus Table
            action = InspectionStatus();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Inspection Table
            action = Inspection();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region InspectionImage Table
            action = InspectionImage();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region KeywordCategory Table
            action = KeywordCategory();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Keyword Table
            action = Keyword();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Module Table
            action = Module();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region QuestionSort Table
            action = QuestionSort();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Question Table
            action = Question();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region Questionaire Table
            action = Questionaire();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region QuestionAnswer Table
            action = QuestionAnswer();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region QuestionaireModule Table
            action = QuestionaireModule();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            #region QuestionKeyword Table
            action = QuestionKeyword();
            if (action.Item1)
            {
                foreach (var item in action.Item2)
                {
                    UpdateMessages.Add(item);
                }
            }
            else
            {
                return Tuple.Create(false, UpdateMessages);
            }
            #endregion

            return Tuple.Create(true, UpdateMessages);
        }

        private Tuple<bool, List<UpdateMessage>> Region()
        {
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Region> list = new List<Domain.Region>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Regions.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Region r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Region region = (from x in context.Region where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (region.DateUpdated == r.DateUpdated)
                            {
                                region.Region1 = r.Region1;
                                region.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Region";
                                message.CentralDatabaseName = "Region";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = region.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = region.DateUpdated;
                                message.Message = "Er was een conflict bij deze region. Wat u wilt opslaan: '" + r.Region1 + "'. Wat centraal opgeslagen staat: '" + region.Region1 + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.EmployeeStatu> list = new List<Domain.EmployeeStatu>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.EmployeeStatus.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.EmployeeStatu r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.EmployeeStatus central = (from x in context.EmployeeStatus where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "EmployeeStatus";
                                message.CentralDatabaseName = "EmployeeStatus";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze werknemer status. Wat u wilt opslaan: '" + r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Employee> list = new List<Domain.Employee>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Employees.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Employee r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Employee central = (from x in context.Employee where x.Id == _id select x).First();

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
                                {
                                    central.ManagerId = null;
                                }
                                else
                                {
                                    central.ManagerId = Convert.ToInt32(r.ManagerId);
                                }
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Employee";
                                message.CentralDatabaseName = "Employee";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze werknemer. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Account> list = new List<Domain.Account>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Accounts.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Account r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Account central = (from x in context.Account where x.Id == _id select x).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Account";
                                message.CentralDatabaseName = "Account";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij dit account. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Availability> list = new List<Domain.Availability>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Availabilities.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Availability r in list)
                        {
                            int _id = Convert.ToInt32(r.EmployeeId);
                            DateTime _date = r.Date;
                            ParkInspectGroupC.DOMAIN.Availability central = (from x in context.Availabilities where x.EmployeeId == _id select x).Where(x => (x.Date == _date)).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Availability";
                                message.CentralDatabaseName = "Availability";
                                message.LocalId = Convert.ToInt32(r.EmployeeId);
                                message.CentralId = central.EmployeeId;
                                message.LocalId_DateTime = r.Date;
                                message.CentralId_DateTime = central.Date;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze beschikbaarheid. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.WorkingHour> list = new List<Domain.WorkingHour>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.WorkingHours.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.WorkingHour r in list)
                        {
                            int _id = Convert.ToInt32(r.EmployeeId);
                            DateTime _date = r.Date;
                            ParkInspectGroupC.DOMAIN.WorkingHour central = (from x in context.WorkingHours where x.EmployeeId == _id select x).Where(x => (x.Date == _date)).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "WorkingHours";
                                message.CentralDatabaseName = "WorkingHours";
                                message.LocalId = Convert.ToInt32(r.EmployeeId);
                                message.CentralId = central.EmployeeId;
                                message.LocalId_DateTime = r.Date;
                                message.CentralId_DateTime = central.Date;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze gewerkte uren. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Customer> list = new List<Domain.Customer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Customers.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Customer r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Customer central = (from x in context.Customer where x.Id == _id select x).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Customer";
                                message.CentralDatabaseName = "Customer";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze klant. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Assignment> list = new List<Domain.Assignment>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Assignments.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Assignment r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Assignment central = (from x in context.Assignment where x.Id == _id select x).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Assignment";
                                message.CentralDatabaseName = "Assignment";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze opdracht. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.InspectionStatu> list = new List<Domain.InspectionStatu>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionStatus.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.InspectionStatu r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.InspectionStatus central = (from x in context.InspectionStatus where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "InspectionStatus";
                                message.CentralDatabaseName = "InspectionStatus";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze inspectie status. Wat u wilt opslaan: '" + r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Inspection> list = new List<Domain.Inspection>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Inspections.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Inspection r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Inspection central = (from x in context.Inspections where x.Id == _id select x).First();

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
                                {
                                    central.InspectorId = null;
                                }
                                else
                                {
                                    central.InspectorId = Convert.ToInt32(r.InspectorId);
                                }
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Inspection";
                                message.CentralDatabaseName = "Inspection";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze inspectie. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.InspectionImage> list = new List<Domain.InspectionImage>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionImages.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.InspectionImage r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.InspectionImage central = (from x in context.InspectionImage where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.File = r.File;
                                if (r.InspectionId == null)
                                {
                                    central.InspectionId = null;
                                }
                                else
                                {
                                    central.InspectionId = Convert.ToInt32(r.InspectionId);
                                }
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "InspectionImage";
                                message.CentralDatabaseName = "InspectionImage";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict bij deze inspectie foto. Wat u wilt opslaan: '"+ r.File +"'. Wat centraal opgeslagen staat: '" + central.File + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.KeywordCategory> list = new List<Domain.KeywordCategory>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.KeywordCategories.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.KeywordCategory r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.KeywordCategory central = (from x in context.KeywordCategory where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "KeywordCategory";
                                message.CentralDatabaseName = "KeywordCategory";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze keyword category. Wat u wilt opslaan: '" + r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Keyword> list = new List<Domain.Keyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Keywords.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Keyword r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Keyword central = (from x in context.Keyword where x.Id == _id select x).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Keyword";
                                message.CentralDatabaseName = "Keyword";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met dit keyword. Wat u wilt opslaan: '" + r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Module> list = new List<Domain.Module>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Modules.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Module r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Module central = (from x in context.Modules where x.Id == _id select x).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Module";
                                message.CentralDatabaseName = "Module";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze module. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.QuestionSort> list = new List<Domain.QuestionSort>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionSorts.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionSort r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.QuestionSort central = (from x in context.QuestionSorts where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.Description = r.Description;
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionSort";
                                message.CentralDatabaseName = "QuestionSort";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze vraag soort. Wat u wilt opslaan: '" + r.Description + "'. Wat centraal opgeslagen staat: '" + central.Description + "'.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Question> list = new List<Domain.Question>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questions.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Question r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Question central = (from x in context.Questions where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.SortId = Convert.ToInt32(r.SortId);
                                central.Description = r.Description;
                                if (r.ModuleId == null)
                                {
                                    central.ModuleId = null;
                                }
                                else
                                {
                                    central.ModuleId = Convert.ToInt32(r.ModuleId);
                                }
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Question";
                                message.CentralDatabaseName = "Question";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze vraag. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.Questionaire> list = new List<Domain.Questionaire>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questionaires.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Questionaire r in list)
                        {
                            int _id = Convert.ToInt32(r.Id);
                            ParkInspectGroupC.DOMAIN.Questionnaire central = (from x in context.Questionnaires where x.Id == _id select x).First();

                            //Check for database concurrency
                            if (central.DateUpdated == r.DateUpdated)
                            {
                                central.InspectionId = Convert.ToInt32(r.InspectionId);
                                central.DateUpdated = DateTime.Now;
                                context.SaveChanges();
                            }
                            else
                            {
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "Questionaire";
                                message.CentralDatabaseName = "Questionnaire";
                                message.LocalId = Convert.ToInt32(r.Id);
                                message.CentralId = central.Id;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze questionnaire. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.QuestionAnswer> list = new List<Domain.QuestionAnswer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionAnswers.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionAnswer r in list)
                        {
                            int _id = Convert.ToInt32(r.QuestionnaireId);
                            int _questionId = Convert.ToInt32(r.QuestionId);
                            ParkInspectGroupC.DOMAIN.QuestionAnswer central = (from x in context.QuestionAnswer where x.QuestionnaireId == _id select x).Where(x => x.QuestionId == _questionId).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionAnswer";
                                message.CentralDatabaseName = "QuestionAnswer";
                                message.LocalId = Convert.ToInt32(r.QuestionnaireId);
                                message.CentralId = central.QuestionnaireId;
                                message.LocalId_Extra = Convert.ToInt32(r.QuestionId);
                                message.CentralId_Extra = central.QuestionId;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met dit antwoord op een vraag. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.QuestionaireModule> list = new List<Domain.QuestionaireModule>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionaireModules.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionaireModule r in list)
                        {
                            int _id = Convert.ToInt32(r.ModuleId);
                            int _questionId = Convert.ToInt32(r.QuestionaireId);
                            ParkInspectGroupC.DOMAIN.QuestionnaireModule central = (from x in context.QuestionnaireModule where x.QuestionnaireId == _questionId select x).Where(x => x.ModuleId == _id).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionaireModule";
                                message.CentralDatabaseName = "QuestionnaireModule";
                                message.LocalId = Convert.ToInt32(r.QuestionaireId);
                                message.CentralId = central.QuestionnaireId;
                                message.LocalId_Extra = Convert.ToInt32(r.ModuleId);
                                message.CentralId_Extra = central.ModuleId;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze vragen module. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
            List<UpdateMessage> UpdateList = new List<UpdateMessage>();
            try
            {
                List<Domain.QuestionKeyword> list = new List<Domain.QuestionKeyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionKeywords.Where(r => r.ExistsInCentral == 2).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionKeyword r in list)
                        {
                            int _id = Convert.ToInt32(r.QuestionId);
                            int _KeywordId = Convert.ToInt32(r.KeywordId);
                            ParkInspectGroupC.DOMAIN.QuestionKeyword central = (from x in context.QuestionKeyword where x.QuestionId == _id select x).Where(x => x.KeywordId == _KeywordId).First();

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
                                UpdateMessage message = new UpdateMessage();
                                message.LocalDatabaseName = "QuestionKeyword";
                                message.CentralDatabaseName = "QuestionKeyword";
                                message.LocalId = Convert.ToInt32(r.QuestionId);
                                message.CentralId = central.QuestionId;
                                message.LocalId_Extra = Convert.ToInt32(r.KeywordId);
                                message.CentralId_Extra = central.KeywordId;
                                message.LocalDateTime = r.DateUpdated;
                                message.CentralDateTime = central.DateUpdated;
                                message.Message = "Er was een conflict met deze vraag keyword. Open dit conflict om meer informatie te zien.";
                                UpdateList.Add(message);
                            }
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
