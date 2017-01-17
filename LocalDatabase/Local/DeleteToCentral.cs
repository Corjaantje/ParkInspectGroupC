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
    public class DeleteToCentral
    {
        private DatabaseActions _sqliteActions;
        private SQLiteConnection _sqliteConnection;

        public DeleteToCentral(SQLiteConnection conn, DatabaseActions actions)
        {
            _sqliteConnection = conn;
            _sqliteActions = actions;
        }

        public List<string> Save()
        {
            //Start deleting records in the central database, table for table
            var action = "";
            var _temp = new List<string>();
            var messages = new List<string>();

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

            action = Coordinate();
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

            foreach (var m in _temp)
                if (m != "true")
                    messages.Add(m);

            return messages;
        }

        #region Database tables

        private string QuestionKeyword()
        {
            try
            {
                var list = new List<QuestionKeyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionKeyword.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.QuestionKeyword();
                            _new.KeywordId = Convert.ToInt32(r.KeywordId);
                            _new.QuestionId = Convert.ToInt32(r.QuestionId);
                            context.QuestionKeyword.Attach(_new);
                            context.QuestionKeyword.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon question keyword(s) niet verwijderen! Wordt het keyword wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string QuestionaireModule()
        {
            try
            {
                var list = new List<QuestionaireModule>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionaireModule.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new QuestionnaireModule();
                            _new.ModuleId = Convert.ToInt32(r.ModuleId);
                            _new.QuestionnaireId = Convert.ToInt32(r.QuestionaireId);
                            context.QuestionnaireModule.Attach(_new);
                            context.QuestionnaireModule.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon question module(s) niet verwijderen! Wordt het module wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string QuestionAnswer()
        {
            try
            {
                var list = new List<QuestionAnswer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionAnswer.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.QuestionAnswer();
                            _new.QuestionId = Convert.ToInt32(r.QuestionId);
                            _new.QuestionnaireId = Convert.ToInt32(r.QuestionnaireId);
                            context.QuestionAnswer.Attach(_new);
                            context.QuestionAnswer.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon question antwoord(en) niet verwijderen! Wordt het antwoord wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string Questionaire()
        {
            try
            {
                var list = new List<Questionaire>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Questionaire.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new Questionnaire();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Questionnaire.Attach(_new);
                            context.Questionnaire.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon questionnaire niet verwijderen! Wordt het questionnaire wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string Question()
        {
            try
            {
                var list = new List<Question>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Question.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Question();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Question.Attach(_new);
                            context.Question.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<QuestionSort>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.QuestionSort.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.QuestionSort();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.QuestionSort.Attach(_new);
                            context.QuestionSort.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon question sort niet verwijderen! Wordt het question sort wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string Module()
        {
            try
            {
                var list = new List<Module>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Module.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Module();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Module.Attach(_new);
                            context.Module.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Keyword>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Keyword.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Keyword();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Keyword.Attach(_new);
                            context.Keyword.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<KeywordCategory>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.KeywordCategory.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.KeywordCategory();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.KeywordCategory.Attach(_new);
                            context.KeywordCategory.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon keyword categorie niet verwijderen! Wordt de categorie wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string InspectionImage()
        {
            try
            {
                var list = new List<InspectionImage>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionImage.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.InspectionImage();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.InspectionImage.Attach(_new);
                            context.InspectionImage.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return "Kon de foto niet verwijderen! Wordt de foto die u wou weggooien nog ergens gebruikt?";
            }
        }

        private string Coordinate()
        {
            try
            {
                var list = new List<Coordinate>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Coordinate.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Coordinate();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Coordinate.Attach(_new);
                            context.Coordinate.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon de coordinaten niet verwijderen! Worden de coordinaten die u wou weggooien nog ergens gebruikt?";
            }
        }

        private string Inspection()
        {
            try
            {
                var list = new List<Inspection>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Inspection.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Inspection();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Inspection.Attach(_new);
                            context.Inspection.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<InspectionStatus>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.InspectionStatus.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.InspectionStatus();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.InspectionStatus.Attach(_new);
                            context.InspectionStatus.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Assignment>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Assignment.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Assignment();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Assignment.Attach(_new);
                            context.Assignment.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Customer>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Customer.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Customer();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Customer.Attach(_new);
                            context.Customer.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<WorkingHours>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.WorkingHours.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.WorkingHours();
                            _new.EmployeeId = Convert.ToInt32(r.EmployeeId);
                            _new.Date = r.Date;
                            context.WorkingHours.Attach(_new);
                            context.WorkingHours.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Availability>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Availability.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Availability();
                            _new.EmployeeId = Convert.ToInt32(r.EmployeeId);
                            _new.Date = r.Date;
                            context.Availability.Attach(_new);
                            context.Availability.Remove(_new);
                        }
                        context.SaveChanges();
                    }
                return "true";
            }
            catch (Exception)
            {
                return
                    "Kon beschikbaarheid niet verwijderen! Wordt de beschikbaarheid wat u wou weggooien nog ergens gebruikt?";
            }
        }

        private string Account()
        {
            try
            {
                var list = new List<Account>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Account.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Account();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Account.Attach(_new);
                            context.Account.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Employee>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Employee.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Employee();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Employee.Attach(_new);
                            context.Employee.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<EmployeeStatus>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.EmployeeStatus.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.EmployeeStatus();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.EmployeeStatus.Attach(_new);
                            context.EmployeeStatus.Remove(_new);
                        }
                        context.SaveChanges();
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
                var list = new List<Region>();

                using (var localcontext = new LocalParkInspectEntities())
                {
                    list = localcontext.Region.Where(r => r.ExistsInCentral == 3).ToList();
                }

                if (list.Count > 0)
                    using (var context = new ParkInspectEntities())
                    {
                        foreach (var r in list)
                        {
                            var _new = new ParkInspectGroupC.DOMAIN.Region();
                            _new.Id = Convert.ToInt32(r.Id);
                            context.Region.Attach(_new);
                            context.Region.Remove(_new);
                        }
                        context.SaveChanges();
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