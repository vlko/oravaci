using System.ComponentModel.DataAnnotations;

namespace OravaciData.Model
{
    public class TimeEvent
    {
        [Required(ErrorMessage = "- chýbajúci rok")]
        public int Id { get; set; }

        [Required(ErrorMessage = "- chýbajúca zemepisná šírka")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "- chýbajúca zemepisná dĺžka")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "- chýbajúci titulok")]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}