
namespace Dima.Core.Common.Extencions
{
    public static class DateTimeExtencion
    {
        public static DateTime GetFirstDay(this DateTime date, int? year = null, int? month = null) 
            => new DateTime(year ?? date.Year, month ?? date.Month, 1); // Pegar o primeiro dia do mês

        public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
        => new DateTime(year ?? date.Year, month ?? date.Month, 1).AddMonths(1).AddDays(-1); // Pegar o ultimo dia do mês
    }
}

