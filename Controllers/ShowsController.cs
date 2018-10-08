

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[Controller]")]
public class ShowsController: ControllerBase{
    AppDb _db;
    public ShowsController(AppDb db)
    {
        _db = db;
    }    
    public ActionResult Get(int page = 1,int pageSize = 20){
        var shows = _db.Shows
                    .Where(x=>!x.Feed.IsDisabled)
                    .OrderByDescending(x=>x.PubDate)
                    .Include(x=>x.Feed)
                    .Skip(pageSize * (page -1))
                    .Take(pageSize)
                    .Select(x=>new{
                        Id = x.Id,
                        Title = x.Title,
                        Name = x.Feed.Name,
                        Website = x.Feed.Website,
                        PubDate = x.PubDate.HasValue ? x.PubDate.Value.ToString("yyyy-MM-dd") : "",
                        Url=x.Url
                    })
                    .ToList();
        return Ok(shows);
    }

    [HttpPost]
    [Route("stats/{id}")]
    public ActionResult UpdateStats(int id){
        var show = _db.Shows.FirstOrDefault(x=>x.Id == id);
        if (show != null){
            show.Counts +=1;
            _db.SaveChanges();
        }
        return NoContent();
    }
}