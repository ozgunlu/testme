using BuildingBlocks.Persistence.Mongo;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Read;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Data;

public class PlateRecognitionReadDbContext : MongoDbContext
{
    public PlateRecognitionReadDbContext(IOptions<MongoOptions> options) : base(options)
    {
        Records = GetCollection<RecordReadModel>(nameof(Records).Underscore());
    }

    public IMongoCollection<RecordReadModel> Records { get; }
}
