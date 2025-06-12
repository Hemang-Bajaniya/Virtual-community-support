 using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace VirtualCommunitySupportWebApi.Models
{
    /// <summary>
    /// filteration model for user entiry
    /// </summary>
    public class FilterModel 
    {
        public int PageSize { get; set; } = 4;
        public int PageNumber { get; set; } = 1;
        public string SearchString { get; set; } = "";
        public string SortDirecrtion { get; set; } = "Asc";
        public string SortBy { get; set; } = "Name";
    }
}
