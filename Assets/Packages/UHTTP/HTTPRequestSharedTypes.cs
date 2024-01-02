namespace UHTTP
{
    public enum HTTPRequestMethod 
    { 
        GET, 
        POST, 
        PUT 
    }

    public enum HTTPResponseCodes
    {
        OK_200 = 200,
        AUTHORIZED_201 = 201,
        NOT_FOUND_404 = 404,
        SERVER_ERROR_500 = 500,
        UNAUTHORIZED_401 = 401,
        FORBIDEN_403 = 401,
    }   
}