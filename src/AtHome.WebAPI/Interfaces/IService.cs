using AtHome.Shared;
using AtHome.Shared.Interfaces;

namespace AtHome.WebApi.Interfaces;

public interface IService<T>
{
    public Task<ErrorOr<T>> CreateAsync(ICreateDto<T> request);
    public Task<ErrorOr<T>> UpdateAsync(IUpdateDto<T> request, int id);
    public Task<ErrorOr<T>> DeleteAsync(int id);
}