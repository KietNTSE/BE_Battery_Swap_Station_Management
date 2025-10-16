namespace Service.Utils;

public class RandomUtils
{
    public static string GenerateOtp()
    {
        var rng = Random.Shared.Next(100000, 999999);
        return rng.ToString();
    }
}