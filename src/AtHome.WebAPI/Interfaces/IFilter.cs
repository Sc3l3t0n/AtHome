namespace AtHome.WebApi.Interfaces;

public interface IFilter<in T>
{
    public Func<T, bool> ToPredicate();
}