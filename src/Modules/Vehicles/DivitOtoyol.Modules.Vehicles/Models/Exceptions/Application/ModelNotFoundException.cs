using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;

public class ModelNotFoundException : NotFoundException
{
    public ModelNotFoundException(long id) : base($"Araç modeli bulunamadı: '{id}'")
    {
    }

    public ModelNotFoundException(string message) : base(message)
    {
    }
}
