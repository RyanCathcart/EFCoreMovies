namespace EFCoreMovies.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool InCinemas { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string PosterUrl { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<CinemaHall> CinemaHalls { get; set; }
    public ICollection<MovieActor> MovieActors { get; set; }
}
