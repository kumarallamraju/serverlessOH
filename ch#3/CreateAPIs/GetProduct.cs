using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CreateAPIs
{
    public static class GetProduct
    {
        [FunctionName("GetProduct")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB("serverlessDB", "products", ConnectionStringSetting = "RatingsDatabase",
            SqlQuery = "SELECT * from products c where c.productId = {productId}")] IEnumerable<RatingModel> product,
            ILogger log)
        {
            log.LogInformation("Getting Product");
            if (product == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(product);
            }
        }
    }
}

//Select* from ratings r where r.id = {id}

