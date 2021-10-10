namespace MVCAgenda.Core.Helpers
{
    public class StringHelpers
    {
        public static string SuccesMessage = "Succes.";
        public static string FailMessage = "Fail.";

        public static string MakeSuccesMessage(string message)
        {
            return SuccesMessage + " " + message;
        }

        public static string MakeFailMessage(string message)
        {
            return FailMessage + " " + message;
        }
    }
}