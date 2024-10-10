namespace StackNServe.Shared 
{
    public class OrderCheckRequest
    {
        public List<string> UserOrderList { get; set; }
        public List<string> RequiredOrderList { get; set; }
        public int CurrentPlayerScore { get; set; }
        public int OrderPrice { get; set; }
    }

    public class OrderCheckResponse
    {
        public bool IsOrderCorrect { get; set; }
        public int FinalScore { get; set; }
        public string Message { get; set; }
    }
}
