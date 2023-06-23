using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Options.ValueObjects;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Options.Data;

public class OptionDataSeeder : IDataSeeder
{
    private readonly ISystemDbContext _dbContext;

    public OptionDataSeeder(ISystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAllAsync()
    {
        if (await _dbContext.Options.AnyAsync())
            return;

        var options = new List<Option>
        {
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "TERMINAL_USER_NAME", "divit", "Identity", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "TERMINAL_PASS", "divit", "Identity", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "EXPLORATION_ACTIVE", "0", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "ENABLE_BARRIER_CONTROL", "1", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "TEDES_ACTIVE", "0", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "ACTIVE_FTP_DIR_KEY", "IMAGE_FTP_DIR", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "IMAGE_FTP_DIR", "c:\\Images\\", "PlateRecognitions", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "PLATELESS_CAR_NAME", "Plakasız", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "PARKING_FEE_MANAGEMENT", "1", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "SMALL_JPEG_WITH", "400", "PlateRecognitions", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "REPORT_FTP_DIR", "c:\\Reports\\", "Reports", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "KURUM_ADI", "Divit Imge ve Video Teknolojileri", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "DEFAULT_PASSWORD", "123456", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "SYNC_READY", "1", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "LED_ACTIVE", "1", "none", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "IMAGE_FTP_DIR_2", "c:\\Images\\", "PlateRecognitions", "", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "LED_LOGO_FILENAME", "logo.mp4", "none", "Remote serverdan eklenen logo ismi", true, false),
            Option.Create(new OptionId(SnowFlakIdGenerator.NewId()), "DONEM_DATE", "2023-06-30", "none", "İTÜ için dönem sonu tarihi (yyyy-aa-gg)", true, false),
        };

        await _dbContext.Options.AddRangeAsync(options);
        await _dbContext.SaveChangesAsync();
    }
}
