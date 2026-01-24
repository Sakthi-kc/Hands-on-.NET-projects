using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using get_partner_details.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services.Implementation
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
        public List<Dictionary<string,string>> GetPartnerData()
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

            var headers = data[0];

            var result = data
                .Skip(1)
                // Loop through each row from data
                .Select(
                    row => headers
                    // For each header, select its value and index as {header = "", index = 0}
                    // Each of this pair is item
                    .Select(
                        (header, index) => new { header, index }
                    )
                    // Convert to dictionary (keySelector, valueSelector)
                    .ToDictionary(item => item.header, item => row[item.index])
                )
                // Collect all row dictionaries into a list
                .ToList();

            return result;
        }
    }
}
