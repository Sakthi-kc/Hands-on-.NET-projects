using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services.BusinessLogic
{
    public class SNRFService
    {
        
        public string GenerateSNRF(string prefix)
        {
            //get current date for guid
            string currentDate = DateTime.UtcNow.Ticks
                .ToString("X")[..4];


            //get a number between 1 - 9999
            string randomNumber = Random.Shared.Next(1, 10000).ToString("D4");


            return $"{prefix}{currentDate}{randomNumber}";
        }

    }
}
