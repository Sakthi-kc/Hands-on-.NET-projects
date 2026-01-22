using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace get_partner_details.Services
{
    public class PartnerService
    {
        public Dictionary<string, string>? GetPartnerDetails(List<List<string>> partnerData, string partnerId)
        {
            var headerRow = partnerData[0];

            int idIndex = headerRow.IndexOf("Id");

            var result = new Dictionary<string, string>();
            
            foreach (var row in partnerData.Skip(1))
            {
                if (row[idIndex].Equals(partnerId, StringComparison.OrdinalIgnoreCase))
                {
                    //stores all the cell values of the row into dictionary
                    for (int i = 0; i < headerRow.Count; i++)
                    {
                        //example: result["Id"] = "MK";
                        result[headerRow[i]] = row[i];
                    }

                    return result;
                }
            }

            return null;
        }
    }
}
