namespace Acorn.Domain.Entities.SiteSettings;

public enum LicenseEnum
{
    MIT = 1,
    Apache2_0 = 2
}

public record License(LicenseEnum Id, string Name, string Content)
{
    public static License Mit => new License(LicenseEnum.MIT, "MIT", "");
    public static License Apache2_0 => new License(LicenseEnum.Apache2_0, "Apache 2.0", "");
}