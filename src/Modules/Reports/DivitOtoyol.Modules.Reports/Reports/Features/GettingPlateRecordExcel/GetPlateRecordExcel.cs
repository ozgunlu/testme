using System.Data;
using System.IO.Compression;
using System.Net;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using EventStore.ClientAPI;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Reports.Reports.Features.GettingPlateRecordExcel;

public record GetPlateRecordExcel : ListQuery<GetPlateRecordExcelResponse>
{
}

internal class GetOptionsValidator : AbstractValidator<GetOptions>
{
    public GetOptionsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetOptionsHandler : IQueryHandler<GetOptions, GetOptionsResponse>
{
    private readonly ISystemDbContext _systemDbContext;
    private readonly IMapper _mapper;

    public GetOptionsHandler(ISystemDbContext systemDbContext, IMapper mapper)
    {
        _systemDbContext = systemDbContext;
        _mapper = mapper;
    }

    public async Task<GetPlateRecordZipResponse> Handle(
        GetPlateRecordZipRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.Type == 1)
            {
                if (request.Filter.StartDate > DateTime.Now.AddDays(-29))
                {
                    // plateRecordList = db.GetArchiveListByFilterForReportsTest(filter, response.userPK, response.SessionPK, response.Ip);
                }
                else if (request.Filter.StartDate < DateTime.Now.AddDays(-29) && request.Filter.EndDate > DateTime.Now.AddDays(-29))
                {
                    // plateRecordList = db.GetArchiveListByFilterForReportsTest(filter, response.userPK, response.SessionPK, response.Ip);
                    // plateRecordList.Merge(db.GetArchiveListByFilterForReportsTestFromHistory(filter, response.userPK, response.SessionPK, response.Ip));
                }
                else
                {
                    // plateRecordList = (db.GetArchiveListByFilterForReportsTestFromHistory(filter, response.userPK, response.SessionPK, response.Ip));
                }
            }
            else if (request.Type == 2 || request.Type == 3)
            {
                request.Filter.StartDate = DateTime.UtcNow.AddDays(-720);
                if (request.Filter.StartDate > DateTime.Now.AddDays(-29))
                {
                    // plateRecordList = db.GetArchiveListByFilterForReportsTest(filter, response.userPK, response.SessionPK, response.Ip);
                }
                else if (request.Filter.StartDate < DateTime.Now.AddDays(-29) && request.Filter.EndDate > DateTime.Now.AddDays(-29))
                {
                    // plateRecordList = db.GetArchiveListByFilterForReportsTest(filter, response.userPK, response.SessionPK, response.Ip);
                    // plateRecordList.Merge(db.GetArchiveListByFilterForReportsTestFromHistory(filter, response.userPK, response.SessionPK, response.Ip));
                }
                else
                {
                    // plateRecordList = (db.GetArchiveListByFilterForReportsTestFromHistory(filter, response.userPK, response.SessionPK, response.Ip));
                }
            }

            if (!System.IO.Directory.Exists(ReportPath))
            {
                System.IO.Directory.CreateDirectory(ReportPath);
            }

            DeleteAllFilesinTemp(); // Daha önceden oluşturulan ve indirilen raporlar siliniyor.

            var columnCount = 3;
            long dateValue = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            string filename = "Zip_Rapor_" + dateValue.ToString();
            string excelFileName = "Excel_Rapor_" + dateValue.ToString();

            using (ZipFile zip = new ZipFile())
            {
                foreach (DataRow row in plateRecordList.Rows)
                {
                    string imageFilename = row["FileName"].ToString();
                    ZipEntry entry = new ZipEntry();

                    var Folder = "";
                    if (File.Exists(imagePath + imageFilename))
                        Folder = imagePath;
                    else
                        Folder = imagePath2;

                    FileInfo info = new FileInfo(Folder + imageFilename);
                    if (File.Exists(Folder + imageFilename) && !zip.EntryFileNames.Contains("images/" + info.Name))
                    {
                        entry = zip.AddFile(Folder + imageFilename, "images");
                    }
                }
                ExportToOxml(true, ReportPath + excelFileName + ".xlsx", plateRecordList);
                //foreach (Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                //{
                //    proc.Kill();
                //}
                zip.AddFile(ReportPath + excelFileName + ".xlsx", "");
                zip.Save(ReportPath + filename + ".zip");

                //LOG
                string logString = "Kullancı:" + user.UserName + ";" + "ReportType:Zip;Sorgu Nedeni: " + filter.Notes;
                helper.CreateLog(Logs.Report, ProcessType.QUERY, user.PK, response.SessionPK, response.Ip, logString);
                //LOG
                return request.CreateResponse(HttpStatusCode.OK, filename + ".zip");
            }
        }
        catch (Exception)
        {

            return request.CreateResponse(HttpStatusCode.BadRequest);
        }


        return new GetPlateRecordZipResponse { ReportFileName = "your_zip_file_name" };
    }
}
