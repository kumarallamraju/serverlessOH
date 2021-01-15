using System;

namespace CreateAPIs
{
    public class RatingModel
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public DateTime timeStamp { get; set; }
        //public string timeStamp1 { get; internal set; }
        public string locationName { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }
    }
}