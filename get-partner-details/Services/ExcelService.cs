using System;
using System.Collections.Generic;
using System.Text;

using ClosedXML.Excel;

namespace get_partner_details.Services
{
    //defines how excel content is read
    public class ExcelService : IExcelService
    {
        //readonly can be assigned only once in constructor and cannot be changed
        private readonly string _filepath;


        //called when you create object for ExcelReader
        public ExcelService(string filepath)
        {
            _filepath = filepath;
        }


        //reads list of rows where each row is a list of string
        public List<List<string>> GetPartnerData()
        {
            //represents entire excel file
            var workBook = new XLWorkbook(_filepath);

            var workSheet = workBook.Worksheet(1);

            //stores all the rows, used for return value
            var data = new List<List<string>>();

            foreach (var rows in workSheet.RowsUsed())
            {
                //stores all cell values for 1 row
                var row = new List<string>();

                foreach (var cell in rows.CellsUsed())
                {
                    row.Add(cell.GetValue<string>());
                }

                data.Add(row);
            }

            return data;
        }
    }
}
