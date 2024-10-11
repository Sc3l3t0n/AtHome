namespace AtHome.Shared.Interfaces;

public interface IFilter<in T>
{
    public Func<T, bool> ToPredicate();
}