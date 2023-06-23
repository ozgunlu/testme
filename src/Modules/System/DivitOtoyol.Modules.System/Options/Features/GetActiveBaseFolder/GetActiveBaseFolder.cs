using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Systems.Options.Dtos;
using DivitOtoyol.Modules.Systems.Options.Exceptions.Application;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using DivitOtoyol.Modules.Systems.Shared.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Options.Features.GetActiveBaseFolder;

public record GetActiveBaseFolder() : IQuery<GetActiveBaseFolderResponse>;

public class GetActiveBaseFolderHandler : IQueryHandler<GetActiveBaseFolder, GetActiveBaseFolderResponse>
{
    private readonly ISystemDbContext _systemDbContext;
    private readonly IMapper _mapper;

    public GetActiveBaseFolderHandler(ISystemDbContext systemDbContext, IMapper mapper)
    {
        _systemDbContext = systemDbContext;
        _mapper = mapper;
    }

    public async Task<GetActiveBaseFolderResponse> Handle(GetActiveBaseFolder command, CancellationToken cancellationToken)
    {
        string FtpKey0 = "IMAGE_FTP_DIR";
        string FtpKey1 = "IMAGE_FTP_DIR_2";
        string ActiveFtpKey = "ACTIVE_FTP_DIR_KEY";

        var ActiveDirKeyOpt = await _systemDbContext.Options.FirstOrDefaultAsync(option => option.Key == ActiveFtpKey, cancellationToken);
        Guard.Against.ExistsOptionByKey(ActiveDirKeyOpt != null, ActiveFtpKey);

        var FtpOpt = await _systemDbContext.Options.FirstOrDefaultAsync(option => option.Key == ActiveDirKeyOpt!.Value, cancellationToken);
        Guard.Against.ExistsOptionByKey(FtpOpt != null, ActiveDirKeyOpt!.Value);

        var driveName = Path.GetPathRoot(FtpOpt!.Value);
        DriveInfo drive = new DriveInfo(driveName);

        double freeGB = drive.AvailableFreeSpace / 1048576D;
        double totalGB = drive.TotalSize / 1048576D;
        if (freeGB / totalGB < 0.01)
        {
            ActiveDirKeyOpt.ChangeValue(ActiveDirKeyOpt.Value.Equals(FtpKey0) ? FtpKey1 : FtpKey0);
            await _systemDbContext.SaveChangesAsync(cancellationToken);

            FtpOpt = await _systemDbContext.Options.FirstOrDefaultAsync(option => option.Key == ActiveDirKeyOpt.Value, cancellationToken);
            Guard.Against.ExistsOptionByKey(FtpOpt != null, ActiveDirKeyOpt!.Value);
        }

        return new GetActiveBaseFolderResponse(FtpOpt!.Value);
    }
}
