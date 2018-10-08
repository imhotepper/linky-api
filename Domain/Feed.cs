


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Feed
{
    public int Id { get; set; }

    public int? AccountId { get; set; }
    public virtual Account Account { get; set; }

    [Required]
    public string Website { get; set; }
    public string FeedUrl { get; set; }
    public bool IsPrivate { get; set; }
    public string Name { get; set; }
    public bool IsDisabled { get; set; }
    public IList<Show> Shows { get; set; }  

    public Feed()
    {
        Shows = new List<Show>();  
    }
}