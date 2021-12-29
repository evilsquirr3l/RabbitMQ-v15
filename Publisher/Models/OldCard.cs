namespace Publisher.Models;

public class OldCard : IComparable
{
    public int Id { get; set; }

    public Country Country { get; set; }
    
    public Theme Theme { get; set; }

    public bool IsSent { get; set; }

    public CardType CardType { get; set; }

    public int PublishingYear { get; set; }

    public string Author { get; set; } = string.Empty;

    public Valuable Valuable { get; set; }


    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(this, obj)) return 0;
        if (ReferenceEquals(this, null)) return -1;
        if (ReferenceEquals(obj, null)) return 1;
        return this.Id.CompareTo(((obj as OldCard)!).Id);
    }
}