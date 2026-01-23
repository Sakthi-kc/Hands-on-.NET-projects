using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services.Interfaces
{
    public interface IDataService
    {
        List<List<string>> GetPartnerData();
    }
}
