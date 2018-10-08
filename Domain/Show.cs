

using System;

public class Show
{
    public int Id { get; set; } 
    public int FeedId { get; set; } 
    public virtual Feed Feed { get; set; }
    public string Title { get; set; }
    public DateTime? PubDate { get; set; }
    public string Url { get; set; }
     public string Source { get; set; }

    public int Counts { get; set; }
}