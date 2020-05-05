[System.Serializable]
public class SpaceNotWithinRangeException : System.Exception
{
    public SpaceNotWithinRangeException() : base("Space is not within the monster's movement or attack range, or there is another monster in the space..") { }
    public SpaceNotWithinRangeException(string message) : base(message) { }
    public SpaceNotWithinRangeException(string message, System.Exception inner) : base(message, inner) { }
    protected SpaceNotWithinRangeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}