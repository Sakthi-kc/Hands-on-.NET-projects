using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services
{
    public interface IDataService
    {
        List<List<string>> GetPartnerData();
    }
}
