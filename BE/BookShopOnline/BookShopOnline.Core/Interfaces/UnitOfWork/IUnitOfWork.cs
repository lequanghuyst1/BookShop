using BookShopOnline.Core.Interfaces.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopOnline.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        ICartItemRepository CartItems { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; } 
        IBookRepository Book { get; }

        /// <summary>
        /// Bắt đầu transition
        /// </summary>
        /// /// Created By: LQHUY(15/01/2024)
        void BeginTransaction();

        /// <summary>
        /// Commit vào database
        /// </summary>
        /// Created By: LQHUY(15/01/2024)
        void Commit();

        /// <summary>
        /// Quay trở lại trasition
        /// </summary>
        /// Created By: LQHUY(15/01/2024)
        void Rollback();
    }
}
