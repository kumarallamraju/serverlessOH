using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
//using SOH19DryRunFunctionApp.Models;
using System.Collections.Generic;

namespace CreateAPIs
{
    public static class GetProduct
    {
        [FunctionName("GetProduct")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{productId}")] HttpRequest req,
            [CosmosDB("serverlessDB", "products", ConnectionStringSetting = "RatingsDatabase",
            SqlQuery = "Select * from products r where r.productId = {productId}")] IEnumerable<RatingModel> rating,
            ILogger log)
        {
            log.LogInformation("Getting Product");
            if (rating == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(rating);
            }
        }
    }
}
