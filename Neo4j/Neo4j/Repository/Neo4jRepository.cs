using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace Neo4j.Repository
//{
//    public class Neo4jRepository
//    {
//    }
//}
//using Microsoft.Extensions.Configuration;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using MongoDB.Driver.Linq;
//using Microsoft.EntityFrameworkCore;
//using Common.Helpers;
//using Polly;
//using Common.Models;
//using Konnectrix.Common.Models;
//using Konnectrix.Common.Constants;
//using Neo4j.Driver;
//using Konnectrix.Common.ViewModels;

namespace Neo4j.Repository
{
    public class Neo4jRepository : INeo4jRepository
    {
        //private readonly IDriver _driver;

        //public Neo4jRepository(IConfiguration configuration)
        //{
        //    var url = configuration.GetNeo4jUrl();
        //    var username = configuration.GetNeo4jUsername();
        //    var password = configuration.GetNeo4jPassword();

        //    _driver = GraphDatabase.Driver(url, AuthTokens.Basic(username, password));
        //}

        //public IDriver GetContext()
        //{
        //    return this._driver;
        //}

        //public async Task<bool> CreateConnection(ConnectionRequest AcceptedConnectionRequest)
        //{
        //    var session = _driver.AsyncSession();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (user1:User),(user2:User) " +
        //            $"WHERE " +
        //            $"user1.id = '{AcceptedConnectionRequest.RequesterId}' AND user2.id = '{AcceptedConnectionRequest.ReceiverId}' " +
        //            $"CREATE (user1)-[r:Connected {{}} ]->(user2)"
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    return true;
        //}

        //public async Task<bool> RemoveConnection(string loggedInUserId, string userId)
        //{
        //    var session = _driver.AsyncSession();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (loggedInUser{{id:\"{loggedInUserId}\"}})-[friend:Connected]-(User{{id:\"{userId}\"}}) delete friend "
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    return true;
        //}

        //public async Task<bool> UpdateUser(UserProfile user)
        //{
        //    var session = _driver.AsyncSession();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (usr{{id:\"{user.UserId}\"}}) " +
        //            $"SET " +
        //            $"usr.name  = \"{string.Concat(user.FirstName, " ", user.LastName)}\" ," +
        //            $"usr.designation   = \"{user.Occupation }\"  ," +
        //            $"usr.location  = \"{string.Concat(user.City.Name, ", ", user.Country.Name)}\" "
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    return true;
        //}

        //public async Task<bool> UpdateUserProfilePicture(string loggedInUserId, string userProflePicture)
        //{
        //    var session = _driver.AsyncSession();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (usr{{id:\"{loggedInUserId}\"}}) " +
        //            $"SET " +
        //            $"usr.photo = \"{userProflePicture}\""
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    return true;
        //}

        //public async Task<List<string>> ReadAllRecordsNeo4jTest()
        //{
        //    var records = new List<string>();
        //    var session = _driver.AsyncSession();

        //    try
        //    {
        //        // Send cypher query to the database.
        //        // The existing IResult interface implements IEnumerable
        //        // and does not play well with asynchronous use cases. The replacement
        //        // IResultCursor interface is returned from the RunAsync
        //        // family of methods instead and provides async capable methods.
        //        var reader = await session.RunAsync(
        //            "MATCH (p) RETURN p"
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );

        //        // Loop through the records asynchronously
        //        while (await reader.FetchAsync())
        //        {
        //            // Each current read in buffer can be reached via Current
        //            records.Add(reader.Current[0].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    return records;
        //}

        //public async Task<List<ConnectionsViewModel>> GetAllConnections(string userId, int page, int pageSize)
        //{
        //    var session = _driver.AsyncSession();
        //    var records = new List<ConnectionsViewModel>();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (me:User {{id:\"{userId}\"}})-[:Connected]-(friend:User) " +
        //            "OPTIONAL MATCH(friend) -[:Connected]-(fnf: User) " +
        //            "WHERE ID(me) <> ID(fnf) " +
        //            "OPTIONAL MATCH(fnf)-[:Connected] - (fnfL22: User) " +
        //            "WHERE ID(me) = ID(fnfL22) " +
        //            "RETURN friend.id as UserId, friend.name as Name, " +
        //            " friend.location as Location, " +
        //            " friend.designation as Designation, friend.photo as Photo , count(fnf)+1 as TotalFriendsCount, count(fnfL22) as MutualFriendsCount " +
        //            " ORDER BY friend.id " +
        //            $" skip {((page - 1) * pageSize)} limit {pageSize}; "
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //        while (await reader.FetchAsync())
        //        {
        //            // Each current read in buffer can be reached via Current
        //            var toAdd = new ConnectionsViewModel();

