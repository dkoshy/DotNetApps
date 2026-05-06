using Globomatics.Web.ValueProviders;

namespace Globomatics.Web.Models;

public class UpdateQuantitiesModel
{
    [FromSession]
    public Guid? CartId { get; set; }

    public IEnumerable<ProductModel>? Products { get; set; }
}