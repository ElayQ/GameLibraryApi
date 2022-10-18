using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibraryAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string GenresString { get; set; }
    }
}
