using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Query;
using BuildingBlocks.Core.CQRS.Query;
using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Types.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.GettingTypes;

public record GetTypes : ListQuery<GetTypesResponse>
{
}

internal class GetTypesValidator : AbstractValidator<GetTypes>
{
    public GetTypesValidator()
    {
        /*
         * FluentValidation'da CascadeMode özelliği, doğrulama kuralının nasıl çalıştırılacağını ve
         * bir hata oluştuğunda nasıl devam edileceğini belirler. CascadeMode.Stop değeri, mevcut
         * doğrulayıcı (validator) içinde bir hata meydana geldiğinde, o validator'daki diğer kuralların
         * çalıştırılmasını durdurur ve hataların kümelenmesini önler.
         * Yani, CascadeMode.Stop kullanıldığında, doğrulayıcının mevcut kuralları sırayla çalıştırılır
         * ve herhangi bir kural hata verdiğinde, o doğrulayıcının geri kalan kurallarının kontrolü atlanır.
         * Bu, genellikle performansı artırmak ve gereksiz doğrulamaları önlemek için kullanılır.
         */
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetTypesHandler : IQueryHandler<GetTypes, GetTypesResponse>
{
    private readonly IVehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;

    public GetTypesHandler(IVehicleDbContext vehicleDbContext, IMapper mapper)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
    }

    public async Task<GetTypesResponse> Handle(
        GetTypes command,
        CancellationToken cancellationToken)
    {
        var sortTuples = command.Sorts?.Select(sort => (sortBy: sort, sortOrder: "asc"));
        var types = await _vehicleDbContext.VehicleTypes
            .Where(x => x.Id != 1) // Root kaydını atlar
            .OrderByDescending(x => x.Created)
            .ApplyIncludeList(command.Includes)
            .ApplyFilter(command.Filters)
            .ApplySort(sortTuples)
            .AsNoTracking()
            .ApplyPagingAsync<VehicleType, TypeDto>(_mapper.ConfigurationProvider, command.Page, command.PageSize, cancellationToken: cancellationToken);

        return new GetTypesResponse(types);
    }
}
