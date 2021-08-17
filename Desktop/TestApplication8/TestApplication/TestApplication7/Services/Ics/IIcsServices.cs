namespace TestApplication7.Services.Ics
{
    public interface IIcsServices
    {
        decimal GetEstimatedCharges(int w, int width, int l, int h, string postalCode);
    }
}
