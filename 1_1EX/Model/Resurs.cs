using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

namespace _1_1EX.Model
{
    public enum Frekvencija { Rare, Common, Universal };
    public enum Mera { Scoop, Barrel, Ton, Kilogram };
    public class Resurs
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
        ObservableCollection<Etiketa> etikete;

        public Resurs()
        {

            id = "";
            ikonica = "";
            cena = 0;
            etikete = new ObservableCollection<Etiketa>();
        }

        public Resurs(string Id, string Ime, string Opis, TipResursa Tip, Frekvencija Frek, string Ikonica, bool Obnovljiv,
            bool Vaznost, bool Eksploatacija, Mera Mer, double Cena, DateTime Datum, ObservableCollection<Etiketa> Etikete)
        {

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
            etikete = Etikete;
        }

        public void dodajEtiketu(Etiketa etiketa)
        {
            etikete.Add(etiketa);
        }

        public ObservableCollection<Etiketa> Etikete1
        {
            get { return etikete; }
            set { etikete = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public TipResursa Tip
        {
            get { return tip; }
            set { tip = value; }
        }

        public Mera Mera1
        {
            get { return mera; }
            set { mera = value; }
        }


        public Frekvencija Frekvencija1
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

        public override string ToString()
        {
            return String.Format("Id: {0}, Ime: {1}, Opis: {2}, Tip: {3}, Frekvencija: {4}, Ikonica: {5}, Obnovljiv: {6}, Vaznost: {7}, Eksploatacija: {8}, Mera: {9}, Cena: {10}, Datum: {11}, Etikete: {12}", Id, Ime, Opis, Tip.ToString(), Frekvencija1.ToString(), Ikonica, Obnovljiv.ToString(), Vaznost.ToString(), Eksploatacija.ToString(), Mera1.ToString(), Cena.ToString(), Datum.ToString(), etiketeToString());
        }

        public string etiketeToString()
        {
            string s = "";
            foreach (Etiketa e in Etikete1)
            {
                s += "[ " + e.ToString() + " ] ";
            }
            return s;
        }
    }
}
