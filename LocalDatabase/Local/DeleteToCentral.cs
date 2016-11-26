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
    public class DeleteToCentral
    {
        SQLiteConnection _sqliteConnection;
        DatabaseActions _sqliteActions;
        public DeleteToCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public List<string> Save()
        {
            //Start deleting records in the central database, table for table
            string action = "";
            List<string> _temp = new List<string>();
            List<string> messages = new List<string>();

            action = QuestionKeyword();
            _temp.Add(action);

            action = QuestionaireModule();
            _temp.Add(action);

            action = QuestionAnswer();
            _temp.Add(action);

            action = Questionaire();
            _temp.Add(action);

            action = Question();
            _temp.Add(action);

            action = QuestionSort();
            _temp.Add(action);

            action = Module();
            _temp.Add(action);

            action = Keyword();
            _temp.Add(action);

            action = KeywordCategory();
            _temp.Add(action);

            action = InspectionImage();
            _temp.Add(action);

            action = Inspection();
            _temp.Add(action);

            action = InspectionStatus();
            _temp.Add(action);

            action = Assignment();
            _temp.Add(action);

            action = Customer();
            _temp.Add(action);

            action = WorkingHours();
            _temp.Add(action);

            action = Availability();
            _temp.Add(action);

            action = Account();
            _temp.Add(action);

            action = Employee();
            _temp.Add(action);

            action = EmployeeStatus();
            _temp.Add(action);

            action = Region();
            _temp.Add(action);

            foreach (string m in _temp)
            {
                if (m != "true")
                {
                    messages.Add(m);
                }
            }

            return messages;
        }

        #region Database tables
        private string QuestionKeyword()
        {
            try
            {
                List<Domain.QuestionKeyword> list = new List<Domain.QuestionKeyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionKeywords.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionKeyword r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionKeyword _new = new ParkInspectGroupC.DOMAIN.QuestionKeyword();
                            _new.KeywordId = Convert.ToInt32(r.KeywordId);
                            _new.QuestionId = Convert.ToInt32(r.QuestionId);
                            context.QuestionKeyword.Attach(_new);
                            context.QuestionKeyword.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon question keyword(s) niet verwijderen! Wordt het keyword wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string QuestionaireModule()
        {
            try
            {
                List<Domain.QuestionaireModule> list = new List<Domain.QuestionaireModule>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionaireModules.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionaireModule r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionnaireModule _new = new ParkInspectGroupC.DOMAIN.QuestionnaireModule();
                            _new.ModuleId = Convert.ToInt32(r.ModuleId);
                            _new.QuestionnaireId = Convert.ToInt32(r.QuestionaireId);
                            context.QuestionnaireModule.Attach(_new);
                            context.QuestionnaireModule.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon question module(s) niet verwijderen! Wordt het module wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string QuestionAnswer()
        {
            try
            {
                List<Domain.QuestionAnswer> list = new List<Domain.QuestionAnswer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionAnswers.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionAnswer r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionAnswer _new = new ParkInspectGroupC.DOMAIN.QuestionAnswer();
                            _new.QuestionId = Convert.ToInt32(r.QuestionId);
                            _new.QuestionnaireId = Convert.ToInt32(r.QuestionnaireId);
                            context.QuestionAnswer.Attach(_new);
                            context.QuestionAnswer.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon question antwoord(en) niet verwijderen! Wordt het antwoord wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Questionaire()
        {
            try
            {
                List<Domain.Questionaire> list = new List<Domain.Questionaire>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questionaires.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Questionaire r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Questionnaire _new = new ParkInspectGroupC.DOMAIN.Questionnaire();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Questionnaires.Attach(_new);
                            context.Questionnaires.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon questionnaire niet verwijderen! Wordt het questionnaire wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Question()
        {
            try
            {
                List<Domain.Question> list = new List<Domain.Question>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questions.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Question r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Question _new = new ParkInspectGroupC.DOMAIN.Question();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Questions.Attach(_new);
                            context.Questions.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon question niet verwijderen! Wordt de question wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string QuestionSort()
        {
            try
            {
                List<Domain.QuestionSort> list = new List<Domain.QuestionSort>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionSorts.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.QuestionSort r in list)
                        {
                            ParkInspectGroupC.DOMAIN.QuestionSort _new = new ParkInspectGroupC.DOMAIN.QuestionSort();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.QuestionSorts.Attach(_new);
                            context.QuestionSorts.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon question sort niet verwijderen! Wordt het question sort wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Module()
        {
            try
            {
                List<Domain.Module> list = new List<Domain.Module>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Modules.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Module r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Module _new = new ParkInspectGroupC.DOMAIN.Module();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Modules.Attach(_new);
                            context.Modules.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon de module(s) niet verwijderen! Wordt het module wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Keyword()
        {
            try
            {
                List<Domain.Keyword> list = new List<Domain.Keyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Keywords.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Keyword r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Keyword _new = new ParkInspectGroupC.DOMAIN.Keyword();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Keyword.Attach(_new);
                            context.Keyword.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon keyword(s) niet verwijderen! Wordt het keyword wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string KeywordCategory()
        {
            try
            {
                List<Domain.KeywordCategory> list = new List<Domain.KeywordCategory>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.KeywordCategories.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.KeywordCategory r in list)
                        {
                            ParkInspectGroupC.DOMAIN.KeywordCategory _new = new ParkInspectGroupC.DOMAIN.KeywordCategory();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.KeywordCategory.Attach(_new);
                            context.KeywordCategory.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon keyword categorie niet verwijderen! Wordt de categorie wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string InspectionImage()
        {
            try
            {
                List<Domain.InspectionImage> list = new List<Domain.InspectionImage>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionImages.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.InspectionImage r in list)
                        {
                            ParkInspectGroupC.DOMAIN.InspectionImage _new = new ParkInspectGroupC.DOMAIN.InspectionImage();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.InspectionImage.Attach(_new);
                            context.InspectionImage.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon de foto niet verwijderen! Wordt de foto die u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Inspection()
        {
            try
            {
                List<Domain.Inspection> list = new List<Domain.Inspection>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Inspections.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Inspection r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Inspection _new = new ParkInspectGroupC.DOMAIN.Inspection();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Inspections.Attach(_new);
                            context.Inspections.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon inspectie(s) niet verwijderen! Wordt de inspectie die u wou weggooien nog ergens gebruikt?";
            }
        }
        private string InspectionStatus()
        {
            try
            {
                List<Domain.InspectionStatu> list = new List<Domain.InspectionStatu>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionStatus.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.InspectionStatu r in list)
                        {
                            ParkInspectGroupC.DOMAIN.InspectionStatus _new = new ParkInspectGroupC.DOMAIN.InspectionStatus();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.InspectionStatus.Attach(_new);
                            context.InspectionStatus.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon inspectie status niet verwijderen! Wordt de status die u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Assignment()
        {
            try
            {
                List<Domain.Assignment> list = new List<Domain.Assignment>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Assignments.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Assignment r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Assignment _new = new ParkInspectGroupC.DOMAIN.Assignment();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Assignment.Attach(_new);
                            context.Assignment.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon opdracht niet verwijderen! Wordt de opdracht wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Customer()
        {
            try
            {
                List<Domain.Customer> list = new List<Domain.Customer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Customers.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Customer r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Customer _new = new ParkInspectGroupC.DOMAIN.Customer();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Customer.Attach(_new);
                            context.Customer.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon klant niet verwijderen! Wordt de klant die u wou weggooien nog ergens gebruikt?";
            }
        }
        private string WorkingHours()
        {
            try
            {
                List<Domain.WorkingHour> list = new List<Domain.WorkingHour>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.WorkingHours.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.WorkingHour r in list)
                        {
                            ParkInspectGroupC.DOMAIN.WorkingHour _new = new ParkInspectGroupC.DOMAIN.WorkingHour();
                            _new.EmployeeId = Convert.ToInt32(r.EmployeeId);
                            _new.Date = r.Date;
                            context.WorkingHours.Attach(_new);
                            context.WorkingHours.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon werk uren niet verwijderen! Worden de werk uren die u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Availability()
        {
            try
            {
                List<Domain.Availability> list = new List<Domain.Availability>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Availabilities.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Availability r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Availability _new = new ParkInspectGroupC.DOMAIN.Availability();
                            _new.EmployeeId = Convert.ToInt32(r.EmployeeId);
                            _new.Date = r.Date;
                            context.Availabilities.Attach(_new);
                            context.Availabilities.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon beschikbaarheid niet verwijderen! Wordt de beschikbaarheid wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Account()
        {
            try
            {
                List<Domain.Account> list = new List<Domain.Account>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Accounts.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Account r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Account _new = new ParkInspectGroupC.DOMAIN.Account();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Account.Attach(_new);
                            context.Account.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon account niet verwijderen! Wordt het account wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Employee()
        {
            try
            {
                List<Domain.Employee> list = new List<Domain.Employee>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Employees.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Employee r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Employee _new = new ParkInspectGroupC.DOMAIN.Employee();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Employee.Attach(_new);
                            context.Employee.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon werknemer niet verwijderen! Wordt de werknemer wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string EmployeeStatus()
        {
            try
            {
                List<Domain.EmployeeStatu> list = new List<Domain.EmployeeStatu>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.EmployeeStatus.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.EmployeeStatu r in list)
                        {
                            ParkInspectGroupC.DOMAIN.EmployeeStatus _new = new ParkInspectGroupC.DOMAIN.EmployeeStatus();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.EmployeeStatus.Attach(_new);
                            context.EmployeeStatus.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon werknemer status niet verwijderen! Wordt de status wat u wou weggooien nog ergens gebruikt?";
            }
        }
        private string Region()
        {
            try
            {
                List<Domain.Region> list = new List<Domain.Region>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Regions.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                {
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (Domain.Region r in list)
                        {
                            ParkInspectGroupC.DOMAIN.Region _new = new ParkInspectGroupC.DOMAIN.Region();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Region.Attach(_new);
                            context.Region.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                }
                return "true";
            }
            catch (Exception)
            {
                return "Kon region niet verwijderen! Wordt het region wat u wou weggooien nog ergens gebruikt?";
            }
        }
        #endregion
    }
}