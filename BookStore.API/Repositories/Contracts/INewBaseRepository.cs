﻿namespace BookStore.API.Repositories.Contracts;

public interface INewBaseRepository<T> where T : class
{
    Task<bool> Create<TMap>(TMap entityDto) where TMap : class;
    Task<bool> DeleteAsync(int id);
    Task<List<TMap>> GetAllAsync<TMap>() where TMap : class;
    Task<VirtualizeResponse<TMap>> GetAllWithPg<TMap>(QueryParameters queryParams) where TMap : class;
    Task<TMap> GetAsync<TMap>(int? id) where TMap : class;
    Task<bool> UpdateAsync<TMap>(TMap entityDto) where TMap : class;
}