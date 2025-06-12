namespace VirtualCommunitySupportWebApi.Util
{
    public class ApiResult<T>
    {
        public int result { get; set; }
        public ApiResponse<T> data { get; set; } = null!;
    }

}
