[System.Serializable]
public class MissingMonsterException : System.Exception
{
    public MissingMonsterException() : base("There are no monsters on the provided space.") { }
    public MissingMonsterException(string message) : base(message) { }
    public MissingMonsterException(string message, System.Exception inner) : base(message, inner) { }
    protected MissingMonsterException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}