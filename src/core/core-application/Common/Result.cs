namespace core_application.Common
{
    public class Result<T> : Result
    {
        private Result(T data, bool isSuccess, IEnumerable<string> errors) : base(isSuccess, errors)
        {
            Data = data;
        }

        public T Data { get; private set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data, true, new string[] { });
        }
    }

    public class Result
    {
        protected Result(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors.ToArray();
        }

        protected Result()
        {
            IsSuccess = true;
            Errors = new string[] { };
        }

        public bool IsSuccess { get; private set; }
        public string[] Errors { get; private set; }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Success()
        {
            return new Result(true, new List<string>());
        }
    }
}
