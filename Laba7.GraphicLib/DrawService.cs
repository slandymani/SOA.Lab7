using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Laba7.GraphicLib;


public class DrawService
{
    public DrawResult DrawLinesAndIntersection(DrawParams drawParams)
    {
        using var image = new Image<Rgba32>(400, 400);
        var pen = new SolidPen(Color.Black, 2);
        image.Mutate(img => img.Fill(Color.White));
        image.Mutate(img => img.DrawLine(pen,
                new PointF((float)drawParams.TwoLines.Line1Point1[0], (float)drawParams.TwoLines.Line1Point1[1]),
                new PointF((float)drawParams.TwoLines.Line1Point2[0], (float)drawParams.TwoLines.Line1Point2[1]))
            .DrawLine(pen,
                new PointF((float)drawParams.TwoLines.Line2Point1[0], (float)drawParams.TwoLines.Line2Point1[1]),
                new PointF((float)drawParams.TwoLines.Line2Point2[0], (float)drawParams.TwoLines.Line2Point2[1])));

        if (drawParams.IntersectionPoint != null)
        {
            var ellipse = new EllipsePolygon((float)drawParams.IntersectionPoint.X - 3,
                (float)drawParams.IntersectionPoint.Y - 3, 6, 6);
            var pen2 = new SolidPen(Color.Red, 2);
            image.Mutate(img => img.Draw(pen2, ellipse));
        }
        using var ms = new MemoryStream();
        image.Save(ms, PngFormat.Instance);

        return new DrawResult
        {
            Data = ms.ToArray(),
        };
    }
}