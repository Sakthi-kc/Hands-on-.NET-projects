using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services
{

    //defines what excel file to read
    public class ExcelDataSource : IDataSource
    {
        private readonly IExcelService _excelService;

        public ExcelDataSource(IExcelService excelService)
        {
            //creates ExcelService object, calls the constructor and assigns value
            _excelService = excelService;
        }

        public List<List<string>> GetSourceData()
        {
            return _excelService.GetPartnerData();
        }
    }
}
