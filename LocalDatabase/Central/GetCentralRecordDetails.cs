using LocalDatabase.Domain;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Linq;

namespace LocalDatabase.Central
{
    public class GetCentralRecordDetails
    {
        public string GetRegion(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Region record = (from x in context.Regions where x.Id == message.CentralId select x).First();

                msg = "Regio: " + record.Region1;
            }

            return msg;
        }
        public string GetEmployeeStatus(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.EmployeeStatu record = (from x in context.EmployeeStatus where x.Id == message.CentralId select x).First();

                msg = "Status: " + record.Description;
            }

            return msg;
        }
        public string GetEmployee(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Employee record = (from x in context.Employees where x.Id == message.CentralId select x).First();

                msg = "Voornaam: " + record.FirstName + Environment.NewLine +
                    "Tussenvoegsel: " + record.Prefix + Environment.NewLine +
                    "Achternaam: " + record.SurName + Environment.NewLine +
                    "Geslacht: " + record.Gender + Environment.NewLine +
                    "Plaats: " + record.City + Environment.NewLine +
                    "Adres: " + record.Address + Environment.NewLine +
                    "Postcode: " + record.ZipCode + Environment.NewLine +
                    "Telefoon: " + record.Phonenumber + Environment.NewLine +
                    "E-mail: " + record.Email + Environment.NewLine +
                    "Regio: " + record.Region.Region1 + Environment.NewLine +
                    "Status: " + record.EmployeeStatu.Description + Environment.NewLine +
                    "Is Inspecteur: " + record.IsInspecter.ToString() + Environment.NewLine +
                    "Is Manager: " + record.IsManager.ToString() + Environment.NewLine +
                    "Manager: " + record.ManagerId.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetAccount(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Account record = (from x in context.Accounts where x.Id == message.CentralId select x).First();

                msg = "Gebruikersnaam: " + record.Username + Environment.NewLine +
                    "Werknemer: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine;
            }

            return msg;
        }
        public string GetAvailability(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Availability record = (from x in context.Availabilities where x.EmployeeId == message.CentralId select x).Where(x => x.Date == message.CentralId_DateTime).First();

                msg = "Werknemer: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine +
                    "Datum: " + record.Date.ToString() + Environment.NewLine +
                    "Start tijd: " + record.StartTime.ToString() + Environment.NewLine +
                    "Eind tijd: " + record.EndTime.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetWorkingHours(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.WorkingHour record = (from x in context.WorkingHours where x.EmployeeId == message.CentralId select x).Where(x => x.Date == message.CentralId_DateTime).First();

                msg = "Werknemer: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine +
                    "Datum: " + record.Date.ToString() + Environment.NewLine +
                    "Start tijd: " + record.StartTime.ToString() + Environment.NewLine +
                    "Eind tijd: " + record.EndTime.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetCustomer(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Customer record = (from x in context.Customers where x.Id == message.CentralId select x).First();

                msg = "Naam: " + record.Name + Environment.NewLine +
                    "Adres: " + record.Address + Environment.NewLine +
                    "Plaats: " + record.Location + Environment.NewLine +
                    "Telefoon: " + record.Phonenumber + Environment.NewLine +
                    "E-mail: " + record.Email + Environment.NewLine;
            }

            return msg;
        }
        public string GetAssignment(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Assignment record = (from x in context.Assignments where x.Id == message.CentralId select x).First();

                msg = "Klant: " + record.Customer.Name + Environment.NewLine +
                    "Manager: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine +
                    "Beschrijving: " + record.Description + Environment.NewLine +
                    "Start datum: " + record.StartDate.ToString() + Environment.NewLine +
                    "Eind datum: " + record.EndDate.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetInspectionStatus(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.InspectionStatu record = (from x in context.InspectionStatus where x.Id == message.CentralId select x).First();

                msg = "Beschrijving: " + record.Description + Environment.NewLine;
            }

            return msg;
        }
        public string GetInspection(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Inspection record = (from x in context.Inspections where x.Id == message.CentralId select x).First();

                msg = "Opdracht nummer: " + record.AssignmentId.ToString() + Environment.NewLine +
                    "Regio: " + record.Region.Region1 + Environment.NewLine +
                    "Locatie: " + record.Location + Environment.NewLine +
                    "Start datum: " + record.StartDate.ToString() + Environment.NewLine +
                    "Eind datum: " + record.EndDate.ToString() + Environment.NewLine +
                    "Status: " + record.InspectionStatu.Description + Environment.NewLine +
                    "Inspecteur: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine;
            }

            return msg;
        }
        public string GetCoordinate(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Coordinate record = (from x in context.Coordinates where x.Id == message.CentralId select x).First();

                msg = "Coordinaat nummer: " + record.Id.ToString() + Environment.NewLine +
                     "Longitude: " + record.Longitude + Environment.NewLine +
                     "Latitude: " + record.Latitude + Environment.NewLine +
                     "Note: " + record.Note + Environment.NewLine +
                     "Opdracht nummer: " + record.InspectionId.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetInspectionImage(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.InspectionImage record = (from x in context.InspectionImages where x.Id == message.CentralId select x).First();

                msg = "Bestand: " + record.File + Environment.NewLine +
                    "Opdracht nummer: " + record.InspectionId.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetKeywordCategory(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.KeywordCategory record = (from x in context.KeywordCategories where x.Id == message.CentralId select x).First();

                msg = "Beschrijving: " + record.Description;
            }

            return msg;
        }
        public string GetKeyword(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Keyword record = (from x in context.Keywords where x.Id == message.CentralId select x).First();

                msg = "Beschrijving: " + record.Description + Environment.NewLine +
                    "Categorie: " + record.KeywordCategory.Description + Environment.NewLine;
            }

            return msg;
        }
        public string GetModule(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Module record = (from x in context.Modules where x.Id == message.CentralId select x).First();

                msg = "Naam: " + record.Name + Environment.NewLine +
                    "Beschrijving: " + record.Description + Environment.NewLine +
                    "Note: " + record.Note + Environment.NewLine;
            }

            return msg;
        }
        public string GetQuestionSort(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.QuestionSort record = (from x in context.QuestionSorts where x.Id == message.CentralId select x).First();

                msg = "Beschrijving: " + record.Description;
            }

            return msg;
        }
        public string GetQuestion(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Question record = (from x in context.Questions where x.Id == message.CentralId select x).First();

                msg = "Soort: " + record.QuestionSort.Description + Environment.NewLine +
                    "Beschrijving: " + record.Description + Environment.NewLine +
                    "Module: " + record.Module.Name + Environment.NewLine;
            }

            return msg;
        }
        public string GetQuestionaire(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.Questionnaire record = (from x in context.Questionnaires where x.Id == message.CentralId select x).First();

                msg = "Inspectie nummer: " + record.InspectionId.ToString();
            }

            return msg;
        }
        public string GetQuestionAnswer(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.QuestionAnswer record = (from x in context.QuestionAnswers where x.QuestionnaireId == message.CentralId select x).Where(x => x.QuestionId == message.CentralId_Extra).First();

                msg = "Questionnaire nummer: " + record.QuestionnaireId.ToString() + Environment.NewLine +
                    "Vraag nummer: " + record.QuestionId.ToString() + Environment.NewLine +
                    "Resultaat: " + record.Result;
            }

            return msg;
        }
        public string GetQuestionaireModule(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.QuestionnaireModule record = (from x in context.QuestionnaireModules where x.QuestionnaireId == message.CentralId select x).Where(x => x.ModuleId == message.CentralId_Extra).First();

                msg = "Module: " + record.Module.Name + Environment.NewLine +
                    "Questionnaire nummer: " + record.QuestionnaireId.ToString();
            }

            return msg;
        }
        public string GetQuestionKeyword(UpdateMessage message)
        {
            string msg = null;

            using (var context = new ParkInspectEntities())
            {
                ParkInspectGroupC.DOMAIN.QuestionKeyword record = (from x in context.QuestionKeywords where x.QuestionId == message.CentralId select x).Where(x => x.KeywordId == message.CentralId_Extra).First();

                msg = "Vraag nummer: " + record.QuestionId.ToString() + Environment.NewLine +
                    "Keyword: " + record.Keyword.Description;
            }

            return msg;
        }
    }
}
