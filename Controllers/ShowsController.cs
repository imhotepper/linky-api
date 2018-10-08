

using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class ShowsController: ControllerBase{
    AppDb _db;
    public ShowsController(AppDb db)
    {
        _db = db;
    }
    
    public ActionResult Get(int page = 1,int pageSize = 20){
        var shows = _db.Shows.OrderByDescending(x=>x.PubDate)
                    .Skip(pageSize * (page -1))
                    .Take(pageSize)
                    .Select(x=>new{
                        Id = x.Id,
                        Title = x.Title,
                        PubDate = x.PubDate.HasValue ? x.PubDate.Value.ToString("yyyy-MM-dd") : "",
                        Url=x.Url
                    })
                    .ToList();
        return Ok(shows);
    }
}