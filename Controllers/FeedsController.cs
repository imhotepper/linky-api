


using System;
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
        //Console.WriteLine($"url: {model.Url} with source: {model.Source}");
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var  feed = new Feed { FeedUrl = model.FeedUrl , Website = model.Website, Name = model.Name};
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
    [Required] public string Name { get; set; }
    [Required]public string FeedUrl { get; set; }
    [Required]public string Website { get; set; }

}