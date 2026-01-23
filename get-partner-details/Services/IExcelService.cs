using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services
{
    public interface IExcelService
    {
        List<List<string>> GetPartnerData();
    }
}
