namespace BlazorSample.Shared
{
    public static class DecimalExtensions
    {
        public static string EmptyIfZero(this decimal value)
        {
            if (value == 0)
            {
                return string.Empty;
            }

            return "$" + value.ToString("0.00");
        }
    }
}
