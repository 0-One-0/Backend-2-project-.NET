namespace Inlämning_Backend.Dtos
{
    public record ProductDto(int Id, string Name,
        decimal Price, string Description, string Image, string UrlSlug);
}
