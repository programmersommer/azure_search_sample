using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace search_sample.Models
{

    [SerializePropertyNamesAsCamelCase]
    public class Person
    {        
        [IsRetrievable(true)]
        public string Id { get; set; }

        [IsSearchable, IsRetrievable(true)]
        public string Name { get; set; }

        [IsFilterable, IsRetrievable(true)]
        public string Key { get; set; }

        [IsRetrievable(true)]
        public string rid { get; set; }

        public double Score { get; set; }
    }
}