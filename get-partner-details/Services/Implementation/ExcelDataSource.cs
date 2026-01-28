using get_partner_details.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services.Implementation
{

    //defines what excel file to read
    public class ExcelDataSource : IDataSource
    {
        private readonly IExcelService _excelService;

        public ExcelDataSource(IExcelService excelService)
        {
            //stores ExcelService object for future reference
            _excelService = excelService;
        }

        public List<Dictionary<string,string>> GetSourceData()
        {
            return _excelService.GetPartnerData();
        }
    }
}
