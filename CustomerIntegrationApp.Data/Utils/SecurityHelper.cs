using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerIntegrationApp.Data.Utils
{
    public static class SecurityHelper
    {
        public static long GenerateToken(int rotationNumber, long cardNumber, DateTime dateTime)
        {
            List<int> lastNumbers = Convert.ToInt32(cardNumber.ToString().Substring(cardNumber.ToString().Length - 4, 4)).ToString().Select(t => int.Parse(t.ToString())).ToList();

            for (int i = 0; i < rotationNumber; i++)
            {
                int last = lastNumbers[2];
                lastNumbers.RemoveAt(2);
                lastNumbers.Insert(0, last);
            }

            int finalNumbers = Convert.ToInt32(string.Join("", lastNumbers.Select(x => x.ToString()).ToArray()));

            long token = Convert.ToInt64(string.Format("{0}{1}", dateTime.ToString("yyyyMMdd"), finalNumbers));

            return token;
        }
    }
}
