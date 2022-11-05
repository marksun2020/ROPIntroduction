using System;
using FinalVersion.Model;

namespace FinalVersion
{
#pragma warning disable CS8604
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
                throw new InvalidOperationException();
            if (!isSuccess && error == string.Empty)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public Result OnSuccess(Func<Result> func)
        {
            if (this.IsFailure)
                return this;

            return func();
        }

        public Result<T> OnSuccess<T>(Func<Result<T>> func)
        {
            if (this.IsFailure)
                return Result.Fail<T>(this.Error);

            return func();
        }



        public Result OnFailure(Action action)
        {
            if (this.IsFailure)
            {
                action();
            }

            return this;
        }

        public Result OnFailure(Func<Result, Result> func)
        {
            if (this.IsFailure)
            {
                func(this);
            }

            return this;
        }

        public Result OnBoth(Action<Result> action)
        {
            action(this);

            return this;
        }

        public string OnBoth(Func<Result, string> func)
        {
            return func(this);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Ok();
        }
    }


    public class Result<T> : Result
    {
        private readonly T value;
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return value;
            }
        }

        protected internal Result(T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            this.value = value;
        }

        public Result OnSuccess(Func<Result<T>, Result> func)
        {
            if (this.IsFailure)
                return this;

            return func(this);
        }

        public Result<S> OnSuccess<S>(Func<Result<T>, Result<S>> func)
        {
            if (this.IsFailure)
                return Result.Fail<S>(this.Error);

            return func(this);
        }
    }
}
