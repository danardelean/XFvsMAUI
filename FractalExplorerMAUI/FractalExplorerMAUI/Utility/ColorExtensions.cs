using System;

namespace FractlExplorer;

static class ColorExtensions
{
    public static int ToABGR(this Color c)
    {
        return 255 << 24 
        | ((byte)(c.Blue * 255)) << 16
        | ((byte)(c.Green * 255)) << 8
        | ((byte)(c.Red * 255));
    }
}
