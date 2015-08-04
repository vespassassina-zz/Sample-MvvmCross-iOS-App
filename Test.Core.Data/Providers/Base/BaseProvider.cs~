using System;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using Cirrious.CrossCore;
using System.IO;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Test.Core.Data
{
	public interface IBaseProvider<T>
	{

		void DropTable();

		int Insert(T obj);

		int Delete(T obj);

		int Update(T obj);

		int TotalCount();

		bool Exists(Expression<Func<T, bool>> predicate);

		T Find(Expression<Func<T, bool>> predicate);

		List<T> Search(Expression<Func<T, bool>> predicate);

		List<T> AsList();

	}

	public abstract class BaseProvider<T> : IDisposable where T : new()
	{

		const string dbname = "test.db.sql";
		ISQLiteConnection connection;
		object connectionLock = new object();


		protected ISQLiteConnection Connection {
			get {

				//double lock pattern
				if (connection == null) {
					lock (connectionLock) {
						if (connection == null) {
							var connectionFactory = Mvx.GetSingleton<ISQLiteConnectionFactory>();
							connection = connectionFactory.Create(dbname);
							connection.CreateTable<T>();
						}
					}
				}

				return this.connection;
			}
			set {
				this.connection = value;
			}
		}

		public void Dispose()
		{
			if (connection != null) {

				try {
					connection.Close();
				} catch (Exception ex) {
					//keep cleaning, it could have been already closed
				}

				connection.Dispose();
				connection = null;
			}
		}

		public void DropTable()
		{
			Connection.DropTable<T>();
			Connection.CreateTable<T>();
		}

		public int Insert(T obj)
		{
			return Connection.Insert(obj);
		}

		public int Delete(T obj)
		{
			return Connection.Delete(obj);
		}

		public int Update(T obj)
		{

			return Connection.Update(obj);
		}

		public int TotalCount()
		{
			return Connection.Table<T>().Count();
		}

		public bool Exists(Expression<Func<T, bool>> predicate)
		{
			return Connection.Table<T>().Where(predicate).Any();
		}

		public List<T> AsList()
		{
			return Connection.Table<T>().ToList();
		}

		public T Find(Expression<Func<T, bool>> predicate)
		{
			return Connection.Table<T>().Where(predicate).FirstOrDefault();
		}

		public List<T> Search(Expression<Func<T, bool>> predicate)
		{
			return Connection.Table<T>().Where(predicate).ToList();
		}
				

	}
}

