


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
        _db.Add(new Feed{Url = model.Url});
        _db.SaveChanges();
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