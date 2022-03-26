namespace UpdevCommunity.Entities
{
    public record Error(string Message)
    {
        public static Error Empty() => new(string.Empty);

        public bool IsEmpty() => string.IsNullOrEmpty(Message);
    };
}
