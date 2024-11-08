namespace _project.Scripts.EnvironmentLogic
{
    [System.Flags]
    public enum EnvironmentType
    {
        None = 0,
        StableGround = 1 << 0,
        Ladder = 1 << 1,
    }
}