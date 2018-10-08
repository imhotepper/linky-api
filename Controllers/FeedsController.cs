


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[Controller]")]
public class FeedsController: ControllerBase{

    AppDb _db;
    UpdaterJob _updater;
    public FeedsController(AppDb db, UpdaterJob updater) { _db = db; _updater = updater;}

    [HttpPost]
    public ActionResult Post([FromBody] FeedVM model){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var  feed = new Feed { Url = model.Url };
        _db.Add(feed);
        _db.SaveChanges();
        _updater.UpdateFeed(feed);
        return Created("",model);
    }

    [HttpGet]
    [Route("update")]
    public ActionResult Update(){
      var count =  _updater.Execute();
        return Ok(count);
    }
}

public class FeedVM{
    [Required]public string Url { get; set; }

}