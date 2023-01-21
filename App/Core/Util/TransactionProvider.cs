

// Decompiled with JetBrains decompiler
// Type: LeoMongo.Transaction.TransactionProvider
// Assembly: LeoMongo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C0A5A32B-7776-416B-BD5C-0C9DD2A9631E
// Assembly location: /home/deb/.nuget/packages/htlleonding.database.leomongo/1.0.0/lib/net7.0/LeoMongo.dll
// Edited by Florian Keintzel

using LeoMongo.Database;
using MongoDB.Driver;
using Nito.AsyncEx;
using LeoMongo.Transaction;


#nullable enable
namespace MongoDBDemoApp.Core.Util
{
    public sealed class TransactionProvider : ITransactionProvider, ISessionProvider
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly AsyncLock _mutex;
        private IClientSessionHandle? _session;

        public TransactionProvider(IDatabaseProvider databaseProvider)
        {
            this._databaseProvider = databaseProvider;
            this._mutex = new AsyncLock();
        }

        public IClientSessionHandle Session => this.InTransaction && this._session != null ? this._session : throw new InvalidOperationException("transaction not started");

        public bool InTransaction { get; private set; }

        public async Task<LeoMongo.Transaction.Transaction> BeginTransaction()
        {
            LeoMongo.Transaction.Transaction transaction;
            using (await this._mutex.LockAsync())
            {
                if (this.InTransaction)
                    throw new InvalidOperationException("transaction already started");
                IClientSessionHandle clientSessionHandle = await this._databaseProvider.StartSession();
                this._session = clientSessionHandle;
                clientSessionHandle = (IClientSessionHandle) null!;
                this._session.StartTransaction();
                this.InTransaction = true;
                transaction = new LeoMongo.Transaction.Transaction(this._session);
            }
            return transaction;
        }
    }
}
