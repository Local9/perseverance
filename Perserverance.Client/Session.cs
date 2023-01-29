namespace Perserverance.Client
{
    internal class Session
    {
        internal static bool IsSessionReady;
        internal static async Task IsReady()
        {
            while (IsSessionReady)
            {
                await BaseScript.Delay(0);
            }
        }
    }
}
