
namespace WhenToDig83.Core.Helpers
{
    public static class DateHelper
    {
        private static string[] _months = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public static string MonthAbbreviation(int monthId)
        {
            return _months[monthId];
        }
    }
}
