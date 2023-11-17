namespace MiddleEarth.Builder.Application.Extensions;

public static class ListExtensions
{
    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        if (indexA < 0 || indexB < 0 || indexA >= list.Count || indexB >= list.Count)
            return list;

        (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        return list;
    }
}
