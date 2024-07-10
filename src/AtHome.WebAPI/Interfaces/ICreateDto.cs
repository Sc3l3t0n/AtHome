namespace AtHome.WebApi.Interfaces;

public interface ICreateDto<T>
{
    public ErrorOr<T> ToEntity();
}