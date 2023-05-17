namespace Classes
{
    internal class Login
    {
        public enum loginMessages
        {
            startLogin, insertName, insertSurname
        };
        public string printMessages(loginMessages messages)
        {
            switch (messages)
            {
                case loginMessages.startLogin:
                    return "LOGOWANIE";
                case loginMessages.insertName:
                    return "Podaj imię: ";
                case loginMessages.insertSurname:
                    return "Podaj nazwisko: ";
                default:
                    return "Błąd";
            }
        }
    }
}
