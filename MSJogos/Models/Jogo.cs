namespace MSJogos.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public DateTime DataJogo { get; set; }
        public List<string> Equipas { get; set; }

        public Jogo()
        {
            Equipas = new List<string>();
        }
    }
}