        //            foreach (var item in reader.Current.Values)
        //            {
        //                //a.Name = "";// item["Name"];
        //                if (item.Key == "UserId")
        //                {
        //                    toAdd.UserId = item.Value?.ToString();
        //                }
        //                else if (item.Key == "Name")
        //                {
        //                    toAdd.Name = item.Value?.ToString();
        //                }
        //                else if (item.Key == "Location")
        //                {
        //                    toAdd.Location = item.Value?.ToString();
        //                }
        //                else if (item.Key == "Designation")
        //                {
        //                    toAdd.Designation = item.Value?.ToString();
        //                }
        //                else if (item.Key == "Photo")
        //                {
        //                    toAdd.ProfilePicture = item.Value?.ToString();
        //                }
        //                else if (item.Key == "TotalFriendsCount")
        //                {
        //                    toAdd.TotalConnectionsCount = Convert.ToInt32(item.Value?.ToString());
        //                }
        //                else if (item.Key == "MutualFriendsCount")
        //                {
        //                    toAdd.MutualConnectionsCount = Convert.ToInt32(item.Value?.ToString());
        //                }
        //            }
        //            records.Add(toAdd);
        //        }
        //        return records;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<ConnectionsViewModel>();
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    //return new List<ConnectionsViewModel>();
        //}

        //public async Task<ConnectionsViewModel> GetVisitedMutualConnections(string userId, string visitedUserId)
        //{
        //    var session = _driver.AsyncSession();
        //    var records = new ConnectionsViewModel();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (usr:User {{id:\"{visitedUserId}\"}})-[:Connected]-(friend:User)" +
        //            $"OPTIONAL MATCH (usr)-[:Connected]-(meAsFriend:User{{id:\'{userId}\'}})" +
        //            $"OPTIONAL MATCH (friend)-[:Connected]-(me:User{{id:'{userId}'}})" +
        //            "RETURN count(friend) as TotalFriendsCount ,count(me) as MutualFriendsCount ," +
        //            "count(meAsFriend)> 0 as IsFriend"
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //        while (await reader.FetchAsync())
        //        {
        //            // Each current read in buffer can be reached via Current
        //            var toAdd = new ConnectionsViewModel();

        //            foreach (var item in reader.Current.Values)
        //            {
        //                //a.Name = "";// item["Name"];
        //                if (item.Key == "IsFriend")
        //                {
        //                    toAdd.IsConnected = Convert.ToBoolean(item.Value?.ToString());
        //                }
        //                else if (item.Key == "TotalFriendsCount")
        //                {
        //                    toAdd.TotalConnectionsCount = Convert.ToInt32(item.Value?.ToString());
        //                }
        //                else if (item.Key == "MutualFriendsCount")
        //                {
        //                    toAdd.MutualConnectionsCount = Convert.ToInt32(item.Value?.ToString());
        //                }
        //            }
        //            records = toAdd;
        //        }
        //        return records;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ConnectionsViewModel();
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }

        //    //return new List<ConnectionsViewModel>();
        //}
        //public async Task<List<ConnectionsViewModel>> GetMutualConnectionsForSearchResults(string userId, List<string> searchedUserList)
        //{
        //    var session = _driver.AsyncSession();
        //    var records = new List<ConnectionsViewModel>();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $"MATCH (usr:User)-[:Connected]-(friend:User) " +
        //            $"WHERE usr.id in ['{string.Join("','", searchedUserList)}']" +
        //            $"OPTIONAL MATCH (usr)-[:Connected]-(meAsFriend:User{{id:\'{userId}\'}})" +
        //            $"OPTIONAL MATCH (friend)-[:Connected]-(me:User{{id:'{userId}'}})" +
        //            "RETURN usr.id as UserId, count(friend) as TotalFriendsCount ,count(me) as MutualFriendsCount ," +
        //            "count(meAsFriend)> 0 as IsFriend"
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //        while (await reader.FetchAsync())
        //        {
        //            // Each current read in buffer can be reached via Current
        //            var toAdd = new ConnectionsViewModel();

        //            foreach (var item in reader.Current.Values)
        //            {
        //                if (item.Key == "UserId")
        //                {
        //                    toAdd.UserId = item.Value?.ToString();
        //                }
        //                else if (item.Key == "IsFriend")
        //                {
        //                    toAdd.IsConnected = Convert.ToBoolean(item.Value?.ToString());
        //                }
        //                else if (item.Key == "TotalFriendsCount")
        //                {
        //                    toAdd.TotalConnectionsCount = Convert.ToInt32(item.Value?.ToString());
        //                }
        //                else if (item.Key == "MutualFriendsCount")
        //                {
        //                    toAdd.MutualConnectionsCount = Convert.ToInt32(item.Value?.ToString());
        //                }
        //            }
        //            records.Add(toAdd);
        //        }
        //        return records;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<ConnectionsViewModel>();
        //    }
        //    finally
        //    {
        //        // asynchronously close session
        //        await session.CloseAsync();
        //    }
        //}
        //public async Task<List<string>> GetAllConnectionsIds(string userId)
        //{
        //    var session = _driver.AsyncSession();
        //    var records = new List<string>();

        //    try
        //    {
        //        var reader = await session.RunAsync(
        //            $" MATCH (me:User {{id:\"{userId}\"}})-[:Connected]-(friend:User) " +
        //            $" RETURN friend.id as UserId"
        //            , // Cypher query
        //            new { id = 0 } // Parameters in the query, if any
        //        );
        //        while (await reader.FetchAsync())
        //        {
        //            var toAdd = new List<string>();
        //            foreach (var item in reader.Current.Values)
        //            {
        //                if (item.Key == "UserId")
        //                {
        //                    var temp = item.Value?.ToString();
        //                    records.Add(temp);
        //                }
        //            }
        //        }
        //        return records;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<string>();
        //    }
        //    finally
        //    {
        //        await session.CloseAsync();
        //    }
        //}

    }
}
