using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4j.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //crud operations link for neo4j:
        //https://neo4j.com/docs/cypher-manual/current/clauses/create/

        private readonly IDriver _driver;

        public HomeController()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "123"));
        }

        #region Nodes
        [HttpPost("node/create")]
        public async Task<IActionResult> CreateNode(string person, string name, string title)
        {
            var statementText = new StringBuilder();
            var session = this._driver.AsyncSession();
            try
            {
                statementText.Append("CREATE (person:" + person + " {name: $name, title: $title})");
                var statementParameters = new Dictionary<string, object>
                {
                    {"name", name },
                    { "title", title}
                };
                var result = await session.WriteTransactionAsync(tx => tx.RunAsync(statementText.ToString(), statementParameters));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Node Not created!");
            }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            return StatusCode(201, "Node has been created in the database");
        }

        [HttpGet("node/get")]
        public async Task<List<string>> ReturnNode(string nodeName)//returns title of node
        {
            var session = _driver.AsyncSession();
            var result = new List<string>();
            try
            {
                var res = await session.RunAsync(
                    "MATCH (n {name: '"+nodeName+"'})" +
                    "RETURN n.title"
                    , // Cypher query
                    new { id = 0 } // Parameters in the query, if any
                );
                while(await res.FetchAsync())
                {
                    var toAdd = new List<string>();
                    foreach (var item in res.Current.Values)
                    {
                        if (item.Key == "n.title")
                        {
                            var temp = item.Value?.ToString();
                            result.Add(temp);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        [HttpPut("node/update")]
        public async Task<IActionResult> UpdateNode(string oldName, string newName)
        {
            var session = _driver.AsyncSession();

            try
            {
                var reader = await session.RunAsync(
                    $"MATCH (Person{{name:\"{oldName}\"}}) " +
                    $"SET " +
                    $"Person.name  = \"{newName}\" "
                    , // Cypher query
                    new { id = 0 } // Parameters in the query, if any
                );
            }
            catch (Exception)
            {
                return StatusCode(500, "Node Not Updated!");
            }
            finally
            {
                await session.CloseAsync();
            }
            return StatusCode(201, "Node has been updated!");
        }

        [HttpDelete("node/delete")]//delete node with its relations
        public async Task<IActionResult> DeleteNode(string nodeName)
        {
            var session = _driver.AsyncSession();

            try
            {
                var reader = await session.RunAsync(
                    "MATCH (n {name: '" + nodeName + "'})" +
                    "DETACH DELETE n"
                    , // Cypher query
                    new { id = 0 } // Parameters in the query, if any
                );
            }
            catch (Exception)
            {
                return StatusCode(500, "Node Not Deleted!");
            }
            finally
            {
                await session.CloseAsync();
            }
            return StatusCode(201, "Node " + nodeName + " & its relations has been deleted");
        }

        #endregion

        #region Connections
        [HttpPost("relation/create")]
        public async Task<IActionResult> CreateRelation(string name1, string name2, string relation)
        {
            var session = _driver.AsyncSession();

            try
            {
                var reader = await session.RunAsync(
                    $"MATCH (a:Person),(b:Person) " +
                    $"WHERE " +
                    $"a.name = '{name1}' AND b.name = '{name2}' " +
                    $"CREATE (a)-[r:{relation}]->(b)"
                    , // Cypher query
                    new { id = 0 } // Parameters in the query, if any
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Relation Not created!");
            }
            finally
            {
                await session.CloseAsync();
            }
            return StatusCode(201, "Relation has been created between " + name1 + " & " + name2);
        }

        [HttpGet("relation/get")]

        #endregion

        [HttpGet("read/AllRecords")]
        public async Task<List<string>> ReadAllRecords()
        {
            var records = new List<string>();
            var session = _driver.AsyncSession();

            try
            {
                // Send cypher query to the database.
                // The existing IResult interface implements IEnumerable
                // and does not play well with asynchronous use cases. The replacement
                // IResultCursor interface is returned from the RunAsync
                // family of methods instead and provides async capable methods.
                var reader = await session.RunAsync(
                    "MATCH (n) RETURN n"
                    , // Cypher query
                    new { id = 0 } // Parameters in the query, if any
                );

                // Loop through the records asynchronously
                while (await reader.FetchAsync())
                {
                    // Each current read in buffer can be reached via Current
                    records.Add(reader.Current[0].ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                // asynchronously close session
                await session.CloseAsync();
            }
            return records;
        }
    }
}
