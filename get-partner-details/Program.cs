//Generate SNRF: prefix-datetime-random

using get_partner_details.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;


//read user input
Console.Write("Enter the parnter ID: ");
string userInputId = Console.ReadLine()?.Trim() ?? "";
Console.WriteLine();

//app config
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


//copy the file to CurrentDirectory
string filePath = config["ExcelFileName"];


//ExcelDatasource depends on contract (interface) not concrete class - loosely coupled
IDataService excelService = new ExcelService(filePath);


//You inject the dependency (excelService) into ExcelDataSource
IDataSource dataSource = new ExcelDataSource(excelService);
var retrievedData = dataSource.GetSourceData();


//get the partner details
PartnerService fetchData = new();
var partnerDetails = fetchData.GetPartnerDetails(retrievedData, userInputId);

if (partnerDetails is null)
{
    Console.WriteLine("Please provide a valid id");
    return;
}

//checks if this key exists in partnerDetails, returns true or false and sets accordingly
if (!partnerDetails.TryGetValue(config["ExcelColumn"], out string partnerPrefix))
{
    Console.WriteLine("Column not exist, please check source data");
    return;
}


//convert the row information to json string
Console.WriteLine(JsonSerializer.Serialize(
    partnerDetails,
    new JsonSerializerOptions { WriteIndented = true }
    ));


//generate snrf
Console.Write("\nSNRF generated: ");

SNRFService snrf = new();
Console.WriteLine(snrf.GenerateSNRF(partnerPrefix));

return;