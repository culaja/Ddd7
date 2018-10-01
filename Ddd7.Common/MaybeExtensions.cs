using System;

namespace Ddd7.Common
{
    public static class MaybeExtensions
    {
        public static T Unwrap<T>(this Maybe<T> maybe, Action<T> action, T defaultValue = default(T))
        {
            if (maybe.HasValue)
            {
                action(maybe.Value);
                return maybe.Value;
            }

            return defaultValue;
        }

        public static K Unwrap<T, K>(this Maybe<T> maybe, Func<T, K> selector, Func<K> onNoneFunc)
        {
            if (maybe.HasValue)
                return selector(maybe.Value);

            return onNoneFunc();
        }

        public static Maybe<K> Map<T, K>(this Maybe<T> maybe, Func<T, K> transformer)
        {
            if (maybe.HasValue)
            {
                return Maybe<K>.From(transformer(maybe.Value));
            }

            return Maybe<K>.None;
        }

        public static Maybe<K> FlatMap<T, K>(this Maybe<T> maybe, Func<T, Maybe<K>> transformer)
        {
            if (maybe.HasValue)
            {
                return transformer(maybe.Value);
            }

            return Maybe<K>.None;
        }

        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe)
        {
            if (maybe.HasValue)
            {
                return maybe.Value;
            }

            return Maybe<T>.None;
        }

        public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<T> transformer)
        {
            if (maybe.HasNoValue)
            {
                return transformer();
            }

            return maybe;
        }

        public static Maybe<K> ConvertToDerived<T, K>(this Maybe<T> baseMaybe) where K : T
        {
            if (baseMaybe.HasValue)
            {
                return Maybe<K>.From((K)baseMaybe.Value);
            }
            else
            {
                return Maybe<K>.None;
            }
        }

        public static Maybe<T> ToMaybe<T>(this T value) where T : class => Maybe<T>.From(value);

        public static Maybe<T> Ensure<T>(this Maybe<T> maybe, Func<T, bool> predicate, Action<T> onErrorAction)
        {
            if (maybe.HasValue && !predicate(maybe.Value))
            {
                onErrorAction(maybe.Value);
            }

            return maybe;
        }

        public static Maybe<T> Ensure<T>(this Maybe<T> maybe, Func<Maybe<T>, bool> predicate, Action<Maybe<T>> onErrorAction)
        {
            if (!predicate(maybe))
            {
                onErrorAction(maybe);
            }

            return maybe;
        }

        public static Maybe<T> Ensure<T>(this Maybe<T> maybe, Func<Maybe<T>, bool> predicate, Action onErrorAction)
        {
            if (!predicate(maybe))
            {
                onErrorAction();
            }

            return maybe;
        }

        public static Result ToResult(this Maybe<Result> maybeResult) =>
            maybeResult.HasValue ? maybeResult.Value : Result.Ok();

        public static Result ToResult<T>(this Maybe<Result<T>> maybeResult) =>
            maybeResult.HasValue ? maybeResult.Value : Result.Ok();
    }
}