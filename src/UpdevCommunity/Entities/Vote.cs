namespace UpdevCommunity.Entities
{
    public class VoteStatus
    {
        public const string Up = "UpVote";
        public const string Down = "DownVote";
    }
    public class Vote
    {
        public string Status { get; set; } = VoteStatus.Up;

        public string UserId { get; set; } = null!;
        public UpdevUser? User { get; set; }
    }
}
