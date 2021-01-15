using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
//comment

namespace CreateAPIs
{
    public static class GetProductsFunctions
    {
        [FunctionName(nameof(GetProducts))]
        public static IActionResult GetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        //        [CosmosDB(databaseName: @"%RatingsDbName%", collectionName: @"%RatingsCollectionName%", ConnectionStringSetting = @"RatingsDatabase")] IEnumerable<JObject> allRatings)
                  [CosmosDB(databaseName: "serverlessDB", collectionName: "products", ConnectionStringSetting = "RatingsDatabase")] IEnumerable<JObject> allRatings)

        {
            //string userId = null;
            string productId = null;

            if (req.GetQueryParameterDictionary()?.TryGetValue(@"productId", out productId) == true
                && !string.IsNullOrWhiteSpace(productId))
            {
                var userRatings = allRatings.Where(r => r.Value<string>(@"productId") == productId);

                return !userRatings.Any() ? new NotFoundObjectResult($@"No ratings found for user '{productId}'") : (IActionResult)new OkObjectResult(userRatings);

            }
            else
            {
                return new BadRequestObjectResult(@"productId is required as a query parameter");
            }
        }
    }
}
