using System.Text;
using System.Text.RegularExpressions;

namespace Classes
{
    internal class HolidayRequest
    {
        string name, surname;
        DateTime holidayStartDate, holidayEndDate;


        public HolidayRequest()
        {
            name = "";
            surname = "";
            holidayStartDate = DateTime.MinValue;
            holidayEndDate = DateTime.MinValue;
        }

        public void setName(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public void setDate(DateTime holidayStartDate, DateTime holidayEndDate)
        {
            this.holidayStartDate = holidayStartDate;
            this.holidayEndDate = holidayEndDate;
        }

        public string isNameValid()
        {
            string namePattern = @"^[A-Z][a-zA-Z]{1,}$";

            if (!Regex.IsMatch(name, namePattern))
            {
                return "Niepoprawne imię";
            }

            if (!Regex.IsMatch(surname, namePattern))
            {
                return "Niepoprawne nazwisko";
            }

            return "OK";
        }

        public string isDateValid()
        {
            if (holidayStartDate < DateTime.Today)
            {
                return "Data początkowa nie może być przed datą dzisiejszą";
            }

            if (holidayEndDate < holidayStartDate)
            {
                return "Data końcowa musi być późniejsza niż data początkowa";
            }

            return "OK";
        }


        private int daysCounting(DateTime startDate, DateTime endDate)
        {
            int days = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    days++;
            }
            return days;
        }

        public string GetSummary()
        {
            int days = daysCounting(holidayStartDate, holidayEndDate);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Przyjęto wniosek urlopowy dla {name} {surname} od {holidayStartDate.ToString("d")} do {holidayEndDate.ToString("d")}");
            stringBuilder.AppendLine($"Ilość dni: {days}");
            return stringBuilder.ToString();
        }

        public string createId()
        {
            string fullName = name + surname;
            int hash = fullName.GetHashCode();
            return hash.ToString();
        }

        public string getName()
        {
            return name;
        }

        public string getSurname()
        {
            return surname;
        }

        public string getStartDate()
        {
            return holidayStartDate.ToString("yyyy-MM-dd");
        }

        public string getEndDate()
        {
            return holidayEndDate.ToString("yyyy-MM-dd");

        }
    }
}