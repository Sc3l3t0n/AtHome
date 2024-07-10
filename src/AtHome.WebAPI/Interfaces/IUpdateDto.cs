namespace AtHome.WebApi.Interfaces;

public interface IUpdateDto<T>
{
    public ErrorOr<T> UpdateEntity(T entity);
}