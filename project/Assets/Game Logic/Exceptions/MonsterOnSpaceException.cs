[System.Serializable]
public class MonsterOnSpaceException : System.Exception
{
    public MonsterOnSpaceException() : base("There is already a monster on the space.") { }
    public MonsterOnSpaceException(string message) : base(message) { }
    public MonsterOnSpaceException(string message, System.Exception inner) : base(message, inner) { }
    protected MonsterOnSpaceException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}