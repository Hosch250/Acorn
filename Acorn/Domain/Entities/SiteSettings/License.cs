namespace Acorn.Domain.Entities.SiteSettings;

public enum LicenseEnum
{
    MIT,
    Apache2_0
}

public record License(int Id, string Name, string Content)
{
    public static License Mit => new License((int)LicenseEnum.MIT, "MIT", "");
    public static License Apache2_0 => new License((int)LicenseEnum.Apache2_0, "Apache 2.0", "");
}