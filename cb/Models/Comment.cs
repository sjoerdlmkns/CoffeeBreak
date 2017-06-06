namespace cb.Models
{
    public class Comment
    {
        public int Id { get; private set; }
        public User User { get; private set; }
        public string Message { get; private set; }
        public int Score { get; private set; }

        public Comment(int id, User user, string message, int score)
        {
            Id = id;
            User = user;
            Message = message;
            Score = score;
        }
    }
}