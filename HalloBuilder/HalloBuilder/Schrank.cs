namespace HalloBuilder
{
    internal class Schrank
    {
        public int AnzahlTüren { get; private set; }
        public int AnzahlBöden { get; private set; }
        public string Farbe { get; private set; } = string.Empty;
        public bool Kleiderstange { get; private set; }
        public Oberfläche Oberfläche { get; set; }

        private Schrank() { }

        public class Builder
        {
            private Schrank schrank = new Schrank();

            public Builder SetTüren(int anzahl)
            {
                if (anzahl < 2 || anzahl > 7)
                    throw new ArgumentException();

                schrank.AnzahlTüren = anzahl;
                return this;
            }

            public Builder SetBöden(int anzahl)
            {
                if (anzahl < 0 || anzahl > 6)
                    throw new ArgumentException();

                schrank.AnzahlBöden = anzahl;
                return this;
            }

            public Builder SetFarbe(string farbe)
            {
                if (schrank.Oberfläche != Oberfläche.Lackiert)
                    throw new ArgumentException();

                schrank.Farbe = farbe;
                return this;
            }

            public Builder SetOberfläche(Oberfläche oberfläche)
            {
                schrank.Oberfläche = oberfläche;
                return this;
            }

            public Schrank Build()
            {
                return schrank;
            }
        }
    }

    public enum Oberfläche
    {
        Natur,
        Gewachst,
        Lackiert
    }
}
