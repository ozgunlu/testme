using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.IdsGenerator;
using BuildingBlocks.Core.Messaging;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using IdGen;
using Microsoft.EntityFrameworkCore;
namespace DivitOtoyol.Modules.Statistics.Communications.Records.Features.CreatingRecord.Events.External;

public record RecordCreated(long Id, string Plate, long CameraId, string CameraName, long? MakeId, string? MakeName, long? ModelId, string? ModelName, long? ColorId, string? ColorName, DateTime LprDate) :
    IntegrationEvent, ITxRequest;

public class RecordCreatedConsumer : IMessageHandler<RecordCreated>
{
    private readonly IStatisticDbContext _statisticDbContext;

    public RecordCreatedConsumer(IStatisticDbContext statisticDbContext)
    {
        _statisticDbContext = statisticDbContext;
    }

    public async Task HandleAsync(
        IConsumeContext<RecordCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        var recordCreated = messageContext.Message;

        var cameraStatistic = await _statisticDbContext.CameraStatistics.FirstOrDefaultAsync(cs => cs.CameraInformation.Id.Value == recordCreated.CameraId);
        if (cameraStatistic != null)
        {
            cameraStatistic.UpdatePassages(recordCreated.LprDate);
        } else
        {
            cameraStatistic = CameraStatistic.Create(
                SnowFlakIdGenerator.NewId(),
                LocationInformation.Create(recordCreated.CameraId, recordCreated.CameraName),
                CameraInformation.Create(recordCreated.CameraId, recordCreated.CameraName),
                TypeInformation.Create(recordCreated.MakeId, recordCreated.MakeName),
                MakeInformation.Create(recordCreated.MakeId, recordCreated.MakeName),
                ModelInformation.Create(recordCreated.ModelId, recordCreated.ModelName),
                ColorInformation.Create(recordCreated.ColorId, recordCreated.ColorName),
                recordCreated.Plate,
                recordCreated.LprDate);

            await _statisticDbContext.CameraStatistics.AddAsync(cameraStatistic, cancellationToken: cancellationToken);

            await _statisticDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
