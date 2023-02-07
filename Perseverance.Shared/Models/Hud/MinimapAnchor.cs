namespace Perseverance.Shared.Models.Hud
{
    internal class MinimapAnchor
    {
        internal float X { get; set; }
        internal float RightX { get; set; }
        internal float Y { get; set; }
        internal float BottomY { get; set; }
        internal float Width { get; set; }
        internal float Height { get; set; }
        internal float UnitX { get; set; }
        internal float UnitY { get; set; }

        internal float XPlusWidthDividedByTwo => X + Width / 2f;
        internal float YMinusHeightDividedByTwo => Y - Height / 2f;
        internal float YPlusHeightDividedByTwo => Y + Height / 2f;
    }
}
