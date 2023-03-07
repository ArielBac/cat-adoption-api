﻿using CatAdoptionApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected CatAdoptionContext _context;

        public Repository(CatAdoptionContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }


        public void Delete(T entity)
        {
            _context.Remove(entity);
        }
    }
}