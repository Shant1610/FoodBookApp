using System.ComponentModel.DataAnnotations.Schema;
namespace Food_Blog.Models{
    public class Ingredient{
        public int ID { get; set; } 
        [ForeignKey("Food")]
        public int FoodId {get; set;}
        public Food? Hello {get; set; }
        public string? Name { get; set; }
        public string? ingredient1 { get; set; }
        public string? amount1 { get; set; }
        public string? ingredient2 { get; set; }
        public string? amount2 { get; set; }
        public string? ingredient3 { get; set; }
        public string? amount3 { get; set; }
        public string? ingredient4 { get; set; }
        public string? amount4 { get; set; }
        public string? ingredient5 { get; set; }
        public string? amount5 { get; set; }
    }
}
