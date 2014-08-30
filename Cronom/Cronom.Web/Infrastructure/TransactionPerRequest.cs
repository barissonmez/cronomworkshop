using System.Data.Entity;
using System.Web;
using Cronom.Web.Data;
using Cronom.Web.Infrastructure.Tasks;

namespace Cronom.Web.Infrastructure
{
	public class TransactionPerRequest :
		IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
	{
		private readonly CronomDBContext _dbContext;
		private readonly HttpContextBase _httpContext;

		public TransactionPerRequest(CronomDBContext dbContext,HttpContextBase httpContext)
		{
			_dbContext = dbContext;
			_httpContext = httpContext;
		}

		void IRunOnEachRequest.Execute()
		{
			_httpContext.Items["_Transaction"] =
				_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
		}

		void IRunOnError.Execute()
		{
			_httpContext.Items["_Error"] = true;
		}

		void IRunAfterEachRequest.Execute()
		{
			var transaction = (DbContextTransaction) _httpContext.Items["_Transaction"];

			if (_httpContext.Items["_Error"] != null)
			{
				transaction.Rollback();
			}
			else
			{
				transaction.Commit();				
			}
		}
	}
}