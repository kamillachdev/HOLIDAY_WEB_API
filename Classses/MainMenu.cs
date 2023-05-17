using System.Globalization;
using System.Text;

namespace Classes
{
    internal class Menu
    {
        public enum menuMessages { insertChoice, insertStartDate, insertEndDate };

        public string printMessages(menuMessages messages)
        {
            switch (messages)
            {
                case menuMessages.insertChoice:
                    return "Wybór: ";
                case menuMessages.insertStartDate:
                    return "Podaj datę początkową w formacie dd-MM-yyyy: ";
                case menuMessages.insertEndDate:
                    return "Podaj datę końcową w formacie dd-MM-yyyy: ";
                default:
                    return "Błąd";
            }
        }

        public string printMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MENU");
            sb.AppendLine("1. Nowy wniosek urlopowy");
            sb.AppendLine("2. Pokaż listę wniosków");
            sb.AppendLine("3. Wyloguj się");
            return sb.ToString();
        }

        public enum MenuChoice { Undefined, CreateHolidayRequest, showRequests, LogOut };

        public MenuChoice ReadChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    return MenuChoice.CreateHolidayRequest;
                case "2":
                    return MenuChoice.showRequests;
                case "3":
                    return MenuChoice.LogOut;
                default:
                    return MenuChoice.Undefined;
            }
        }

        public string menuAction(MenuChoice menuChoice, HolidayRequest holidayRequest)
        {
            string actionOutput = "";

            if (menuChoice == MenuChoice.LogOut)
            {
                Environment.Exit(0);
            }
            else if (menuChoice == MenuChoice.showRequests)
            {
                actionOutput = "this option gets all the requests for a specific user";
            }
            else if (menuChoice == MenuChoice.CreateHolidayRequest)
            {
                actionOutput = "this option creates user, or if already exists, gets his id, then sending the request to the database, and shows the summary of the request, if not done correctly, return error message";
            }

            return actionOutput;
        }


        public DateTime convertToDate(string date)
        {
            DateTime returnDate;
            DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out returnDate);
            return returnDate;
        }
    }
}