namespace BlogBackend2.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            this.IsSuccess = success;
        }

        public Result(bool success, string message) : this(success)
        {
            this.Message = message;
        }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
