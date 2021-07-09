using System;
using Microsoft.Extensions.Configuration;

namespace Neo4j.Helpers
{
	public static class ConfigurationAppSettingsExtensions
	{
		public static int GetPatienDoctorCallMinutes(this IConfiguration configuration)
		{
			if (int.TryParse(configuration["PatientDoctorCallMinutes"], out int callMinutes))
			{
				return callMinutes;
			}
			return 10;
		}
		//public static string GetConnectionStringMongo(this IConfiguration configuration)
		//{
		//	return configuration["ConnectionStrings:Mongo"];
		//}
		public static string GetNeo4jUrl(this IConfiguration configuration)
		{
			return configuration["Neo4j:Url"];
		}
		public static string GetNeo4jUsername(this IConfiguration configuration)
		{
			return configuration["Neo4j:Username"];
		}
		public static string GetNeo4jPassword(this IConfiguration configuration)
		{
			return configuration["Neo4j:Password"];
		}
		//public static int GetIdentityRefreshExpireInMins(this IConfiguration configuration)
		//{
		//	return int.Parse(configuration["Identity:RefreshExpireInMins"]);
		//}
		//public static string GetIdentityRefreshProtectorKey(this IConfiguration configuration)
		//{
		//	return configuration["Identity:RefreshProtectorKey"];
		//}
		//public static int GetIdentitySmsConfirmationExpiresInHours(this IConfiguration configuration)
		//{
		//	return int.Parse(configuration["Identity:SmsConfirmationExpiresInHours"]);
		//}
		//public static int GetIdentityEmailConfirmationExpiresInHours(this IConfiguration configuration)
		//{
		//	return int.Parse(configuration["Identity:EmailConfirmationExpiresInHours"]);
		//}
		//public static string GetUserManagerSetupTfaProtectorKey(this IConfiguration configuration)
		//{
		//	return configuration["UserManager:SetupTfaProtectorKey"];
		//}
		//public static string GetUserManagerLoginTfaProtectorKey(this IConfiguration configuration)
		//{
		//	return configuration["UserManager:LoginTfaProtectorKey"];
		//}

		///// <summary>
		///// "OpenId:Authority"
		///// </summary>
		///// <param name="configuration"></param>
		///// <returns></returns>
		//public static string GetOpenIdAuthority(this IConfiguration configuration)
		//{
		//	return configuration["OpenId:Authority"];
		//}

		///// <summary>
		///// "OpenId:ClientId"
		///// </summary>
		///// <param name="configuration"></param>
		///// <returns></returns>
		//public static string GetOpenIdClientId(this IConfiguration configuration)
		//{
		//	return configuration["OpenId:ClientId"];
		//}

		///// <summary>
		///// "OpenId:ClientSecret"
		///// </summary>
		///// <param name="configuration"></param>
		///// <returns></returns>
		//public static string GetOpenIdClientSecret(this IConfiguration configuration)
		//{
		//	return configuration["OpenId:ClientSecret"];
		//}

		///// <summary>
		///// "OpenId:ApiName"
		///// </summary>
		///// <param name="configuration"></param>
		///// <returns></returns>
		//public static string GetOpenIdApiName(this IConfiguration configuration)
		//{
		//	return configuration["OpenId:ApiName"];
		//}

		///// <summary>
		///// BlobStorage:Expiry
		///// </summary>
		///// <returns></returns>
		//public static DateTimeOffset GetBlobExpiry(this IConfiguration configuration)
		//{
		//	var parseRes = DateTime.TryParse(configuration["BlobStorage:Expiry"], out DateTime expiryDate);

		//	if (parseRes)
		//		return new DateTimeOffset(expiryDate);
		//	else
		//		return new DateTimeOffset(new DateTime(2022, 12, 31));
		//}

	}
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Neo4j.Helpers
//{
//    public class ConfigurationAppSettingsExtensions
//    {
//    }
//}
