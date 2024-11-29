namespace Laba7.CalculationLib;

public class FindIntersectionService
{
    public IntersectionPoint Find(TwoLines twoLines)
    {
        double x1 = twoLines.Line1Point1[0], y1 = twoLines.Line1Point1[1];
        double x2 = twoLines.Line1Point2[0], y2 = twoLines.Line1Point2[1];
        double x3 = twoLines.Line2Point1[0], y3 = twoLines.Line2Point1[1];
        double x4 = twoLines.Line2Point2[0], y4 = twoLines.Line2Point2[1];

        double denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if (denominator == 0)
        {
            throw new Exception("Прямі паралельні або співпадають.");
        }

        double px = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominator;
        double py = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominator;

        return new IntersectionPoint { X = px, Y = py };
    }
}