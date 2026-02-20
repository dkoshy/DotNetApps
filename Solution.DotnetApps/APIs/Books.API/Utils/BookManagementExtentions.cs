namespace Books.API.Utils;

public static class BookManagementExtentions
{

    public static string GetQuerystring(this IEnumerable<Guid> guids , string queryParamName)
    {
        if(guids == null)
            return string.Empty;

       return  "?" + string.Join($"&{queryParamName}=", guids).Substring(1);
        
    }

}

