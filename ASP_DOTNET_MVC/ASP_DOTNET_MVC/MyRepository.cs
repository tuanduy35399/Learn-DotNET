namespace ASP_DOTNET_MVC
{
    public class MyRepository: IRepository
    {
        public string GetById(string id)
        {
            return "ID: " + id;
        }
    }
}
