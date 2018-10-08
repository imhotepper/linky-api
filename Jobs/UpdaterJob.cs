

using System;
using System.Linq;
using CodeHollow.FeedReader;

public class UpdaterJob
{
    AppDb _db;

    public UpdaterJob(AppDb db) => _db = db;
    public int Execute()
    {
        //var existing = _db.Shows
        Console.WriteLine("updating feeds ....");
        var feeds = _db.Feeds.ToList();
        var counts = _db.Shows.Count();
        feeds.ForEach(f => UpdateFeed(f));

        return _db.Shows.Count() - counts;
    }

    private int UpdateFeed(Feed f)
    {
        var itemList = FeedReader.ReadAsync(f.Url).Result.Items;

        itemList.ToList().ForEach(x =>{
             if (!_db.Shows.Any(s=> s.FeedId == f.Id && s.Title == x.Title))
                _db.Add(new Show{ FeedId = f.Id, Title = x.Title , PubDate = x.PublishingDate, Url = x.Link});

                Console.WriteLine(x.Title);
             });
        _db.SaveChanges();
        return itemList.Count;
    }
}