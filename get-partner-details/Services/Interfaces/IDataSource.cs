using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services.Interfaces
{
    public interface IDataSource
    {
        List<Dictionary<string,string>> GetSourceData();
    }
}
