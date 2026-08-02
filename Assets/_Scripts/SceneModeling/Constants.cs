// https://stackoverflow.com/questions/73183879/c-sharp-constants-management-best-practices

namespace SceneModeling
{
    public static class Constants
    {
        public const float LocalScale = 1f;
        public const float RoomWidth = 10f;
        public const float RoomHeight = 5f;
        public const float WallThickness = 0.2f;
        public const float HalfWidth = RoomWidth / 2f;
        public const float Offset = HalfWidth - (WallThickness / 2f); // place wall center so inner face aligns with floor edge
        public const float TableHeight = 0.5f;
        public const float LegHeight = TableHeight;
        public const float LegThickness = 0.05f;
        public const float TableTopThickness = 0.1f;
    }
}
