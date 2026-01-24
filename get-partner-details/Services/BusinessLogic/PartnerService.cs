using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace get_partner_details.Services.BusinessLogic
{
    public class PartnerService
    {
        public Dictionary<string, string>? GetPartnerDetails(List<Dictionary<string,string>> partnerData, string partnerId)
        {
            foreach (var row in partnerData)
            {
                if (row["Id"].Equals(partnerId, StringComparison.OrdinalIgnoreCase))
                {
                    return row;
                }
            }

            return null;
        }
    }
}
