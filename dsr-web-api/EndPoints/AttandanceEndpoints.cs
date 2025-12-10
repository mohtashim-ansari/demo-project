using System;

namespace dsr_web_api.EndPoints;

public static class AttandanceEndpoints
{
    public static RouteGroupBuilder MapAttandanceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("attandance");

        //        GET /games


        return group;
    }

}
