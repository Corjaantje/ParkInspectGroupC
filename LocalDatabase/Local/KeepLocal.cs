using LocalDatabase.Domain;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDatabase.Local
{
    public class KeepLocal
    {
        public bool Region(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Region r = (from x in context.Region where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Region central = (from x in centralContext.Region where x.Id == r.Id select x).First();

                        central.Region1 = r.Region1;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EmployeeStatus(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.EmployeeStatus r = (from x in context.EmployeeStatus where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.EmployeeStatus central = (from x in centralContext.EmployeeStatus where x.Id == r.Id select x).First();

                        central.Description = r.Description;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Employee(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Employee r = (from x in context.Employee where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Employee central = (from x in centralContext.Employee where x.Id == r.Id select x).First();

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
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Account(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Account r = (from x in context.Account where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Account central = (from x in centralContext.Account where x.Id == r.Id select x).First();

                        central.Username = r.Username;
                        central.Password = r.Password;
                        central.UserGuid = r.UserGuid;
                        central.EmployeeId = Convert.ToInt32(r.EmployeeId);
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Availability(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Availability r = (from x in context.Availability where x.EmployeeId == message.LocalId select x).Where(x => x.Date == message.LocalId_DateTime).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        DateTime _date = r.Date;
                        ParkInspectGroupC.DOMAIN.Availability central = (from x in centralContext.Availability where x.EmployeeId == r.EmployeeId select x).Where(x => (x.Date == _date)).First();

                        central.EmployeeId = Convert.ToInt32(r.EmployeeId);
                        central.Date = r.Date;
                        central.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                        central.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool WorkingHours(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.WorkingHours r = (from x in context.WorkingHours where x.EmployeeId == message.LocalId select x).Where(x => x.Date == message.LocalId_DateTime).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        DateTime _date = r.Date;
                        ParkInspectGroupC.DOMAIN.WorkingHours central = (from x in centralContext.WorkingHours where x.EmployeeId == r.EmployeeId select x).Where(x => (x.Date == _date)).First();

                        central.EmployeeId = Convert.ToInt32(r.EmployeeId);
                        central.Date = r.Date;
                        central.StartTime = Convert.ToDateTime(r.StartTime).TimeOfDay;
                        central.EndTime = Convert.ToDateTime(r.EndTime).TimeOfDay;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Customer(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Customer r = (from x in context.Customer where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Customer central = (from x in centralContext.Customer where x.Id == r.Id select x).First();

                        central.Name = r.Name;
                        central.Address = r.Address;
                        central.Location = r.Location;
                        central.Phonenumber = r.Phonenumber;
                        central.Email = r.Email;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Assignment(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Assignment r = (from x in context.Assignment where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Assignment central = (from x in centralContext.Assignment where x.Id == r.Id select x).First();

                        central.CustomerId = Convert.ToInt32(r.CustomerId);
                        central.ManagerId = Convert.ToInt32(r.ManagerId);
                        central.Description = r.Description;
                        central.StartDate = r.StartDate;
                        central.EndDate = r.EndDate;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InspectionStatus(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.InspectionStatus r = (from x in context.InspectionStatus where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.InspectionStatus central = (from x in centralContext.InspectionStatus where x.Id == r.Id select x).First();

                        central.Description = r.Description;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Coordinate(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Coordinate r = (from x in context.Coordinate where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Coordinate central = (from x in centralContext.Coordinate where x.Id == r.Id select x).First();

                        central.Longitude = r.Longitude;
                        central.Latitude = r.Latitude;
                        central.Note = r.Note;
                        central.InspectionId = Convert.ToInt32(r.InspectionId);
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Inspection(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Inspection r = (from x in context.Inspection where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Inspection central = (from x in centralContext.Inspection where x.Id == r.Id select x).First();

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
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InspectionImage(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.InspectionImage r = (from x in context.InspectionImage where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.InspectionImage central = (from x in centralContext.InspectionImage where x.Id == r.Id select x).First();

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
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool KeywordCategory(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.KeywordCategory r = (from x in context.KeywordCategory where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.KeywordCategory central = (from x in centralContext.KeywordCategory where x.Id == r.Id select x).First();

                        central.Description = r.Description;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Keyword(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Keyword r = (from x in context.Keyword where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Keyword central = (from x in centralContext.Keyword where x.Id == r.Id select x).First();

                        central.CategoryId = Convert.ToInt32(r.CategoryId);
                        central.Description = r.Description;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Module(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Module r = (from x in context.Module where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Module central = (from x in centralContext.Module where x.Id == r.Id select x).First();

                        central.Name = r.Name;
                        central.Description = r.Description;
                        central.Note = r.Note;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool QuestionSort(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.QuestionSort r = (from x in context.QuestionSort where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.QuestionSort central = (from x in centralContext.QuestionSort where x.Id == r.Id select x).First();

                        central.Description = r.Description;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Question(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Question r = (from x in context.Question where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Question central = (from x in centralContext.Question where x.Id == r.Id select x).First();

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
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Questionaire(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.Questionaire r = (from x in context.Questionaire where x.Id == message.LocalId select x).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        ParkInspectGroupC.DOMAIN.Questionnaire central = (from x in centralContext.Questionnaire where x.Id == r.Id select x).First();

                        central.InspectionId = Convert.ToInt32(r.InspectionId);
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool QuestionAnswer(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.QuestionAnswer r = (from x in context.QuestionAnswer where x.QuestionnaireId == message.LocalId select x).Where(x => x.QuestionId == message.LocalId_Extra).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        int _id = Convert.ToInt32(r.QuestionnaireId);
                        int _questionId = Convert.ToInt32(r.QuestionId);
                        ParkInspectGroupC.DOMAIN.QuestionAnswer central = (from x in centralContext.QuestionAnswer where x.QuestionnaireId == _id select x).Where(x => x.QuestionId == _questionId).First();

                        central.QuestionnaireId = Convert.ToInt32(r.QuestionnaireId);
                        central.QuestionId = Convert.ToInt32(r.QuestionId);
                        central.Result = r.Result;
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool GetQuestionaireModule(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.QuestionaireModule r = (from x in context.QuestionaireModule where x.QuestionaireId == message.LocalId select x).Where(x => x.ModuleId == message.LocalId_Extra).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        int _id = Convert.ToInt32(r.ModuleId);
                        int _questionId = Convert.ToInt32(r.QuestionaireId);
                        ParkInspectGroupC.DOMAIN.QuestionnaireModule central = (from x in centralContext.QuestionnaireModule where x.QuestionnaireId == _questionId select x).Where(x => x.ModuleId == _id).First();

                        central.QuestionnaireId = Convert.ToInt32(r.QuestionaireId);
                        central.ModuleId = Convert.ToInt32(r.ModuleId);
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool QuestionKeyword(UpdateMessage message)
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    Domain.QuestionKeyword r = (from x in context.QuestionKeyword where x.QuestionId == message.LocalId select x).Where(x => x.KeywordId == message.LocalId_Extra).First();

                    using (var centralContext = new ParkInspectEntities())
                    {
                        int _id = Convert.ToInt32(r.QuestionId);
                        int _KeywordId = Convert.ToInt32(r.KeywordId);
                        ParkInspectGroupC.DOMAIN.QuestionKeyword central = (from x in centralContext.QuestionKeyword where x.QuestionId == _id select x).Where(x => x.KeywordId == _KeywordId).First();

                        central.QuestionId = Convert.ToInt32(r.QuestionId);
                        central.KeywordId = Convert.ToInt32(r.KeywordId);
                        central.DateUpdated = DateTime.Now;
                        centralContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
