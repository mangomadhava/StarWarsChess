[System.Serializable]
public class InvalidMonsterCountException : System.Exception
{
    public InvalidMonsterCountException() : base("Only four monsters are allowed.") { }
    public InvalidMonsterCountException(string message) : base(message) { }
    public InvalidMonsterCountException(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidMonsterCountException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}