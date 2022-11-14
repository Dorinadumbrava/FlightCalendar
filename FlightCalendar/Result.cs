namespace FlightCalendar
{
    public struct Result
    {
        public Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result Failed(string error)
        {
            return new Result(false, error);
        }
    }

    public struct Result<T>
        where T: class
    {
        public Result(T value):this(value,true, string.Empty)
        {
            Value = value;
        }

        public Result(string error): this(null, false, error)
        {
            Error = error;
        }

        public Result(T value, bool isSuccess, string error)
        {
            Error = error;
            Value = value;
            IsSuccess = isSuccess;
        }

        public T Value { get; }
        public string Error { get; }
        public bool IsSuccess { get; }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failed(string error)
        {
            return new Result<T>(error);
        }
    }
}