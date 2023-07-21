namespace CompanyPL.Common.Utility.Extensions;

/// <summary>
/// Extension methods for <see cref="IAsyncEnumerable{T}"/>
/// </summary>
public static class AsyncEnumerableExtensions
{
    /// <summary>
    /// Converts <see cref="IAsyncEnumerable{T}"/> to <see cref="List{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
    public static Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return ExecuteAsync();

        async Task<List<T>> ExecuteAsync()
        {
            var list = new List<T>();

            await foreach (var element in source)
            {
                list.Add(element);
            }

            return list;
        }
    }
}