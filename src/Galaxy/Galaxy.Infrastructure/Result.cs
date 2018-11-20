using System;
namespace Galaxy.Infrastructure
{
    /// <summary>
    /// Result.
    /// </summary>
    public class Result<T> : Result
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        /// <summary>
        /// 隐藏构造
        /// </summary>
        protected Result() { }

        /// <summary>
        /// Success the specified data and message.
        /// </summary>
        /// <returns>The success.</returns>
        /// <param name="data">Data.</param>
        /// <param name="message">Message.</param>
        public static Result<T> Success(T data, string message = "")
        {
            return new Result<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }

        /// <summary>
        /// Fail the specified message.
        /// </summary>
        /// <returns>The fail.</returns>
        /// <param name="message">Message.</param>
        public new static Result<T> Failed(string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message
            };
        }

        /// <summary>
        /// Fail the specified data and message.
        /// </summary>
        /// <returns>The fail.</returns>
        /// <param name="data">Data.</param>
        /// <param name="message">Message.</param>
        public static Result<T> Failed(T data, string message)
        {
            return new Result<T>
            {
                Data = data,
                IsSuccess = false,
                Message = message
            };
        }
    }

    public class Result
    {
        public bool IsSuccess { get; protected set; }

        public string Message { get; protected set; }

        protected Result()
        {
        }

        public static Result Success()
        {
            return Success(string.Empty);
        }

        public static Result Success(string message)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static Result Failed(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
