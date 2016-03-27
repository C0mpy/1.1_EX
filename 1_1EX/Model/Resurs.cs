using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_1EX.Model
{
    public enum Frekvencija { Redak, Cest, Univerzalan};
    public enum Mera { Merica, Barel, Tona, Kilogram };
    class Resurs
    {
        string id;
        string ime;
        string opis;
        TipResursa tip;
        Frekvencija frekvencija;
        string ikonica;
        bool obnovljiv;
        bool vaznost;
        bool eksploatacija;
        Mera mera;
        double cena;
        DateTime datum;
        List<Etiketa> etikete;

        public Resurs(string Id, string Ime, string Opis, TipResursa Tip, Frekvencija Frek, string Ikonica, bool Obnovljiv,
            bool Vaznost, bool Eksploatacija, Mera Mer, double Cena, DateTime Datum) {

                id = Id;
                ime = Ime;
                opis = Opis;
                tip = Tip;
                frekvencija = Frek;
                ikonica = Ikonica;
                obnovljiv = Obnovljiv;
                vaznost = Vaznost;
                eksploatacija = Eksploatacija;
                mera = Mer;
                cena = Cena;
                datum = Datum;
                etikete = new List<Etiketa>();
        }
        
        public void dodajEtiketu(Etiketa etiketa){
            etikete.Add(etiketa);
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        internal TipResursa Tip
        {
            get { return tip; }
            set { tip = value; }
        }

        internal Mera Mera1
        {
            get { return mera; }
            set { mera = value; }
        }
        

        internal Frekvencija Frekvencija1
        {
            get { return frekvencija; }
            set { frekvencija = value; }
        }

        public double Cena
        {
            get { return cena; }
            set { cena = value; }
        }

        public bool Eksploatacija
        {
            get { return eksploatacija; }
            set { eksploatacija = value; }
        }

        public bool Vaznost
        {
            get { return vaznost; }
            set { vaznost = value; }
        }

        public bool Obnovljiv
        {
            get { return obnovljiv; }
            set { obnovljiv = value; }
        }


        public string Ikonica
        {
            get { return ikonica; }
            set { ikonica = value; }
        }

        public string Opis
        {
            get { return opis; }
            set { opis = value; }
        }

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
