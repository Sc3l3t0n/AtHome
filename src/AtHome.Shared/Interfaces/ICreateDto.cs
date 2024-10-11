namespace AtHome.Shared.Interfaces;

public interface ICreateDto<T>
{
    public ErrorOr<T> ToEntity();
}