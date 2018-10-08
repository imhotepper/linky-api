


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Feed
{
    public virtual Account Account { get; set; }
    public int Id { get; set; }

    public int? AccountId { get; set; }
    [Required]
    public string Url { get; set; }
    public bool IsPrivate { get; set; }

    public IList<Show> Shows { get; set; }  

    public Feed()
    {
        Shows = new List<Show>();
    }
}