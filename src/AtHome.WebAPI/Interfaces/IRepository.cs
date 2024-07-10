﻿namespace AtHome.WebApi.Interfaces;

public interface IRepository<T>
{
    public Task<ErrorOr<IEnumerable<T>>> GetAll();
    public Task<ErrorOr<T>> Get(int id);
}