namespace Inlämning_Backend.Dtos
{
    public record CreateProductRequest(
        string Name,
        decimal Price,
        string Description,
        string Image);
}
