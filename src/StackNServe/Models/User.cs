namespace StackNServe.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public Users()
        {
            Id = 1;
            Balance = 100;
        }
    }
}
