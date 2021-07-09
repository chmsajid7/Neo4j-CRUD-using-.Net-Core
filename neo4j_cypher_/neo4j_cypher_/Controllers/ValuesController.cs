using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using neo4j_cypher_.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo4j_cypher_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBoltGraphClient _boltGraphClient;
        //var client = ""
        public ValuesController()
        {
            try
            {
                var client = new GraphClient(new Uri("http://192.168.100.37:7474"), "neo4j", "123456");
                //
                //new GraphClient(new Uri("bolt://localhost/"))
                //new GraphClient(new Uri("http://localhost.:7474/db/data"))
                //_boltGraphClient = new BoltGraphClient("bolt://localhost:7687", "neo4j", "123");
                client.ConnectAsync();
                client.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("GetMutualConnectionsForSearchResults")]//incomplete
        public async Task<List<ConnectionsViewModel>> GetMutualConnectionsForSearchResults(string userId, List<string> searchedUserList)
        {
            //await _boltGraphClient.ConnectAsync();
            var records = new List<ConnectionsViewModel>();

            try
            {
                //var reader = await session.RunAsync(
                //    $"MATCH (usr:User)-[:Connected]-(friend:User) " +
                //    $"WHERE usr.id in ['{string.Join("','", searchedUserList)}']" +
                //    $"OPTIONAL MATCH (usr)-[:Connected]-(meAsFriend:User{{id:\'{userId}\'}})" +
                //    $"OPTIONAL MATCH (friend)-[:Connected]-(me:User{{id:'{userId}'}})" +
                //    "RETURN usr.id as UserId, count(friend) as TotalFriendsCount ,count(me) as MutualFriendsCount ," +
                //    "count(meAsFriend)> 0 as IsFriend"
                //    , // Cypher query
                //    new { id = 0 } // Parameters in the query, if any
                //);

                //var reader = await _boltGraphClient.Cypher
                //    .Match("(user1:Person)-[r:Connected]-(friend:Person)")
                //    .Where((ConnectionsViewModel user1) => user1.name == "Salman")
                //    .OptionalMatch("(user1)-[r:Connected]-(meAsFriend:Person)")
                //    .Where((ConnectionsViewModel meAsFriend) => meAsFriend.name == "Yawar")
                //    .OptionalMatch("(user1)-[r]-(mutualFriend)-[r1]-(user2:Person)")
                //    .Where((ConnectionsViewModel user2) => user2.name == "Yawar")
                //    .Return((mutualFriend, friend, meAsFriend) => new
                //    {
                //        MutualFriendsCount = mutualFriend.Count(),
                //        TotalFriendsCount = friend.Count(),
                //        IsFriend = meAsFriend.Count()
                //    })
                //    .ResultsAsync;

                //var toAdd = new ConnectionsViewModel();
                //foreach (var item in reader)
                //{
                //    if (item.IsFriend.ToString() == "1")
                //        toAdd.IsConnected = true;

                //    toAdd.TotalConnectionsCount = Convert.ToInt32(item.TotalFriendsCount.ToString());
                //    toAdd.MutualConnectionsCount = Convert.ToInt32(item.MutualFriendsCount.ToString());
                //    records.Add(toAdd);
                //}

                //var reader = await _boltGraphClient.Cypher
                //    .Match("(usr:Person)-[r:Connected]-(friend:Person)")
                //    .Where((ConnectionsViewModel usr) => usr.name == "Umair")
                //    .OptionalMatch("(usr)-[r:Connected]-(meAsFriend:Person)")
                //    .Where((ConnectionsViewModel meAsFriend) => meAsFriend.name == "Yawar")
                //    .OptionalMatch("(friend)-[r:Connected]-(me:Person)")
                //    .Where((ConnectionsViewModel me) => me.name == "Yawar")
                //    .Return((usr, friend, meAsFriend, me) => new
                //    {
                //        UserId = usr,
                //        TotalFriendsCount = friend.Count(),
                //        MutualFriendsCount = me.Count(),
                //        IsFriend = meAsFriend.Count()
                //    })
                //    .ResultsAsync;

                //var toAdd = new ConnectionsViewModel();
                //foreach (var item in reader)
                //{
                //    if (item.IsFriend.ToString() == "1")
                //        toAdd.IsConnected = true;

                //    toAdd.TotalConnectionsCount = Convert.ToInt32(item.TotalFriendsCount.ToString());
                //    toAdd.MutualConnectionsCount = Convert.ToInt32(item.MutualFriendsCount.ToString());
                //    records.Add(toAdd);
                //}
            }
            catch (Exception ex)
            {
                return new List<ConnectionsViewModel>();
            }
            finally
            {
                //_boltGraphClient.Dispose();
            }
            return records;
        }

        [HttpGet("Create_Connection")]//done
        public async Task<bool> CreateConnection()
        {
            try
            {
                var client = new GraphClient(new Uri("http://192.168.100.37:7474"), "neo4j", "123456");
                await client.ConnectAsync();

                await client.Cypher
                    .Match("(user1:User)", "(user2:User)")
                    .Where((User user1) => user1.id == "547f1a57-8c88-4343-b897-2d93981772cd")//RequesterId
                    .AndWhere((User user2) => user2.id == "547f1a57-8c88-4343-b897-2d93981772cd")//ReceiverId
                    .Create("(user1)-[a:Connected]->(user2)")
                    .ExecuteWithoutResultsAsync();

                client.Dispose();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpGet("Remove_Connection")]//done
        public async Task<bool> RemoveConnection(string loggedInUserId, string userId)
        {
            try
            {
                //await _boltGraphClient.ConnectAsync();

                await _boltGraphClient.Cypher
                    .Match("(user1:Person)-[r:FRIENDS_WITH]-(user2:Person)")
                    .Where((User user1) => user1.name == "Amir")
                    .AndWhere((User user2) => user2.name == "Abid")
                    .Delete("r")
                    .ExecuteWithoutResultsAsync();

                //_boltGraphClient.Dispose();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpPost("CreateUser")]//done
        public async Task<List<string>> CreateUser(UserProfile user)
        {
            var records = new List<string>();
            await _boltGraphClient.ConnectAsync();

            try
            {
                string UserId = Guid.NewGuid().ToString();
                var newUser = new User { id = UserId, name = user.FirstName +" "+ user.LastName };

                await _boltGraphClient.Cypher
                    .Create("(user:Person $newUser)")
                    .WithParam("newUser", newUser)
                    .ExecuteWithoutResultsAsync();

                records.Add("User Created!");
            }
            catch (Exception ex)
            {
                return records;
            }
            finally
            {
                _boltGraphClient.Dispose();
            }
            return records;
        }

        [HttpPost("UpdateUser")]//done
        public async Task<bool> UpdateUser(User user)
        {
            await _boltGraphClient.ConnectAsync();

            try
            {
                await _boltGraphClient.Cypher
                    .Match("(user:Person)")
                    .Where((User user) => user.id == user.id)
                    .Set("user.name = $name")
                    .WithParam("name", user.name)
                    .ExecuteWithoutResultsAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _boltGraphClient.Dispose();
            }
            return true;
        }

        [HttpPost("UpdateUserProfilePicture")]//done
        public async Task<bool> UpdateUserProfilePicture(string loggedInUserId, string userProflePicture)
        {
            await _boltGraphClient.ConnectAsync();

            try
            {
                await _boltGraphClient.Cypher
                    .Match("(user:Person)")
                    .Where((User user) => user.id == loggedInUserId)
                    .Set("user.photo = $photo")
                    .WithParam("photo", userProflePicture)
                    .ExecuteWithoutResultsAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _boltGraphClient.Dispose();
            }
            return true;
        }

        [HttpGet("ReadAllRecords")]
        public async Task<List<string>> ReadAllRecordsNeo4jTest()
        {
            await _boltGraphClient.ConnectAsync();

            var records = new List<string>();
            
            try
            {
                var reader = await _boltGraphClient.Cypher
                    .Match("(user:Person)")
                    .Return(user => user.As<User>())
                    .ResultsAsync;

                foreach(var item in reader)
                {
                    records.Add(item.name.ToString());
                }

                _boltGraphClient.Dispose();
            }
            catch (Exception ex)
            {
            }
            return records;
        }

        [HttpGet("GetAllConnections")]//incomplete
        public async Task<List<UserConnections>> GetAllConnections(string userId, int page, int pageSize)
        {
            await _boltGraphClient.ConnectAsync();
            var records = new List<UserConnections>();

            try
            {
                var reader = await _boltGraphClient.Cypher
                    .Match("(me:Person)-[r:Connected]-(friend:Person)")
                    .Where((UserConnections me) => me.name == "Umair")
                    .OptionalMatch("(friend)-[r:Connected]-(fnf:Person)")
                    .Where("(ID(me)) <> (ID(fnf))")
                    .OptionalMatch("(fnf)-[r:Connected]-(fnfL22:Person)")
                    .Where("ID(me)=ID(fnfL22)")
                    .Return((friend, fnf, fnfL22) => new
                    {
                        Friend= friend.As<UserConnections>(),
                        FNF=fnf.Count(),
                        FNFL22 =fnfL22.Count(),
                    })
                    .ResultsAsync;

                /*MATCH (me:Person {name:'Umair'})-[:Connected]-(friend:Person)
                OPTIONAL MATCH(friend) -[:Connected]-(fnf: Person)
                WHERE ID(me) <> ID(fnf)
                OPTIONAL MATCH(fnf)-[:Connected] - (fnfL22: Person)
                WHERE ID(me) = ID(fnfL22)
                RETURN friend.id as UserId, friend.name as Name,
                friend.location as Location,
                friend.designation as Designation, friend.photo as Photo , count(fnf)+1 as TotalFriendsCount, count(fnfL22) as MutualFriendsCount
                ORDER BY friend.name*/

                foreach(var item in reader)
                {
                    var toAdd = new UserConnections();

                    if (item.Friend.id != null)
                    {
                        toAdd.id = item.Friend.id.ToString();
                    }
                    if (item.Friend.name != null)
                    {
                        toAdd.name = item.Friend.name.ToString();
                    }
                    if (item.Friend.location != null)
                    {
                        toAdd.location = item.Friend.location.ToString();
                    }
                    if (item.Friend.designation != null)
                    {
                        toAdd.designation = item.Friend.designation.ToString();
                    }
                    if (item.Friend.photo != null)
                    {
                        toAdd.photo = item.Friend.photo.ToString();
                    }
                    if (item.FNF != 0)
                    {
                        toAdd.totalConnectionsCount = item.FNF.ToString();
                    }
                    if (item.FNFL22 != 0)
                    {
                        toAdd.mutualConnectionsCount = item.FNFL22.ToString();
                    }
                    records.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                return new List<UserConnections>();
            }
            finally
            {
                _boltGraphClient.Dispose();
            }
            return records;
        }

        [HttpGet("GetVisitedMutualConnections")]//done
        public async Task<List<ConnectionsViewModel>> GetVisitedMutualConnections(string userId, string visitedUserId)
        {
            await _boltGraphClient.ConnectAsync();
            var records = new List<ConnectionsViewModel>();

            try
            {
                var reader = await _boltGraphClient.Cypher
                    .Match("(user1:Person)-[r:Connected]-(friend:Person)")
                    .Where((ConnectionsViewModel user1) => user1.name == "Salman")
                    .OptionalMatch("(user1)-[r:Connected]-(meAsFriend:Person)")
                    .Where((ConnectionsViewModel meAsFriend) => meAsFriend.name == "Yawar")
                    .OptionalMatch("(user1)-[r]-(mutualFriend)-[r1]-(user2:Person)")
                    .Where((ConnectionsViewModel user2) => user2.name == "Yawar")
                    .Return((mutualFriend, friend, meAsFriend) => new
                    {
                        MutualFriendsCount = mutualFriend.Count(),
                        TotalFriendsCount = friend.Count(),
                        IsFriend = meAsFriend.Count()
                    })
                    .ResultsAsync;

                /**Neo4j Query is below**/
                //MATCH(user1: Person { name: "Umair"})-[r: Connected] - (friend: Person)
                //OPTIONAL MATCH(user1:Person { name: "Umair"})-[r: Connected] - (meAsFriend: Person{ name: 'Abid'})
                //OPTIONAL MATCH(user1:Person { name: "Umair"})-[r] - (mutualFriend) -[r1] - (user2: Person { name: "Yawar"})
                //RETURN COUNT(mutualFriend) as MutualFriendsCount, count(friend) AS TotalFriendsCount,
                //count(meAsFriend)> 0 as IsFriend

                var toAdd = new ConnectionsViewModel();
                foreach (var item in reader)
                {
                    if (item.IsFriend.ToString() == "1")
                        toAdd.IsConnected = true;

                    toAdd.TotalConnectionsCount = Convert.ToInt32(item.TotalFriendsCount.ToString());
                    toAdd.MutualConnectionsCount = Convert.ToInt32(item.MutualFriendsCount.ToString());
                    records.Add(toAdd);
                }
            }
            catch (Exception ex)
            {
                return new List<ConnectionsViewModel>();
            }
            finally
            {
                _boltGraphClient.Dispose();
            }
            return records;
        }

        //[HttpGet("GetMutualConnectionsForSearchResults")]
        //public async Task<List<ConnectionsViewModel>> GetMutualConnectionsForSearchResults(string userId, List<string> searchedUserList)
        //{
        //    await _boltGraphClient.ConnectAsync();
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
        //                    //toAdd.UserId = item.Value?.ToString();
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
        //        _boltGraphClient.Dispose();
        //    }
        //}

        [HttpGet("GetAllConnectionsIds")]//done
        public async Task<List<string>> GetAllConnectionsIds(string userId)
        {
            await _boltGraphClient.ConnectAsync();
            var records = new List<string>();

            try
            {
                var reader = await _boltGraphClient.Cypher
                    .Match("(user:Person)-[r:Connected]-(friend:Person)")
                    .Where((User user) => user.name == "Umair")
                    .Return((friend) => new
                    {
                        FriendId = friend.As<User>()
                    })
                    .ResultsAsync;

                foreach (var item in reader)
                {
                    records.Add(item.FriendId.name.ToString());
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
            finally
            {
                _boltGraphClient.Dispose();
            }
            return records;
        }

        [HttpGet("get_all_Nodes")]
        public async Task<ActionResult> get_all_Nodes()
        {
            await _boltGraphClient.ConnectAsync();

            var res = await _boltGraphClient.Cypher
                .Match("(user:Person)")
                .Return(user => user.As<User>())
                .ResultsAsync;

            _boltGraphClient.Dispose();
            return Ok(res);
        }

        [HttpGet("get_specific_user")]
        public async Task<ActionResult> get_specific_user(string nodeName)
        {
            await _boltGraphClient.ConnectAsync();

            var res = await _boltGraphClient.Cypher
                .Match("(user:Person)")
                .Where((User user) => user.name == nodeName)
                .Return(user => user.As<User>())
                .ResultsAsync;
            _boltGraphClient.Dispose();
            return Ok(res);
        }

        [HttpPost("create_Node")]
        public async Task<ActionResult> create_Node(string nodeName, string title)
        {
            await _boltGraphClient.ConnectAsync();

            var newUser = new User { name = nodeName, title=title };

            await _boltGraphClient.Cypher
                .Create("(user:Person $newUser)")
                .WithParam("newUser", newUser)
                .ExecuteWithoutResultsAsync();

            _boltGraphClient.Dispose();
            return Ok("Person Added");
        }
    }
}
