using LocalDatabase.Domain;
using System;
using System.Linq;

namespace LocalDatabase.Local
{
    public class GetLocalRecordDetails
    {
        public string GetRegion(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Region record = (from x in context.Region where x.Id == message.LocalId select x).First();

                msg = "Regio: " + record.Region1;
            }

            return msg;
        }
        public string GetEmployeeStatus(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                EmployeeStatus record = (from x in context.EmployeeStatus where x.Id == message.LocalId select x).First();

                msg = "Status: " + record.Description;
            }

            return msg;
        }
        public string GetEmployee(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Employee record = (from x in context.Employee where x.Id == message.LocalId select x).First();

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
                    "Status: " + record.EmployeeStatus.Description + Environment.NewLine +
                    "Is Inspecteur: " + record.IsInspecter.ToString() + Environment.NewLine +
                    "Is Manager: " + record.IsManager.ToString() + Environment.NewLine +
                    "Manager: " + record.ManagerId.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetAccount(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Account record = (from x in context.Account where x.Id == message.LocalId select x).First();

                msg = "Gebruikersnaam: " + record.Username + Environment.NewLine +
                    "Werknemer: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine;
            }

            return msg;
        }
        public string GetAvailability(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Availability record = (from x in context.Availability where x.EmployeeId == message.LocalId select x).Where(x => x.Date == message.LocalId_DateTime).First();

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

            using (var context = new LocalParkInspectEntities())
            {
                WorkingHours record = (from x in context.WorkingHours where x.EmployeeId == message.LocalId select x).Where(x => x.Date == message.LocalId_DateTime).First();

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

            using (var context = new LocalParkInspectEntities())
            {
                Customer record = (from x in context.Customer where x.Id == message.LocalId select x).First();

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

            using (var context = new LocalParkInspectEntities())
            {
                Assignment record = (from x in context.Assignment where x.Id == message.LocalId select x).First();

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

            using (var context = new LocalParkInspectEntities())
            {
                InspectionStatus record = (from x in context.InspectionStatus where x.Id == message.LocalId select x).First();

                msg = "Beschrijving: " + record.Description + Environment.NewLine;
            }

            return msg;
        }
        public string GetCoordinate(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Coordinate record = (from x in context.Coordinate where x.Id == message.LocalId select x).First();

                msg = "Coordinaat nummer: " + record.Id.ToString() + Environment.NewLine +
                     "Longitude: " + record.Longitude + Environment.NewLine +
                     "Latitude: " + record.Latitude + Environment.NewLine +
                     "Note: " + record.Note + Environment.NewLine +
                     "Opdracht nummer: " + record.InspectionId.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetInspection(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Inspection record = (from x in context.Inspection where x.Id == message.LocalId select x).First();

                msg = "Opdracht nummer: " + record.AssignmentId.ToString() + Environment.NewLine +
                    "Regio: " + record.Region.Region1 + Environment.NewLine +
                    "Locatie: " + record.Location + Environment.NewLine +
                    "Start datum: " + record.StartDate.ToString() + Environment.NewLine +
                    "Eind datum: " + record.EndDate.ToString() + Environment.NewLine +
                    "Status: " + record.InspectionStatus.Description + Environment.NewLine +
                    "Inspecteur: " + record.Employee.FirstName + " " + record.Employee.Prefix + " " + record.Employee.SurName + Environment.NewLine;
            }

            return msg;
        }
        public string GetInspectionImage(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                InspectionImage record = (from x in context.InspectionImage where x.Id == message.LocalId select x).First();

                msg = "Bestand: " + record.File + Environment.NewLine +
                    "Opdracht nummer: " + record.InspectionId.ToString() + Environment.NewLine;
            }

            return msg;
        }
        public string GetKeywordCategory(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                KeywordCategory record = (from x in context.KeywordCategory where x.Id == message.LocalId select x).First();

                msg = "Beschrijving: " + record.Description;
            }

            return msg;
        }
        public string GetKeyword(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Keyword record = (from x in context.Keyword where x.Id == message.LocalId select x).First();

                msg = "Beschrijving: " + record.Description + Environment.NewLine + 
                    "Categorie: " + record.KeywordCategory.Description + Environment.NewLine;
            }

            return msg;
        }
        public string GetModule(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Module record = (from x in context.Module where x.Id == message.LocalId select x).First();

                msg = "Naam: " + record.Name + Environment.NewLine + 
                    "Beschrijving: " + record.Description + Environment.NewLine +
                    "Note: " + record.Note + Environment.NewLine;
            }

            return msg;
        }
        public string GetQuestionSort(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                QuestionSort record = (from x in context.QuestionSort where x.Id == message.LocalId select x).First();

                msg = "Beschrijving: " + record.Description;
            }

            return msg;
        }
        public string GetQuestion(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Question record = (from x in context.Question where x.Id == message.LocalId select x).First();

                msg = "Soort: " + record.QuestionSort.Description + Environment.NewLine +
                    "Beschrijving: " + record.Description + Environment.NewLine +
                    "Module: " + record.Module.Name + Environment.NewLine;
            }

            return msg;
        }
        public string GetQuestionaire(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                Questionaire record = (from x in context.Questionaire where x.Id == message.LocalId select x).First();

                msg = "Inspectie nummer: " + record.InspectionId.ToString();
            }

            return msg;
        }
        public string GetQuestionAnswer(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                QuestionAnswer record = (from x in context.QuestionAnswer where x.QuestionnaireId == message.LocalId select x).Where(x => x.QuestionId == message.LocalId_Extra).First();

                msg = "Questionnaire nummer: " + record.QuestionnaireId.ToString() + Environment.NewLine +
                    "Vraag nummer: " + record.QuestionId.ToString() + Environment.NewLine +
                    "Resultaat: " + record.Result;
            }

            return msg;
        }
        public string GetQuestionaireModule(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                QuestionaireModule record = (from x in context.QuestionaireModule where x.QuestionaireId == message.LocalId select x).Where(x => x.ModuleId == message.LocalId_Extra).First();

                msg = "Module: " + record.Module.Name + Environment.NewLine +
                    "Questionnaire nummer: " + record.QuestionaireId.ToString();
            }

            return msg;
        }
        public string GetQuestionKeyword(UpdateMessage message)
        {
            string msg = null;

            using (var context = new LocalParkInspectEntities())
            {
                QuestionKeyword record = (from x in context.QuestionKeyword where x.QuestionId == message.LocalId select x).Where(x => x.KeywordId == message.LocalId_Extra).First();

                msg = "Vraag nummer: " + record.QuestionId.ToString() + Environment.NewLine +
                    "Keyword: " + record.Keyword.Description;
            }

            return msg;
        }
    }
}
