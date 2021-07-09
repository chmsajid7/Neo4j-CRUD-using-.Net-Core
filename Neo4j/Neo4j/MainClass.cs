using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neo4j
{
    public class MainClass : IDisposable
    {
        private readonly IDriver _driver;

        public MainClass(string uri, string userName, string Pass)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(userName, Pass));
        }

        public string printGreeting(string msg)
        {
            //_driver = GraphDatabase.Driver(uri, AuthTokens.Basic(userName, Pass));
            using var session = _driver.AsyncSession();
            var greeting = session.WriteTransactionAsync(tx =>
            {
                var res = tx.RunAsync("CREATE (a.Greeting)" +
                    "SET a.message = $message " +
                    "RETURN a.message + ', from node ' + id(a)",
                    new { msg });
                return res;/*.Single()[0].As<string>();*/
            });
            return greeting.ToString();
        }

        public void Dispose()
        {

        }

    }
}
