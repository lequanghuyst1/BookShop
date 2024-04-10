using BookShopOnline.Core.Interfaces.Infrastructures;
using BookShopOnline.Core.Interfaces.UnitOfWork;
using BookShopOnline.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbContext _dbContext;
        public UnitOfWork(IDbContext dbContext, ICartItemRepository cartItemRepository)
        {
            _dbContext = dbContext;
            CartItems = cartItemRepository;
        }

        

        public ICartItemRepository CartItems { get; }

        public void BeginTransaction()
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Transaction.Commit();
        }

        public void Dispose()
        {
            if (_dbContext.Connection.State == ConnectionState.Open)
            {
                _dbContext.Connection.Close();
            }
            _dbContext.Connection?.Dispose();
        }

        public void Rollback()
        {
            _dbContext.Transaction.Rollback();
        }
    }
}
