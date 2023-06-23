using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.PlateRecognitions.Cameras.Exceptions;
using DivitOtoyol.Modules.PlateRecognitions.Records.Dtos;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Requests;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Camera;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using DivitOtoyol.Modules.PlateRecognitions.Shared.System.Option;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Color;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Make;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord;

public record CreateRecord(
    string Plate,
    long CameraId,
    //string? OcrType,
    string? OcrMake,
    string? OcrModel,
    string? OcrColor,
    DateTime LprDate,
    [JsonProperty("Metadata")] string metadataString,
    string ImageData) : ITxCreateCommand<CreateRecordResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();

    public JObject GetMetadataJObject()
    {
        if (string.IsNullOrWhiteSpace(metadataString))
        {
            return new JObject();
        }

        if (!IsValidJson(metadataString))
        {
            throw new Exceptions.Application.InvalidJsonFormatException("Metadata string is not a valid JSON format.");
        }

        return JObject.Parse(metadataString);
    }

    private static bool IsValidJson(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return true;
        }

        try
        {
            _ = JObject.Parse(jsonString);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}

public class CreateRecordValidator : AbstractValidator<CreateRecord>
{
    public CreateRecordValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.Plate)
            .NotEmpty().WithMessage("Plate is required.");

        RuleFor(x => x.CameraId)
            .NotEmpty()
            .GreaterThan(0).WithMessage("CameraId must be greater than 0");
    }
}

public class CreateRecordHandler : ICommandHandler<CreateRecord, CreateRecordResponse>
{
    private readonly ILogger<CreateRecordHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPlateRecognitionDbContext _plateRecognitionDbContext;
    private readonly ICameraApiClient _cameraApiClient;
    private readonly IMakeApiClient _makeApiClient;
    private readonly IModelApiClient _modelApiClient;
    private readonly IColorApiClient _colorApiClient;
    private readonly IOptionApiClient _optionApiClient;

    public CreateRecordHandler(
        IPlateRecognitionDbContext plateRecognitionDbContext,
        ICameraApiClient cameraApiClient,
        IMakeApiClient makeApiClient,
        IModelApiClient modelApiClient,
        IColorApiClient colorApiClient,
        IOptionApiClient optionApiClient,
        IMapper mapper,
        ILogger<CreateRecordHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _plateRecognitionDbContext = Guard.Against.Null(plateRecognitionDbContext, nameof(plateRecognitionDbContext));
        _cameraApiClient = cameraApiClient;
        _makeApiClient = makeApiClient;
        _modelApiClient = modelApiClient;
        _colorApiClient = colorApiClient;
        _optionApiClient = optionApiClient;
    }

    public async Task<CreateRecordResponse> Handle(
        CreateRecord request,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var camera = (await _cameraApiClient.GetCameraByIdAsync(request.CameraId, cancellationToken))?.Camera;
        Guard.Against.NotFound(camera, new CameraNotFoundException(request.CameraId));
        var cameraInformation = CameraInformation.Create(camera!.Id, camera.Name);

        MakeInformation? makeInformation = null;
        if (!string.IsNullOrEmpty(request.OcrMake))
        {
            var make = (await _makeApiClient.CreateMakeAsync(request.OcrMake, cancellationToken))?.Make;
            makeInformation = MakeInformation.Create(make!.Id, make.Name);
        }

        ModelInformation? modelInformation = null;
        if (!string.IsNullOrEmpty(request.OcrModel))
        {
            var model = (await _modelApiClient.CreateModelAsync(request.OcrModel, cancellationToken))?.Model;
            modelInformation = ModelInformation.Create(model!.Id, model.Name, model.TypeName);
        }

        ColorInformation? colorInformation = null;
        if (!string.IsNullOrEmpty(request.OcrColor))
        {
            var color = (await _colorApiClient.CreateColorAsync(request.OcrColor, cancellationToken))?.Color;
            colorInformation = ColorInformation.Create(color!.Id, color.Name);
        }

        JObject metadata = request.GetMetadataJObject();

        if (metadata.ContainsKey("PlateCountry") && metadata["PlateCountry"]?.ToString().Equals("LAT") == true && request.Plate.Length > 6 && char.IsDigit(request.Plate[0]) && char.IsDigit(request.Plate[1]))
        {
            metadata["PlateCountry"] = "TUR";
        }

        var basePath = await _optionApiClient.GetActiveBaseFolderAsync(cancellationToken);
        var smallImageSize = await _optionApiClient.GetOptionByKeyAsync("SMALL_JPEG_WITH", cancellationToken);
        var imageRequest = new CreateRecordImageRequest
        {
            BasePath = basePath.BaseFolderPath,
            SmallImageSize = smallImageSize.Option.Value,
            Plate = request.Plate,
            PlateCountry = metadata["PlateCountry"].ToString(),
            LprDate = request.LprDate,
            CameraName = camera.Name,
            CameraBiosName = camera.BiosName,
            TypeName = modelInformation?.TypeName ?? "",
            MakeName = makeInformation?.Name ?? "",
            ModelName = modelInformation?.Name ?? "",
            ColorName = colorInformation?.Name ?? "",
            ImageData = request.ImageData,
            NRead = metadata.Value<short?>("NRead"),
            Speed = metadata.Value<short?>("Speed"),
            PlatePos = metadata.Value<string>("PlatePos"),
            Score = metadata.Value<short?>("Score"),
            IsNight = metadata.Value<bool>("IsNight")
        };

        var recordImage = new CreateRecordImage(imageRequest);
        var imagePath = recordImage.SaveImage();

        /*var imagePath = $"{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", imagePath);*/

        /*await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await stream.WriteAsync(imageDataBytes);
        }*/

        var record = Record.Create(
            request.Id,
            request.Plate,
            cameraInformation,
            makeInformation,
            modelInformation,
            colorInformation,
            request.LprDate,
            imagePath,
            request.GetMetadataJObject());

        await _plateRecognitionDbContext.Records.AddAsync(record, cancellationToken: cancellationToken);

        await _plateRecognitionDbContext.SaveChangesAsync(cancellationToken);

        var created = await _plateRecognitionDbContext.Records
            .Include(x => x.CameraInformation)
            .Include(x => x.MakeInformation)
            .Include(x => x.ModelInformation)
            .Include(x => x.ColorInformation)
            .SingleOrDefaultAsync(x => x.Id == record.Id, cancellationToken: cancellationToken);

        var recordDto = _mapper.Map<RecordDto>(created);

        _logger.LogInformation("Record a with ID: '{RecordId} created.'", request.Id);

        return new CreateRecordResponse(recordDto);
    }
}
