namespace Mappers;

public abstract class Mapper<T1, T2, TRes>
{
    public async Task<IEnumerable<TRes>> MappedTwoModelsInOne(IEnumerable<T1> firstEnumerable,
        IEnumerable<T2> secondEnumerable, params object[] helpers)
    {
        if (firstEnumerable is null) throw new ArgumentNullException(nameof(firstEnumerable));
        if (secondEnumerable is null) throw new ArgumentNullException(nameof(secondEnumerable));

        return firstEnumerable
            .Join(secondEnumerable,
                GetId,
                GetId,
                await CreateDto(helpers));
    }

    protected abstract int GetId(T2 second);
    protected abstract int GetId(T1 first);
    protected abstract Task<Func<T1, T2, TRes>> CreateDto(object[] helpers);
}