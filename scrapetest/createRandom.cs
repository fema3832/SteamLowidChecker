namespace scrapetest
{
    public static class createRandom
    {
        public static string random_string(int length)
        {
            var ran = new Random();

            const string b = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = "";

            for (var i = 0; i < length; i++)
            {
                var a = ran.Next(b.Length);
                random += b[a];
            }

            return random;
        }
    }
}
