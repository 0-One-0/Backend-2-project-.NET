using System.Text.RegularExpressions;

namespace Inlämning_Backend.Helper
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string name)
        {
            string slug = name.ToLower().Replace("ä", "a").Replace("ö", "o").Replace("å", "a");
            slug = Regex.Replace(slug, @"[^a-z0-9\s]", "");
            slug = slug.Trim();
            slug = Regex.Replace(slug, @"\s+", "-");
            return slug;
        }
    }
}
